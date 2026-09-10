using UnityEngine;

namespace Garganta.Core
{
    public enum TileType { Plains, Forest, Mountain, Water, Ruins, Blight, Wall, Bridge }
    public enum WeaponType { Sword, Axe, Spear, Bow, Staff, Tome, Dagger, Shield, None }
    public enum GameState { Init, PlayerTurn, EnemyTurn, Victory, Defeat }

    public static class Balance
    {
        public const int CTBThreshold = 100;
        public const int MapWidth = 12;
        public const int MapHeight = 12;

        public static int MoveCost(TileType t)
        {
            switch (t)
            {
                case TileType.Plains: return 1;
                case TileType.Forest: return 2;
                case TileType.Mountain: return 3;
                case TileType.Ruins: return 1;
                case TileType.Blight: return 1;
                case TileType.Bridge: return 1;
                default: return 99; // Water, Wall
            }
        }

        public static bool IsImpassable(TileType t) => t == TileType.Water || t == TileType.Wall;

        public static int DefBonus(TileType t)
        {
            switch (t)
            {
                case TileType.Forest: return 2;
                case TileType.Mountain: return 4;
                case TileType.Ruins: return 1;
                case TileType.Blight: return -2;
                default: return 0;
            }
        }

        public static Color TileColor(TileType t)
        {
            switch (t)
            {
                case TileType.Plains: return new Color(0.35f, 0.45f, 0.30f);
                case TileType.Forest: return new Color(0.23f, 0.37f, 0.23f);
                case TileType.Mountain: return new Color(0.42f, 0.42f, 0.45f);
                case TileType.Water: return new Color(0.20f, 0.35f, 0.55f);
                case TileType.Ruins: return new Color(0.50f, 0.47f, 0.40f);
                case TileType.Blight: return new Color(0.16f, 0.48f, 0.43f);
                case TileType.Wall: return new Color(0.15f, 0.15f, 0.18f);
                case TileType.Bridge: return new Color(0.42f, 0.26f, 0.15f);
                default: return Color.white;
            }
        }
    }
}
