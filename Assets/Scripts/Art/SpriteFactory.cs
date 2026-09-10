using System.Collections.Generic;
using UnityEngine;
using Garganta.Core;

namespace Garganta.Art
{
    // Procedural 2D pixel-art sprites (Point filter, crisp). No external assets needed for M1.
    // Palette follows ART_BRIEF.md: desaturated dark fantasy + Aether glow.
    public static class SpriteFactory
    {
        static readonly Dictionary<string, Sprite> cache = new Dictionary<string, Sprite>();

        static Texture2D NewTex(int w, int h)
        {
            var t = new Texture2D(w, h, TextureFormat.RGBA32, false);
            t.filterMode = FilterMode.Point;
            t.wrapMode = TextureWrapMode.Clamp;
            var clear = new Color[w * h];
            for (int i = 0; i < clear.Length; i++) clear[i] = new Color(0, 0, 0, 0);
            t.SetPixels(clear);
            return t;
        }

        static void Rect(Texture2D t, int x0, int y0, int w, int h, Color c)
        {
            for (int y = y0; y < y0 + h; y++)
                for (int x = x0; x < x0 + w; x++)
                    if (x >= 0 && y >= 0 && x < t.width && y < t.height)
                        t.SetPixel(x, y, c);
        }

        static Color Dark(Color c, float amt) => new Color(c.r * (1 - amt), c.g * (1 - amt), c.b * (1 - amt), c.a);
        static Color Light(Color c, float amt) => new Color(Mathf.Min(1, c.r + amt), Mathf.Min(1, c.g + amt), Mathf.Min(1, c.b + amt), c.a);

