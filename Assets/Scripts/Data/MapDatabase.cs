using Garganta.Core;

namespace Garganta.Data
{
    // M1 Ashfield Ruins 12x12: plains base, forest patches, mountain corner (high),
    // stream (water), blight scars, ruins road, walls border.
    public static class MapDatabase
    {
        public static void GetM1(out TileType[,] types, out int[,] elev)
        {
            int w = Balance.MapWidth, h = Balance.MapHeight;
            types = new TileType[w, h];
            elev = new int[w, h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++) { types[x, y] = TileType.Plains; elev[x, y] = 0; }

            // Border walls (leave two gates)
            for (int i = 0; i < w; i++) { types[i, 0] = TileType.Wall; types[i, h - 1] = TileType.Wall; }
            for (int i = 0; i < h; i++) { types[0, i] = TileType.Wall; types[w - 1, i] = TileType.Wall; }
            types[5, 0] = TileType.Bridge; types[6, 0] = TileType.Bridge;
            types[5, h - 1] = TileType.Bridge; types[6, h - 1] = TileType.Bridge;

            // Forest west
            foreach (var c in new[] { (2, 4), (2, 5), (3, 4), (3, 5), (2, 6), (3, 6) }) types[c.Item1, c.Item2] = TileType.Forest;
            // Mountain north-east (high ground)
            foreach (var c in new[] { (8, 8), (9, 8), (8, 9), (9, 9) }) { types[c.Item1, c.Item2] = TileType.Mountain; elev[c.Item1, c.Item2] = 2; }
            types[7, 8] = TileType.Mountain; elev[7, 8] = 1;
            // Stream vertical (impassable) with bridge crossing
            for (int y = 2; y <= 9; y++) types[6, y] = TileType.Water;
            types[6, 5] = TileType.Bridge;
            // Blight scars center
            types[4, 6] = TileType.Blight; types[5, 6] = TileType.Blight; types[7, 6] = TileType.Blight;
            // Ruins road south
            for (int x = 2; x <= 5; x++) types[x, 2] = TileType.Ruins;
            types[4, 3] = TileType.Ruins;
        }
    }
}
