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
        public static bool Take(string id, int n = 1)
        {
            if (Count(id) < n) return false;
            stock[id] = Count(id) - n;
            return true;
        }

        public static IEnumerable<KeyValuePair<string, int>> Stock() => stock;
    }
}
