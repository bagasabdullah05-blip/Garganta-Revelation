using System;
using System.Collections.Generic;

namespace Garganta.Data
{
    [Serializable]
    public struct MasteryEntry { public string classId; public int pct; }

    [Serializable]
    public struct JobEntry { public string classId; public int level; }

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
        public List<JobEntry> jobs;
        public List<string> known;
        public int corruption;
    }

    [Serializable]
    public struct StockEntry { public string id; public int count; }

    [Serializable]
    public class GameSave
    {
        public int version = 4;
        public int progress; // highest unlocked node index
        public List<UnitSave> roster = new List<UnitSave>();
        public int gold;
        public List<StockEntry> stock = new List<StockEntry>();
        public List<StockEntry> mats = new List<StockEntry>();
        public List<string> ownedEquip = new List<string>();
        public List<Reputation.RepEntry> rep = new List<Reputation.RepEntry>();
        public List<Bonds.BondEntry> bonds = new List<Bonds.BondEntry>();
        public bool hasEnding; // post-game unlocked (Primeval Lair)
        public bool ngPlus;
        public int difficulty = 1; // 0 Story, 1 Normal, 2 Hard, 3 Nightmare
    }
}
