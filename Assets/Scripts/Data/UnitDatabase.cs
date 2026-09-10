using System.Collections.Generic;
using Garganta.Core;

namespace Garganta.Data
{
    // M1 presets from CLASS_SYSTEM.md Tier 1 base stats.
    public static class UnitDatabase
    {
        static readonly Dictionary<string, Units.UnitStats> table = new Dictionary<string, Units.UnitStats>
        {
            ["Kael"]     = S(100, 12, 10, 3, 5, 5, 5, 1, WeaponType.Sword, 1.0f),
            ["Briar"]    = S(110, 13, 11, 2, 4, 4, 4, 1, WeaponType.Axe, 1.1f),
            ["Sera"]     = S(70, 5, 6, 12, 6, 6, 4, 2, WeaponType.Staff, 0.8f),
            ["Voss"]     = S(80, 13, 7, 3, 5, 7, 5, 4, WeaponType.Bow, 1.0f),
            ["Bandit"]   = S(60, 10, 7, 2, 3, 5, 4, 1, WeaponType.Sword, 0.9f),
            ["Goblin"]   = S(45, 9, 5, 2, 2, 8, 5, 1, WeaponType.Dagger, 0.8f),
            ["Wolf"]     = S(55, 11, 6, 1, 3, 9, 6, 1, WeaponType.Dagger, 0.9f),
            ["Skeleton"] = S(50, 9, 8, 2, 4, 3, 3, 1, WeaponType.Sword, 0.9f),
            ["Orc"]      = S(90, 14, 8, 1, 2, 3, 3, 1, WeaponType.Axe, 1.1f),
            ["Imp"]      = S(40, 4, 4, 12, 5, 7, 4, 3, WeaponType.Tome, 1.0f),
        };

        static Units.UnitStats S(int hp, int atk, int def, int mag, int mdef, int spd, int move, int range, WeaponType w, float wm)
            => new Units.UnitStats { MaxHP = hp, ATK = atk, DEF = def, MAG = mag, MDEF = mdef, SPD = spd, Move = move, Range = range, Acc = 90, Eva = 10, Weapon = w, WeaponMult = wm, ClassMult = 1f };

        public static Units.UnitStats Get(string id)
            => table.ContainsKey(id) ? table[id] : table["Bandit"];
    }
}
