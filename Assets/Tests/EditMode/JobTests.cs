using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Garganta.Data;
using Garganta.Units;

public class JobTests
{
    readonly List<GameObject> trash = new List<GameObject>();

    Unit Make(string classId, int level = 1)
    {
        var go = new GameObject("job");
        trash.Add(go);
        var u = go.AddComponent<Unit>();
        u.Init("job", classId, true, ClassDatabase.Get(classId).Base, new Vector2Int(0, 0));
        u.SetClass(classId);
        if (level > 1) u.ApplyLevel(level);
        return u;
    }

    [TearDown]
    public void Cleanup()
    {
        foreach (var go in trash) Object.DestroyImmediate(go);
        trash.Clear();
    }

    [Test]
    public void Reclass_LockedAtStart()
    {
        var u = Make("Squire");
        Assert.IsFalse(ClassDatabase.CanReclass(u, "Paladin"));
    }

    [Test]
    public void Reclass_UnlocksWithJobLevels()
    {
        var u = Make("Squire");
        u.JobLevels["Squire"] = 10;
        u.JobLevels["Acolyte"] = 5;
        Assert.IsTrue(ClassDatabase.CanReclass(u, "Paladin"));
        Assert.IsFalse(ClassDatabase.CanReclass(u, "Dragoon")); // needs Spearman 10
    }

    [Test]
    public void ReclassTo_RebuildsStatsKeepsLevel()
    {
        var u = Make("Squire", 5); // ATK 12 + 2*4 = 20
        Assert.AreEqual(20, u.CoreStats.ATK);
        u.ReclassTo(ClassDatabase.Get("Paladin")); // 15 + 2*4 = 23
        Assert.AreEqual("Paladin", u.ClassId);
        Assert.AreEqual(5, u.Level);
        Assert.AreEqual(23, u.CoreStats.ATK);
        Assert.Contains("Squire", u.KnownClasses);
        Assert.Contains("Paladin", u.KnownClasses);
        Assert.AreEqual(u.Stats.MaxHP, u.HP);
    }

    [Test]
    public void JobLevel_Capped()
    {
        var u = Make("Squire");
        u.JobLevels["Squire"] = 16;
        u.AddJobLevel("Squire");
        Assert.AreEqual(16, u.JobLevelOf("Squire"));
        Assert.AreEqual(0, u.JobLevelOf("Mage"));
    }

    [Test]
    public void UnlockedSkills_UnionAcrossClasses()
    {
        var u = Make("Squire");
        var l1 = ClassDatabase.UnlockedSkillsFor(u);
        Assert.AreEqual(3, l1.Count); // Attack + Talk + PowerStrike
        u.KnownClasses.Add("Mage");
        var both = ClassDatabase.UnlockedSkillsFor(u);
        Assert.AreEqual(4, both.Count); // + Fireball
    }
}
