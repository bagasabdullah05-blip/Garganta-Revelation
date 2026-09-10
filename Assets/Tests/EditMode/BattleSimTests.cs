using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Garganta.AI;
using Garganta.Combat;
using Garganta.Core;
using Garganta.Data;
using Garganta.Grid;
using Garganta.Units;

// Headless auto-battle: both sides driven by AI logic, no coroutines, no input.
// Catches stalls, exceptions and balance outliers in the real combat pipeline.
public class BattleSimTests
{
    List<GameObject> trash;
    GridManager grid;
    CombatManager combat;

    [SetUp]
    public void Setup()
    {
        trash = new List<GameObject>();
        Random.InitState(1234);
        var g = new GameObject("grid");
        trash.Add(g);
        grid = g.AddComponent<GridManager>();
        grid.Generate();
        var c = new GameObject("combat");
        trash.Add(c);
        combat = c.AddComponent<CombatManager>();
    }

    [TearDown]
    public void Cleanup()
    {
        foreach (var go in trash) Object.DestroyImmediate(go);
    }

    Unit Spawn(string id, bool player, Vector2Int c, int level = 1)
    {
        var u = UnitFactory.Create(id, player, c, grid);
        if (level > 1) u.ApplyLevel(level);
        trash.Add(u.gameObject);
        return u;
    }

    string SimBattle(List<Unit> players, List<Unit> foes, int maxTicks, out int ticks, out int actions)
    {
        ticks = 0;
        actions = 0;
        var all = new List<Unit>(players);
        all.AddRange(foes);
        while (ticks < maxTicks)
        {
            ticks++;
            foreach (var u in all) u.GainCTB(TurnManager.ComputeGain(u.Stats.SPD, 0f));
            Unit cur = null;
            foreach (var u in all)
                if (u.IsAlive && u.CTB >= 100 && (cur == null || u.CTB > cur.CTB)) cur = u;
            if (cur == null) continue;
            TakeSimTurn(cur, cur.IsPlayer ? players : foes, cur.IsPlayer ? foes : players);
            cur.TickEndStatus();
            cur.ResetCTB();
            actions++;
            if (!GameManager.AnyAlive(foes)) return "players";
            if (!GameManager.AnyAlive(players)) return "foes";
        }
        return "timeout";
    }

    void TakeSimTurn(Unit u, List<Unit> allies, List<Unit> foes)
    {
        if (u.StunTurns > 0) { u.StunTurns--; return; }
        if (u.Corruption >= 100) { u.TakeDamage(99999); return; }
        if (CombatManager.LoseTurnRoll(u.Corruption, Random.value)) return;

        if (u.Behavior == AIBehavior.Support)
        {
            var patient = AITactics.FindWoundedAlly(allies);
            if (patient != null)
            {
                Skill heal = default;
                foreach (var sk in ClassDatabase.UnlockedSkills(u.ClassId, u.Level))
                    if (sk.TargetsAllies && sk.Effect == SkillEffect.Heal && u.MP >= sk.CostMP) { heal = sk; break; }
                if (heal.Id != null)
                {
                    int range = heal.Range > 0 ? heal.Range : u.Stats.Range;
                    if (GridManager.HexDistance(u.Coord, patient.Coord) > range)
                        StepToward(u, patient.Coord);
                    if (GridManager.HexDistance(u.Coord, patient.Coord) <= range)
                    {
                        combat.ResolveSkill(u, heal, patient, allies, foes);
                        return;
                    }
                }
            }
        }

        var target = AITactics.FindTarget(u, foes, grid);
        var reach = AITactics.ReachableTiles(grid, u.Coord, u.Stats.Move);
        Vector2Int dest = target != null
            ? AITactics.BestTile(u, target, reach, grid)
            : AITactics.FleeTile(u, reach, grid, foes);
        if (dest != u.Coord)
        {
            var path = Pathfinder.FindPath(grid, u.Coord, dest, u.Stats.Move);
            if (path != null) { grid.MoveOccupant(u, dest); u.Coord = dest; }
        }
        Unit victim = null;
        foreach (var f in foes)
            if (f.IsAlive && GridManager.HexDistance(u.Coord, f.Coord) <= u.Stats.Range && (victim == null || f.HP < victim.HP))
                victim = f;
        if (victim != null) combat.Attack(u, victim);
    }

