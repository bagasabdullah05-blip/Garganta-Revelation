using NUnit.Framework;
using UnityEngine;
using Garganta.Grid;

public class PathfinderTests
{
    int[,] Open(int w, int h, int cost = 1)
    {
        var c = new int[w, h];
        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++) c[x, y] = cost;
        return c;
    }

    [Test]
    public void StraightPath_ReachesGoal()
    {
        var path = Pathfinder.FindPath(Open(5, 5), 5, 5, new Vector2Int(0, 0), new Vector2Int(4, 4));
        Assert.IsNotNull(path);
        Assert.AreEqual(new Vector2Int(0, 0), path[0]);
        Assert.AreEqual(new Vector2Int(4, 4), path[path.Count - 1]);
    }

    [Test]
    public void BlockedGoal_ReturnsNull()
    {
        var c = Open(5, 5);
        c[4, 4] = -1;
        Assert.IsNull(Pathfinder.FindPath(c, 5, 5, new Vector2Int(0, 0), new Vector2Int(4, 4)));
    }

    [Test]
    public void WallColumn_RoutesAround()
    {
        var c = Open(5, 5);
        for (int y = 0; y < 4; y++) c[2, y] = -1; // wall with gap at y=4
        var path = Pathfinder.FindPath(c, 5, 5, new Vector2Int(0, 0), new Vector2Int(4, 0));
        Assert.IsNotNull(path);
        foreach (var p in path) Assert.AreNotEqual(-1, c[p.x, p.y]);
    }
}
