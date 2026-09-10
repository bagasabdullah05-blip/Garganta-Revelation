using UnityEngine;
using Garganta.Data;
using Garganta.Flow;

namespace Garganta.UI
{
    public class TitleUI : MonoBehaviour
    {
        bool pickDiff;

        void OnGUI()
        {
            var flow = GameFlow.Instance;
            if (flow == null || flow.State != FlowState.Title) return;
            var r = new Rect(Screen.width / 2 - 180, Screen.height / 2 - 160, 360, 320);
            GUILayout.BeginArea(r, "box");
            var title = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 26, fontStyle = FontStyle.Bold };
            var sub = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            GUILayout.Label("GARGANTA", title);
            GUILayout.Label("REVELATION", title);
            GUILayout.Label("Dark Fantasy Tactical RPG — Act I", sub);
            GUILayout.Space(12);
            if (!pickDiff)
            {
                if (GUILayout.Button("New Game", GUILayout.Height(36))) pickDiff = true;
            }
            else
            {
                GUILayout.Label("Difficulty:", sub);
                for (int d = 0; d < 4; d++)
                {
                    int diff = d;
                    if (GUILayout.Button($"{GameBalance.Name(diff)} (foe x{GameBalance.EnemyStatMult(diff)}, XP x{GameBalance.XpMult(diff)})"))
                    {
                        pickDiff = false;
                        flow.NewGame(diff);
                    }
                }
                if (GUILayout.Button("Back")) pickDiff = false;
            }
            GUI.enabled = SaveSystem.SlotExists(SaveSystem.LastSlot) || SaveSystem.SlotExists(1);
            if (GUILayout.Button("Continue", GUILayout.Height(32))) flow.ContinueGame();
            GUI.enabled = true;
            GUILayout.Label("Load slot:", sub);
            GUILayout.BeginHorizontal();
            for (int s = 1; s <= 3; s++)
            {
                int slot = s;
                GUI.enabled = SaveSystem.SlotExists(slot);
                if (GUILayout.Button($"Slot {slot}")) flow.LoadSlot(slot);
            }
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}
