using UnityEngine;
using Garganta.Core;
using Garganta.Data;

namespace Garganta.UI
{
    public class ActionMenuUI : MonoBehaviour
    {
        int menu; // 0 main, 1 skills, 2 items

        static GUIContent Btn(string iconId, string text)
        {
            var s = Garganta.Art.UiArt.Icon(iconId);
            return s != null ? new GUIContent(text, s.texture) : new GUIContent(text);
        }

        void OnGUI()
        {
            var gm = GameManager.Instance;
            if (gm == null || gm.State != GameState.PlayerTurn || gm.CurrentUnit == null) { menu = 0; return; }
            var u = gm.CurrentUnit;

            if (menu == 1) { SkillsMenu(gm, u); return; }
            if (menu == 2) { ItemsMenu(gm, u); return; }

            GUILayout.BeginArea(new Rect(Screen.width - 270, Screen.height - 300, 254, 292));
            GUILayout.BeginVertical("box");
            GUILayout.Label($"{u.UnitName} Lv{u.Level}", UITheme.Center(20));
            if (GUILayout.Button(Btn("Attack", "Attack"))) gm.ShowAttackRange();
            if (GUILayout.Button(Btn("Skill", "Skill"))) menu = 1;
            if (GUILayout.Button(Btn("Item", "Item"))) menu = 2;
            if (GUILayout.Button(Btn("Wait", "Wait"))) gm.PlayerWait();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void SkillsMenu(GameManager gm, Units.Unit u)
        {
            GUILayout.BeginArea(new Rect(Screen.width - 360, Screen.height - 420, 344, 412));
            GUILayout.BeginVertical("box");
            GUILayout.Label($"Skills (MP {u.MP}/{u.Stats.MaxMP})");
            foreach (var sk in ClassDatabase.UnlockedSkillsFor(u))
            {
                GUI.enabled = u.MP >= sk.CostMP;
                if (GUILayout.Button(Btn(sk.Id, $"{sk.Name} ({sk.CostMP})"))) { gm.SelectSkill(sk); menu = 0; }
            }
            GUI.enabled = true;
            if (GUILayout.Button("Back")) menu = 0;
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void ItemsMenu(GameManager gm, Units.Unit u)
        {
            GUILayout.BeginArea(new Rect(Screen.width - 360, Screen.height - 420, 344, 412));
            GUILayout.BeginVertical("box");
            GUILayout.Label("Items");
            foreach (var kv in Inventory.Stock())
            {
                var item = EquipmentData.FindConsumable(kv.Key);
                if (item.Id == null) continue;
                GUI.enabled = kv.Value > 0;
                if (GUILayout.Button(Btn(item.Id, $"{item.Name} x{kv.Value}"))) { gm.SelectItem(item.Id); menu = 0; }
            }
            GUI.enabled = true;
            if (GUILayout.Button("Back")) menu = 0;
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
