using System.Collections.Generic;
using UnityEngine;
using Garganta.AI;
using Garganta.Core;
using Garganta.Data;

namespace Garganta.Units
{
    public class Unit : MonoBehaviour
    {
        public string UnitName = "Unit";
        public string RosterId; // save-system id (Kael, Briar, ...)
        public bool IsPlayer;
        public AIBehavior Behavior = AIBehavior.Aggressive;
        public string ClassId = "Squire";
        public List<string> KnownClasses = new List<string>();
        public Dictionary<string, int> JobLevels = new Dictionary<string, int>();
        public int Corruption; // 0-100 Aether corruption (M4)
        public string RecruitId; // set on enemies that can be Talk-recruited (M4)
        public float StatMult = 1f; // enemy mook penalty (0.7x); restored on recruit
        public int Level = 1;
        public int XP = 0;
        public int MP;
        public UnitStats CoreStats = new UnitStats(); // base + growth (no equipment)
        public UnitStats Stats = new UnitStats();     // effective in battle
        public Dictionary<EquipSlot, Equipment> Equipped = new Dictionary<EquipSlot, Equipment>();
        public Dictionary<string, int> Mastery = new Dictionary<string, int>();
        public Vector2Int Coord;
        public float CTB;
        public int HP;
        // Temporary battle buffs (shared duration for M2)
        public int BuffAtk, BuffDef, BuffEva, BuffMag, BuffTurns;
        public int StunTurns;
        public bool IsAlive => HP > 0;

        public void Init(string id, string display, bool player, UnitStats s, Vector2Int c)
        {
            name = id;
            UnitName = display;
            IsPlayer = player;
            CoreStats = s.Clone();
            Coord = c;
            HP = s.MaxHP;
            MP = 0;
            CTB = 0f;
            RefreshStats();
        }

        public void SetClass(string classId)
        {
            ClassId = classId;
            if (!KnownClasses.Contains(classId)) KnownClasses.Add(classId);
            if (!JobLevels.ContainsKey(classId)) JobLevels[classId] = 1;
            MaxMPFromMag();
            MP = Stats.MaxMP;
        }

        public int JobLevelOf(string classId) => JobLevels.TryGetValue(classId, out int v) ? v : 0;
        public void AddJobLevel(string classId) => JobLevels[classId] = Mathf.Min(ClassDatabase.JobCap, JobLevelOf(classId) + 1);

        // Barracks reclass: rebuild core stats from the new class, keep HP fraction + gear + skills.
        public void ReclassTo(ClassRecord rec)
        {
            float frac = Stats.MaxHP > 0 ? (float)HP / Stats.MaxHP : 1f;
            CoreStats = rec.Base.Clone();
            for (int i = 1; i < Level; i++)
            {
                CoreStats.MaxHP += rec.GHP;
                CoreStats.ATK += rec.GATK;
                CoreStats.DEF += rec.GDEF;
                CoreStats.MAG += rec.GMAG;
                CoreStats.MDEF += rec.GMDEF;
                CoreStats.SPD += rec.GSPD;
            }
            SetClass(rec.Id);
            RefreshStats();
            HP = Mathf.Max(1, Mathf.RoundToInt(Stats.MaxHP * frac));
            MP = Stats.MaxMP;
        }

        void MaxMPFromMag() => CoreStats.MaxMP = 20 + CoreStats.MAG * 2;

        public void RefreshStats()
        {            Stats = CoreStats.Clone();
            foreach (var kv in Equipped)
            {
                var e = kv.Value;
                Stats.ATK += e.ATK;
                Stats.DEF += e.DEF;
                Stats.MAG += e.MAG;
                Stats.MDEF += e.MDEF;
                Stats.SPD += e.SPD;
                Stats.Move += e.Move;
                Stats.Range += e.Range;
                Stats.Acc += e.Acc;
                Stats.Eva += e.Eva;
                if (kv.Key == EquipSlot.Weapon && e.Weapon != WeaponType.None) Stats.Weapon = e.Weapon;
            }
            Stats.Move = Mathf.Max(1, Stats.Move);
            Stats.Range = Mathf.Max(1, Stats.Range);
            HP = Mathf.Min(HP == 0 ? Stats.MaxHP : HP, Stats.MaxHP);
            MP = Mathf.Min(MP, Stats.MaxMP);
        }

        public static int XpNeed(int level) => Mathf.FloorToInt(100f * Mathf.Pow(level, 1.5f));

