using System.Collections.Generic;

namespace Garganta.Data
{
    public enum SkillEffect { Damage, Heal, BuffAtk, BuffDef, BuffEva, BuffMag, Recruit }

    public struct Skill
    {
        public string Id;
        public string Name;
        public SkillEffect Effect;
        public float Power;      // damage mult | heal MAG mult / pct | buff amount
        public bool HealIsPct;
        public bool Cleanse;
        public bool StealGold;
        public bool Drain;       // heal caster for half the damage dealt
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
            ["Talk"] = new Skill { Id = "Talk", Name = "Talk", Effect = SkillEffect.Recruit, Power = 0f, CostMP = 0, UnlockLevel = 1, Range = 1 },
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
            // Tier 2 (M4)
            ["HolyBlade"] = D("HolyBlade", "Holy Blade", 1.5f, 10, 1, true),
            ["LayOnHands"] = new Skill { Id = "LayOnHands", Name = "Lay on Hands", Effect = SkillEffect.Heal, Power = 1.2f, CostMP = 10, UnlockLevel = 3, Magical = true, TargetsAllies = true, Range = 2 },
            ["DivineShield"] = new Skill { Id = "DivineShield", Name = "Divine Shield", Effect = SkillEffect.BuffDef, Power = 8, CostMP = 8, UnlockLevel = 5, SelfOnly = true, Duration = 3 },
            ["Jump"] = new Skill { Id = "Jump", Name = "Jump", Effect = SkillEffect.Damage, Power = 2f, CostMP = 12, UnlockLevel = 1, IgnoreDefPct = 0.7f },
            ["LanceSpin"] = new Skill { Id = "LanceSpin", Name = "Lance Spin", Effect = SkillEffect.Damage, Power = 1f, CostMP = 10, UnlockLevel = 3, AoE = 1 },
            ["Bulwark"] = new Skill { Id = "Bulwark", Name = "Bulwark", Effect = SkillEffect.BuffDef, Power = 5, CostMP = 6, UnlockLevel = 5, SelfOnly = true, Duration = 3 },
            ["ShadowStrike"] = D("ShadowStrike", "Shadow Strike", 1.6f, 8, 1),
            ["CripplingCut"] = new Skill { Id = "CripplingCut", Name = "Crippling Cut", Effect = SkillEffect.Damage, Power = 1.2f, CostMP = 10, UnlockLevel = 3, StunTurns = 1 },
            ["SmokeVeil"] = new Skill { Id = "SmokeVeil", Name = "Smoke Veil", Effect = SkillEffect.BuffEva, Power = 25, CostMP = 6, UnlockLevel = 5, SelfOnly = true, Duration = 2 },
            ["Firaja"] = new Skill { Id = "Firaja", Name = "Firaja", Effect = SkillEffect.Damage, Power = 1.6f, CostMP = 16, UnlockLevel = 1, Magical = true, AoE = 1 },
            ["Flare"] = D("Flare", "Flare", 2.2f, 18, 3, true),
            ["Stormcall"] = new Skill { Id = "Stormcall", Name = "Stormcall", Effect = SkillEffect.Damage, Power = 1.6f, CostMP = 14, UnlockLevel = 5, Magical = true, IgnoreDefPct = 0.5f },
            ["Benediction"] = new Skill { Id = "Benediction", Name = "Benediction", Effect = SkillEffect.Heal, Power = 60, HealIsPct = true, CostMP = 14, UnlockLevel = 1, Magical = true, TargetsAllies = true, Range = 3 },
            ["Sanctuary"] = new Skill { Id = "Sanctuary", Name = "Sanctuary", Effect = SkillEffect.Heal, Power = 25, HealIsPct = true, Cleanse = true, CostMP = 16, UnlockLevel = 3, Magical = true, TargetsAllies = true, Range = 3, AoE = 2 },
            ["Holy"] = D("Holy", "Holy", 1.6f, 14, 5, true),
            ["RunicBlade"] = new Skill { Id = "RunicBlade", Name = "Runic Blade", Effect = SkillEffect.Damage, Power = 1.4f, CostMP = 10, UnlockLevel = 1, Drain = true },
            ["SpellWard"] = new Skill { Id = "SpellWard", Name = "Spell Ward", Effect = SkillEffect.BuffDef, Power = 6, CostMP = 8, UnlockLevel = 3, SelfOnly = true, Duration = 3 },
            ["AetherEdge"] = D("AetherEdge", "Aether Edge", 1.8f, 14, 5, true),
            // Tier 3 master (M5)
            ["Judgment"] = D("Judgment", "Judgment", 2.5f, 22, 1, true),
            ["DivineGuard"] = new Skill { Id = "DivineGuard", Name = "Divine Guard", Effect = SkillEffect.BuffDef, Power = 10, CostMP = 16, UnlockLevel = 3, TargetsAllies = true, AoE = 9, Duration = 1 },
            ["RadiantStrike"] = D("RadiantStrike", "Radiant Strike", 1.8f, 12, 5),
            ["ElementalSurge"] = new Skill { Id = "ElementalSurge", Name = "Elemental Surge", Effect = SkillEffect.Damage, Power = 1.8f, CostMP = 20, UnlockLevel = 1, Magical = true, AoE = 2 },
            ["EnchantBlade"] = new Skill { Id = "EnchantBlade", Name = "Enchant Blade", Effect = SkillEffect.BuffAtk, Power = 8, CostMP = 12, UnlockLevel = 3, SelfOnly = true, Duration = 3 },
            ["SpellStrike"] = D("SpellStrike", "Spell Strike", 2f, 16, 5),
            ["VoidStep"] = new Skill { Id = "VoidStep", Name = "Void Step", Effect = SkillEffect.Damage, Power = 2f, CostMP = 16, UnlockLevel = 1, AlwaysHit = true },
            ["UmbralSlash"] = new Skill { Id = "UmbralSlash", Name = "Umbral Slash", Effect = SkillEffect.Damage, Power = 1.6f, CostMP = 12, UnlockLevel = 3, StunTurns = 1 },
            ["NightVeil"] = new Skill { Id = "NightVeil", Name = "Night Veil", Effect = SkillEffect.BuffEva, Power = 30, CostMP = 10, UnlockLevel = 5, SelfOnly = true, Duration = 2 },
            ["Benediction2"] = new Skill { Id = "Benediction2", Name = "Grand Benediction", Effect = SkillEffect.Heal, Power = 80, HealIsPct = true, CostMP = 20, UnlockLevel = 1, Magical = true, TargetsAllies = true, Range = 3, AoE = 3 },
            ["SeraphicWard"] = new Skill { Id = "SeraphicWard", Name = "Seraphic Ward", Effect = SkillEffect.BuffDef, Power = 8, CostMP = 14, UnlockLevel = 3, TargetsAllies = true, Range = 3, AoE = 3, Duration = 3 },
            ["Wrath"] = D("Wrath", "Wrath", 2f, 18, 5, true),
        };

        public static Skill Get(string id) => table.TryGetValue(id, out var s) ? s : table["Attack"];
    }
}
