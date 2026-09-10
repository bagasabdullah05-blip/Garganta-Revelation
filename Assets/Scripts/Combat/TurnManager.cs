using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Garganta.Core;
using Garganta.Units;
using Garganta.AI;

namespace Garganta.Combat
{
    // CTB: gauge += Speed * (1 + mods). Threshold 100 acts, then resets.
    public class TurnManager : MonoBehaviour
    {
        public float TickInterval = 0.1f;
        public List<Unit> All = new List<Unit>();
        public Unit ActiveEnemy;
        bool running;
        bool playerBusy;
        float timer;

        public static float ComputeGain(float speed, float mod) => speed * (1f + mod);

        public static int SelectNext(List<float> gauges)
        {
            int best = -1;
            for (int i = 0; i < gauges.Count; i++)
                if (gauges[i] >= Balance.CTBThreshold && (best < 0 || gauges[i] > gauges[best])) best = i;
            return best;
        }

        public void Begin(List<Unit> players, List<Unit> enemies)
        {
            All.Clear();
            All.AddRange(players);
            All.AddRange(enemies);
            running = true;
        }

        public void NotifyPlayerDone() => playerBusy = false;

        void Update()
        {
            if (!running || GameManager.Instance == null) return;
            var st = GameManager.Instance.State;
            if (st == GameState.Victory || st == GameState.Defeat) return;
            if (ActiveEnemy != null || playerBusy) return;

            timer += Time.deltaTime;
            if (timer < TickInterval) return;
            timer = 0f;

            foreach (var u in All) u.GainCTB(ComputeGain(u.Stats.SPD, 0f));

            var gauges = new List<float>();
            foreach (var u in All) gauges.Add(u.IsAlive ? u.CTB : -1f);
            int idx = SelectNext(gauges);
            if (idx < 0) return;

            Unit u2 = All[idx];
            EventBus.TurnAdvanced();
            if (u2.StunTurns > 0)
            {
                u2.StunTurns--;
                EventBus.Log($"{u2.UnitName} is stunned!");
                u2.ResetCTB();
                u2.TickEndStatus();
                return;
            }
            if (u2.IsPlayer)
            {
                GameManager.Instance.SelectedUnit = u2;
                GameManager.Instance.CurrentUnit = u2;
                playerBusy = true;
            }
            else
            {
                ActiveEnemy = u2;
                GameManager.Instance.SetState(GameState.EnemyTurn);
                StartCoroutine(RunEnemy(u2));
            }
        }

        IEnumerator RunEnemy(Unit u)
        {
            yield return AIController.TakeTurn(u);
            GameManager.Instance.OnEnemyDone(u);
            ActiveEnemy = null;
            if (GameManager.Instance.State == GameState.EnemyTurn)
                GameManager.Instance.SetState(GameState.PlayerTurn);
        }
    }
}
