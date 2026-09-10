using System;

namespace Garganta.Core
{
    // Decoupled event bus (Observer pattern). UI / combat / turns talk via here.
    public static class EventBus
    {
        public static event Action<GameState> OnGameStateChanged;
        public static event Action OnTurnAdvanced;
        public static event Action<string, int, string> OnDamageDealt; // attacker, amount, defender
        public static event Action<string> OnLog;

        public static void StateChanged(GameState s) => OnGameStateChanged?.Invoke(s);
        public static void TurnAdvanced() => OnTurnAdvanced?.Invoke();
        public static void Damage(string attacker, int amount, string defender) => OnDamageDealt?.Invoke(attacker, amount, defender);
        public static void Log(string msg) => OnLog?.Invoke(msg);

        public static void Clear()
        {
            OnGameStateChanged = null;
            OnTurnAdvanced = null;
            OnDamageDealt = null;
            OnLog = null;
        }
    }
}
