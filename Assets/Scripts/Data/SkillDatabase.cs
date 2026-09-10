using System.Collections.Generic;

namespace Garganta.Data
{
    public enum SkillEffect { Damage, Heal, BuffAtk, BuffDef, BuffEva, BuffMag }

    public struct Skill
    {
        public string Id;
        public string Name;
        public SkillEffect Effect;
        public float Power;      // damage mult | heal MAG mult / pct | buff amount
        public bool HealIsPct;
        public bool Cleanse;
        public bool StealGold;
        public int CostMP;
        public bool Magical;
        public int Range;        // 0 = unit's range
        public int AoE;          // 0 = single target (radius in hexes)
        public bool TargetsAllies;
        public bool SelfOnly;
        public int Duration;     // buff turns
        public int StunTurns;
        public float IgnoreDefPct;
        public bool AlwaysHit;
        public int UnlockLevel;  // class level required
    }

    public static class SkillDatabase
    {
        static Skill D(string id, string name, float power, int cost, int unlock, bool magical = false)
            => new Skill { Id = id, Name = name, Effect = SkillEffect.Damage, Power = power, CostMP = cost, UnlockLevel = unlock, Magical = magical };

        static readonly Dictionary<string, Skill> table = new Dictionary<string, Skill>
        {
            ["Attack"] = D("Attack", "Attack", 1f, 0, 1),
            // Squire
            ["PowerStrike"] = D("PowerStrike", "Power Strike", 1.5f, 5, 1),
            ["ShieldBash"] = new Skill { Id = "ShieldBash", Name = "Shield Bash", Effect = SkillEffect.Damage, Power = 1f, CostMP = 6, UnlockLevel = 3, StunTurns = 1 },
            ["WarCry"] = new Skill { Id = "WarCry", Name = "War Cry", Effect = SkillEffect.BuffAtk, Power = 3, CostMP = 8, UnlockLevel = 5, TargetsAllies = true, AoE = 2, Duration = 3 },
            ["LimitBreak"] = D("LimitBreak", "Limit Break", 2.5f, 20, 10),
            // Spearman
            ["Pierce"] = D("Pierce", "Pierce", 1.3f, 5, 1),
            ["Phalanx"] = new Skill { Id = "Phalanx", Name = "Phalanx", Effect = SkillEffect.BuffDef, Power = 5, CostMP = 6, UnlockLevel = 3, SelfOnly = true, Duration = 3 },
            ["Sweep"] = new Skill { Id = "Sweep", Name = "Sweep", Effect = SkillEffect.Damage, Power = 0.8f, CostMP = 8, UnlockLevel = 5, AoE = 1 },
            // Archer
            ["AimedShot"] = D("AimedShot", "Aimed Shot", 1.4f, 5, 1),
            ["QuickShot"] = new Skill { Id = "QuickShot", Name = "Quick Shot", Effect = SkillEffect.Damage, Power = 0.8f, CostMP = 4, UnlockLevel = 3, AlwaysHit = true },
            ["RainArrows"] = new Skill { Id = "RainArrows", Name = "Rain Arrows", Effect = SkillEffect.Damage, Power = 0.8f, CostMP = 10, UnlockLevel = 5, AoE = 1 },
            // Acolyte
            ["HealingLight"] = new Skill { Id = "HealingLight", Name = "Healing Light", Effect = SkillEffect.Heal, Power = 30, HealIsPct = true, CostMP = 8, UnlockLevel = 1, Magical = true, TargetsAllies = true, Range = 3 },
            ["Smite"] = D("Smite", "Smite", 1f, 6, 3, true),
            ["Cure"] = new Skill { Id = "Cure", Name = "Cure", Effect = SkillEffect.Heal, Power = 15, HealIsPct = true, Cleanse = true, CostMP = 8, UnlockLevel = 5, Magical = true, TargetsAllies = true, Range = 3 },
            // Mage
            ["Fireball"] = D("Fireball", "Fireball", 1.2f, 10, 1, true),
            ["Blizzard"] = new Skill { Id = "Blizzard", Name = "Blizzard", Effect = SkillEffect.Damage, Power = 1f, CostMP = 12, UnlockLevel = 3, Magical = true, AoE = 1 },
            ["Thunder"] = new Skill { Id = "Thunder", Name = "Thunder", Effect = SkillEffect.Damage, Power = 1.4f, CostMP = 12, UnlockLevel = 5, Magical = true, IgnoreDefPct = 0.5f },
            ["Meteor"] = new Skill { Id = "Meteor", Name = "Meteor", Effect = SkillEffect.Damage, Power = 2f, CostMP = 25, UnlockLevel = 10, Magical = true, AoE = 2 },
            // Thief
            ["Steal"] = new Skill { Id = "Steal", Name = "Steal", Effect = SkillEffect.Damage, Power = 0.5f, CostMP = 0, UnlockLevel = 1, StealGold = true },
            ["Stealth"] = new Skill { Id = "Stealth", Name = "Stealth", Effect = SkillEffect.BuffEva, Power = 30, CostMP = 6, UnlockLevel = 3, SelfOnly = true, Duration = 2 },
            ["Assassinate"] = D("Assassinate", "Assassinate", 1.8f, 14, 10),
        };

        public static Skill Get(string id) => table.TryGetValue(id, out var s) ? s : table["Attack"];
    }
}
