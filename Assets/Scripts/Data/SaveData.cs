using System;
using System.Collections.Generic;

namespace Garganta.Data
{
    [Serializable]
    public struct MasteryEntry { public string classId; public int pct; }

    [Serializable]
    public struct UnitSave
    {
        public string rosterId;
        public string classId;
        public int level;
        public int xp;
        public string weaponId;
        public string armorId;
        public string helmetId;
        public string accId;
        public List<MasteryEntry> mastery;
    }

    [Serializable]
    public struct StockEntry { public string id; public int count; }

    [Serializable]
    public class GameSave
    {
        public int version = 3;
        public int progress; // highest unlocked node index
        public List<UnitSave> roster = new List<UnitSave>();
        public int gold;
        public List<StockEntry> stock = new List<StockEntry>();
        public List<string> ownedEquip = new List<string>();
    }
}
