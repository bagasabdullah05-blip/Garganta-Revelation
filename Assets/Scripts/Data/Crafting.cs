using System.Collections.Generic;

namespace Garganta.Data
{
    // Bastion Workshop: gear + material/consumable -> better gear (per EQUIPMENT_SYSTEM.md §5).
    public static class Crafting
    {
        public struct MatNeed { public string id; public int count; }

        public struct Recipe
        {
            public string Name;
            public string ResultEquip;   // equipment id ("" if consumable result)
            public string ResultConsum;  // consumable id ("" if equipment result)
            public string NeedEquip;     // owned gear consumed ("" if none)
            public List<MatNeed> NeedMats;
            public List<MatNeed> NeedCons; // consumables consumed (as MatNeed)
        }

        static Recipe Gear(string name, string result, string needEquip, params MatNeed[] mats)
            => new Recipe { Name = name, ResultEquip = result, NeedEquip = needEquip, NeedMats = new List<MatNeed>(mats), NeedCons = new List<MatNeed>() };

        static MatNeed M(string id, int n) => new MatNeed { id = id, count = n };

        public static readonly List<Recipe> Recipes = new List<Recipe>
        {
            Gear("Steel Sword", "steel_sword", "iron_sword", M("iron_ore", 3)),
            Gear("Flametongue", "flametongue", "iron_sword", M("fire_crystal", 2)),
            Gear("Partisan", "partisan", "iron_lance", M("iron_ore", 3)),
            Gear("Dragon Mail", "dragon_mail", "chainmail", M("dragon_scale", 2)),
            Gear("Materia Staff", "materia_staff", "iron_staff", M("magic_essence", 2)),
            Gear("Hunter Bow", "hunter_bow", "longbow", M("leather_hide", 2)),
            Gear("Blight Edge", "blight_edge", "steel_sword", M("blight_ichor", 3)),
            new Recipe { Name = "Elixir", ResultConsum = "elixir", NeedEquip = "",
                NeedMats = new List<MatNeed>(), NeedCons = new List<MatNeed> { M("potion", 1), M("ether", 1) } },
        };

        public static readonly Dictionary<string, int> MatPrices = new Dictionary<string, int>
        {
            ["iron_ore"] = 30, ["herbs"] = 20, ["leather_hide"] = 50,
            ["fire_crystal"] = 150, ["magic_essence"] = 150,
            ["dragon_scale"] = 400, ["blight_ichor"] = 200,
        };

        public static string MatName(string id)
        {
            switch (id)
            {
                case "iron_ore": return "Iron Ore";
                case "herbs": return "Herbs";
                case "leather_hide": return "Leather Hide";
                case "fire_crystal": return "Fire Crystal";
                case "magic_essence": return "Magic Essence";
                case "dragon_scale": return "Dragon Scale";
                case "blight_ichor": return "Blight Ichor";
                default: return id;
            }
        }

        public static bool CanCraft(Recipe r, List<string> owned)
        {
            if (!string.IsNullOrEmpty(r.NeedEquip) && !owned.Contains(r.NeedEquip)) return false;
            foreach (var m in r.NeedMats) if (Inventory.MatCount(m.id) < m.count) return false;
            foreach (var c in r.NeedCons) if (Inventory.Count(c.id) < c.count) return false;
            return true;
        }

        public static bool Craft(Recipe r, List<string> owned, List<string> saveOwned)
        {
            if (!CanCraft(r, owned)) return false;
            if (!string.IsNullOrEmpty(r.NeedEquip)) { owned.Remove(r.NeedEquip); saveOwned?.Remove(r.NeedEquip); }
            foreach (var m in r.NeedMats) Inventory.TakeMat(m.id, m.count);
            foreach (var c in r.NeedCons) Inventory.Take(c.id, c.count);
            if (!string.IsNullOrEmpty(r.ResultEquip)) { owned.Add(r.ResultEquip); saveOwned?.Add(r.ResultEquip); }
            if (!string.IsNullOrEmpty(r.ResultConsum)) Inventory.Add(r.ResultConsum);
            return true;
        }
    }
}
