using UnityEngine;
using Garganta.Core;
using Garganta.Data;

namespace Garganta.UI
{
    public class ActionMenuUI : MonoBehaviour
    {
        int menu; // 0 main, 1 skills, 2 items

        void OnGUI()
        {
            var gm = GameManager.Instance;
            if (gm == null || gm.State != GameState.PlayerTurn || gm.CurrentUnit == null) { menu = 0; return; }
            var u = gm.CurrentUnit;

            if (menu == 1) { SkillsMenu(gm, u); return; }
            if (menu == 2) { ItemsMenu(gm, u); return; }

            GUILayout.BeginArea(new Rect(Screen.width - 170, Screen.height - 170, 162, 162));
            GUILayout.BeginVertical("box");
            GUILayout.Label($"{u.UnitName} Lv{u.Level}");
            if (GUILayout.Button("Attack")) gm.ShowAttackRange();
            if (GUILayout.Button("Skill")) menu = 1;
            if (GUILayout.Button("Item")) menu = 2;
            if (GUILayout.Button("Wait")) gm.PlayerWait();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void SkillsMenu(GameManager gm, Units.Unit u)
        {
            GUILayout.BeginArea(new Rect(Screen.width - 250, Screen.height - 300, 242, 292));
            GUILayout.BeginVertical("box");
            GUILayout.Label($"Skills (MP {u.MP}/{u.Stats.MaxMP})");
            foreach (var sk in ClassDatabase.UnlockedSkillsFor(u))
            {
                GUI.enabled = u.MP >= sk.CostMP;
                if (GUILayout.Button($"{sk.Name} ({sk.CostMP})")) { gm.SelectSkill(sk); menu = 0; }
            }
            GUI.enabled = true;
            if (GUILayout.Button("Back")) menu = 0;
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void ItemsMenu(GameManager gm, Units.Unit u)
        {
            GUILayout.BeginArea(new Rect(Screen.width - 250, Screen.height - 300, 242, 292));
            GUILayout.BeginVertical("box");
            GUILayout.Label("Items");
            foreach (var kv in Inventory.Stock())
            {
                var item = EquipmentData.FindConsumable(kv.Key);
                if (item.Id == null) continue;
                GUI.enabled = kv.Value > 0;
                if (GUILayout.Button($"{item.Name} x{kv.Value}")) { gm.SelectItem(item.Id); menu = 0; }
            }
            GUI.enabled = true;
            if (GUILayout.Button("Back")) menu = 0;
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
