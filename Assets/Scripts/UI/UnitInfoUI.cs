using UnityEngine;
using Garganta.Core;
using Garganta.Units;

namespace Garganta.UI
{
    public class UnitInfoUI : MonoBehaviour
    {
        void OnGUI()
        {
            var gm = GameManager.Instance;
            if (gm == null) return;
            Unit u = gm.SelectedUnit ?? gm.CurrentUnit;
            if (u == null) return;
            GUILayout.BeginArea(new Rect(8, Screen.height - 110, 300, 102));
            GUILayout.BeginVertical("box");
            GUILayout.Label($"{u.UnitName}  HP {u.HP}/{u.Stats.MaxHP}");
            GUILayout.Label($"ATK {u.Stats.ATK} DEF {u.Stats.DEF} SPD {u.Stats.SPD} Move {u.Stats.Move} Rng {u.Stats.Range}");
            GUILayout.Label($"CTB {u.CTB:0}/100  {u.Stats.Weapon}");
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
