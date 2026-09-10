using System.Collections.Generic;

namespace Garganta.Data
{
    // Party-wide gold + consumable stock (Bastion shop hooks in M3).
    public static class Inventory
    {
        public static int Gold = 200;
        static readonly Dictionary<string, int> stock = new Dictionary<string, int>();

        static Inventory()
        {
            stock["potion"] = 3;
            stock["ether"] = 1;
        }

        public static int Count(string id) => stock.TryGetValue(id, out int v) ? v : 0;
        public static void Add(string id, int n = 1) => stock[id] = Count(id) + n;
        public static void Clear() => stock.Clear();
        public static bool Take(string id, int n = 1)
        {
            if (Count(id) < n) return false;
            stock[id] = Count(id) - n;
            return true;
        }

        public static IEnumerable<KeyValuePair<string, int>> Stock() => stock;

        // Owned (unequipped) gear by equipment id. Mirrors GameSave.ownedEquip at runtime.
        public static readonly List<string> OwnedEquip = new List<string>();
        public static void AddOwned(string id) => OwnedEquip.Add(id);
        public static bool RemoveOwned(string id) => OwnedEquip.Remove(id);

        public static bool TryBuyConsumable(string id)
        {
            var c = EquipmentData.FindConsumable(id);
            if (c.Id == null || Gold < c.Price) return false;
            Gold -= c.Price;
            Add(id);
            return true;
        }

        public static bool TryBuyEquipment(Equipment e, List<string> saveOwned)
        {
            if (Gold < e.Price) return false;
            Gold -= e.Price;
            OwnedEquip.Add(e.Id);
            saveOwned?.Add(e.Id);
            return true;
        }
    }
}
