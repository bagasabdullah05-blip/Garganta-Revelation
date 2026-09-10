using NUnit.Framework;
using System.Linq;
using Garganta.Core;
using Garganta.Data;
using Garganta.Story;

public class ChapterTests
{
    [Test]
    public void Nodes_ValidContent()
    {
        Assert.AreEqual(3, ChapterDatabase.NodeCount);
        for (int i = 0; i < ChapterDatabase.NodeCount; i++)
        {
            var n = ChapterDatabase.GetNode(i);
            Assert.IsNotEmpty(n.PlayerIds, n.Id);
            Assert.IsNotEmpty(n.EnemyIds, n.Id);
            Assert.IsNotEmpty(n.Pre, n.Id);
            Assert.IsNotEmpty(n.Post, n.Id);
            Assert.GreaterOrEqual(n.MapVariant, 0);
            Assert.LessOrEqual(n.MapVariant, 2);
        }
    }

    [Test]
    public void Ch1_IsTutorialSolo()
    {
        var ch1 = ChapterDatabase.GetById("ch1");
        Assert.IsTrue(ch1.Tutorial);
        Assert.AreEqual(1, ch1.PlayerIds.Length);
        Assert.AreEqual("Kael", ch1.PlayerIds[0]);
    }

    [Test]
    public void UnlockChain_CoversRoster()
    {
        var unlocked = new System.Collections.Generic.HashSet<string> { "Kael", "Briar" };
        for (int i = 0; i < ChapterDatabase.NodeCount; i++)
        {
            var n = ChapterDatabase.GetNode(i);
            foreach (var p in n.PlayerIds)
                Assert.Contains(p, unlocked.ToArray(), $"{n.Id} needs locked {p}");
            foreach (var u in n.Unlocks) unlocked.Add(u);
        }
        Assert.Contains("Sera", unlocked.ToArray());
        Assert.Contains("Voss", unlocked.ToArray());
    }

    [Test]
    public void ParseLine_SplitsSpeaker()
    {
        var (s, t) = DialogueUI.ParseLine("Kael: hello there");
        Assert.AreEqual("Kael", s);
        Assert.AreEqual("hello there", t);
        var (s2, t2) = DialogueUI.ParseLine("no colon here");
        Assert.AreEqual("", s2);
        Assert.AreEqual("no colon here", t2);
    }

    [Test]
    public void MapVariants_AllWalkableSpawns()
    {
        var playerSpawns = new[] { (1, 1), (2, 1), (1, 2), (2, 2) };
        var enemySpawns = new[] { (9, 9), (10, 9), (9, 10), (10, 10), (8, 9), (9, 8) };
        for (int v = 0; v <= 2; v++)
        {
            MapDatabase.GetMap(v, out var types, out var elev);
            foreach (var (x, y) in playerSpawns)
                Assert.IsTrue(types[x, y] != TileType.Wall && types[x, y] != TileType.Water, $"v{v} player ({x},{y})");
            foreach (var (x, y) in enemySpawns)
                Assert.IsTrue(types[x, y] != TileType.Wall && types[x, y] != TileType.Water, $"v{v} enemy ({x},{y})");
        }
    }
}
