using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Garganta.Data;
using Garganta.Units;

public class Tier3Tests
{
    readonly List<GameObject> trash = new List<GameObject>();

    Unit Make()
    {
        var go = new GameObject("t3");
        trash.Add(go);
        var u = go.AddComponent<Unit>();
        u.Init("t3", "T3", true, ClassDatabase.Get("Squire").Base, new Vector2Int(0, 0));
        u.SetClass("Squire");
        return u;
    }

    [TearDown]
    public void Cleanup()
    {
        foreach (var go in trash) Object.DestroyImmediate(go);
        trash.Clear();
    }

    [Test]
    public void Reqs_Tier3()
    {
        var reqs = ClassDatabase.ReclassReqs("HolyKnight");
        Assert.AreEqual(2, reqs.Count);
        Assert.AreEqual("Paladin", reqs[0].classId);
        Assert.AreEqual(12, reqs[0].jobLevel);
        Assert.IsTrue(ClassDatabase.IsTier2("Seraph")); // req table covers Tier3 too
    }

    [Test]
    public void CanReclass_Tier3Gated()
    {
        var u = Make();
        u.JobLevels["Paladin"] = 11;
        u.JobLevels["Dragoon"] = 12;
        Assert.IsFalse(ClassDatabase.CanReclass(u, "HolyKnight"));
        u.JobLevels["Paladin"] = 12;
        Assert.IsTrue(ClassDatabase.CanReclass(u, "HolyKnight"));
    }

    [Test]
    public void MasterSkills_Exist()
    {
        Assert.AreEqual(2.5f, SkillDatabase.Get("Judgment").Power);
        Assert.IsTrue(SkillDatabase.Get("Benediction2").HealIsPct);
        Assert.IsTrue(SkillDatabase.Get("VoidStep").AlwaysHit);
        Assert.AreEqual(9, SkillDatabase.Get("DivineGuard").AoE);
        Assert.IsTrue(SkillDatabase.Get("RunicBlade").Drain);
    }

    [Test]
    public void Tier3_ReclassKeepsLevel()
    {
        var u = Make();
        u.ApplyLevel(10);
        u.JobLevels["Paladin"] = 12;
        u.JobLevels["Dragoon"] = 12;
        u.ReclassTo(ClassDatabase.Get("HolyKnight"));
        Assert.AreEqual("HolyKnight", u.ClassId);
        Assert.AreEqual(10, u.Level);
        Assert.Contains("Squire", u.KnownClasses);
    }
}

public class EndingTests
{
    GameSave SaveWith(int rosterCount)
    {
        var s = new GameSave();
        for (int i = 0; i < rosterCount; i++)
            s.roster.Add(new UnitSave { rosterId = "U" + i });
        return s;
    }

    [Test]
    public void True_NeedsFullRoster()
    {
        Assert.AreEqual("true", Endings.Compute(SaveWith(15), 0).Id);
        Assert.AreNotEqual("true", Endings.Compute(SaveWith(14), 0).Id);
    }

    [Test]
    public void Blight_HighCorruption()
    {
        Assert.AreEqual("blight", Endings.Compute(SaveWith(5), 80).Id);
        Assert.AreNotEqual("blight", Endings.Compute(SaveWith(5), 10).Id);
    }

    [Test]
    public void Shadow_TopFaction()
    {
        var s = SaveWith(5);
        Reputation.Add(s, "Shadow", 5);
        Assert.AreEqual("shadow", Endings.Compute(s, 0).Id);
    }

    [Test]
    public void Sacrifice_AllianceTop()
    {
        var s = SaveWith(5);
        Reputation.Add(s, "Alliance", 4);
        Reputation.Add(s, "Shadow", 1);
        Assert.AreEqual("sacrifice", Endings.Compute(s, 0).Id);
    }

    [Test]
    public void Light_Default()
    {
        Assert.AreEqual("light", Endings.Compute(SaveWith(5), 0).Id);
    }
}

public class SkirmishTests
{
    [Test]
    public void Scaling_CountAndLevel()
    {
        var early = SkirmishGen.Build(5, 0, 42, false);
        Assert.AreEqual(3, early.EnemyIds.Length);
        Assert.AreEqual(5, early.EnemyLevel);
        Assert.GreaterOrEqual(early.MapVariant, 0);
        Assert.LessOrEqual(early.MapVariant, 2);
        var late = SkirmishGen.Build(8, 8, 7, true);
        Assert.AreEqual(6, late.EnemyIds.Length);
        Assert.AreEqual(10, late.EnemyLevel);
    }

    [Test]
    public void Deterministic_SameSeed()
    {
        var a = SkirmishGen.Build(5, 2, 99, false);
        var b = SkirmishGen.Build(5, 2, 99, false);
        Assert.AreEqual(a.EnemyIds, b.EnemyIds);
    }

    [Test]
    public void PartyAverage_Math()
    {
        var roster = new List<UnitSave>
        {
            new UnitSave { level = 4 },
            new UnitSave { level = 6 },
        };
        Assert.AreEqual(5, SkirmishGen.PartyAverage(roster));
        Assert.AreEqual(1, SkirmishGen.PartyAverage(new List<UnitSave>()));
    }
}

public class WaveTests
{
    [Test]
    public void Chapters_WithWaves()
    {
        Assert.IsNotEmpty(ChapterDatabase.GetById("ch8").Wave2Ids);
        Assert.IsNotEmpty(ChapterDatabase.GetById("ch9").Wave2Ids);
        Assert.IsNotEmpty(ChapterDatabase.GetById("ch11").Wave2Ids);
        var ch1 = ChapterDatabase.GetById("ch1");
        Assert.IsTrue(ch1.Wave2Ids == null || ch1.Wave2Ids.Length == 0);
    }

    [Test]
    public void FinalNode_IsCh11()
    {
        var last = ChapterDatabase.GetNode(ChapterDatabase.NodeCount - 1);
        Assert.AreEqual("ch11", last.Id);
        Assert.AreEqual(6, last.PlayerIds.Length);
        Assert.Contains("Usurper", last.EnemyIds);
    }

    [Test]
    public void NodeCount_Twelve()
    {
        Assert.AreEqual(12, ChapterDatabase.NodeCount);
    }
}
