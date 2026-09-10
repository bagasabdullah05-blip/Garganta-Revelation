using System.Collections.Generic;
using UnityEngine;
using Garganta.Data;
using Garganta.Flow;

namespace Garganta.UI
{
    // Bastion hub: Party view / Equip / Shop / Save.
    public class BaseUI : MonoBehaviour
    {
        int tab;
        string selUnit = "Kael";

        static readonly string[] shopConsumables = { "potion", "hi_potion", "ether", "antidote", "phoenix_down", "bomb" };
        static readonly string[] shopGear =
        {
            "iron_sword", "steel_sword", "iron_lance", "partisan", "battle_axe",
            "longbow", "hunter_bow", "fire_tome", "iron_staff", "materia_staff",
            "chainmail", "plate", "bronze_helm", "power_band", "speed_ring", "iron_charm",
        };

        void OnGUI()
        {
            var flow = GameFlow.Instance;
            if (flow == null || flow.State != FlowState.Base) return;
            var save = SaveSystem.Current;
            if (save == null) return;

            GUILayout.BeginArea(new Rect(20, 20, 560, Screen.height - 40));
            GUILayout.BeginVertical("box");
            GUILayout.Label($"BASTION — Gold: {Inventory.Gold}G");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Party")) tab = 0;
            if (GUILayout.Button("Equip")) tab = 1;
            if (GUILayout.Button("Shop")) tab = 2;
            if (GUILayout.Button("Save")) tab = 3;
            GUILayout.EndHorizontal();

            switch (tab)
            {
                case 0: PartyTab(save); break;
                case 1: EquipTab(save); break;
                case 2: ShopTab(save); break;
                case 3: SaveTab(flow); break;
            }
            if (GUILayout.Button("Back to Map", GUILayout.Height(32))) flow.ToMap();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void PartyTab(GameSave save)
        {
            foreach (var r in save.roster)
            {
                var rec = ClassDatabase.Get(r.classId);
                int need = Units.Unit.XpNeed(r.level);
                GUILayout.Label($"{r.rosterId} [{rec.Name} Lv{r.level}] XP {r.xp}/{need}  gear: {GearName(r.weaponId)}/{GearName(r.armorId)}/{GearName(r.helmetId)}/{GearName(r.accId)}");
            }
            GUILayout.Label("HP/MP fully restored at every battle. Victory auto-saves.");
        }

        void EquipTab(GameSave save)
        {
            GUILayout.BeginHorizontal();
            foreach (var r in save.roster)
                if (GUILayout.Button(r.rosterId)) selUnit = r.rosterId;
            GUILayout.EndHorizontal();
            int i = save.roster.FindIndex(r => r.rosterId == selUnit);
            if (i < 0) { selUnit = save.roster[0].rosterId; return; }
            var u = save.roster[i];
            foreach (var slot in new[] { EquipSlot.Weapon, EquipSlot.Armor, EquipSlot.Helmet, EquipSlot.Accessory })
            {
                string cur = SlotId(u, slot);
                GUILayout.Label($"{EquipmentData.SlotName(slot)}: {(string.IsNullOrEmpty(cur) ? "-" : GearName(cur))}");
                foreach (var oid in new List<string>(Inventory.OwnedEquip))
                {
                    var e = EquipmentData.FindAny(oid);
                    if (e.Id == null || e.Slot != slot) continue;
                    if (GUILayout.Button($"Equip {e.Name}")) { SwapGear(save, i, slot, oid); }
                }
            }
        }

        void SwapGear(GameSave save, int i, EquipSlot slot, string newId)
        {
            var u = save.roster[i];
            string old = SlotId(u, slot);
            if (!string.IsNullOrEmpty(old)) Inventory.OwnedEquip.Add(old);
            Inventory.OwnedEquip.Remove(newId);
            SetSlot(ref u, slot, newId);
            save.roster[i] = u;
            if (!save.ownedEquip.Contains(newId)) { /* ownedEquip mirrors runtime below */ }
            save.ownedEquip.Clear();
            save.ownedEquip.AddRange(Inventory.OwnedEquip);
        }

        void ShopTab(GameSave save)
        {
            GUILayout.Label("Consumables:");
            foreach (var id in shopConsumables)
            {
                var c = EquipmentData.FindConsumable(id);
                GUI.enabled = Inventory.Gold >= c.Price;
                if (GUILayout.Button($"{c.Name} ({c.Price}G) — {c.Desc} [x{Inventory.Count(id)}]"))
                    Inventory.TryBuyConsumable(id);
            }
            GUI.enabled = true;
            GUILayout.Label("Gear:");
            foreach (var id in shopGear)
            {
                var e = EquipmentData.FindAny(id);
                if (e.Id == null) continue;
                GUI.enabled = Inventory.Gold >= e.Price;
                if (GUILayout.Button($"{e.Name} ({e.Price}G)"))
                    Inventory.TryBuyEquipment(e, save.ownedEquip);
            }
            GUI.enabled = true;
        }

        void SaveTab(GameFlow flow)
        {
            GUILayout.Label("Save game (progress + roster + gold):");
            for (int s = 1; s <= 3; s++)
            {
                int slot = s;
                string tag = SaveSystem.SlotExists(slot) ? "(used)" : "(empty)";
                if (GUILayout.Button($"Save to Slot {slot} {tag}"))
                {
                    SaveSystem.CaptureRuntime();
                    var save = SaveSystem.Current;
                    save.ownedEquip.Clear();
                    save.ownedEquip.AddRange(Inventory.OwnedEquip);
                    SaveSystem.Save(slot);
                }
            }
        }

        static string SlotId(UnitSave r, EquipSlot slot)
        {
            switch (slot)
            {
                case EquipSlot.Weapon: return r.weaponId;
                case EquipSlot.Armor: return r.armorId;
                case EquipSlot.Helmet: return r.helmetId;
                default: return r.accId;
            }
        }

        static void SetSlot(ref UnitSave r, EquipSlot slot, string id)
        {
            switch (slot)
            {
                case EquipSlot.Weapon: r.weaponId = id; break;
                case EquipSlot.Armor: r.armorId = id; break;
                case EquipSlot.Helmet: r.helmetId = id; break;
                default: r.accId = id; break;
            }
        }

        static string GearName(string id) => string.IsNullOrEmpty(id) ? "-" : (EquipmentData.FindAny(id).Name ?? "-");
    }
}
