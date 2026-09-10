using System.Collections.Generic;
using Garganta.Core;
using Garganta.Units;

namespace Garganta.Data
{
    public class ClassRecord
    {
        public string Id;
        public string Name;
        public UnitStats Base;
        public int GHP, GATK, GDEF, GMAG, GMDEF, GSPD;
        public WeaponType Weapon;
        public string[] Skills;
    }

    // 6 Tier-1 base classes per CLASS_SYSTEM.md (base stats + per-level growth + skill lists).
    public static class ClassDatabase
    {
        static UnitStats S(int hp, int atk, int def, int mag, int mdef, int spd, int move, int range, WeaponType w)
            => new UnitStats { MaxHP = hp, ATK = atk, DEF = def, MAG = mag, MDEF = mdef, SPD = spd, Move = move, Range = range, Acc = 90, Eva = 10, Weapon = w, WeaponMult = 1f, ClassMult = 1f, MaxMP = 20 + mag * 2 };

        static readonly Dictionary<string, ClassRecord> table = new Dictionary<string, ClassRecord>
        {
            ["Squire"] = new ClassRecord { Id = "Squire", Name = "Squire", Base = S(100, 12, 10, 3, 5, 5, 5, 1, WeaponType.Sword),
                GHP = 8, GATK = 2, GDEF = 1, GMAG = 0, GMDEF = 0, GSPD = 1, Weapon = WeaponType.Sword,
                Skills = new[] { "PowerStrike", "ShieldBash", "WarCry", "LimitBreak" } },
            ["Spearman"] = new ClassRecord { Id = "Spearman", Name = "Spearman", Base = S(110, 11, 12, 3, 5, 5, 4, 2, WeaponType.Spear),
                GHP = 9, GATK = 1, GDEF = 2, GMAG = 0, GMDEF = 0, GSPD = 1, Weapon = WeaponType.Spear,
                Skills = new[] { "Pierce", "Phalanx", "Sweep" } },
            ["Archer"] = new ClassRecord { Id = "Archer", Name = "Archer", Base = S(80, 13, 7, 3, 5, 7, 5, 4, WeaponType.Bow),
                GHP = 6, GATK = 2, GDEF = 1, GMAG = 0, GMDEF = 0, GSPD = 2, Weapon = WeaponType.Bow,
                Skills = new[] { "AimedShot", "QuickShot", "RainArrows" } },
            ["Acolyte"] = new ClassRecord { Id = "Acolyte", Name = "Acolyte", Base = S(70, 5, 6, 12, 6, 6, 4, 2, WeaponType.Staff),
                GHP = 5, GATK = 0, GDEF = 1, GMAG = 2, GMDEF = 2, GSPD = 1, Weapon = WeaponType.Staff,
                Skills = new[] { "HealingLight", "Smite", "Cure" } },
            ["Mage"] = new ClassRecord { Id = "Mage", Name = "Mage", Base = S(65, 4, 5, 14, 5, 8, 4, 3, WeaponType.Tome),
                GHP = 4, GATK = 0, GDEF = 0, GMAG = 3, GMDEF = 1, GSPD = 1, Weapon = WeaponType.Tome,
                Skills = new[] { "Fireball", "Blizzard", "Thunder", "Meteor" } },
            ["Thief"] = new ClassRecord { Id = "Thief", Name = "Thief", Base = S(75, 10, 6, 5, 5, 12, 6, 1, WeaponType.Dagger),
                GHP = 5, GATK = 1, GDEF = 0, GMAG = 0, GMDEF = 0, GSPD = 3, Weapon = WeaponType.Dagger,
                Skills = new[] { "Steal", "Stealth", "Assassinate" } },
            // Tier 2 advanced (M4). Base stats extrapolated from Tier 1 + growth below.
            ["Paladin"] = new ClassRecord { Id = "Paladin", Name = "Paladin", Base = S(130, 15, 13, 6, 8, 6, 5, 1, WeaponType.Sword),
                GHP = 10, GATK = 2, GDEF = 2, GMAG = 1, GMDEF = 1, GSPD = 1, Weapon = WeaponType.Sword,
                Skills = new[] { "HolyBlade", "LayOnHands", "DivineShield" } },
            ["Dragoon"] = new ClassRecord { Id = "Dragoon", Name = "Dragoon", Base = S(135, 15, 13, 3, 5, 6, 5, 2, WeaponType.Spear),
                GHP = 11, GATK = 2, GDEF = 2, GMAG = 0, GMDEF = 0, GSPD = 1, Weapon = WeaponType.Spear,
                Skills = new[] { "Jump", "LanceSpin", "Bulwark" } },
            ["Assassin"] = new ClassRecord { Id = "Assassin", Name = "Assassin", Base = S(90, 16, 8, 5, 5, 14, 6, 1, WeaponType.Dagger),
                GHP = 6, GATK = 2, GDEF = 0, GMAG = 0, GMDEF = 0, GSPD = 3, Weapon = WeaponType.Dagger,
                Skills = new[] { "ShadowStrike", "CripplingCut", "SmokeVeil" } },
            ["BlackMage"] = new ClassRecord { Id = "BlackMage", Name = "Black Mage", Base = S(75, 4, 6, 20, 8, 9, 4, 3, WeaponType.Tome),
                GHP = 5, GATK = 0, GDEF = 0, GMAG = 4, GMDEF = 2, GSPD = 1, Weapon = WeaponType.Tome,
                Skills = new[] { "Firaja", "Flare", "Stormcall" } },
            ["WhiteMage"] = new ClassRecord { Id = "WhiteMage", Name = "White Mage", Base = S(85, 5, 8, 18, 14, 7, 4, 3, WeaponType.Staff),
                GHP = 6, GATK = 0, GDEF = 1, GMAG = 3, GMDEF = 3, GSPD = 1, Weapon = WeaponType.Staff,
                Skills = new[] { "Benediction", "Sanctuary", "Holy" } },
            ["RuneKnight"] = new ClassRecord { Id = "RuneKnight", Name = "Rune Knight", Base = S(115, 15, 12, 12, 9, 7, 5, 1, WeaponType.Sword),
                GHP = 8, GATK = 2, GDEF = 1, GMAG = 2, GMDEF = 1, GSPD = 1, Weapon = WeaponType.Sword,
                Skills = new[] { "RunicBlade", "SpellWard", "AetherEdge" } },
        };

