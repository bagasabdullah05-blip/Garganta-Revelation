using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Garganta.Data;
using Garganta.Units;

public class LevelingTests
{
    readonly List<GameObject> trash = new List<GameObject>();

    Unit MakeSquire()
    {
        var go = new GameObject("sq");
        trash.Add(go);
        var u = go.AddComponent<Unit>();
        u.Init("sq", "Sq", true, ClassDatabase.Get("Squire").Base, new Vector2Int(0, 0));
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
    public void XpNeed_MatchesFormula()
    {
        Assert.AreEqual(100, Unit.XpNeed(1));
        Assert.AreEqual(282, Unit.XpNeed(2)); // floor(100 * 2^1.5)
        Assert.AreEqual(1118, Unit.XpNeed(5));
    }

    [Test]
    public void LevelUp_AppliesGrowth()
    {
        var u = MakeSquire();
        Assert.IsTrue(u.GainXP(100));
        Assert.AreEqual(2, u.Level);
        Assert.AreEqual(14, u.CoreStats.ATK); // 12 + 2
        Assert.AreEqual(108, u.CoreStats.MaxHP); // 100 + 8
        Assert.AreEqual(108, u.HP);
    }

    [Test]
    public void LevelUp_MultipleLevels()
    {
        var u = MakeSquire();
        u.GainXP(100 + 282); // L1->2->3
        Assert.AreEqual(3, u.Level);
        Assert.AreEqual(16, u.CoreStats.ATK);
    }

    [Test]
    public void Mastery_CappedAt100()
    {
        var u = MakeSquire();
        u.AddMastery("Squire", 150);
        Assert.AreEqual(100, u.MasteryOf("Squire"));
    }

    [Test]
    public void DeadUnit_GainsNoXP()
    {
        var u = MakeSquire();
        u.TakeDamage(9999);
        Assert.IsFalse(u.GainXP(500));
        Assert.AreEqual(1, u.Level);
    }
}
