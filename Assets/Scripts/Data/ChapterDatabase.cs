using System.Collections.Generic;

namespace Garganta.Data
{
    public struct ChapterChoice
    {
        public string Prompt;
        public string AText;
        public string BText;
        public string AFaction;
        public int APts;
        public string BFaction;
        public int BPts;
        public int AGold;
        public int BGold;
        public string AJoin; // rosterId or ""
        public string BJoin;
    }

    public struct ChapterConfig
    {
        public string Id;
        public string Title;
        public string Subtitle;
        public int MapVariant;
        public bool Tutorial;
        public string[] PlayerIds;
        public string[] EnemyIds;
        public string[] RecruitIds; // parallel to EnemyIds ("" = not recruitable)
        public int EnemyLevel;
        public string[] PreJoins; // roster ids joining before this battle
        public string[] Unlocks; // roster ids joining after victory
        public ChapterChoice Choice;
        public string[] Pre;     // "Speaker: line"
        public string[] Post;
        public bool HasChoice => Choice.Prompt != null;
    }

    // Act I-II: Ch1-Ch2 (M3) + Ch3 First Blood, Ch4 Iron & Wood, Ch5 Betrayal,
    // Ch6 Underground, Ch7 Counter (M4). Roster grows to 9.
    public static class ChapterDatabase
    {
        static readonly List<ChapterConfig> nodes = new List<ChapterConfig>
        {
            new ChapterConfig {
                Id = "ch1", Title = "Ch.1 — Ashes", Subtitle = "Ashfield Ruins",
                MapVariant = 0, Tutorial = true,
                PlayerIds = new[] { "Kael" },
                EnemyIds = new[] { "Goblin", "Goblin" }, RecruitIds = new string[0], EnemyLevel = 1,
                PreJoins = new string[0], Unlocks = new[] { "Briar" },
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
                EnemyIds = new[] { "Bandit", "Bandit", "Goblin", "Wolf" }, RecruitIds = new string[0], EnemyLevel = 2,
                PreJoins = new string[0], Unlocks = new[] { "Sera" },
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
                EnemyIds = new[] { "Bandit", "Cultist", "Skeleton", "Skeleton" }, RecruitIds = new string[0], EnemyLevel = 3,
                PreJoins = new string[0], Unlocks = new[] { "Voss" },
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
            new ChapterConfig {
                Id = "ch3", Title = "Ch.3 — First Blood", Subtitle = "Ashfield Outskirts",
                MapVariant = 0, Tutorial = false,
                PlayerIds = new[] { "Kael", "Briar", "Sera" },
                EnemyIds = new[] { "Bandit", "Bandit", "RogueKnight", "Wolf" }, RecruitIds = new string[0], EnemyLevel = 3,
                PreJoins = new string[0], Unlocks = new string[0],
                Choice = new ChapterChoice {
                    Prompt = "Captured bandits beg for mercy. What is your justice?",
                    AText = "Spare them", BText = "Execute them",
                    AFaction = "Valenwood", APts = 2, BFaction = "Ironhold", BPts = 2,
                },
                Pre = new[] {
                    "Dawn: Hold, travelers! I am Dawn of the Dominion — those prisoners are mine by right of capture.",
                    "Kael: They torched a village. The Bastion must decide what justice means now.",
                    "Voss: Mercy gets you killed. Ask the trees — oh wait, you burned those too.",
                },
                Post = new[] {
                    "Dawn: It is done. The Bastion will hear of this — of all of this.",
                    "Briar: First real blood. It doesn't get easier, ash-man. Just... clearer.",
                },
            },
            new ChapterConfig {
                Id = "ch4", Title = "Ch.4 — Iron & Wood", Subtitle = "Valenwood Approach",
                MapVariant = 1, Tutorial = false,
                PlayerIds = new[] { "Kael", "Sera", "Dawn", "Lyra" },
                EnemyIds = new[] { "Hunter", "Hunter", "Shaman", "Wolf", "Wolf" }, RecruitIds = new string[0], EnemyLevel = 4,
                PreJoins = new[] { "Dawn" },
                Unlocks = new string[0],
                Choice = new ChapterChoice {
                    Prompt = "A Shadow Court mage offers her tome to your cause.",
                    AText = "Accept Lyra (Shadow +2)", BText = "Refuse (+300G, Ironhold +2)",
                    AFaction = "Shadow", APts = 2, BFaction = "Ironhold", BPts = 2,
                    AGold = 0, BGold = 300, AJoin = "Lyra", BJoin = "",
                },
                Pre = new[] {
                    "Dawn: The Concord will not treat while Ironhold dogs march our roads!",
                    "Briar: And the Dominion lectures while villages burn. Charming parley.",
                    "Lyra: ...How delightfully doomed you all are. Perhaps I can arrange otherwise.",
                    "Kael: A Shadow mage. Just what this war council needed.",
                },
                Post = new[] {
                    "Dawn: The bargain holds — for now. The Bastion stands united, if only barely.",
                    "Lyra: Oh, I do love a fragile alliance. So many ways to... mend it.",
                },
            },
            new ChapterConfig {
                Id = "ch5", Title = "Ch.5 — Betrayal", Subtitle = "Bastion Undercroft",
                MapVariant = 2, Tutorial = false,
                PlayerIds = new[] { "Kael", "Briar", "Sera", "Voss" },
                EnemyIds = new[] { "Bandit", "Korr", "Goblin", "Skeleton" },
                RecruitIds = new[] { "", "Korr", "", "" }, EnemyLevel = 5,
                PreJoins = new string[0], Unlocks = new string[0],
                Pre = new[] {
                    "Sera: The stores are poisoned. Someone inside the Bastion did this.",
                    "Marcus: Clever little healer. The Shadow Court pays better than oaths, girl.",
                    "Korr: ...I didn't sign for poison, Marcus. Steel, yes. This? No.",
                    "Kael: Then stand aside, spearman — or be talked down when you're bleeding.",
                    "SYSTEM: Weaken Korr below 30% HP, then use Talk!",
                },
                Post = new[] {
                    "Marcus: This isn't over, Ashwalker. The Court has deeper knives.",
                    "Briar: He ran. Coward. But the Bastion still stands — barely.",
                },
            },
            new ChapterConfig {
                Id = "ch6", Title = "Ch.6 — Underground", Subtitle = "Blight Tunnels",
                MapVariant = 0, Tutorial = false,
                PlayerIds = new[] { "Kael", "Renn", "Briar", "Sera" },
                EnemyIds = new[] { "Skeleton", "Skeleton", "Zombie", "Zombie", "Cultist" }, RecruitIds = new string[0], EnemyLevel = 5,
                PreJoins = new[] { "Renn" },
                Unlocks = new string[0],
                Pre = new[] {
                    "Renn: Psst! Down here, big folk! I know these tunnels like my own pockets.",
                    "Renn: ...Which is to say, full of things that bite. Stay close, yeah?",
                    "Sera: The Blight pools glow brighter below. Something feeds them.",
                    "Kael: Then we starve it. Blades out.",
                },
                Post = new[] {
                    "Renn: Told ya I'd earn my keep! ...Do I get paid? In food? Mostly food.",
                    "Kael: You get to live, thief. Spend it wisely.",
                },
            },
            new ChapterConfig {
                Id = "ch7", Title = "Ch.7 — Counter", Subtitle = "Ironhold Checkpoint",
                MapVariant = 2, Tutorial = false,
                PlayerIds = new[] { "Kael", "Briar", "Dawn", "Voss", "Zara" },
                EnemyIds = new[] { "RogueKnight", "Hunter", "Shaman", "Orc", "Bandit", "Skeleton" }, RecruitIds = new string[0], EnemyLevel = 6,
                PreJoins = new string[0],
                Unlocks = new string[0],
                Choice = new ChapterChoice {
                    Prompt = "A wandering Rune Knight offers her blade for the siege.",
                    AText = "Accept Zara (Alliance +2)", BText = "Decline (+400G)",
                    AFaction = "Alliance", APts = 2, BFaction = "Ironhold", BPts = 1,
                    AGold = 0, BGold = 400, AJoin = "Zara", BJoin = "",
                },
                Pre = new[] {
                    "Zara: I have walked every road of this war, and all of them end here.",
                    "Dawn: Then hold this gate with us, wanderer. The counter-attack begins at dawn.",
                    "Voss: For once I agree with the tin-can. Nock arrows!",
                },
                Post = new[] {
                    "Zara: The gate holds. ...Strange. For the first time in years, somewhere feels worth defending.",
                    "Kael: Rest. Act II looms — and the Blight Heart beats louder every night.",
                },
            },
        };

        public static int NodeCount => nodes.Count;
        public static ChapterConfig GetNode(int i) => nodes[i];
        public static ChapterConfig GetById(string id) => nodes.Find(n => n.Id == id);
    }
}
