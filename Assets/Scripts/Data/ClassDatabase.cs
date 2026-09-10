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
        };

        public static ClassRecord Get(string id) => table.TryGetValue(id, out var r) ? r : table["Squire"];

        public static List<Skill> UnlockedSkills(string classId, int level)
        {
            var rec = Get(classId);
            var list = new List<Skill> { SkillDatabase.Get("Attack") };
            foreach (var sid in rec.Skills)
            {
                var sk = SkillDatabase.Get(sid);
                if (level >= sk.UnlockLevel) list.Add(sk);
            }
            return list;
        }
    }
}
