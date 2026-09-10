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
            GUILayout.BeginArea(new Rect(8, Screen.height - 130, 320, 122));
            GUILayout.BeginVertical("box");
            GUILayout.Label($"{u.UnitName} [{u.ClassId} Lv{u.Level}]  HP {u.HP}/{u.Stats.MaxHP}  MP {u.MP}/{u.Stats.MaxMP}");
            GUILayout.Label($"ATK {u.Stats.ATK} DEF {u.Stats.DEF} MAG {u.Stats.MAG} SPD {u.Stats.SPD} Move {u.Stats.Move} Rng {u.Stats.Range}");
            string status = u.StunTurns > 0 ? "STUNNED " : "";
            if (u.BuffTurns > 0) status += $"BUFF+{u.BuffAtk}/{u.BuffDef}/{u.BuffEva}/{u.BuffMag}({u.BuffTurns}) ";
            GUILayout.Label($"CTB {u.CTB:0}/100  {u.Stats.Weapon}  {status}");
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
