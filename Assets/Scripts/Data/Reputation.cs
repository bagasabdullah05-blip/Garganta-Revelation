using System.Collections.Generic;

namespace Garganta.Data
{
    // Faction standing drives the 5 endings (M6). Choice dialogue + battle events adjust it.
    public static class Reputation
    {
        public static readonly string[] Factions = { "Dominion", "Ironhold", "Valenwood", "Shadow", "Alliance" };

        [System.Serializable]
        public struct RepEntry { public string faction; public int pts; }

        public static int Get(GameSave save, string faction)
        {
            if (save == null) return 0;
            int i = save.rep.FindIndex(r => r.faction == faction);
            return i >= 0 ? save.rep[i].pts : 0;
        }

        public static void Add(GameSave save, string faction, int pts)
        {
            if (save == null || string.IsNullOrEmpty(faction)) return;
            int i = save.rep.FindIndex(r => r.faction == faction);
            if (i >= 0) { var e = save.rep[i]; e.pts += pts; save.rep[i] = e; }
            else save.rep.Add(new RepEntry { faction = faction, pts = pts });
        }

        public static string Summary(GameSave save)
        {
            if (save == null) return "";
            var parts = new List<string>();
            foreach (var f in Factions) parts.Add($"{f}:{Get(save, f)}");
            return string.Join(" ", parts);
        }
    }
}
