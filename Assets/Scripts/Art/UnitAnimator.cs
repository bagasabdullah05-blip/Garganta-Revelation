using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Garganta.Art
{
    public enum AnimClip { Idle, Walk, Attack, Skill, Hit, Death }

    // Code-driven sprite animation. Strips live at Resources/Art/Anims/
    // as horizontal PNG strips named anim_<Id>_<clip> (clip lowercase),
    // frames 32x48 (units) — derived count = width / 32. Missing strip =
    // silent fallback to the single base sprite (game never breaks).
    public class UnitAnimator : MonoBehaviour
    {
        public int Fps = 8;

        SpriteRenderer sr;
        Sprite baseSprite;
        readonly Dictionary<AnimClip, List<Sprite>> strips = new Dictionary<AnimClip, List<Sprite>>();
        AnimClip current = AnimClip.Idle;
        float t;
        bool holdPose; // walk holds last state via PlayWalk(false)

        public static int FrameAt(float time, int fps, int count, bool loop)
        {
            if (count <= 0) return 0;
            int f = (int)(time * fps);
            return loop ? f % count : Math.Min(f, count - 1);
        }

        public static int StripCount(Texture2D tex, int frameW)
            => tex == null || frameW <= 0 ? 0 : Math.Max(1, tex.width / frameW);

        public void Setup(string id, Sprite fallback)
        {
            sr = GetComponent<SpriteRenderer>();
            baseSprite = fallback;
            foreach (AnimClip clip in Enum.GetValues(typeof(AnimClip)))
            {
                var tex = Resources.Load<Texture2D>($"Art/Anims/anim_{id}_{clip.ToString().ToLower()}");
                int n = StripCount(tex, 32);
                if (n <= 1 && (tex == null || tex.width < 64)) continue; // need a real strip
                var frames = new List<Sprite>();
                for (int i = 0; i < n; i++)
                    frames.Add(Sprite.Create(tex, new Rect(i * 32, 0, 32, 48), new Vector2(0.5f, 0.5f), 32f));
                strips[clip] = frames;
            }
        }

        public bool HasClip(AnimClip clip) => strips.ContainsKey(clip);

        public void SetFacing(float dirX)
        {
            if (sr == null) sr = GetComponent<SpriteRenderer>();
            if (Math.Abs(dirX) > 0.01f && sr != null) sr.flipX = dirX < 0;
        }

        public void PlayIdle() { current = AnimClip.Idle; t = 0f; holdPose = false; }
        public void PlayWalk(bool on)
        {
            if (on) { current = AnimClip.Walk; t = 0f; }
            else { current = AnimClip.Idle; t = 0f; }
            holdPose = false;
        }

        public void PlayAttack(Action onDone = null) => PlayOnce(AnimClip.Attack, onDone);
        public void PlaySkill(Action onDone = null) => PlayOnce(HasClip(AnimClip.Skill) ? AnimClip.Skill : AnimClip.Attack, onDone);

        public void PlayHit()
        {
            if (!HasClip(AnimClip.Hit)) return;
            current = AnimClip.Hit;
            t = 0f;
            holdPose = false;
        }

        public void PlayDeath(Action onDone = null)
        {
            if (!HasClip(AnimClip.Death)) return;
            current = AnimClip.Death;
            t = 0f;
            holdPose = true; // stay on last frame (corpse)
            if (onDone != null) StartCoroutine(FinishAfter(ClipLength(AnimClip.Death), onDone));
        }

        void PlayOnce(AnimClip clip, Action onDone)
        {
            current = HasClip(clip) ? clip : AnimClip.Idle;
            t = 0f;
            holdPose = false;
            if (onDone != null && HasClip(clip)) StartCoroutine(FinishAfter(ClipLength(clip), onDone));
            else if (onDone != null) onDone();
        }

        float ClipLength(AnimClip clip)
            => HasClip(clip) ? (float)strips[clip].Count / Math.Max(1, Fps) : 0f;

        IEnumerator FinishAfter(float dur, Action onDone)
        {
            yield return new WaitForSeconds(dur);
            current = AnimClip.Idle;
            t = 0f;
            onDone();
        }

        void Update()
        {
            if (sr == null) return;
            if (!strips.TryGetValue(current, out var frames) || frames.Count == 0)
            {
                if (sr.sprite != baseSprite && !holdPose) sr.sprite = baseSprite;
                return;
            }
            bool loop = current == AnimClip.Idle || current == AnimClip.Walk;
            sr.sprite = frames[FrameAt(t, Fps, frames.Count, loop)];
            t += Time.deltaTime;
            if (!loop && !holdPose && t * Fps >= frames.Count)
            {
                current = AnimClip.Idle;
                t = 0f;
            }
        }
    }
}
