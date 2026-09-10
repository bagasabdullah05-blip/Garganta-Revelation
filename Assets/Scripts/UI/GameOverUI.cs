using UnityEngine;
using Garganta.Core;

namespace Garganta.UI
{
    public class GameOverUI : MonoBehaviour
    {
        void OnGUI()
        {
            var gm = GameManager.Instance;
            if (gm == null) return;
            if (gm.State != GameState.Victory && gm.State != GameState.Defeat) return;
            var r = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 60, 300, 120);
            GUILayout.BeginArea(r, "box");
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            GUILayout.Label(gm.State == GameState.Victory ? "VICTORY — all enemies down" : "DEFEAT — party wiped", style);
            if (GUILayout.Button("Restart (reload scene)"))
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            GUILayout.EndArea();
        }
    }
}
