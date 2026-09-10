using System.Collections.Generic;
using UnityEngine;
using Garganta.Grid;
using Garganta.Units;
using Garganta.Combat;
using Garganta.AI;

namespace Garganta.Core
{
    // Singleton + State Machine. Owns battle setup and player input (click to move/attack).
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState State { get; private set; } = GameState.Init;
        public List<Unit> PlayerUnits = new List<Unit>();
        public List<Unit> EnemyUnits = new List<Unit>();
        public Unit SelectedUnit;
        public Unit CurrentUnit; // whose turn is active (player)
        public bool TargetingAttack;

        GridManager grid;
        GridVisualizer visual;
        TurnManager turns;
        CombatManager combat;

        HashSet<Vector2Int> moveRange = new HashSet<Vector2Int>();

        void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        void Start()
        {
            grid = FindAnyObjectByType<GridManager>();
            visual = FindAnyObjectByType<GridVisualizer>();
            turns = FindAnyObjectByType<TurnManager>();
            combat = FindAnyObjectByType<CombatManager>();
            SetupBattle();
        }

        public void SetState(GameState s)
        {
            State = s;
            EventBus.StateChanged(s);
        }

        void SetupBattle()
        {
            grid.Generate();
            visual.Build(grid);
            SpawnSides();
            var cam = Camera.main;
            if (cam != null) cam.transform.position = grid.CoordToWorld(new Vector2Int(5, 5)) + new Vector3(0, 0, -10);
            SetState(GameState.PlayerTurn);
            turns.Begin(PlayerUnits, EnemyUnits);
            EventBus.Log("Battle start: 4 vs 6 on Ashfield 12x12");
        }

        void SpawnSides()
        {
            string[] players = { "Kael", "Briar", "Sera", "Voss" };
            Vector2Int[] pPos = { new Vector2Int(1, 1), new Vector2Int(2, 1), new Vector2Int(1, 2), new Vector2Int(2, 2) };
            for (int i = 0; i < players.Length; i++)
                PlayerUnits.Add(UnitFactory.Create(players[i], true, pPos[i], grid));

            string[] enemies = { "Bandit", "Bandit", "Goblin", "Goblin", "Wolf", "Skeleton" };
            Vector2Int[] ePos = { new Vector2Int(9, 9), new Vector2Int(10, 9), new Vector2Int(9, 10), new Vector2Int(10, 10), new Vector2Int(8, 9), new Vector2Int(9, 8) };
            for (int i = 0; i < enemies.Length; i++)
                EnemyUnits.Add(UnitFactory.Create(enemies[i], false, ePos[i], grid));
        }

        void Update()
        {
            if (State != GameState.PlayerTurn || CurrentUnit == null) return;
            if (Input.GetMouseButtonDown(0)) HandleClick();
            if (Input.GetMouseButtonDown(1)) { TargetingAttack = false; visual.ClearHighlights(); }
        }

        void HandleClick()
        {
            Vector3 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int cell = grid.WorldToCoord(mw);

            if (TargetingAttack)
            {
                Unit foe = UnitAt(cell, EnemyUnits);
                if (foe != null && HexDist(CurrentUnit.Coord, cell) <= CurrentUnit.Stats.Range)
                {
                    combat.Attack(CurrentUnit, foe);
                    EndPlayerAction();
                }
                else { TargetingAttack = false; visual.ClearHighlights(); ShowMoveRange(); }
                return;
            }

            Unit pick = UnitAt(cell, PlayerUnits);
            if (pick != null && pick.IsAlive && pick == CurrentUnit)
            {
                SelectedUnit = pick;
                ShowMoveRange();
                return;
            }

            if (SelectedUnit != null && moveRange.Contains(cell) && grid.IsWalkable(cell) && UnitAt(cell, PlayerUnits) == null && UnitAt(cell, EnemyUnits) == null)
            {
                var path = Pathfinder.FindPath(grid, SelectedUnit.Coord, cell, SelectedUnit.Stats.Move);
                if (path != null)
                {
                    grid.MoveOccupant(SelectedUnit, cell);
                    SelectedUnit.Coord = cell;
                    StartCoroutine(SelectedUnit.GetComponent<UnitMovement>().FollowPath(path, grid, null));
                    visual.ClearHighlights();
                }
            }
        }

        void ShowMoveRange()
        {
            moveRange = AITactics.ReachableTiles(grid, SelectedUnit.Coord, SelectedUnit.Stats.Move);
            visual.ShowRange(moveRange, new Color(0.3f, 0.6f, 1f, 0.45f));
        }

        public void ShowAttackRange()
        {
            TargetingAttack = true;
            visual.ClearHighlights();
            var cells = AITactics.TilesInRange(grid, CurrentUnit.Coord, CurrentUnit.Stats.Range);
            visual.ShowRange(cells, new Color(1f, 0.35f, 0.3f, 0.45f));
        }

        public void PlayerWait() => EndPlayerAction();

        void EndPlayerAction()
        {
            TargetingAttack = false;
            SelectedUnit = null;
            visual.ClearHighlights();
            CurrentUnit.ResetCTB();
            CurrentUnit = null;
            turns.NotifyPlayerDone();
            CheckEnd();
        }

        public void OnEnemyDone(Unit u)
        {
            u.ResetCTB();
            CheckEnd();
        }

        public void CheckEnd()
        {
            if (!AnyAlive(EnemyUnits)) SetState(GameState.Victory);
            else if (!AnyAlive(PlayerUnits)) SetState(GameState.Defeat);
        }

        public static bool AnyAlive(List<Unit> list)
        {
            foreach (var u in list) if (u.IsAlive) return true;
            return false;
        }

        public static Unit UnitAt(Vector2Int c, List<Unit> list)
        {
            foreach (var u in list) if (u.IsAlive && u.Coord == c) return u;
            return null;
        }

        public static int HexDist(Vector2Int a, Vector2Int b) => GridManager.HexDistance(a, b);
    }
}
