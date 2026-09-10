using System;
using System.Collections.Generic;

namespace Garganta.Data
{
    // Repeatable training battles scale with the party (M5 grinding + post-game).
    public static class SkirmishGen
    {
        static readonly string[] poolEarly = { "Bandit", "Goblin", "Goblin", "Wolf", "Skeleton" };
        static readonly string[] poolLate = { "RogueKnight", "Hunter", "Shaman", "Orc", "Cultist", "Wolf" };

        public struct Skirmish
        {
            public string[] EnemyIds;
            public int EnemyLevel;
            public int MapVariant;
        }

        public static Skirmish Build(int avgLevel, int progress, int seed, bool ngPlus)
        {
            var rng = new Random(seed);
            var pool = progress >= 5 ? poolLate : poolEarly;
            int count = Math.Min(6, 3 + progress / 2);
            var ids = new string[count];
            for (int i = 0; i < count; i++) ids[i] = pool[rng.Next(pool.Length)];
            return new Skirmish
            {
                EnemyIds = ids,
                EnemyLevel = Math.Max(1, avgLevel + (ngPlus ? 2 : 0)),
                MapVariant = rng.Next(3),
            };
        }

        public static int PartyAverage(List<UnitSave> roster)
        {
            if (roster == null || roster.Count == 0) return 1;
            int sum = 0;
            foreach (var r in roster) sum += Math.Max(1, r.level);
            return Math.Max(1, sum / roster.Count);
        }
    }
}
