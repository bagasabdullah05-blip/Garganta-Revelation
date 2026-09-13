using System.Collections.Generic;
using UnityEngine;

namespace Garganta.Art
{
    // Menu art loaders: portraits, button icons, fullscreen backgrounds.
    // All null-safe with silent fallbacks (letter box / text-only / no bg).
    public static class UiArt
    {
        static readonly Dictionary<string, Sprite> icons = new Dictionary<string, Sprite>();
        static readonly Dictionary<string, Sprite> portraits = new Dictionary<string, Sprite>();
        static readonly Dictionary<string, Texture2D> bgs = new Dictionary<string, Texture2D>();

        public static Sprite Icon(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            if (icons.TryGetValue(id, out var s)) return s;
            s = Resources.Load<Sprite>($"Art/Icons/icon_{id}");
            icons[id] = s;
            return s;
        }

        public static Sprite Portrait(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            if (portraits.TryGetValue(name, out var s)) return s;
            s = Resources.Load<Sprite>($"Art/portrait_{name}");
            portraits[name] = s;
            return s;
        }

        public static Texture2D Bg(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            if (bgs.TryGetValue(id, out var t)) return t;
            t = Resources.Load<Texture2D>($"Art/bg_{id}");
            bgs[id] = t;
            return t;
        }

        public static void Clear()
        {
            icons.Clear();
            portraits.Clear();
            bgs.Clear();
        }
    }
}