        public static ClassRecord Get(string id) => table.TryGetValue(id, out var r) ? r : table["Squire"];

        public const int JobCap = 16;

        public struct JobReq { public string classId; public int jobLevel; }

        // Tier 2 gates per CLASS_SYSTEM.md (job levels, FFT-style).
        static readonly Dictionary<string, List<JobReq>> reqs = new Dictionary<string, List<JobReq>>
        {
            ["Paladin"] = new List<JobReq> { new JobReq { classId = "Squire", jobLevel = 10 }, new JobReq { classId = "Acolyte", jobLevel = 5 } },
            ["Dragoon"] = new List<JobReq> { new JobReq { classId = "Spearman", jobLevel = 10 }, new JobReq { classId = "Squire", jobLevel = 5 } },
            ["Assassin"] = new List<JobReq> { new JobReq { classId = "Thief", jobLevel = 10 }, new JobReq { classId = "Archer", jobLevel = 5 } },
            ["BlackMage"] = new List<JobReq> { new JobReq { classId = "Mage", jobLevel = 10 }, new JobReq { classId = "Archer", jobLevel = 5 } },
            ["WhiteMage"] = new List<JobReq> { new JobReq { classId = "Acolyte", jobLevel = 10 }, new JobReq { classId = "Mage", jobLevel = 5 } },
            ["RuneKnight"] = new List<JobReq> { new JobReq { classId = "Squire", jobLevel = 10 }, new JobReq { classId = "Mage", jobLevel = 5 } },
        };

        public static List<JobReq> ReclassReqs(string classId)
            => reqs.TryGetValue(classId, out var r) ? r : new List<JobReq>();

        public static bool IsTier2(string classId) => reqs.ContainsKey(classId);

        public static bool CanReclass(Units.Unit u, string target)
        {
            foreach (var q in ReclassReqs(target))
                if (u.JobLevelOf(q.classId) < q.jobLevel) return false;
            return ReclassReqs(target).Count > 0;
        }

        public static List<Skill> UnlockedSkills(string classId, int level)
        {
            var rec = Get(classId);
            var list = new List<Skill> { SkillDatabase.Get("Attack"), SkillDatabase.Get("Talk") };
            foreach (var sid in rec.Skills)
            {
                var sk = SkillDatabase.Get(sid);
                if (level >= sk.UnlockLevel) list.Add(sk);
            }
            return list;
        }

        // Union across every mastered class (reclass keeps old skills).
        public static List<Skill> UnlockedSkillsFor(Units.Unit u)
        {
            var list = new List<Skill>();
            var seen = new HashSet<string>();
            foreach (var cls in u.KnownClasses)
                foreach (var sk in UnlockedSkills(cls, u.Level))
                    if (seen.Add(sk.Id)) list.Add(sk);
            if (list.Count == 0) list.Add(SkillDatabase.Get("Attack"));
            return list;
        }
    }
}
