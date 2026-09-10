using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garganta.Audio
{
    // Null-safe: silent until clips exist under Resources/Audio/{Music,SFX}/<id>.
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Range(0f, 1f)] public float MusicVolume = 0.7f;
        [Range(0f, 1f)] public float SfxVolume = 0.9f;
        public int SfxVoices = 8;

        AudioSource musicA, musicB;
        bool useA = true;
        readonly List<AudioSource> sfxPool = new List<AudioSource>();
        int sfxNext;
        readonly Dictionary<string, AudioClip> clipCache = new Dictionary<string, AudioClip>();
        string currentMusic;

        void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            EnsureInit();
        }

        void EnsureInit()
        {
            if (musicA == null) { musicA = gameObject.AddComponent<AudioSource>(); musicA.loop = true; }
            if (musicB == null) { musicB = gameObject.AddComponent<AudioSource>(); musicB.loop = true; }
            while (sfxPool.Count < SfxVoices)
            {
                var s = gameObject.AddComponent<AudioSource>();
                s.playOnAwake = false;
                sfxPool.Add(s);
            }
        }

        public static float ClampVol(float v) => Mathf.Clamp01(v);

        AudioClip LoadClip(string folder, string id)
        {
            string key = folder + "/" + id;
            if (clipCache.TryGetValue(key, out var c)) return c;
            c = Resources.Load<AudioClip>("Audio/" + key);
            clipCache[key] = c; // may be null: stay silent until asset drops in
            return c;
        }

        public void PlayMusic(string id, float fade = 0.8f)
        {
            EnsureInit();
            if (id == currentMusic) return;
            var clip = LoadClip("Music", id);
            if (clip == null) return;
            currentMusic = id;
            StopAllCoroutines();
            StartCoroutine(Crossfade(clip, fade));
        }

        IEnumerator Crossfade(AudioClip clip, float fade)
        {
            var next = useA ? musicB : musicA;
            var prev = useA ? musicA : musicB;
            useA = !useA;
            next.clip = clip;
            next.volume = 0f;
            next.Play();
            float t = 0f;
            while (t < fade)
            {
                t += Time.deltaTime;
                float k = Mathf.Min(1f, t / Mathf.Max(0.01f, fade));
                next.volume = MusicVolume * k;
                prev.volume = MusicVolume * (1f - k);
                yield return null;
            }
            prev.Stop();
        }

        public void StopMusic()
        {
            EnsureInit();
            currentMusic = null;
            StopAllCoroutines();
            musicA.Stop();
            musicB.Stop();
        }

        public void PlaySfx(string id, float pitch = 1f)
        {
            EnsureInit();
            var clip = LoadClip("SFX", id);
            if (clip == null || sfxPool.Count == 0) return;
            var s = sfxPool[sfxNext % sfxPool.Count];
            sfxNext++;
            s.clip = clip;
            s.volume = SfxVolume;
            s.pitch = pitch;
            s.Play();
        }
    }
}
