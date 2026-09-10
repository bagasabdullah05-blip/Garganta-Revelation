using System.Collections.Generic;
using UnityEngine;
using Garganta.Core;
using Garganta.Grid;
using Garganta.Units;

namespace Garganta.AI
{
    // Shared tactics: reachability, targeting lowest HP, best DEF tile, retreat check.
    public static class AITactics
    {
        public static HashSet<Vector2Int> ReachableTiles(GridManager grid, Vector2Int start, int movePoints)
        {
            var reached = new Dictionary<Vector2Int, int> { [start] = 0 };
            var q = new Queue<Vector2Int>();
            q.Enqueue(start);
            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                foreach (var n in GridManager.Neighbors(cur))
                {
                    if (!grid.InBounds(n) || !grid.Tiles[n.x, n.y].Walkable) continue;
                    int nc = reached[cur] + grid.Tiles[n.x, n.y].Cost;
                    if (nc > movePoints) continue;
                    if (!reached.ContainsKey(n) || nc < reached[n]) { reached[n] = nc; q.Enqueue(n); }
                }
            }
            return new HashSet<Vector2Int>(reached.Keys);
        }

        public static HashSet<Vector2Int> TilesInRange(GridManager grid, Vector2Int center, int range)
        {
            var set = new HashSet<Vector2Int>();
            for (int x = 0; x < grid.Width; x++)
                for (int y = 0; y < grid.Height; y++)
                {
                    var c = new Vector2Int(x, y);
                    if (GridManager.HexDistance(center, c) <= range) set.Add(c);
                }
            return set;
        }

        public static Unit FindTarget(Unit self, List<Unit> enemies, GridManager grid)
        {
            Unit best = null;
            foreach (var e in enemies)
            {
                if (!e.IsAlive) continue;
                int d = GridManager.HexDistance(self.Coord, e.Coord);
                if (d > self.Stats.Move + e.Stats.Range + self.Stats.Range) continue; // rough reach check
                if (d > self.Stats.Move + self.Stats.Range && self.Behavior == AIBehavior.Defensive) continue;
                if (best == null || e.HP < best.HP) best = e;
            }
            return best;
        }

        public static Vector2Int BestTile(Unit self, Unit target, HashSet<Vector2Int> reachable, GridManager grid)
        {
            Vector2Int best = self.Coord;
            int bestScore = int.MinValue;
            foreach (var t in reachable)
            {
                var occ = grid.Tiles[t.x, t.y].Occupant;
                if (occ != null && occ != self) continue;
                int score = Balance.DefBonus(grid.Tiles[t.x, t.y].Type) * 10;
                if (target != null && GridManager.HexDistance(t, target.Coord) <= self.Stats.Range) score += 100;
                if (target != null) score -= GridManager.HexDistance(t, target.Coord);
                if (score > bestScore) { bestScore = score; best = t; }
            }
            return best;
        }

        public static bool ShouldRetreat(Unit self)
            => self.Behavior != AIBehavior.Aggressive && self.Behavior != AIBehavior.Boss
               && self.HP < self.Stats.MaxHP * 0.25f;

        public static Unit FindWoundedAlly(List<Unit> allies)
        {
            Unit best = null;
            float bestFrac = 1f;
            foreach (var a in allies)
            {
                if (!a.IsAlive) continue;
                float frac = (float)a.HP / a.Stats.MaxHP;
                if (frac < bestFrac) { bestFrac = frac; best = a; }
            }
            return bestFrac < 1f ? best : null;
        }

        // Reachable tile closest to a point of interest (ally to heal / foe to chase).
        public static Vector2Int TileToward(Unit self, Vector2Int goal, HashSet<Vector2Int> reachable, GridManager grid)
        {
            Vector2Int best = self.Coord;
            int bestD = GridManager.HexDistance(self.Coord, goal);
            foreach (var t in reachable)
            {
                var occ = grid.Tiles[t.x, t.y].Occupant;
                if (occ != null && occ != self) continue;
                int d = GridManager.HexDistance(t, goal);
                if (d < bestD) { bestD = d; best = t; }
            }
            return best;
        }

        public static Vector2Int FleeTile(Unit self, HashSet<Vector2Int> reachable, GridManager grid, List<Unit> foes)
        {
            Vector2Int best = self.Coord;
            int bestD = int.MinValue;
            foreach (var t in reachable)
            {
                var occ = grid.Tiles[t.x, t.y].Occupant;
                if (occ != null && occ != self) continue;
                int minD = int.MaxValue;
                foreach (var f in foes)
                {
                    if (!f.IsAlive) continue;
                    minD = Mathf.Min(minD, GridManager.HexDistance(t, f.Coord));
                }
                if (minD > bestD) { bestD = minD; best = t; }
            }
            return best;
        }
    }
}
