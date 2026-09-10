using Garganta.Core;

namespace Garganta.Data
{
    // 12x12 hex-offset maps. Variant 0 Ashfield (Ch1), 1 Valenwood (Ch2a), 2 Bastion gate (Ch2b).
    // Spawn corners (1-2,1-2) and (8-10,8-10) stay walkable in every variant.
    public static class MapDatabase
    {
        public static void GetM1(out TileType[,] types, out int[,] elev) => GetMap(0, out types, out elev);

        public static void GetMap(int variant, out TileType[,] types, out int[,] elev)
        {
            int w = Balance.MapWidth, h = Balance.MapHeight;
            types = new TileType[w, h];
            elev = new int[w, h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++) { types[x, y] = TileType.Plains; elev[x, y] = 0; }

            for (int i = 0; i < w; i++) { types[i, 0] = TileType.Wall; types[i, h - 1] = TileType.Wall; }
            for (int i = 0; i < h; i++) { types[0, i] = TileType.Wall; types[w - 1, i] = TileType.Wall; }
            types[5, 0] = TileType.Bridge; types[6, 0] = TileType.Bridge;
            types[5, h - 1] = TileType.Bridge; types[6, h - 1] = TileType.Bridge;

            switch (variant)
            {
                case 1: PaintValenwood(types, elev); break;
                case 2: PaintBastion(types, elev); break;
                default: PaintAshfield(types, elev); break;
            }
        }

        static void PaintAshfield(TileType[,] t, int[,] e)
        {
            foreach (var c in new[] { (2, 4), (2, 5), (3, 4), (3, 5), (2, 6), (3, 6) }) t[c.Item1, c.Item2] = TileType.Forest;
            foreach (var c in new[] { (8, 8), (9, 8), (8, 9), (9, 9) }) { t[c.Item1, c.Item2] = TileType.Mountain; e[c.Item1, c.Item2] = 2; }
            t[7, 8] = TileType.Mountain; e[7, 8] = 1;
            for (int y = 2; y <= 9; y++) t[6, y] = TileType.Water;
            t[6, 5] = TileType.Bridge;
            t[4, 6] = TileType.Blight; t[5, 6] = TileType.Blight; t[7, 6] = TileType.Blight;
            for (int x = 2; x <= 5; x++) t[x, 2] = TileType.Ruins;
            t[4, 3] = TileType.Ruins;
        }

        static void PaintValenwood(TileType[,] t, int[,] e)
        {
            // Dense forest west + east groves, stream far east, clearing center.
            for (int y = 1; y <= 10; y++) { t[3, y] = TileType.Forest; if (y % 2 == 0) t[4, y] = TileType.Forest; }
            foreach (var c in new[] { (9, 3), (10, 3), (9, 4), (10, 6), (9, 7) }) t[c.Item1, c.Item2] = TileType.Forest;
            for (int y = 2; y <= 7; y++) t[8, y] = TileType.Water;
            t[8, 5] = TileType.Bridge;
            t[7, 9] = TileType.Mountain; e[7, 9] = 1;
            t[5, 4] = TileType.Ruins; t[6, 4] = TileType.Ruins;
            t[5, 7] = TileType.Blight; t[6, 7] = TileType.Blight;
        }

        static void PaintBastion(TileType[,] t, int[,] e)
        {
            // Fortified checkpoint: central wall with gate, ruins plaza, watch hills.
            for (int x = 2; x <= 9; x++) t[x, 6] = TileType.Wall;
            t[5, 6] = TileType.Bridge; // gate
            for (int x = 4; x <= 7; x++)
                for (int y = 3; y <= 5; y++) t[x, y] = TileType.Ruins;
            t[3, 8] = TileType.Mountain; e[3, 8] = 2;
            t[4, 8] = TileType.Mountain; e[4, 8] = 1;
            t[9, 3] = TileType.Mountain; e[9, 3] = 1;
            t[2, 3] = TileType.Forest; t[2, 4] = TileType.Forest;
            t[7, 8] = TileType.Blight; t[8, 8] = TileType.Blight;
        }
    }
}
