namespace Garganta.Data
{
    // 5 endings per PRD 6: True / Light / Shadow / Sacrifice / Blight.
    public static class Endings
    {
        public struct Ending { public string Id; public string Title; public string Text; }

        public static Ending Compute(GameSave save, int kaelCorruption)
        {
            if (save != null && save.roster.Count >= 15)
                return new Ending { Id = "true", Title = "TRUE ENDING — Dawn of Garganta",
                    Text = "Every soul gathered, every bond unbroken. The Blight is unmade — not sealed, not ruled, but understood and released. Garganta dawns." };
            if (kaelCorruption >= 75)
                return new Ending { Id = "blight", Title = "BLIGHT ENDING — The Hollow Crown",
                    Text = "The Ashwalker refused to let go. Corruption crowned, Garganta kneels to its new Blight Lord." };
            string top = TopFaction(save);
            if (top == "Shadow")
                return new Ending { Id = "shadow", Title = "SHADOW ENDING — Obsidian Throne",
                    Text = "The Shadow Court claims the Aether. The protagonist rules at their side — peace, at the price of truth." };
            if (top == "Alliance")
                return new Ending { Id = "sacrifice", Title = "SACRIFICE ENDING — The Last Seal",
                    Text = "United but incomplete, the Alliance begs a final price. The Ashwalker pays it, sealing the Blight forever." };
            return new Ending { Id = "light", Title = "LIGHT ENDING — Purified Dawn",
                Text = "The Blight is purified. Factions unite beneath a fragile dawn — and the survivors rebuild." };
        }

        static string TopFaction(GameSave save)
        {
            string best = "Alliance";
            int bestPts = int.MinValue;
            foreach (var f in Reputation.Factions)
            {
                int p = Reputation.Get(save, f);
                if (p > bestPts) { bestPts = p; best = f; }
            }
            return best;
        }
    }
}
