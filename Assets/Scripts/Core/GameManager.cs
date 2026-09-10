using System.Collections.Generic;
using UnityEngine;
using Garganta.AI;
using Garganta.Combat;
using Garganta.Data;
using Garganta.Grid;
using Garganta.Units;

namespace Garganta.Core
{
    // Singleton + State Machine. Owns battle setup and player input (move/attack/skill/item).
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState State { get; private set; } = GameState.Init;
        public List<Unit> PlayerUnits = new List<Unit>();
        public List<Unit> EnemyUnits = new List<Unit>();
        public Unit SelectedUnit;
        public Unit CurrentUnit; // whose turn is active (player)
        public bool TargetingAttack;
        public Skill? PendingSkill;
        public string PendingItem;
        public string BattleReport = "";

        GridManager grid;
        GridVisualizer visual;
        TurnManager turns;
        CombatManager combat;

        HashSet<Vector2Int> moveRange = new HashSet<Vector2Int>();
        ChapterConfig ActiveCfg;
        bool WavePending;

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
            if (Flow.GameFlow.Instance != null) return; // M3: flow drives battles
            SetupBattle();
        }

        public void SetState(GameState s)
        {
            State = s;
            EventBus.StateChanged(s);
        }

        void SetupBattle()
        {
            Garganta.Art.ArtOverride.LoadAll();
            Music("battle");
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

            string[] enemies = { "Bandit", "Cultist", "Goblin", "Goblin", "Wolf", "Skeleton" };
            Vector2Int[] ePos = { new Vector2Int(9, 9), new Vector2Int(10, 9), new Vector2Int(9, 10), new Vector2Int(10, 10), new Vector2Int(8, 9), new Vector2Int(9, 8) };
            for (int i = 0; i < enemies.Length; i++)
                EnemyUnits.Add(UnitFactory.Create(enemies[i], false, ePos[i], grid));
        }

        // M3 chapter battle: roster from save, map variant per chapter.
        public void StartBattle(ChapterConfig cfg)
        {
            ClearBattle();
            ActiveCfg = cfg;
            WavePending = cfg.Wave2Ids != null && cfg.Wave2Ids.Length > 0;
            Garganta.Art.ArtOverride.LoadAll();
            Music(cfg.Id == "ch9" || cfg.Id == "ch11" || cfg.Id == "primeval" ? "boss" : "battle");
            grid.GenerateVariant(cfg.MapVariant);
            visual.Build(grid);
            SpawnRoster(cfg);
            SpawnEnemies(cfg);
            ApplySupportBonds();
            var cam = Camera.main;
            if (cam != null) cam.transform.position = grid.CoordToWorld(new Vector2Int(5, 5)) + new Vector3(0, 0, -10);
            BattleReport = "";
            SetState(GameState.PlayerTurn);
            turns.Begin(PlayerUnits, EnemyUnits);
            EventBus.Log($"Battle start: {cfg.Title} ({cfg.Subtitle})");
        }

        void ClearBattle()
        {
            foreach (var u in PlayerUnits) if (u != null) Destroy(u.gameObject);
            foreach (var u in EnemyUnits) if (u != null) Destroy(u.gameObject);
            PlayerUnits.Clear();
            EnemyUnits.Clear();
            SelectedUnit = null;
            CurrentUnit = null;
            if (visual != null)
                foreach (Transform c in visual.transform) Destroy(c.gameObject);
            CancelTargeting();
        }

        void SpawnRoster(ChapterConfig cfg)
        {
            var save = SaveSystem.Current;
            Vector2Int[] slots = { new Vector2Int(1, 1), new Vector2Int(2, 1), new Vector2Int(1, 2), new Vector2Int(2, 2), new Vector2Int(1, 3), new Vector2Int(3, 1) };
            int i = 0;
            foreach (var pid in cfg.PlayerIds)
            {
                if (i >= slots.Length) break;
                if (save == null || !save.roster.Exists(r => r.rosterId == pid)) continue; // locked chars sit out
                PlayerUnits.Add(UnitFactory.CreateFromSave(save.roster.Find(r => r.rosterId == pid), slots[i], grid, true));
                i++;
            }
        }

        static readonly Vector2Int[] EnemySlots = {
            new Vector2Int(9, 9), new Vector2Int(10, 9), new Vector2Int(9, 10), new Vector2Int(10, 10),
            new Vector2Int(8, 9), new Vector2Int(9, 8), new Vector2Int(7, 10), new Vector2Int(10, 7),
        };

        void SpawnEnemies(ChapterConfig cfg)
        {
            var save = SaveSystem.Current;
            int ng = (save != null && save.ngPlus) ? 2 : 0;
            for (int i = 0; i < cfg.EnemyIds.Length && i < EnemySlots.Length; i++)
            {
                Unit u = UnitFactory.Create(cfg.EnemyIds[i], false, EnemySlots[i], grid);
                if (cfg.RecruitIds != null && i < cfg.RecruitIds.Length) u.RecruitId = cfg.RecruitIds[i];
                int lv = (cfg.EnemyLvls != null && i < cfg.EnemyLvls.Length) ? cfg.EnemyLvls[i] : cfg.EnemyLevel;
                if (lv + ng > 1) u.ApplyLevel(lv + ng);
                float em = GameBalance.EnemyStatMult(save != null ? save.difficulty : 1);
                if (em != 1f) u.ScaleStats(em);
                EnemyUnits.Add(u);
            }
        }

        void SpawnWave()
        {
            var cfg = ActiveCfg;
            var save = SaveSystem.Current;
            int ng = (save != null && save.ngPlus) ? 2 : 0;
            int n = 0;
            for (int i = 0; i < cfg.Wave2Ids.Length && i < EnemySlots.Length; i++)
            {
                Vector2Int cell = EnemySlots[i];
                var occ = grid.Tiles[cell.x, cell.y].Occupant;
                if (occ != null && occ.IsAlive) continue;
                Unit u = UnitFactory.Create(cfg.Wave2Ids[i], false, cell, grid);
                if (cfg.Wave2Level + ng > 1) u.ApplyLevel(cfg.Wave2Level + ng);
                EnemyUnits.Add(u);
                n++;
            }
            EventBus.Log($"Reinforcements! ({n} foes)");
        }

        void Update()
        {
            if (State != GameState.PlayerTurn || CurrentUnit == null) return;
            if (Input.GetMouseButtonDown(0)) HandleClick();
            if (Input.GetMouseButtonDown(1)) CancelTargeting();
        }

        void HandleClick()
        {
            Vector3 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int cell = grid.WorldToCoord(mw);

            if (PendingSkill.HasValue) { ResolveSkillTarget(cell); return; }
            if (PendingItem != null) { ResolveItemTarget(cell); return; }

            if (TargetingAttack)
            {
                Unit foe = UnitAt(cell, EnemyUnits);
                if (foe != null && HexDist(CurrentUnit.Coord, cell) <= CurrentUnit.Stats.Range)
                {
                    combat.Attack(CurrentUnit, foe);
                    EndPlayerAction();
                }
                else { CancelTargeting(); ShowMoveRange(); }
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

        void ResolveSkillTarget(Vector2Int cell)
        {
            var sk = PendingSkill.Value;
            if (sk.SelfOnly)
            {
                combat.ResolveSkill(CurrentUnit, sk, CurrentUnit, PlayerUnits, EnemyUnits);
                CancelTargeting();
                EndPlayerAction();
                return;
            }
            int range = sk.Range > 0 ? sk.Range : CurrentUnit.Stats.Range;
            var pool = sk.TargetsAllies ? PlayerUnits : EnemyUnits;
            Unit t = UnitAt(cell, pool);
            if (t != null && HexDist(CurrentUnit.Coord, cell) <= range)
            {
                combat.ResolveSkill(CurrentUnit, sk, t, PlayerUnits, EnemyUnits);
                CancelTargeting();
                EndPlayerAction();
            }
            else { CancelTargeting(); ShowMoveRange(); }
        }

        void ResolveItemTarget(Vector2Int cell)
        {
            var item = EquipmentData.FindConsumable(PendingItem);
            if (item.BombAll || item.Revive)
            {
                combat.UseItem(CurrentUnit, item, null, PlayerUnits, EnemyUnits);
                CancelTargeting();
                EndPlayerAction();
                return;
            }
            Unit t = UnitAt(cell, PlayerUnits);
            if (t != null)
            {
                combat.UseItem(CurrentUnit, item, t, PlayerUnits, EnemyUnits);
                CancelTargeting();
                EndPlayerAction();
            }
            else { CancelTargeting(); ShowMoveRange(); }
        }

        void ShowMoveRange()
        {
            moveRange = AITactics.ReachableTiles(grid, SelectedUnit.Coord, SelectedUnit.Stats.Move);
            visual.ShowRange(moveRange, new Color(0.3f, 0.6f, 1f, 0.45f));
        }

        public void ShowAttackRange()
        {
            CancelTargeting();
            TargetingAttack = true;
            var cells = AITactics.TilesInRange(grid, CurrentUnit.Coord, CurrentUnit.Stats.Range);
            visual.ShowRange(cells, new Color(1f, 0.35f, 0.3f, 0.45f));
        }

        public void SelectSkill(Skill sk)
        {
            CancelTargeting();
            PendingSkill = sk;
            int range = sk.Range > 0 ? sk.Range : CurrentUnit.Stats.Range;
            var cells = AITactics.TilesInRange(grid, CurrentUnit.Coord, range);
            visual.ShowRange(cells, new Color(1f, 0.85f, 0.3f, 0.45f));
        }

        public void SelectItem(string id)
        {
            var item = EquipmentData.FindConsumable(id);
            if (Inventory.Count(id) <= 0) return;
            if (item.BombAll || item.Revive)
            {
                combat.UseItem(CurrentUnit, item, null, PlayerUnits, EnemyUnits);
                EndPlayerAction();
                return;
            }
            CancelTargeting();
            PendingItem = id;
        }

        public void CancelTargeting()
        {
            TargetingAttack = false;
            PendingSkill = null;
            PendingItem = null;
            visual.ClearHighlights();
        }

        public void PlayerWait() => EndPlayerAction();

        // Test/auto-play hooks: drive the active player unit without mouse input.
        public void DebugPlayerAttack()
        {
            if (CurrentUnit == null) return;
            Unit victim = null;
            foreach (var f in EnemyUnits)
            {
                if (!f.IsAlive) continue;
                if (HexDist(CurrentUnit.Coord, f.Coord) <= CurrentUnit.Stats.Range && (victim == null || f.HP < victim.HP))
                    victim = f;
            }
            if (victim != null) combat.Attack(CurrentUnit, victim);
            EndPlayerAction();
        }

        public void DebugPlayerMoveToward()
        {
            if (CurrentUnit == null) return;
            Unit foe = null;
            int best = int.MaxValue;
            foreach (var f in EnemyUnits)
            {
                if (!f.IsAlive) continue;
                int d = HexDist(CurrentUnit.Coord, f.Coord);
                if (d < best) { best = d; foe = f; }
            }
            if (foe == null) { EndPlayerAction(); return; }
            SelectedUnit = CurrentUnit;
            var reach = AITactics.ReachableTiles(grid, CurrentUnit.Coord, CurrentUnit.Stats.Move);
            Vector2Int dest = AITactics.BestTile(CurrentUnit, foe, reach, grid);
            if (dest != CurrentUnit.Coord)
            {
                var path = Pathfinder.FindPath(grid, CurrentUnit.Coord, dest, CurrentUnit.Stats.Move);
                if (path != null)
                {
                    grid.MoveOccupant(CurrentUnit, dest);
                    CurrentUnit.Coord = dest;
                    StartCoroutine(CurrentUnit.GetComponent<UnitMovement>().FollowPath(path, grid, null));
                }
            }
            if (HexDist(CurrentUnit.Coord, foe.Coord) <= CurrentUnit.Stats.Range)
            {
                combat.Attack(CurrentUnit, foe);
                EndPlayerAction();
            }
        }

        void EndPlayerAction()
        {
            CancelTargeting();
            SelectedUnit = null;
            if (CurrentUnit != null)
            {
                CurrentUnit.TickEndStatus();
                CurrentUnit.ResetCTB();
                CurrentUnit = null;
            }
            turns.NotifyPlayerDone();
            CheckEnd();
        }

        public void OnEnemyDone(Unit u)
        {
            u.TickEndStatus();
            u.ResetCTB();
            CheckEnd();
        }

        public void CheckEnd()
        {
            if (State == GameState.Victory || State == GameState.Defeat) return;
            if (!AnyAlive(EnemyUnits))
            {
                if (WavePending) { WavePending = false; SpawnWave(); return; }
                Music("victory");
                AwardVictory(); SetState(GameState.Victory);
            }
            else if (!AnyAlive(PlayerUnits)) { Music("defeat"); SetState(GameState.Defeat); }
        }

        void ApplySupportBonds()
        {
            var save = SaveSystem.Current;
            if (save == null) return;
            foreach (var u in PlayerUnits)
            {
                int best = 0;
                foreach (var a in PlayerUnits)
                {
                    if (a == u || string.IsNullOrEmpty(a.RosterId)) continue;
                    best = Mathf.Max(best, Bonds.LevelBetween(save, u.RosterId, a.RosterId));
                }
                if (best > 0)
                {
                    u.Stats.MaxHP = Mathf.RoundToInt(u.Stats.MaxHP * (1f + 0.05f * best));
                    u.Stats.ATK = Mathf.RoundToInt(u.Stats.ATK * (1f + 0.03f * best));
                    u.HP = u.Stats.MaxHP;
                }
            }
        }

        void AwardVictory()
        {
            var save = SaveSystem.Current;
            var alive = PlayerUnits.FindAll(p => p.IsAlive);
            int xpEach = 0;
            foreach (var e in EnemyUnits) xpEach += 50 + e.Level * 25;
            if (alive.Count > 0) xpEach /= alive.Count;
            xpEach = Mathf.RoundToInt(xpEach * GameBalance.XpMult(save != null ? save.difficulty : 1));
            var lines = new List<string> { $"Victory! +{xpEach} XP each" };
            foreach (var p in alive)
            {
                int xp = (save != null && Bonds.HasBondedNeighbor(save, p, PlayerUnits))
                    ? Mathf.RoundToInt(xpEach * 1.2f) : xpEach;
                bool up = p.GainXP(xp);
                p.AddMastery(p.ClassId, 5);
                p.AddJobLevel(p.ClassId);
                if (up) { lines.Add($"{p.UnitName} reached Lv {p.Level}!"); Sfx("levelup"); }
            }
            if (save != null)
            {
                foreach (var n in Bonds.RecordBattle(save, PlayerUnits)) lines.Add(n);
                // Classic permadeath (off on Story): the fallen stay fallen — except Kael.
                bool pd = save != null && GameBalance.Permadeath(save.difficulty);
                foreach (var u in PlayerUnits)
                    if (pd && !u.IsAlive && u.RosterId != "Kael")
                    {
                        save.roster.RemoveAll(r => r.rosterId == u.RosterId);
                        lines.Add($"{u.RosterId} has fallen... (permadeath)");
                    }
            }
            int gold = 0;
            foreach (var e in EnemyUnits) gold += Random.Range(10, 31);
            Inventory.Gold += gold;
            lines.Add($"+{gold}G (purse {Inventory.Gold}G)");
            Sfx("gold");
            foreach (var e in EnemyUnits)
            {
                float r = Random.value;
                if (r < 0.3f) { Inventory.Add("potion"); lines.Add("Loot: Potion"); }
                else if (r < 0.4f) { Inventory.Add("ether"); lines.Add("Loot: Ether"); }
            }
            foreach (var e in EnemyUnits)
            {
                if (Random.value < 0.4f)
                {
                    string mat = MatForClass(e.ClassId);
                    Inventory.AddMat(mat);
                    lines.Add($"Loot: {Crafting.MatName(mat)}");
                }
            }
            BattleReport = string.Join("\n", lines);
        }

        static string MatForClass(string cls)
        {
            switch (cls)
            {
                case "Mage":
                case "Acolyte":
                case "WhiteMage":
                case "BlackMage": return "magic_essence";
                case "Archer": return "leather_hide";
                case "Assassin":
                case "Shadowblade": return "blight_ichor";
                case "Thief": return "herbs";
                default: return "iron_ore";
            }
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

        void Music(string id)
        {
            var am = FindAnyObjectByType<Garganta.Audio.AudioManager>();
            if (am != null) am.PlayMusic(id);
        }

        void Sfx(string id)
        {
            var am = FindAnyObjectByType<Garganta.Audio.AudioManager>();
            if (am != null) am.PlaySfx(id);
        }
    }
}