        static Sprite Make(string key, Texture2D t, float ppu)
        {
            var ov = ArtOverride.Get(key);
            if (ov != null) { Object.Destroy(t); return ov; }
            t.Apply();
            var s = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), ppu);
            cache[key] = s;
            return s;
        }

        // 64x48 diamond -> 1 x 0.75 world. Beveled: light top facet, dark bottom + outline.
        public static Sprite Diamond(Color fill)
        {
            string key = $"dia_{ColorUtility.ToHtmlStringRGB(fill)}";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(64, 48);
            for (int y = 0; y < 48; y++)
                for (int x = 0; x < 64; x++)
                {
                    float d = Mathf.Abs(x - 31.5f) / 32f + Mathf.Abs(y - 23.5f) / 24f;
                    if (d > 1f) continue;
                    Color c = fill;
                    if (d > 0.85f) c = Dark(fill, 0.45f);
                    else if (y > 27) c = Light(fill, 0.10f);
                    else if (y < 19) c = Dark(fill, 0.12f);
                    t.SetPixel(x, y, c);
                }
            return Make(key, t, 64f);
        }

        public static Sprite BridgeDiamond()
        {
            const string key = "bridge";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(64, 48);
            var wood = new Color(0.42f, 0.26f, 0.15f);
            for (int y = 0; y < 48; y++)
                for (int x = 0; x < 64; x++)
                {
                    float d = Mathf.Abs(x - 31.5f) / 32f + Mathf.Abs(y - 23.5f) / 24f;
                    if (d > 1f) continue;
                    Color c = wood;
                    if (d > 0.85f) c = Dark(wood, 0.4f);
                    else if (y % 8 < 1) c = Dark(wood, 0.35f); // plank gaps
                    else if (y > 27) c = Light(wood, 0.08f);
                    t.SetPixel(x, y, c);
                }
            return Make(key, t, 64f);
        }

        public static Sprite RuinDiamond()
        {
            const string key = "ruin";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(64, 48);
            var stone = new Color(0.50f, 0.47f, 0.40f);
            for (int y = 0; y < 48; y++)
                for (int x = 0; x < 64; x++)
                {
                    float d = Mathf.Abs(x - 31.5f) / 32f + Mathf.Abs(y - 23.5f) / 24f;
                    if (d > 1f) continue;
                    Color c = stone;
                    if (d > 0.85f) c = Dark(stone, 0.45f);
                    else if ((x + y * 3) % 17 < 2) c = Dark(stone, 0.4f); // cracks
                    else if (y > 27) c = Light(stone, 0.08f);
                    t.SetPixel(x, y, c);
                }
            return Make(key, t, 64f);
        }

        public static Sprite WaterDiamond(int frame)
        {
            string key = $"water{frame}";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(64, 48);
            var deep = new Color(0.16f, 0.30f, 0.50f);
            var foam = new Color(0.53f, 0.81f, 0.92f);
            for (int y = 0; y < 48; y++)
                for (int x = 0; x < 64; x++)
                {
                    float d = Mathf.Abs(x - 31.5f) / 32f + Mathf.Abs(y - 23.5f) / 24f;
                    if (d > 1f) continue;
                    Color c = deep;
                    if (d > 0.85f) c = Dark(deep, 0.4f);
                    else if ((y + (frame == 1 ? 4 : 0)) % 12 < 1 && (x % 8 < 4) == (frame == 1)) c = foam; // ripple
                    t.SetPixel(x, y, c);
                }
            return Make(key, t, 64f);
        }

        public static Sprite BlightDiamond(int frame)
        {
            string key = $"blight{frame}";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(64, 48);
            var base_ = new Color(0.16f, 0.42f, 0.38f);
            var vein = new Color(0.30f, 1f, 0.83f);
            for (int y = 0; y < 48; y++)
                for (int x = 0; x < 64; x++)
                {
                    float d = Mathf.Abs(x - 31.5f) / 32f + Mathf.Abs(y - 23.5f) / 24f;
                    if (d > 1f) continue;
                    Color c = base_;
                    if (d > 0.85f) c = Dark(base_, 0.4f);
                    else if ((x * 3 + y * 7) % 11 < (frame == 1 ? 3 : 2)) c = vein; // pulsing veins
                    else if (y > 27) c = Light(base_, 0.06f);
                    t.SetPixel(x, y, c);
                }
            return Make(key, t, 64f);
        }

        public static Sprite Tree()
        {
            const string key = "tree";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(48, 64);
            var trunk = new Color(0.42f, 0.26f, 0.15f);
            var leaf = new Color(0.23f, 0.37f, 0.23f);
            var leafD = new Color(0.18f, 0.31f, 0.18f);
            Rect(t, 22, 0, 5, 16, trunk);
            Rect(t, 8, 14, 32, 12, leafD);
            Rect(t, 12, 26, 24, 12, leaf);
            Rect(t, 16, 38, 16, 12, leafD);
            Rect(t, 12, 26, 3, 12, Light(leaf, 0.12f)); // rim light
            return Make(key, t, 48f);
        }

        public static Sprite Rock()
        {
            const string key = "rock";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(40, 32);
            var gray = new Color(0.42f, 0.44f, 0.48f);
            for (int y = 0; y < 26; y++)
            {
                int hw = 4 + (int)(y * 0.62f);
                for (int x = 20 - hw; x <= 20 + hw; x++)
                {
                    Color c = gray;
                    if (y > 18) c = Light(gray, 0.12f);
                    if ((x * 5 + y * 11) % 23 < 2) c = Dark(gray, 0.3f);
                    t.SetPixel(x, y, c);
                }
            }
            return Make(key, t, 40f);
        }

        public static Sprite WallBlock()
        {
            const string key = "wall";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(64, 64);
            var stone = new Color(0.30f, 0.30f, 0.34f);
            var top = new Color(0.45f, 0.45f, 0.50f);
            Rect(t, 0, 0, 64, 64, stone);
            Rect(t, 0, 48, 64, 16, top);
            for (int y = 0; y < 64; y += 8)
                Rect(t, 0, y, 64, 1, Dark(stone, 0.4f));
            for (int y = 4; y < 64; y += 16)
            {
                Rect(t, 16, y, 1, 8, Dark(stone, 0.4f));
                Rect(t, 48, y, 1, 8, Dark(stone, 0.4f));
            }
            Rect(t, 0, 48, 64, 2, Light(top, 0.15f));
            return Make(key, t, 64f);
        }

        public static Sprite Tuft()
        {
            const string key = "tuft";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(24, 16);
            var g1 = new Color(0.30f, 0.45f, 0.25f);
            var g2 = new Color(0.38f, 0.55f, 0.30f);
            int[] xs = { 3, 7, 11, 15, 19 };
            for (int i = 0; i < xs.Length; i++)
            {
                int h = 6 + (i * 5) % 5;
                for (int y = 0; y < h; y++)
                {
                    t.SetPixel(xs[i], y, i % 2 == 0 ? g1 : g2);
                    if (y > 2) t.SetPixel(xs[i] + (i % 2 == 0 ? 1 : -1), y - 2, g1);
                }
            }
            return Make(key, t, 32f);
        }

        public static Sprite BlobShadow()
        {
            const string key = "shadow";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(32, 12);
            for (int y = 0; y < 12; y++)
                for (int x = 0; x < 32; x++)
                {
                    float nx = (x - 15.5f) / 16f, ny = (y - 5.5f) / 6f;
                    float r = nx * nx + ny * ny;
                    if (r <= 1f) t.SetPixel(x, y, new Color(0, 0, 0, 0.35f * (1f - r)));
                }
            return Make(key, t, 32f);
        }

        // 32x48 humanoid. y=0 is feet. Weapon drawn on the right.
        public static Sprite Character(WeaponType weapon, Color tunic, Color trim, bool player)
        {
            string key = $"char_{weapon}_{ColorUtility.ToHtmlStringRGB(trim)}";
            if (cache.TryGetValue(key, out var s)) return s;
            var t = NewTex(32, 48);
            var skin = new Color(0.85f, 0.70f, 0.56f);
            var hair = player ? new Color(0.29f, 0.29f, 0.32f) : new Color(0.23f, 0.16f, 0.16f);
            var dark = new Color(0.10f, 0.10f, 0.18f);
            var pants = new Color(0.25f, 0.22f, 0.20f);
            var steel = new Color(0.75f, 0.78f, 0.82f);
            var wood = new Color(0.42f, 0.26f, 0.15f);

            Rect(t, 12, 0, 3, 10, pants);   // legs
            Rect(t, 17, 0, 3, 10, pants);
            Rect(t, 12, 0, 3, 2, dark);     // boots
            Rect(t, 17, 0, 3, 2, dark);
            Rect(t, 10, 10, 12, 14, tunic); // torso
            Rect(t, 10, 13, 12, 2, Dark(tunic, 0.35f)); // belt
            Rect(t, 8, 16, 2, 8, tunic);    // arms
            Rect(t, 22, 16, 2, 8, tunic);
            Rect(t, 8, 23, 3, 3, trim);     // pauldrons
            Rect(t, 21, 23, 3, 3, trim);
            Rect(t, 12, 27, 8, 8, skin);    // head
            Rect(t, 11, 35, 10, 3, hair);   // hair top
            Rect(t, 11, 27, 1, 8, hair);    // side hair
            Rect(t, 20, 27, 1, 8, hair);
            t.SetPixel(14, 30, dark);       // eyes
            t.SetPixel(17, 30, dark);

            switch (weapon)
            {
                case WeaponType.Sword:
                    Rect(t, 26, 12, 2, 22, steel);
                    Rect(t, 24, 11, 6, 2, wood);
                    Rect(t, 26, 8, 2, 3, wood);
                    break;
                case WeaponType.Axe:
                    Rect(t, 26, 8, 2, 26, wood);
                    Rect(t, 26, 28, 5, 5, steel);
                    break;
                case WeaponType.Spear:
                    Rect(t, 27, 4, 2, 36, wood);
                    Rect(t, 26, 40, 4, 4, steel);
                    break;
                case WeaponType.Bow:
                    for (int y = 10; y < 36; y++) t.SetPixel(27 + ((y - 10) * (y - 10)) / 130 - 2, y, wood);
                    Rect(t, 27, 10, 1, 26, Light(skin, 0.2f)); // string
                    break;
                case WeaponType.Staff:
                    Rect(t, 26, 6, 2, 30, wood);
                    Rect(t, 25, 36, 4, 4, trim);
                    t.SetPixel(27, 38, Color.white);
                    break;
                case WeaponType.Tome:
                    Rect(t, 24, 16, 6, 8, wood);
                    Rect(t, 25, 17, 4, 6, Light(skin, 0.25f));
                    break;
                case WeaponType.Dagger:
                    Rect(t, 26, 18, 2, 10, steel);
                    Rect(t, 26, 16, 2, 2, wood);
                    break;
                default:
                    Rect(t, 26, 14, 2, 20, steel);
                    break;
            }
            return Make(key, t, 32f);
        }
    }
}
