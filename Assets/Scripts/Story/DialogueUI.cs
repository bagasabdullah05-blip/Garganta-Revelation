using System;
using System.Collections.Generic;
using UnityEngine;

namespace Garganta.Story
{
    // Typewriter dialogue box. Lines are "Speaker: text".
    public class DialogueUI : MonoBehaviour
    {
        public bool IsPlaying { get; private set; }
        public float CharsPerSec = 45f;

        List<(string speaker, string text)> lines = new List<(string, string)>();
        int idx;
        float shown;
        Action onDone;

        public static (string speaker, string text) ParseLine(string raw)
        {
            int cut = raw.IndexOf(':');
            if (cut <= 0) return ("", raw.Trim());
            return (raw.Substring(0, cut).Trim(), raw.Substring(cut + 1).Trim());
        }

        public void Play(string[] raw, Action done)
        {
            lines.Clear();
            foreach (var r in raw) lines.Add(ParseLine(r));
            idx = 0;
            shown = 0f;
            onDone = done;
            IsPlaying = lines.Count > 0;
            if (!IsPlaying) done?.Invoke();
        }

        public void Stop()
        {
            IsPlaying = false;
            choiceMode = false;
            lines.Clear();
        }

        bool choiceMode;
        string choicePrompt, choiceA, choiceB;
        Action<bool> onPick;

        // Moral / faction choice (M4). Calls back with true = option A.
        public void PlayChoice(string prompt, string a, string b, Action<bool> pick)
        {
            Stop();
            choiceMode = true;
            choicePrompt = prompt;
            choiceA = a;
            choiceB = b;
            onPick = pick;
        }

        void Update()
        {
            if (!IsPlaying) return;
            shown += Time.deltaTime * CharsPerSec;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                Advance();
        }

        void Pick(bool a)
        {
            choiceMode = false;
            var cb = onPick;
            onPick = null;
            cb?.Invoke(a);
        }

        void Advance()
        {            string full = lines[idx].text;
            if (shown < full.Length) { shown = full.Length; return; }
            idx++;
            shown = 0f;
            if (idx >= lines.Count)
            {
                IsPlaying = false;
                var cb = onDone;
                onDone = null;
                cb?.Invoke();
            }
        }

        static Color SpeakerColor(string s)
        {
            switch (s)
            {
                case "Kael": return new Color(0.5f, 0.7f, 0.9f);
                case "Briar": return new Color(0.95f, 0.5f, 0.35f);
                case "Sera": return new Color(1f, 0.9f, 0.6f);
                case "Voss": return new Color(0.5f, 0.85f, 0.5f);
                case "SYSTEM": return new Color(0.6f, 0.6f, 0.65f);
                default: return Color.white;
            }
        }

        void OnGUI()
        {
            if (choiceMode)
            {
                GUILayout.BeginArea(new Rect(Screen.width / 2 - 220, Screen.height / 2 - 80, 440, 170));
                GUILayout.BeginVertical("box");
                GUILayout.Label(choicePrompt);
                if (GUILayout.Button("A: " + choiceA, GUILayout.Height(36))) Pick(true);
                if (GUILayout.Button("B: " + choiceB, GUILayout.Height(36))) Pick(false);
                GUILayout.EndVertical();
                GUILayout.EndArea();
                return;
            }
            if (!IsPlaying || idx >= lines.Count) return;
            var (speaker, text) = lines[idx];
            int n = Mathf.Min(text.Length, (int)shown);
            GUILayout.BeginArea(new Rect(40, Screen.height - 190, Screen.width - 280, 150));
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            GUI.color = SpeakerColor(speaker);
            GUILayout.Box(string.IsNullOrEmpty(speaker) ? "?" : speaker.Substring(0, 1), GUILayout.Width(44), GUILayout.Height(44));
            GUI.color = Color.white;
            GUILayout.BeginVertical();
            if (!string.IsNullOrEmpty(speaker))
            {
                GUI.color = SpeakerColor(speaker);
                GUILayout.Label(speaker);
                GUI.color = Color.white;
            }
            GUILayout.Label(text.Substring(0, n));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label($"[{idx + 1}/{lines.Count}] Space/klik: lanjut", new GUIStyle(GUI.skin.label) { fontSize = 10 });
            if (GUILayout.Button("Skip >>", GUILayout.Width(90))) { idx = lines.Count; Advance(); }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
