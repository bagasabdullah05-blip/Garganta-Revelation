using System.Collections.Generic;

namespace Garganta.Data
{
    public struct ChapterConfig
    {
        public string Id;
        public string Title;
        public string Subtitle;
        public int MapVariant;
        public bool Tutorial;
        public string[] PlayerIds;
        public string[] EnemyIds;
        public int EnemyLevel;
        public string[] Unlocks; // roster ids joining after victory
        public string[] Pre;     // "Speaker: line"
        public string[] Post;
    }

    // Act I opening: Ch1 Ashes + Ch2 The Road (2 fights). Roster grows Kael -> Briar -> Sera -> Voss.
    public static class ChapterDatabase
    {
        static readonly List<ChapterConfig> nodes = new List<ChapterConfig>
        {
            new ChapterConfig {
                Id = "ch1", Title = "Ch.1 — Ashes", Subtitle = "Ashfield Ruins",
                MapVariant = 0, Tutorial = true,
                PlayerIds = new[] { "Kael" },
                EnemyIds = new[] { "Goblin", "Goblin" }, EnemyLevel = 1,
                Unlocks = new[] { "Briar" },
                Pre = new[] {
                    "Kael: The village is gone. Ash and silence. Ten years since the Blight took everything — and it still hungers.",
                    "Kael: They said no Ashwalker survives the Blight twice. Then what am I still doing here?",
                    "???: Easy, ash-man. You swing that sword like you're already dead.",
                    "Briar: Name's Briar. Ironhold deserter, professional survivor. Those goblins smell your grief — let's move.",
                },
                Post = new[] {
                    "Briar: Not bad. The Bastion could use a blade like yours.",
                    "Kael: Bastion? The Garganta Alliance still holds?",
                    "Briar: Barely. Walk with me, and keep that sword close.",
                    "SYSTEM: Briar joined the party!",
                },
            },
            new ChapterConfig {
                Id = "ch2a", Title = "Ch.2a — The Road", Subtitle = "Valenwood Approach",
                MapVariant = 1, Tutorial = false,
                PlayerIds = new[] { "Kael", "Briar" },
                EnemyIds = new[] { "Bandit", "Bandit", "Goblin", "Wolf" }, EnemyLevel = 2,
                Unlocks = new[] { "Sera" },
                Pre = new[] {
                    "Briar: Valenwood Approach. The trees watch here — Concord territory.",
                    "Kael: Someone's chanting. Acolyte robes... Dominion?",
                    "Sera: Behind me, travelers! These bandits have hounded our chapel for days.",
                    "Briar: Ha! The healer wants to fight. Fine — shield wall, ash-man!",
                },
                Post = new[] {
                    "Sera: The Light preserve you both. I am Sera — my chapel is rubble, but my oath stands.",
                    "Kael: Then take it to the Bastion with us. We march together.",
                    "SYSTEM: Sera joined the party!",
                },
            },
            new ChapterConfig {
                Id = "ch2b", Title = "Ch.2b — The Bastion", Subtitle = "Ironhold Checkpoint",
                MapVariant = 2, Tutorial = false,
                PlayerIds = new[] { "Kael", "Briar", "Sera" },
                EnemyIds = new[] { "Bandit", "Cultist", "Skeleton", "Skeleton" }, EnemyLevel = 3,
                Unlocks = new[] { "Voss" },
                Pre = new[] {
                    "Sera: The Bastion's outer gate. Why do the sentries aim AT us?",
                    "Voss: ...Because the Captain says Alliance dogs carry Blight. Lower your weapons. Slowly.",
                    "Kael: We're no carriers. But those cultists behind you? They reek of it.",
                    "Voss: Hm. Prove it, ash-man. Steel first, questions later.",
                },
                Post = new[] {
                    "Voss: ...Clean kills. The Captain was wrong about you. I was wrong.",
                    "Voss: Valenwood owes you a debt. My bow is yours until it's repaid.",
                    "Briar: The Bastion opens its gates at last. Rest up — war waits for no one.",
                    "SYSTEM: Voss joined the party!",
                },
            },
        };

        public static int NodeCount => nodes.Count;
        public static ChapterConfig GetNode(int i) => nodes[i];
        public static ChapterConfig GetById(string id) => nodes.Find(n => n.Id == id);
    }
}
