using System.Collections.Generic;
using Garganta.Grid;
using Garganta.Units;

namespace Garganta.Data
{
    // Support bonds: +1 per battle together, +3 in support range (<=3 hexes) at victory.
    // C20 B50 A100 S200. Combat aura (HP/ATK %) + XP bonus with bonded neighbor.
    public static class Bonds
    {
        [System.Serializable]
        public struct BondEntry { public string a; public string b; public int points; }

        public static int Level(int points)
        {
            if (points >= 200) return 4;
            if (points >= 100) return 3;
            if (points >= 50) return 2;
            if (points >= 20) return 1;
            return 0;
        }

        static string Key(string x, string y) => string.CompareOrdinal(x, y) < 0 ? x + "|" + y : y + "|" + x;

        public static int Points(GameSave save, string x, string y)
        {
            if (save == null) return 0;
            string k = Key(x, y);
            foreach (var e in save.bonds)
                if (Key(e.a, e.b) == k) return e.points;
            return 0;
        }

        public static int LevelBetween(GameSave save, string x, string y) => Level(Points(save, x, y));

        // Returns level-up messages ("Kael-Briar bond B!") for fresh thresholds.
        public static List<string> RecordBattle(GameSave save, List<Unit> allies)
        {
            var news = new List<string>();
            var alive = allies.FindAll(u => u.IsAlive && !string.IsNullOrEmpty(u.RosterId));
            for (int i = 0; i < alive.Count; i++)
                for (int j = i + 1; j < alive.Count; j++)
                {
                    var a = alive[i].RosterId;
                    var b = alive[j].RosterId;
                    int gain = GridManager.HexDistance(alive[i].Coord, alive[j].Coord) <= 3 ? 3 : 1;
                    int before = Level(Points(save, a, b));
                    AddPoints(save, a, b, gain);
                    int after = Level(Points(save, a, b));
                    if (after > before)
                        news.Add($"{a}-{b} bond {"CBS"[after - 1]}!");
                }
            return news;
        }

        static void AddPoints(GameSave save, string a, string b, int gain)
        {
            string k = Key(a, b);
            for (int i = 0; i < save.bonds.Count; i++)
                if (Key(save.bonds[i].a, save.bonds[i].b) == k)
                {
                    var e = save.bonds[i];
                    e.points += gain;
                    save.bonds[i] = e;
                    return;
                }
            save.bonds.Add(new BondEntry { a = a, b = b, points = gain });
        }

        public static bool HasBondedNeighbor(GameSave save, Unit u, List<Unit> allies)
        {
            if (save == null || string.IsNullOrEmpty(u.RosterId)) return false;
            foreach (var a in allies)
            {
                if (a == u || !a.IsAlive || string.IsNullOrEmpty(a.RosterId)) continue;
                if (LevelBetween(save, u.RosterId, a.RosterId) >= 1
                    && GridManager.HexDistance(u.Coord, a.Coord) <= 3) return true;
            }
            return false;
        }
    }
}
