using System.Collections.Generic;
using UnityEngine;
using Garganta.AI;
using Garganta.Art;
using Garganta.Data;
using Garganta.Grid;
using Garganta.UI;

namespace Garganta.Units
{
    public static class UnitFactory
    {
        static readonly Dictionary<string, string> unitClass = new Dictionary<string, string>
        {
            ["Kael"] = "Squire", ["Briar"] = "Squire", ["Sera"] = "Acolyte", ["Voss"] = "Archer",
            ["Bandit"] = "Thief", ["Goblin"] = "Thief", ["Wolf"] = "Thief", ["Skeleton"] = "Spearman",
            ["Orc"] = "Spearman", ["Imp"] = "Mage", ["Cultist"] = "Acolyte",
        };

        static readonly Dictionary<string, string> unitWeapon = new Dictionary<string, string>
        {
            ["Kael"] = "iron_sword", ["Briar"] = "hand_axe", ["Sera"] = "wooden_staff", ["Voss"] = "short_bow",
            ["Bandit"] = "rusty_sword", ["Skeleton"] = "wooden_spear", ["Orc"] = "hand_axe",
            ["Imp"] = "grimoire", ["Cultist"] = "grimoire",
        };

        public static Unit Create(string id, bool isPlayer, Vector2Int coord, GridManager grid)
        {
            string classId = unitClass.TryGetValue(id, out var c) ? c : "Squire";
            var rec = ClassDatabase.Get(classId);

            var go = new GameObject(id);
            go.transform.position = grid.TileTop(coord);

            var unit = go.AddComponent<Unit>();
            unit.Init(isPlayer ? $"P_{id}" : $"E_{id}_{coord}", id, isPlayer, rec.Base, coord);
            unit.RosterId = id;
            unit.SetClass(classId);
            if (unitWeapon.TryGetValue(id, out var wid))
                unit.Equipped[EquipSlot.Weapon] = EquipmentData.FindWeapon(wid);
            unit.RefreshStats();
            unit.HP = unit.Stats.MaxHP;
            unit.MP = unit.Stats.MaxMP;

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = SpriteFactory.Character(unit.Stats.Weapon,
                isPlayer ? new Color(0.36f, 0.42f, 0.48f) : new Color(0.36f, 0.23f, 0.23f),
                isPlayer ? new Color(0.29f, 0.42f, 0.54f) : new Color(0.55f, 0.15f, 0.10f), isPlayer);
            go.AddComponent<YSort>();

            var sh = new GameObject("Shadow");
            sh.transform.SetParent(go.transform);
            sh.transform.localPosition = new Vector3(0, -0.62f, 0);
            var shsr = sh.AddComponent<SpriteRenderer>();
            shsr.sprite = SpriteFactory.BlobShadow();
            var shSort = sh.AddComponent<YSort>();
            shSort.Offset = -1;

            if (!isPlayer)
            {
                if (id == "Skeleton") unit.Behavior = AIBehavior.Defensive;
                else if (id == "Cultist") unit.Behavior = AIBehavior.Support;
                else if (id == "Wolf") unit.Behavior = AIBehavior.Skirmisher;
                else unit.Behavior = AIBehavior.Aggressive;
            }
            go.AddComponent<UnitMovement>();
            go.AddComponent<HealthBarUI>();
            grid.Tiles[coord.x, coord.y].Occupant = unit;
            return unit;
        }

        // Rebuild a roster member from save data (level, xp, mastery, gear).
        public static Unit CreateFromSave(UnitSave save, Vector2Int coord, GridManager grid, bool isPlayer)
        {
            var rec = ClassDatabase.Get(save.classId);
            var go = new GameObject(save.rosterId);
            go.transform.position = grid.TileTop(coord);

            var unit = go.AddComponent<Unit>();
            unit.Init((isPlayer ? "P_" : "E_") + save.rosterId, save.rosterId, isPlayer, rec.Base, coord);
            unit.RosterId = save.rosterId;
            unit.SetClass(save.classId);
            unit.ApplyLevel(Mathf.Max(1, save.level));
            unit.XP = save.xp;
            foreach (var m in save.mastery) unit.Mastery[m.classId] = m.pct;
            Equip(unit, EquipSlot.Weapon, save.weaponId);
            Equip(unit, EquipSlot.Armor, save.armorId);
            Equip(unit, EquipSlot.Helmet, save.helmetId);
            Equip(unit, EquipSlot.Accessory, save.accId);
            unit.RefreshStats();
            unit.HP = unit.Stats.MaxHP;
            unit.MP = unit.Stats.MaxMP;

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = SpriteFactory.Character(unit.Stats.Weapon,
                isPlayer ? new Color(0.36f, 0.42f, 0.48f) : new Color(0.36f, 0.23f, 0.23f),
                isPlayer ? new Color(0.29f, 0.42f, 0.54f) : new Color(0.55f, 0.15f, 0.10f), isPlayer);
            go.AddComponent<YSort>();

            var sh = new GameObject("Shadow");
            sh.transform.SetParent(go.transform);
            sh.transform.localPosition = new Vector3(0, -0.62f, 0);
            var shsr = sh.AddComponent<SpriteRenderer>();
            shsr.sprite = SpriteFactory.BlobShadow();
            var shSort = sh.AddComponent<YSort>();
            shSort.Offset = -1;

            go.AddComponent<UnitMovement>();
            go.AddComponent<HealthBarUI>();
            grid.Tiles[coord.x, coord.y].Occupant = unit;
            return unit;
        }

        static void Equip(Unit unit, EquipSlot slot, string id)
        {
            if (string.IsNullOrEmpty(id)) return;
            var e = EquipmentData.FindAny(id);
            if (e.Id != null) unit.Equipped[slot] = e;
        }
    }
}
