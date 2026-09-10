using UnityEngine;
using Garganta.Core;

namespace Garganta.UI
{
    public class ActionMenuUI : MonoBehaviour
    {
        void OnGUI()
        {
            var gm = GameManager.Instance;
            if (gm == null || gm.State != GameState.PlayerTurn || gm.CurrentUnit == null) return;
            GUILayout.BeginArea(new Rect(Screen.width - 160, Screen.height - 140, 152, 132));
            GUILayout.BeginVertical("box");
            GUILayout.Label("Action");
            if (GUILayout.Button("Attack")) gm.ShowAttackRange();
            if (GUILayout.Button("Wait")) gm.PlayerWait();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
