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
            GUILayout.BeginArea(new Rect(8, Screen.height - 160, 460, 152));
            GUILayout.BeginHorizontal("box");
            var face = Garganta.Art.UiArt.Portrait(u.RosterId);
            if (face != null)
            {
                var fr = GUILayoutUtility.GetRect(56, 56, GUILayout.Width(56), GUILayout.Height(56));
                GUI.DrawTexture(fr, face.texture, ScaleMode.ScaleToFit);
            }
            GUILayout.BeginVertical();
            GUILayout.Label($"{u.UnitName} [{u.ClassId} Lv{u.Level}/J{u.JobLevelOf(u.ClassId)}]  HP {u.HP}/{u.Stats.MaxHP}  MP {u.MP}/{u.Stats.MaxMP}", UITheme.BodyText());
            GUILayout.Label($"ATK {u.Stats.ATK} DEF {u.Stats.DEF} MAG {u.Stats.MAG} SPD {u.Stats.SPD} Move {u.Stats.Move} Rng {u.Stats.Range}", UITheme.BodyText());
            string status = u.StunTurns > 0 ? "STUNNED " : "";
            if (u.Corruption >= 25) status += $"BLIGHT{u.Corruption}% ";
            if (u.BuffTurns > 0) status += $"BUFF+{u.BuffAtk}/{u.BuffDef}/{u.BuffEva}/{u.BuffMag}({u.BuffTurns}) ";
            GUILayout.Label($"CTB {u.CTB:0}/100  {u.Stats.Weapon}  {status}", UITheme.BodyText());
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}
