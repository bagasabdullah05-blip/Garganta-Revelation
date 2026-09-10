using UnityEngine;
using Garganta.Data;
using Garganta.Flow;

namespace Garganta.UI
{
    public class WorldMapUI : MonoBehaviour
    {
        void OnGUI()
        {
            var flow = GameFlow.Instance;
            if (flow == null || flow.State != FlowState.Map) return;
            var save = SaveSystem.Current;
            if (save == null) return;

            GUILayout.BeginArea(new Rect(20, 20, 340, Screen.height - 40));
            GUILayout.BeginVertical("box");
            GUILayout.Label("WORLD MAP — Act I");
            GUILayout.Label($"Gold: {Inventory.Gold}G   Party: {save.roster.Count}");
            GUILayout.Label(Reputation.Summary(save));
            GUILayout.Space(8);
            for (int i = 0; i < ChapterDatabase.NodeCount; i++)
            {
                var cfg = ChapterDatabase.GetNode(i);
                bool done = save.progress > i;
                bool open = save.progress >= i;
                GUI.enabled = open && !done;
                string label = done ? $"[DONE] {cfg.Title}" : $"{cfg.Title}\n{cfg.Subtitle}";
                if (GUILayout.Button(label, GUILayout.Height(52)))
                {
                    int node = i;
                    flow.StartBattle(node);
                }
            }
            GUI.enabled = true;
            GUILayout.Space(8);
            if (GUILayout.Button("Bastion (Base)", GUILayout.Height(40))) flow.ToBase();
            if (GUILayout.Button("Skirmish (training, repeatable)", GUILayout.Height(32))) flow.StartSkirmish();
            if (save.hasEnding && GUILayout.Button("Primeval Lair (superboss)", GUILayout.Height(32))) flow.StartPrimeval();
            if (GUILayout.Button("Back to Title")) flow.ToTitle();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
