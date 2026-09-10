using System.Collections.Generic;
using UnityEngine;

namespace Garganta.Art
{
    // Drop-in pipeline for final art: any Sprite under Resources/Art/** named
    // exactly like a SpriteFactory cache key (see docs/ART_TASKS.md) wins
    // automatically over the procedural placeholder. No code changes needed.
    public static class ArtOverride
    {
        static Dictionary<string, Sprite> table;

        public static void LoadAll()
        {
            table = new Dictionary<string, Sprite>();
            foreach (var s in Resources.LoadAll<Sprite>("Art"))
                if (s != null && !table.ContainsKey(s.name)) table[s.name] = s;
        }

        public static Sprite Get(string key)
        {
            if (table == null) return null;
            return table.TryGetValue(key, out var s) ? s : null;
        }

        public static void Register(string key, Sprite s)
        {
            if (table == null) table = new Dictionary<string, Sprite>();
            table[key] = s;
        }

        public static void Clear() => table?.Clear();
        public static int Count => table?.Count ?? 0;
    }
}
