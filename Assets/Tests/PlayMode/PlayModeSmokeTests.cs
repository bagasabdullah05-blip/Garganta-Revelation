using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Garganta.Art;
using Garganta.Audio;
using Garganta.Combat;
using Garganta.Core;
using Garganta.Grid;
using Garganta.UI;
using Garganta.Units;

// Headless auto-play: builds a battle without a scene file, drives player turns
// through the same public hooks as mouse input, and requires a decisive result.
// Validates the real runtime lifecycle: Start/Awake, CTB ticks, AI + movement
// coroutines, combat, UI updates, victory/defeat flow.
public class PlayModeSmokeTests
{
    [UnityTest]
    public IEnumerator Battle_PlaysToEnd()
    {
        Random.InitState(42);
        Time.timeScale = 20f;
        try
        {
            var camGo = new GameObject("Main Camera");
            var cam = camGo.AddComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 7f;

            var battle = new GameObject("Battle");
            battle.AddComponent<GameManager>();
            battle.AddComponent<GridManager>();
            battle.AddComponent<GridVisualizer>();
            var turns = battle.AddComponent<TurnManager>();
            turns.TickInterval = 0.001f; // accelerate CTB for headless run
            battle.AddComponent<CombatManager>();
            battle.AddComponent<ObjectPool>();
            battle.AddComponent<AudioManager>();

            var ui = new GameObject("UI");
            ui.AddComponent<UIManager>();
            ui.AddComponent<TurnOrderUI>();
            ui.AddComponent<UnitInfoUI>();
            ui.AddComponent<ActionMenuUI>();
            ui.AddComponent<CombatUI>();
            ui.AddComponent<GameOverUI>();

            yield return null;
            yield return null;

            var gm = GameManager.Instance;
            Assert.IsNotNull(gm, "GameManager singleton missing");
            Assert.AreEqual(4, gm.PlayerUnits.Count, "player roster");
            Assert.AreEqual(6, gm.EnemyUnits.Count, "enemy roster");
            Assert.AreEqual(GameState.PlayerTurn, gm.State, "battle auto-started");

            int frames = 0;
            int stuck = 0;
            Unit last = null;
            while (gm.State != GameState.Victory && gm.State != GameState.Defeat && frames < 20000)
            {
                if (gm.State == GameState.PlayerTurn && gm.CurrentUnit != null)
                {
                    if (gm.CurrentUnit == last) stuck++;
                    else { stuck = 0; last = gm.CurrentUnit; }
                    if (stuck > 10) gm.PlayerWait(); // anti-stall fallback
                    else gm.DebugPlayerMoveToward();
                }
                else { stuck = 0; last = null; }
                frames++;
                yield return null;
            }

            Assert.IsTrue(gm.State == GameState.Victory || gm.State == GameState.Defeat,
                $"battle stalled after {frames} frames");
            TestContext.WriteLine($"battle ended: {gm.State} in {frames} frames");
            if (gm.State == GameState.Victory)
                Assert.IsNotEmpty(gm.BattleReport, "victory report");
        }
        finally
        {
            Time.timeScale = 1f;
        }
    }
}