    void StepToward(Unit u, Vector2Int goal)
    {
        var reach = AITactics.ReachableTiles(grid, u.Coord, u.Stats.Move);
        Vector2Int dest = AITactics.TileToward(u, goal, reach, grid);
        if (dest == u.Coord) return;
        var path = Pathfinder.FindPath(grid, u.Coord, dest, u.Stats.Move);
        if (path != null) { grid.MoveOccupant(u, dest); u.Coord = dest; }
    }

    [Test]
    public void Ch1Mirror_KaelBeatsTwoGoblins()
    {
        var kael = Spawn("Kael", true, new Vector2Int(1, 1), 2); // real Ch.1 starts Kael at Lv2
        var g1 = Spawn("Goblin", false, new Vector2Int(9, 9));
        var g2 = Spawn("Goblin", false, new Vector2Int(10, 9));
        string winner = SimBattle(new List<Unit> { kael }, new List<Unit> { g1, g2 }, 2000, out int ticks, out int actions);
        Assert.AreEqual("players", winner);
        Assert.Less(ticks, 2000);
        Assert.Greater(actions, 0);
    }

    [Test]
    public void FullParty_NoStall()
    {
        var players = new List<Unit>
        {
            Spawn("Kael", true, new Vector2Int(1, 1)),
            Spawn("Briar", true, new Vector2Int(2, 1)),
            Spawn("Sera", true, new Vector2Int(1, 2)),
            Spawn("Voss", true, new Vector2Int(2, 2)),
        };
        players[2].Behavior = AIBehavior.Support;
        var foes = new List<Unit>
        {
            Spawn("Bandit", false, new Vector2Int(9, 9)),
            Spawn("Bandit", false, new Vector2Int(10, 9)),
            Spawn("Goblin", false, new Vector2Int(9, 10)),
            Spawn("Goblin", false, new Vector2Int(10, 10)),
            Spawn("Wolf", false, new Vector2Int(8, 9)),
            Spawn("Skeleton", false, new Vector2Int(9, 8)),
        };
        string winner = SimBattle(players, foes, 3000, out int ticks, out int actions);
        Assert.AreNotEqual("timeout", winner);
        Assert.Greater(actions, 10);
    }

    [Test]
    public void CorruptedBattle_Terminates()
    {
        var players = new List<Unit> { Spawn("Kael", true, new Vector2Int(1, 1), 2) };
        var foes = new List<Unit> { Spawn("Goblin", false, new Vector2Int(2, 2)), Spawn("Wolf", false, new Vector2Int(3, 3)) };
        foreach (var u in players) u.Corruption = 60;
        foreach (var u in foes) u.Corruption = 60;
        string winner = SimBattle(players, foes, 3000, out _, out _);
        Assert.AreNotEqual("timeout", winner);
    }

    [Test]
    public void BlightMax_KillsOnOwnTurn()
    {
        var players = new List<Unit> { Spawn("Kael", true, new Vector2Int(1, 1)) };
        var foes = new List<Unit> { Spawn("Goblin", false, new Vector2Int(9, 9)) };
        players[0].Corruption = 100;
        string winner = SimBattle(players, foes, 2000, out _, out _);
        Assert.AreEqual("foes", winner); // Kael consumes himself, goblin wins by default
    }

    [Test]
    public void Support_HealsWoundedAlly()
    {
        var sera = Spawn("Sera", true, new Vector2Int(1, 1));
        sera.Behavior = AIBehavior.Support;
        var kael = Spawn("Kael", true, new Vector2Int(1, 2));
        kael.HP = 20;
        var gob = Spawn("Goblin", false, new Vector2Int(9, 9));
        int mpBefore = sera.MP;
        TakeSimTurn(sera, new List<Unit> { sera, kael }, new List<Unit> { gob });
        Assert.Greater(kael.HP, 20);
        Assert.Less(sera.MP, mpBefore);
    }
}
