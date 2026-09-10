using System.Collections.Generic;
using UnityEngine;
using Garganta.Core;
using Garganta.Units;

namespace Garganta.UI
{
    public class TurnOrderUI : MonoBehaviour
    {
        void OnGUI()
        {
            var gm = GameManager.Instance;
            if (gm == null) return;
            GUILayout.BeginArea(new Rect(8, 8, Screen.width - 16, 30));
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Turn order (CTB):", GUILayout.Width(120));
            var all = new List<Unit>();
            all.AddRange(gm.PlayerUnits);
            all.AddRange(gm.EnemyUnits);
            all.Sort((a, b) => b.CTB.CompareTo(a.CTB));
            foreach (var u in all)
            {
                if (!u.IsAlive) continue;
                string mark = gm.CurrentUnit == u ? "*" : "";
                GUI.color = u.IsPlayer ? Color.cyan : Color.red;
                GUILayout.Label($"{mark}{u.UnitName}:{u.CTB:0}", GUILayout.Width(90));
            }
            GUI.color = Color.white;
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}
