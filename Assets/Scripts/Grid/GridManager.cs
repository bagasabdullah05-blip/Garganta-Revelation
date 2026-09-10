using UnityEngine;
using Garganta.Core;
using Garganta.Data;
using Garganta.Units;

namespace Garganta.Grid
{
    // Odd-r hex-offset grid rendered as staggered rows. Size 12x12 for M1 Ashfield.
    public class GridManager : MonoBehaviour
    {
        public int Width = Balance.MapWidth;
        public int Height = Balance.MapHeight;
        public HexTile[,] Tiles;

        // Staggered world layout: x offset on odd rows, row height 0.75 (hex packing).
        public Vector3 CoordToWorld(Vector2Int c)
        {
            float x = c.x + 0.5f * (c.y & 1);
            float y = c.y * 0.75f;
            return new Vector3(x, y, 0);
        }

        public const float ElevY = 0.18f; // visual lift per elevation level (keeps click picking stable)

        public Vector3 TileTop(Vector2Int c) => CoordToWorld(c) + new Vector3(0, Tiles[c.x, c.y].Elevation * ElevY, 0);

        public Vector2Int WorldToCoord(Vector3 w)
        {
            int row = Mathf.RoundToInt(w.y / 0.75f);
            int col = Mathf.RoundToInt(w.x - 0.5f * (row & 1));
            row = Mathf.Clamp(row, 0, Height - 1);
            col = Mathf.Clamp(col, 0, Width - 1);
            return new Vector2Int(col, row);
        }

        public void Generate() => GenerateVariant(0);

        public void GenerateVariant(int variant)
        {
            MapDatabase.GetMap(variant, out TileType[,] types, out int[,] elev);
            Width = types.GetLength(0);
            Height = types.GetLength(1);
            Tiles = new HexTile[Width, Height];
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    Tiles[x, y] = new HexTile(new Vector2Int(x, y), types[x, y], elev[x, y]);
        }

        public bool InBounds(Vector2Int c) => c.x >= 0 && c.y >= 0 && c.x < Width && c.y < Height;
        public bool IsWalkable(Vector2Int c) => InBounds(c) && Tiles[c.x, c.y].Walkable;
        public int CostOf(Vector2Int c) => Tiles[c.x, c.y].Cost;

        public void MoveOccupant(Unit u, Vector2Int to)
        {
            Tiles[u.Coord.x, u.Coord.y].Occupant = null;
            Tiles[to.x, to.y].Occupant = u;
        }

        public static Vector2Int[] Neighbors(Vector2Int c)
        {
            bool odd = (c.y & 1) == 1;
            if (!odd) return new[] {
                new Vector2Int(c.x+1,c.y), new Vector2Int(c.x-1,c.y),
                new Vector2Int(c.x,c.y-1), new Vector2Int(c.x-1,c.y-1),
                new Vector2Int(c.x,c.y+1), new Vector2Int(c.x-1,c.y+1) };
            return new[] {
                new Vector2Int(c.x+1,c.y), new Vector2Int(c.x-1,c.y),
                new Vector2Int(c.x+1,c.y-1), new Vector2Int(c.x,c.y-1),
                new Vector2Int(c.x+1,c.y+1), new Vector2Int(c.x,c.y+1) };
        }

        // Odd-r -> cube -> distance.
        public static int HexDistance(Vector2Int a, Vector2Int b)
        {
            Vector3Int ac = OddRToCube(a), bc = OddRToCube(b);
            return (Mathf.Abs(ac.x - bc.x) + Mathf.Abs(ac.y - bc.y) + Mathf.Abs(ac.z - bc.z)) / 2;
        }

        static Vector3Int OddRToCube(Vector2Int c)
        {
            int x = c.x - (c.y - (c.y & 1)) / 2;
            int z = c.y;
            return new Vector3Int(x, -x - z, z);
        }
    }
}
