using UnityEngine;
using Garganta.AI;
using Garganta.Data;
using Garganta.Grid;
using Garganta.UI;

namespace Garganta.Units
{
    public static class UnitFactory
    {
        static readonly Color PlayerColor = new Color(0.29f, 0.42f, 0.54f); // Ash blue
        static readonly Color EnemyColor = new Color(0.55f, 0.15f, 0.10f);  // Blood red

        public static Unit Create(string id, bool isPlayer, Vector2Int coord, GridManager grid)
        {
            UnitStats s = UnitDatabase.Get(id).Clone();
            var go = new GameObject(id);
            go.transform.position = grid.CoordToWorld(coord);
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = GridVisualizer.WhiteSquare;
            sr.color = isPlayer ? PlayerColor : EnemyColor;
            go.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
            sr.sortingOrder = 10;

            var unit = go.AddComponent<Unit>();
            unit.Init(isPlayer ? $"P_{id}" : $"E_{id}_{coord}", id, isPlayer, s, coord);
            if (!isPlayer) unit.Behavior = id == "Skeleton" ? AIBehavior.Defensive : AIBehavior.Aggressive;
            go.AddComponent<UnitMovement>();
            go.AddComponent<HealthBarUI>();
            grid.Tiles[coord.x, coord.y].Occupant = unit;
            return unit;
        }
    }
}
