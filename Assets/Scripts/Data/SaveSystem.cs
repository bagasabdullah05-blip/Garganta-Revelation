using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Garganta.Data
{
    // JSON save slots. Pure serialize/deserialize covered by EditMode tests.
    public static class SaveSystem
    {
        public static GameSave Current;
        public static int LastSlot = 1;

        public static string Serialize(GameSave save) => JsonUtility.ToJson(save, true);
        public static GameSave Deserialize(string json) => JsonUtility.FromJson<GameSave>(json);

        static string SlotPath(int slot, string dir) => Path.Combine(dir ?? Application.persistentDataPath, $"save{slot}.json");
        public static bool SlotExists(int slot, string dir = null) => File.Exists(SlotPath(slot, dir));

        public static void Save(int slot, string dir = null)
        {
            LastSlot = slot;
            File.WriteAllText(SlotPath(slot, dir), Serialize(Current));
        }

        public static bool Load(int slot, string dir = null)
        {
            string path = SlotPath(slot, dir);
            if (!File.Exists(path)) return false;
            LastSlot = slot;
            Current = Deserialize(File.ReadAllText(path));
            ApplyToRuntime();
            return Current != null;
        }

        public static void Delete(int slot, string dir = null)
        {
            string path = SlotPath(slot, dir);
            if (File.Exists(path)) File.Delete(path);
        }

        public static GameSave NewGame()
        {
            var save = new GameSave { version = 3, progress = 0, gold = 200 };
            save.roster.Add(MakeUnit("Kael", "Squire", 2, "iron_sword", "", "", ""));
            save.roster.Add(MakeUnit("Briar", "Squire", 1, "hand_axe", "", "", ""));
            save.stock.Add(new StockEntry { id = "potion", count = 3 });
            save.stock.Add(new StockEntry { id = "ether", count = 1 });
            save.ownedEquip.Add("leather_armor");
            save.ownedEquip.Add("iron_helm");
            save.ownedEquip.Add("power_band");
            Current = save;
            ApplyToRuntime();
            return save;
        }

        static UnitSave MakeUnit(string id, string cls, int lv, string w, string a, string h, string acc)
            => new UnitSave { rosterId = id, classId = cls, level = lv, xp = 0, weaponId = w, armorId = a, helmetId = h, accId = acc, mastery = new List<MasteryEntry>() };

        // Push save -> runtime singletons (Inventory gold/stock).
        public static void ApplyToRuntime()
        {
            if (Current == null) return;
            Inventory.Gold = Current.gold;
            Inventory.Clear();
            foreach (var s in Current.stock) Inventory.Add(s.id, s.count);
        }

        // Pull runtime -> save (call before Save()).
        public static void CaptureRuntime()
        {
            if (Current == null) return;
            Current.gold = Inventory.Gold;
            Current.stock.Clear();
            foreach (var kv in Inventory.Stock())
                Current.stock.Add(new StockEntry { id = kv.Key, count = kv.Value });
        }
    }
}
