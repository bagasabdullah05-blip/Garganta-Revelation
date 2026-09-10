using System.Collections.Generic;

namespace Garganta.Data
{
    public struct Skill
    {
        public string Id;
        public string Name;
        public float Power;
        public int CostMP;
        public bool Magical;
    }

    // M1: basic Attack only (+2 preview skills for M2 job system).
    public static class SkillDatabase
    {
        static readonly Dictionary<string, Skill> table = new Dictionary<string, Skill>
        {
            ["Attack"] = new Skill { Id = "Attack", Name = "Attack", Power = 1f, CostMP = 0, Magical = false },
            ["PowerStrike"] = new Skill { Id = "PowerStrike", Name = "Power Strike", Power = 1.5f, CostMP = 5, Magical = false },
            ["Fireball"] = new Skill { Id = "Fireball", Name = "Fireball", Power = 1.2f, CostMP = 10, Magical = true },
        };

        public static Skill Get(string id) => table.ContainsKey(id) ? table[id] : table["Attack"];
    }
}
