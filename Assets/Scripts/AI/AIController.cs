using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Garganta.Core;
using Garganta.Grid;
using Garganta.Units;
using Garganta.Combat;

namespace Garganta.AI
{
    public static class AIController
    {
        public static IEnumerator TakeTurn(Unit u)
        {
            var gm = GameManager.Instance;
            var grid = Object.FindAnyObjectByType<GridManager>();
            var combat = Object.FindAnyObjectByType<CombatManager>();
            if (gm == null || grid == null) yield break;

            yield return new WaitForSeconds(0.3f);
            var foes = gm.PlayerUnits;

            if (u.Behavior == AIBehavior.Defensive && !EnemyInRange(u, foes, grid))
            {
                yield break; // hold position
            }

            Unit target = AITactics.FindTarget(u, foes, grid);
            var reach = AITactics.ReachableTiles(grid, u.Coord, u.Stats.Move);
            Vector2Int dest = AITactics.BestTile(u, target, reach, grid);

            if (dest != u.Coord)
            {
                var path = Pathfinder.FindPath(grid, u.Coord, dest, u.Stats.Move);
                if (path != null)
                {
                    grid.MoveOccupant(u, dest);
                    u.Coord = dest;
                    var mv = u.GetComponent<UnitMovement>();
                    bool done = false;
                    gm.StartCoroutine(mv.FollowPath(path, grid, () => done = true));
                    while (!done) yield return null;
                }
            }

            // Attack lowest-HP foe in range after moving
            Unit victim = null;
            foreach (var f in foes)
            {
                if (!f.IsAlive) continue;
                if (GridManager.HexDistance(u.Coord, f.Coord) <= u.Stats.Range && (victim == null || f.HP < victim.HP))
                    victim = f;
            }
            if (victim != null)
            {
                yield return new WaitForSeconds(0.2f);
                combat.Attack(u, victim);
            }
            gm.CheckEnd();
        }

        static bool EnemyInRange(Unit u, List<Unit> foes, GridManager grid)
        {
            foreach (var f in foes)
                if (f.IsAlive && GridManager.HexDistance(u.Coord, f.Coord) <= u.Stats.Range) return true;
            return false;
        }
    }
}