        // Difficulty scaling for enemies (Story 0.8x ... Nightmare 1.5x).
        public void ScaleStats(float mult)
        {
            CoreStats.ATK = Mathf.Max(1, Mathf.RoundToInt(CoreStats.ATK * mult));
            CoreStats.DEF = Mathf.Max(0, Mathf.RoundToInt(CoreStats.DEF * mult));
            CoreStats.MAG = Mathf.Max(1, Mathf.RoundToInt(CoreStats.MAG * mult));
            CoreStats.MDEF = Mathf.Max(0, Mathf.RoundToInt(CoreStats.MDEF * mult));
            CoreStats.MaxHP = Mathf.Max(1, Mathf.RoundToInt(CoreStats.MaxHP * mult));
            RefreshStats();
            HP = Stats.MaxHP;
            MP = Stats.MaxMP;
        }

        // Returns true if at least one level gained.
        public bool GainXP(int amount)
        {
            if (!IsAlive || Level >= 50) return false;
            XP += amount;
            bool leveled = false;
            while (Level < 50 && XP >= XpNeed(Level))
            {
                XP -= XpNeed(Level);
                LevelUpOnce();
                leveled = true;
            }
            if (leveled) RefreshStats();
            return leveled;
        }

        // Raise to an exact level (roster restore). No XP cost.
        public void ApplyLevel(int target)
        {
            var rec = ClassDatabase.Get(ClassId);
            while (Level < target && Level < 50) { Level++; ApplyGrowth(rec); }
            RefreshStats();
            HP = Stats.MaxHP;
            MP = Stats.MaxMP;
        }

        void LevelUpOnce()
        {
            Level++;
            ApplyGrowth(ClassDatabase.Get(ClassId));
            MaxMPFromMag();
            MP = Stats.MaxMP;
        }

        void ApplyGrowth(ClassRecord rec, bool withHp = true)
        {
            CoreStats.MaxHP += rec.GHP;
            CoreStats.ATK += rec.GATK;
            CoreStats.DEF += rec.GDEF;
            CoreStats.MAG += rec.GMAG;
            CoreStats.MDEF += rec.GMDEF;
            CoreStats.SPD += rec.GSPD;
            if (withHp) HP += rec.GHP;
        }

        // Full rebuild at current level (recruit penalty removal / reclass repair).
        public void RebuildStats()
        {
            var rec = ClassDatabase.Get(ClassId);
            CoreStats = rec.Base.Clone();
            for (int i = 1; i < Level; i++) ApplyGrowth(rec, false);
            RefreshStats();
            HP = Stats.MaxHP;
            MP = Stats.MaxMP;
        }

        public UnitSave Capture()
        {
            var save = new UnitSave
            {
                rosterId = RosterId,
                classId = ClassId,
                level = Level,
                xp = XP,
                mastery = new List<MasteryEntry>(),
                jobs = new List<JobEntry>(),
                known = new List<string>(KnownClasses),
                corruption = Corruption,
            };
            foreach (var kv in JobLevels)
                save.jobs.Add(new JobEntry { classId = kv.Key, level = kv.Value });
            foreach (var kv in Mastery)
                save.mastery.Add(new MasteryEntry { classId = kv.Key, pct = kv.Value });
            save.weaponId = Equipped.TryGetValue(EquipSlot.Weapon, out var w) ? w.Id : "";
            save.armorId = Equipped.TryGetValue(EquipSlot.Armor, out var a) ? a.Id : "";
            save.helmetId = Equipped.TryGetValue(EquipSlot.Helmet, out var h) ? h.Id : "";
            save.accId = Equipped.TryGetValue(EquipSlot.Accessory, out var ac) ? ac.Id : "";
            return save;
        }

        public int MasteryOf(string classId) => Mastery.TryGetValue(classId, out int v) ? v : 0;
        public void AddMastery(string classId, int amt) => Mastery[classId] = Mathf.Min(100, MasteryOf(classId) + amt);

        public bool UseMP(int cost)
        {
            if (MP < cost) return false;
            MP -= cost;
            return true;
        }

        public void GainCTB(float amount)
        {
            if (IsAlive) CTB += amount;
        }

        public void ResetCTB() => CTB = 0f;

        public void TakeDamage(int amount)
        {
            HP = Mathf.Max(0, HP - amount);
            if (!IsAlive)
            {
                var sr = GetComponent<SpriteRenderer>();
                if (sr != null) sr.color = Color.gray;
            }
        }

        public void Heal(int amount) => HP = Mathf.Min(Stats.MaxHP, HP + amount);

        public void TickEndStatus()
        {
            if (BuffTurns > 0 && --BuffTurns <= 0) { BuffAtk = BuffDef = BuffEva = BuffMag = 0; }
        }
    }
}
