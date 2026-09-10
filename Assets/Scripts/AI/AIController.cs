using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Garganta.Core;
using Garganta.Data;
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
            if (gm == null || grid == null || combat == null) yield break;

            yield return new WaitForSeconds(0.3f);
            var allies = u.IsPlayer ? gm.PlayerUnits : gm.EnemyUnits;
            var foes = u.IsPlayer ? gm.EnemyUnits : gm.PlayerUnits;

            if (AITactics.ShouldRetreat(u))
            {
                yield return MoveTo(u, AITactics.FleeTile(u, AITactics.ReachableTiles(grid, u.Coord, u.Stats.Move), grid, foes), grid, gm);
                gm.CheckEnd();
                yield break;
            }

            if (u.Behavior == AIBehavior.Support)
            {
                yield return SupportTurn(u, allies, foes, grid, gm, combat);
                gm.CheckEnd();
                yield break;
            }

            if (u.Behavior == AIBehavior.Skirmisher)
            {
                yield return SkirmisherTurn(u, allies, foes, grid, gm, combat);
                gm.CheckEnd();
                yield break;
            }

            if (u.Behavior == AIBehavior.Defensive && !EnemyInRange(u, foes))
            {
                yield break; // hold position
            }

            Unit target = AITactics.FindTarget(u, foes, grid);
            var reach = AITactics.ReachableTiles(grid, u.Coord, u.Stats.Move);
            yield return MoveTo(u, AITactics.BestTile(u, target, reach, grid), grid, gm);
            AttackLowest(u, foes, combat);
            gm.CheckEnd();
        }

        static IEnumerator SupportTurn(Unit u, List<Unit> allies, List<Unit> foes, GridManager grid, GameManager gm, CombatManager combat)
        {
            Skill heal = FindHealSkill(u);
            Unit patient = AITactics.FindWoundedAlly(allies);
            if (heal.Id == null || patient == null)
            {
                // Fallback: fight like aggressive
                Unit target = AITactics.FindTarget(u, foes, grid);
                var reach = AITactics.ReachableTiles(grid, u.Coord, u.Stats.Move);
                yield return MoveTo(u, AITactics.BestTile(u, target, reach, grid), grid, gm);
                AttackLowest(u, foes, combat);
                yield break;
            }
            int range = heal.Range > 0 ? heal.Range : u.Stats.Range;
            if (GridManager.HexDistance(u.Coord, patient.Coord) > range)
            {
                var reach = AITactics.ReachableTiles(grid, u.Coord, u.Stats.Move);
                yield return MoveTo(u, AITactics.TileToward(u, patient.Coord, reach, grid), grid, gm);
            }
            if (GridManager.HexDistance(u.Coord, patient.Coord) <= range)
            {
                yield return new WaitForSeconds(0.2f);
                combat.ResolveSkill(u, heal, patient, allies, foes);
            }
        }

        static IEnumerator SkirmisherTurn(Unit u, List<Unit> allies, List<Unit> foes, GridManager grid, GameManager gm, CombatManager combat)
        {
            AttackLowest(u, foes, combat); // hit first...
            var reach = AITactics.ReachableTiles(grid, u.Coord, u.Stats.Move); // ...then run
            int step = Mathf.Max(1, u.Stats.Move / 2);
            var small = new HashSet<Vector2Int>();
            foreach (var t in reach)
                if (GridManager.HexDistance(u.Coord, t) <= step) small.Add(t);
            if (small.Count == 0) small = reach;
            yield return MoveTo(u, AITactics.FleeTile(u, small, grid, foes), grid, gm);
            if (!EnemyInRange(u, foes))
            {
                Unit target = AITactics.FindTarget(u, foes, grid);
                yield return MoveTo(u, AITactics.TileToward(u, target != null ? target.Coord : u.Coord, reach, grid), grid, gm);
                AttackLowest(u, foes, combat);
            }
        }

        static Skill FindHealSkill(Unit u)
        {
            foreach (var sk in ClassDatabase.UnlockedSkills(u.ClassId, u.Level))
                if (sk.TargetsAllies && sk.Effect == SkillEffect.Heal && u.MP >= sk.CostMP) return sk;
            return default;
        }

        static void AttackLowest(Unit u, List<Unit> foes, CombatManager combat)
        {
            Unit victim = null;
            foreach (var f in foes)
            {
                if (!f.IsAlive) continue;
                if (GridManager.HexDistance(u.Coord, f.Coord) <= u.Stats.Range && (victim == null || f.HP < victim.HP))
                    victim = f;
            }
            if (victim != null) combat.Attack(u, victim);
        }

        static IEnumerator MoveTo(Unit u, Vector2Int dest, GridManager grid, GameManager gm)
        {
            if (dest == u.Coord) yield break;
            var path = Pathfinder.FindPath(grid, u.Coord, dest, u.Stats.Move);
            if (path == null) yield break;
            grid.MoveOccupant(u, dest);
            u.Coord = dest;
            bool done = false;
            gm.StartCoroutine(u.GetComponent<UnitMovement>().FollowPath(path, grid, () => done = true));
            while (!done) yield return null;
        }

        static bool EnemyInRange(Unit u, List<Unit> foes)
        {
            foreach (var f in foes)
                if (f.IsAlive && GridManager.HexDistance(u.Coord, f.Coord) <= u.Stats.Range) return true;
            return false;
        }
    }
}
