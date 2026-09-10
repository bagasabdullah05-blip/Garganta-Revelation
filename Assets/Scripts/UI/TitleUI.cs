using UnityEngine;
using Garganta.Data;
using Garganta.Flow;

namespace Garganta.UI
{
    public class TitleUI : MonoBehaviour
    {
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
            if (GUILayout.Button("New Game", GUILayout.Height(36))) flow.NewGame();
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
