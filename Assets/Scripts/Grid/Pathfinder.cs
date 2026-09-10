using System.Collections.Generic;
using UnityEngine;

namespace Garganta.Grid
{
    // A* over odd-r hex grid. Pure overload works on cost arrays (used by EditMode tests).
    public static class Pathfinder
    {
        public static List<Vector2Int> FindPath(GridManager grid, Vector2Int start, Vector2Int goal, int movePoints = -1)
        {
            int w = grid.Width, h = grid.Height;
            int[,] costs = new int[w, h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    costs[x, y] = grid.Tiles[x, y].Walkable ? grid.Tiles[x, y].Cost : -1;
            if (movePoints >= 0)
            {
                var path = FindPath(costs, w, h, start, goal);
                if (path == null) return null;
                int total = 0;
                for (int i = 1; i < path.Count; i++) total += costs[path[i].x, path[i].y];
                return total <= movePoints ? path : null;
            }
            return FindPath(costs, w, h, start, goal);
        }

        public static List<Vector2Int> FindPath(int[,] costs, int w, int h, Vector2Int start, Vector2Int goal)
        {
            if (!Inside(start, w, h) || !Inside(goal, w, h)) return null;
            if (costs[goal.x, goal.y] < 0 || costs[start.x, start.y] < 0) return null;

            var open = new List<Vector2Int> { start };
            var came = new Dictionary<Vector2Int, Vector2Int>();
            var g = new Dictionary<Vector2Int, int> { [start] = 0 };
            var closed = new HashSet<Vector2Int>();

            while (open.Count > 0)
            {
                Vector2Int cur = open[0];
                int best = g[cur] + Heuristic(cur, goal);
                for (int i = 1; i < open.Count; i++)
                {
                    int f = g[open[i]] + Heuristic(open[i], goal);
                    if (f < best) { best = f; cur = open[i]; }
                }
                open.Remove(cur);
                if (cur == goal) return Reconstruct(came, cur);
                closed.Add(cur);

                foreach (var n in GridManager.Neighbors(cur))
                {
                    if (!Inside(n, w, h) || costs[n.x, n.y] < 0 || closed.Contains(n)) continue;
                    int ng = g[cur] + costs[n.x, n.y];
                    if (!g.ContainsKey(n) || ng < g[n])
                    {
                        g[n] = ng;
                        came[n] = cur;
                        if (!open.Contains(n)) open.Add(n);
                    }
                }
            }
            return null;
        }

        static List<Vector2Int> Reconstruct(Dictionary<Vector2Int, Vector2Int> came, Vector2Int cur)
        {
            var path = new List<Vector2Int> { cur };
            while (came.ContainsKey(cur)) { cur = came[cur]; path.Add(cur); }
            path.Reverse();
            return path;
        }

        static bool Inside(Vector2Int c, int w, int h) => c.x >= 0 && c.y >= 0 && c.x < w && c.y < h;
        static int Heuristic(Vector2Int a, Vector2Int b) => GridManager.HexDistance(a, b);
    }
}
