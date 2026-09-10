using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Garganta.Data;
using Garganta.Units;

public class BondTests
{
    readonly List<GameObject> trash = new List<GameObject>();

    Unit Make(string id, Vector2Int c)
    {
        var go = new GameObject("b" + id);
        trash.Add(go);
        var u = go.AddComponent<Unit>();
        u.Init("b", id, true, ClassDatabase.Get("Squire").Base, c);
        u.SetClass("Squire");
        u.RosterId = id;
        return u;
    }

    [TearDown]
    public void Cleanup()
    {
        foreach (var go in trash) Object.DestroyImmediate(go);
        trash.Clear();
    }

    [Test]
    public void Level_Thresholds()
    {
        Assert.AreEqual(0, Bonds.Level(0));
        Assert.AreEqual(0, Bonds.Level(19));
        Assert.AreEqual(1, Bonds.Level(20));
        Assert.AreEqual(2, Bonds.Level(50));
        Assert.AreEqual(3, Bonds.Level(100));
        Assert.AreEqual(4, Bonds.Level(200));
    }

    [Test]
    public void RecordBattle_AdjacentGives3()
    {
        var save = new GameSave();
        var a = Make("A", new Vector2Int(0, 0));
        var b = Make("B", new Vector2Int(1, 0));
        var news = Bonds.RecordBattle(save, new List<Unit> { a, b });
        Assert.AreEqual(3, Bonds.Points(save, "A", "B"));
        Assert.AreEqual(0, news.Count); // no threshold crossed yet
    }

    [Test]
    public void RecordBattle_FarGives1AndRanksUp()
    {
        var save = new GameSave();
        var a = Make("A", new Vector2Int(0, 0));
        var b = Make("B", new Vector2Int(1, 0));
        List<string> news = null;
        for (int i = 0; i < 7; i++) news = Bonds.RecordBattle(save, new List<Unit> { a, b });
        Assert.AreEqual(21, Bonds.Points(save, "A", "B"));
        Assert.AreEqual(1, news.Count);
        Assert.IsTrue(news[0].Contains("C!"));
    }

    [Test]
    public void HasBondedNeighbor_RangeGated()
    {
        var save = new GameSave();
        var a = Make("A", new Vector2Int(0, 0));
        var b = Make("B", new Vector2Int(1, 0));
        for (int i = 0; i < 7; i++) Bonds.RecordBattle(save, new List<Unit> { a, b });
        Assert.IsTrue(Bonds.HasBondedNeighbor(save, a, new List<Unit> { a, b }));
        b.Coord = new Vector2Int(10, 10);
        Assert.IsFalse(Bonds.HasBondedNeighbor(save, a, new List<Unit> { a, b }));
    }
}
