using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Garganta.Editor
{
    // Always-on progress panel: menu Garganta -> Progress, dock beside Inspector.
    // Reads docs/PROGRESS.md, click toggles write straight back to the file.
    public class ProgressTracker : EditorWindow
    {
        class Item
        {
            public string Text;
            public bool Done;
            public int Line;
        }

        class Section
        {
            public string Title;
            public bool Open = true;
            public readonly List<Item> Items = new List<Item>();
        }

        string path;
        List<string> lines = new List<string>();
        readonly List<Section> sections = new List<Section>();
        Vector2 scroll;
        string error;

        [MenuItem("Garganta/Progress")]
        public static void Open() => GetWindow<ProgressTracker>("Garganta Progress");

        void OnEnable() => Reload();

        void OnFocus() => Reload();

        void Reload()
        {
            sections.Clear();
            lines.Clear();
            error = null;
            try
            {
                path = Path.GetFullPath(Path.Combine(Application.dataPath, "..", "docs", "PROGRESS.md"));
                if (!File.Exists(path)) { error = "docs/PROGRESS.md tidak ketemu:\n" + path; return; }
                lines.AddRange(File.ReadAllLines(path));
            }
            catch (System.Exception e) { error = e.Message; return; }

            Section cur = null;
            for (int i = 0; i < lines.Count; i++)
            {
                string t = lines[i].Trim();
                if (t.StartsWith("## "))
                {
                    cur = new Section { Title = t.Substring(3).Trim() };
                    sections.Add(cur);
                }
                else if (cur != null && (t.StartsWith("- [ ]") || t.StartsWith("- [x]") || t.StartsWith("- [X]") || t.StartsWith("* [ ]") || t.StartsWith("* [x]") || t.StartsWith("* [X]")))
                {
                    bool done = t[3] == 'x' || t[3] == 'X';
                    cur.Items.Add(new Item { Text = t.Substring(5).Trim(), Done = done, Line = i });
                }
            }
        }

        void OnGUI()
        {
            if (error != null)
            {
                EditorGUILayout.HelpBox(error, MessageType.Error);
                if (GUILayout.Button("Reload")) Reload();
                return;
            }

            int total = 0, done = 0;
            foreach (var s in sections)
                foreach (var it in s.Items)
                {
                    total++;
                    if (it.Done) done++;
                }
            float pct = total == 0 ? 0 : (float)done / total;
            EditorGUI.ProgressBar(EditorGUILayout.GetControlRect(false, 20), pct, $"Progress: {done}/{total} ({pct * 100f:0}%)");

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Reload", GUILayout.Width(90))) Reload();
            if (GUILayout.Button("Buka file", GUILayout.Width(90))) EditorUtility.OpenWithDefaultApp(path);
            EditorGUILayout.EndHorizontal();

            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (var s in sections)
            {
                int sd = 0;
                foreach (var it in s.Items) if (it.Done) sd++;
                s.Open = EditorGUILayout.Foldout(s.Open, $"{s.Title}  ({sd}/{s.Items.Count})", true);
                if (!s.Open) continue;
                EditorGUI.indentLevel++;
                foreach (var it in s.Items)
                {
                    EditorGUI.BeginChangeCheck();
                    bool v = EditorGUILayout.ToggleLeft(it.Text, it.Done);
                    if (EditorGUI.EndChangeCheck())
                    {
                        it.Done = v;
                        lines[it.Line] = "- [" + (v ? "x" : " ") + "] " + it.Text;
                        File.WriteAllLines(path, lines.ToArray());
                    }
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.HelpBox("Centang = langsung tersimpan ke docs/PROGRESS.md. Commit via git.", MessageType.Info);
        }
    }
}
