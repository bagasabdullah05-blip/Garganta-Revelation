using UnityEngine;
using Garganta.Core;
using Garganta.Flow;

namespace Garganta.UI
{
    public class GameOverUI : MonoBehaviour
    {
        void OnGUI()
        {
            var gm = GameManager.Instance;
            if (gm == null) return;
            if (gm.State != GameState.Victory && gm.State != GameState.Defeat) return;
            var dlg = FindAnyObjectByType<Story.DialogueUI>();
            if (dlg != null && dlg.IsPlaying) return; // let post-battle dialogue finish first
            var r = new Rect(Screen.width / 2 - 170, Screen.height / 2 - 90, 340, 200);
            GUILayout.BeginArea(r, "box");
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            GUILayout.Label(gm.State == GameState.Victory ? "VICTORY" : "DEFEAT — party wiped", style);
            if (gm.State == GameState.Victory && !string.IsNullOrEmpty(gm.BattleReport))
                GUILayout.Label(gm.BattleReport, style);
            var flow = Flow.GameFlow.Instance;
            if (flow != null && flow.State == FlowState.Battle)
            {
                if (GUILayout.Button("Return to Map")) flow.ToMap();
            }
            else if (GUILayout.Button("Restart (reload scene)"))
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            GUILayout.EndArea();
        }
    }
}
