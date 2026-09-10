using UnityEngine;
using Garganta.Data;
using Garganta.Flow;

namespace Garganta.UI
{
    public class EndingsUI : MonoBehaviour
    {
        void OnGUI()
        {
            var flow = GameFlow.Instance;
            if (flow == null || flow.State != FlowState.Ending) return;
            var e = flow.LastEnding;
            var save = SaveSystem.Current;
            var r = new Rect(Screen.width / 2 - 260, Screen.height / 2 - 190, 520, 380);
            GUILayout.BeginArea(r, "box");
            var title = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 20, fontStyle = FontStyle.Bold };
            var body = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, wordWrap = true };
            GUILayout.Label(e.Title ?? "ENDING", title);
            GUILayout.Space(8);
            GUILayout.Label(e.Text ?? "", body);
            GUILayout.Space(8);
            if (save != null)
            {
                GUILayout.Label($"Party: {save.roster.Count}/15   {Reputation.Summary(save)}", body);
                GUILayout.Label(save.ngPlus ? "Cycle: NG+" : "Cycle: 1st", body);
            }
            GUILayout.Space(8);
            if (GUILayout.Button("New Game+ (keep roster, +2 enemy levels)", GUILayout.Height(36)))
                flow.StartNewGamePlus();
            if (GUILayout.Button("Back to Title")) flow.ToTitle();
            GUILayout.EndArea();
        }
    }
}
