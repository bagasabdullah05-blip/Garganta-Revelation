using UnityEngine;
using Garganta.Core;
using Garganta.Units;

namespace Garganta.Grid
{
    public class HexTile
    {
        public Vector2Int Coord;
        public TileType Type;
        public int Elevation;
        public Unit Occupant;

        public HexTile(Vector2Int c, TileType t, int e) { Coord = c; Type = t; Elevation = e; }
        public bool Walkable => !Balance.IsImpassable(Type);
        public int Cost => Balance.MoveCost(Type);
    }
}
