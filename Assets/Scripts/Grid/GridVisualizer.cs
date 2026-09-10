using System.Collections.Generic;
using UnityEngine;
using Garganta.Art;
using Garganta.Core;

namespace Garganta.Grid
{
    // 2D isometric look: diamond tiles, decorations, animated water/blight, Y depth.
    public class GridVisualizer : MonoBehaviour
    {
        GameObject[,] highlights;
        GridManager grid;

        readonly List<SpriteRenderer> waterTiles = new List<SpriteRenderer>();
        readonly List<SpriteRenderer> blightTiles = new List<SpriteRenderer>();
        float animT;
        bool frame;

        static Sprite whiteSquare;
        public static Sprite WhiteSquare
        {
            get
            {
                if (whiteSquare == null)
                {
                    var tex = new Texture2D(64, 64) { filterMode = FilterMode.Point };
                    var px = new Color[64 * 64];
                    for (int i = 0; i < px.Length; i++) px[i] = Color.white;
                    tex.SetPixels(px);
                    tex.Apply();
                    whiteSquare = Sprite.Create(tex, new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f), 64f);
                }
                return whiteSquare;
            }
        }

        public void Build(GridManager g)
        {
            grid = g;
            highlights = new GameObject[g.Width, g.Height];
            for (int x = 0; x < g.Width; x++)
                for (int y = 0; y < g.Height; y++)
                {
                    var t = g.Tiles[x, y];
                    Vector3 pos = g.TileTop(t.Coord);
                    int order = -200 + y * 2 + t.Elevation;

                    var go = new GameObject($"Tile_{x}_{y}");
                    go.transform.position = pos;
                    go.transform.SetParent(transform);
                    var sr = go.AddComponent<SpriteRenderer>();
                    sr.sortingOrder = order;

                    switch (t.Type)
                    {
                        case TileType.Water:
                            sr.sprite = SpriteFactory.WaterDiamond(0);
                            waterTiles.Add(sr);
                            break;
                        case TileType.Blight:
                            sr.sprite = SpriteFactory.BlightDiamond(0);
                            blightTiles.Add(sr);
                            break;
                        case TileType.Bridge:
                            sr.sprite = SpriteFactory.BridgeDiamond();
                            break;
                        case TileType.Ruins:
                            sr.sprite = SpriteFactory.RuinDiamond();
                            break;
                        case TileType.Wall:
                            sr.sprite = SpriteFactory.WallBlock();
                            sr.sortingOrder = order + 4;
                            break;
                        default:
                            sr.sprite = SpriteFactory.Diamond(Balance.TileColor(t.Type));
                            break;
                    }

                    AddDeco(t, pos, order);

                    var hl = new GameObject($"HL_{x}_{y}");
                    hl.transform.position = pos;
                    hl.transform.SetParent(go.transform);
                    var hsr = hl.AddComponent<SpriteRenderer>();
                    hsr.sprite = SpriteFactory.Diamond(Color.white);
                    hsr.color = new Color(1, 1, 1, 0);
                    hsr.sortingOrder = order + 3;
                    highlights[x, y] = hl;
                }
        }

        void AddDeco(HexTile t, Vector3 pos, int order)
        {
            Sprite deco = null;
            Vector3 off = Vector3.zero;
            switch (t.Type)
            {
                case TileType.Forest: deco = SpriteFactory.Tree(); off = new Vector3(0, 0.55f, 0); break;
                case TileType.Mountain: deco = SpriteFactory.Rock(); off = new Vector3(0, 0.28f, 0); break;
                case TileType.Plains:
                    if ((t.Coord.x * 7 + t.Coord.y * 13) % 5 == 0) { deco = SpriteFactory.Tuft(); off = new Vector3(0.1f, 0.1f, 0); }
                    break;
            }
            if (deco == null) return;
            var go = new GameObject($"Deco_{t.Coord.x}_{t.Coord.y}");
            go.transform.position = pos + off;
            go.transform.SetParent(transform);
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = deco;
            sr.sortingOrder = order + 2;
        }

        void Update()
        {
            if (waterTiles.Count == 0 && blightTiles.Count == 0) return;
            animT += Time.deltaTime;
            if (animT < 0.45f) return;
            animT = 0f;
            frame = !frame;
            int f = frame ? 1 : 0;
            foreach (var sr in waterTiles) if (sr != null) sr.sprite = SpriteFactory.WaterDiamond(f);
            foreach (var sr in blightTiles) if (sr != null) sr.sprite = SpriteFactory.BlightDiamond(f);
        }

        public void ShowRange(HashSet<Vector2Int> cells, Color c)
        {
            foreach (var cell in cells)
            {
                if (cell.x < 0 || cell.y < 0 || cell.x >= grid.Width || cell.y >= grid.Height) continue;
                highlights[cell.x, cell.y].GetComponent<SpriteRenderer>().color = c;
            }
        }

        public void ClearHighlights()
        {
            if (highlights == null) return;
            foreach (var hl in highlights)
                if (hl != null) hl.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }
}
