using UnityEngine;
using Garganta.AI;
using Garganta.Art;
using Garganta.Data;
using Garganta.Grid;
using Garganta.UI;

namespace Garganta.Units
{
    public static class UnitFactory
    {
        static readonly Color PlayerTrim = new Color(0.29f, 0.42f, 0.54f); // Ash blue
        static readonly Color EnemyTrim = new Color(0.55f, 0.15f, 0.10f);  // Blood red
        static readonly Color PlayerTunic = new Color(0.36f, 0.42f, 0.48f);
        static readonly Color EnemyTunic = new Color(0.36f, 0.23f, 0.23f);

        public static Unit Create(string id, bool isPlayer, Vector2Int coord, GridManager grid)
        {
            UnitStats s = UnitDatabase.Get(id).Clone();
            var go = new GameObject(id);
            go.transform.position = grid.TileTop(coord);

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = SpriteFactory.Character(s.Weapon, isPlayer ? PlayerTunic : EnemyTunic, isPlayer ? PlayerTrim : EnemyTrim, isPlayer);
            go.AddComponent<YSort>();

            var sh = new GameObject("Shadow");
            sh.transform.SetParent(go.transform);
            sh.transform.localPosition = new Vector3(0, -0.62f, 0);
            var shsr = sh.AddComponent<SpriteRenderer>();
            shsr.sprite = SpriteFactory.BlobShadow();
            var shSort = sh.AddComponent<YSort>();
            shSort.Offset = -1;

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
