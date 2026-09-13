using System.Collections.Generic;
using UnityEngine;
using Garganta.Data;
using Garganta.Flow;

namespace Garganta.UI
{
    public class TitleUI : MonoBehaviour
    {
        bool pickDiff;

        public const int SlideCount = 4;
        public const float SlideSecs = 8f;
        public const float FadeSecs = 1.5f;

        public static int SlideIndex(float time, int count, float per)
            => count <= 0 || per <= 0 ? 0 : (int)(time / per) % count;

        public static float SlideBlend(float time, float per, float fade)
        {
            if (per <= 0 || fade <= 0) return 0f;
            return Mathf.Clamp01(((time % per) - (per - fade)) / fade);
        }

        void OnGUI()
        {
            var flow = GameFlow.Instance;
            if (flow == null || flow.State != FlowState.Title) return;
            var bg = Garganta.Art.UiArt.Bg("title");
            var slides = new List<Texture2D>();
            for (int i = 0; i < SlideCount; i++)
            {
                var s = Garganta.Art.UiArt.Bg("title_" + i);
                if (s != null) slides.Add(s);
            }
            var full = new Rect(0, 0, Screen.width, Screen.height);
            if (slides.Count > 0)
            {
                int idx = SlideIndex(Time.time, slides.Count, SlideSecs);
                GUI.DrawTexture(full, slides[idx], ScaleMode.ScaleAndCrop);
                float a = SlideBlend(Time.time, SlideSecs, FadeSecs);
                if (a > 0f)
                {
                    GUI.color = new Color(1, 1, 1, a);
                    GUI.DrawTexture(full, slides[(idx + 1) % slides.Count], ScaleMode.ScaleAndCrop);
                    GUI.color = Color.white;
                }
            }
            else if (bg != null) GUI.DrawTexture(full, bg, ScaleMode.ScaleAndCrop);
            var r = new Rect(Screen.width / 2 - 180, Screen.height / 2 - 160, 360, 320);
            GUILayout.BeginArea(r, "box");
            var title = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 26, fontStyle = FontStyle.Bold };
            if (UITheme.Display() != null) title.font = UITheme.Display();
            title.normal.textColor = UITheme.Gold;
            var sub = UITheme.Center(13);
            var logo = Garganta.Art.UiArt.Bg("logo");
            if (logo != null)
            {
                var lr = GUILayoutUtility.GetRect(320, 100, GUILayout.Width(320), GUILayout.Height(100));
                GUI.DrawTexture(lr, logo, ScaleMode.ScaleToFit);
            }
            else
            {
                GUILayout.Label("GARGANTA", title);
                GUILayout.Label("REVELATION", title);
            }
            GUILayout.Label("Dark Fantasy Tactical RPG — Act I", sub);
            GUILayout.Space(12);
            if (!pickDiff)
            {
                if (GUILayout.Button("New Game", UITheme.MenuButton(), GUILayout.Height(36))) pickDiff = true;
            }
            else
            {
                GUILayout.Label("Difficulty:", sub);
                for (int d = 0; d < 4; d++)
                {
                    int diff = d;
                    if (GUILayout.Button($"{GameBalance.Name(diff)} (foe x{GameBalance.EnemyStatMult(diff)}, XP x{GameBalance.XpMult(diff)})", UITheme.MenuButton()))
                    {
                        pickDiff = false;
                        flow.NewGame(diff);
                    }
                }
                if (GUILayout.Button("Back", UITheme.MenuButton())) pickDiff = false;
            }
            GUI.enabled = SaveSystem.SlotExists(SaveSystem.LastSlot) || SaveSystem.SlotExists(1);
            if (GUILayout.Button("Continue", UITheme.MenuButton(), GUILayout.Height(32))) flow.ContinueGame();
            GUI.enabled = true;
            GUILayout.Label("Load slot:", sub);
            GUILayout.BeginHorizontal();
            for (int s = 1; s <= 3; s++)
            {
                int slot = s;
                GUI.enabled = SaveSystem.SlotExists(slot);
                if (GUILayout.Button($"Slot {slot}", UITheme.MenuButton())) flow.LoadSlot(slot);
            }
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}
