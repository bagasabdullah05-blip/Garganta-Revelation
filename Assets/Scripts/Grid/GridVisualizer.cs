using System.Collections.Generic;
using UnityEngine;
using Garganta.Core;

namespace Garganta.Grid
{
    // Placeholder visuals: staggered square sprites colored per TileType + highlight layer.
    public class GridVisualizer : MonoBehaviour
    {
        Sprite tileSprite;
        GameObject[,] highlights;
        GridManager grid;

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
                    var go = new GameObject($"Tile_{x}_{y}");
                    go.transform.position = g.CoordToWorld(t.Coord);
                    go.transform.SetParent(transform);
                    var sr = go.AddComponent<SpriteRenderer>();
                    sr.sprite = WhiteSquare;
                    sr.color = Balance.TileColor(t.Type);
                    sr.sortingOrder = -1;
                    // Elevation hint: shrink slightly per level
                    float s = 0.95f - t.Elevation * 0.05f;
                    go.transform.localScale = new Vector3(s, s * 0.75f, 1f);

                    var hl = new GameObject($"HL_{x}_{y}");
                    hl.transform.position = go.transform.position;
                    hl.transform.SetParent(go.transform);
                    var hsr = hl.AddComponent<SpriteRenderer>();
                    hsr.sprite = WhiteSquare;
                    hsr.color = new Color(1, 1, 1, 0);
                    hsr.sortingOrder = 5;
                    highlights[x, y] = hl;
                }
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
