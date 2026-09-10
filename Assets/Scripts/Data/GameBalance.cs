namespace Garganta.Data
{
    // PRD 8 difficulty: enemy stats, XP gain, permadeath rules.
    public static class GameBalance
    {
        public static readonly string[] Names = { "Story", "Normal", "Hard", "Nightmare" };

        public static string Name(int diff) => Names[UnityEngine.Mathf.Clamp(diff, 0, 3)];

        public static float EnemyStatMult(int diff)
        {
            switch (diff)
            {
                case 0: return 0.8f;
                case 2: return 1.2f;
                case 3: return 1.5f;
                default: return 1f;
            }
        }

        public static float XpMult(int diff)
        {
            switch (diff)
            {
                case 0: return 1.2f;
                case 2: return 0.8f;
                case 3: return 0.6f;
                default: return 1f;
            }
        }

        public static bool Permadeath(int diff) => diff != 0;
    }
}
