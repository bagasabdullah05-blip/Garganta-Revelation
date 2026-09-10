# Content Plan — Garganta Revelation

## 1. Karakter 15

| # | Name | Class | Faction | Recruit |
|---|---|---|---|---|
| 1 | Kael | Squire | Garganta | Ch1 auto |
| 2 | Briar | Warrior | Ironhold | Ch2 auto |
| 3 | Sera | Acolyte | Dominion | Ch3 auto |
| 4 | Voss | Archer | Valenwood | Ch4 sidequest + optional enemy |
| 5 | Dawn | Paladin | Dominion | Ch5 auto |
| 6 | Lyra | Mage | Shadow | Ch5 faction choice |
| 7 | Korr | Spearman | Ironhold | Ch6 battle recruit HP<30%+Talk |
| 8 | Renn | Thief | Garganta | Ch7 auto |
| 9 | Zara | Rune Knight | Neutral | Ch8 sidequest + knight faction choice |
| 10 | Asha | Ranger | Valenwood | Ch9 hidden no casualty |
| 11 | Vael | Dragoon | Garganta | Ch9 hidden item |
| 12 | Thorne | Black Mage | Shadow | Ch10 auto anti-hero |
| 13 | Nyx | Assassin | Shadow | Ch10 faction choice |
| 14 | Eos | White Mage | Dominion | Ch11 auto |
| 15 | Grim | Berserker | Ironhold | Ch11 battle recruit |

Recruitment checklist di PRD section C.

## 2. Enemies 30+

Common 12: Bandit, Goblin, Wolf, Skeleton, Zombie, Slime, Bat, Spider, Cultist, Rogue Knight, Imp, Orc.
Elite 10: Dark Knight, Dragon, Wyvern, Archmage, Lich, Golem, Chimera, Banshee, Shadow Beast, Blight Walker.
Boss 6+: tiap chapter 1 major multi-phase. Detail di ART_BRIEF.

## 3. Maps 30+

12 story, 8 sidequest, 6 random grinding, 4 arena, 1 secret dungeon post-game.
Detail 6 map utama di ART_BRIEF section 3.

## 4. Chapters Detail

Act I Ch1-4 ~10 jam:
Ch1 Ashes village hancur Kael tutorial 1 battle.
Ch2 The Road Briar travel Bastion 2 battles.
Ch3 Bastion join resistance base buka Sera 1+recruit.
Ch4 First Blood moral choice Voss optional 2+choice.

Act II Ch5-8 ~15 jam:
Ch5 Iron & Wood alliance Valenwood Dawn Lyra choice 3 battles.
Ch6 Betrayal mole major death (save jika S-rank) Korr 2 battles.
Ch7 Underground resistance Blight depths Renn 3 battles.
Ch8 Counter siege Zara knight choice 3 battles.

Act III Ch9-12 ~15 jam:
Ch9 All-Out War 3-front Asha Vael hidden 4 battles.
Ch10 Blight Heart corruption peak Thorne Nyx 3 battles.
Ch11 Unity factions unite Eos Grim 2 battles.
Ch12 Final Stand multi-phase 3 endings 1 epic.

Epilogue: character endings bond, faction ending reputation,
post-game super bosses secret dungeon NG+.

Endings 5: Light purified unite, Shadow court wins protagonist rules,
Sacrifice seal Blight, Blight lord bad, True 100% recruit + all S-rank.

## 5. Side Quests 15-20 contoh

- Save village -> Voss join
- Dragoon trial -> Dragoon Lance
- Lost caravan -> crafting recipe
- Haunted chapel -> Eos bond up
- Arena rank 1-5 -> gold + epic gear
- Blight cure research -> cure permanen
- Shadow informant -> Nyx recruit shortcut
- Dll.

## 6. Base: The Bastion

Barracks class change, Library job unlock research lore,
Workshop craft upgrade, Garden passive items,
Tavern recruit merc, Chapel heal revive cleanse,
World Map choose next.

Upgrade paths bertahap buka fitur Tier2/3.

## 7. Milestones & File Manifest Prototype

M1 Prototype minggu1-3: grid pathfinding, CTB, attack only,
4 player 6 enemy, triangle, basic AI, minimal UI, 1 map 12x12.
M2 Core bulan1-2: job 6 base, skill, equip basic, AI full,
turn UI, info UI, action menu, victory defeat.
M3 World bulan3-4: worldmap, dialogue, cutscene, base basic,
Ch1-2, 5 chars, save load, tutorial.
M4 Content bulan5-8: all 12 ch, 15 chars, advanced jobs,
support, recruit, reputation, corruption, all enemies.
M5 Production bulan9-11: pixel art, music SFX, mobile opt,
localization EN ID JP, NG+, post-game, balancing.
M6 Launch bulan12: QA bugfix perf store pages marketing.

30 scripts prototype ~2980 baris:
Constants EventBus GameManager GridManager HexTile Pathfinder
GridVisualizer Unit UnitStats UnitMovement UnitFactory
TurnManager CombatManager WeaponTriangle CombatUI
AIController AITactics AIBehavior UIManager TurnOrderUI
UnitInfoUI ActionMenuUI HealthBarUI GameOverUI
IsometricCamera CameraShake UnitDatabase SkillDatabase MapDatabase
+ README_SETUP.
