# Art Tasks — Garganta Revelation (M6 Production)

Procedural placeholders ship the game TODAY; this file turns them into final
HD-2D pixel art without touching code. Import `docs/PALETTE.gpl` into
Aseprite/GIMP/Piskel first (palettes locked to `docs/ART_BRIEF.md`).

## 1. Drop-in pipeline (no code changes)

1. Draw/export PNG (see specs §3).
2. Save under `Assets/Resources/Art/` with the EXACT file name from §4.
3. Play. `ArtOverride` loads every `Resources/Art/**` sprite at battle start;
   matching names replace placeholders 1:1 (position/scale/sorting unchanged).

Verify: `ArtOverride.Count` in a debug log, or watch tiles change on Play.

## 2. Priority order (do top rows first — visible every battle)

| Pri | Asset | Count | Source suggestion |
|---|---|---|---|
| P0 | Hero units (Kael/Briar/Sera/Voss, 6-dir sheets) | 4 | commission $50–200/sheet OR PixelLab.ai + Aseprite cleanup |
| P0 | Ground diamonds (plains/forest/mountain/water/ruin/blight/bridge) | 7 | Kenney.nl isometric + recolor to palette |
| P1 | Enemy set 1 (Bandit/Goblin/Wolf/Skeleton/Orc/Imp/Cultist) | 7 | OpenGameArt + recolor |
| P1 | Decor (tree/rock/wall/tuft) | 4 | Kenney + recolor |
| P2 | Roster wave 2 (Dawn/Lyra/Korr/Renn/Zara) | 5 | same as P0 |
| P2 | Enemy set 2 (RogueKnight/Hunter/Shaman/Zombie/BlightWalker) | 5 | same as P1 |
| P3 | Bosses (BlightHeart/Usurper/Primeval) | 3 | commission $100–400 |
| P3 | Roster wave 3 (Asha/Vael/Thorne/Nyx/Eos/Grim) | 6 | same as P0 |
| P4 | Portraits (dialogue faces, 128px) | 15 | commission $30–80/portrait |
| P4 | Title key art + world-map parchment | 2 | commission $100–500 / Midjourney concept |

Character prompts: `docs/ART_BRIEF.md §1` (copy-paste AI prompts per hero).
Enemy/map prompts: `§2–§3`. Keep ONE artist/AI seed for style consistency.

## 3. Export specs (must match or rescale in import settings)

| Asset | Canvas | PPU | Pivot | Filter | Notes |
|---|---|---|---|---|---|
| Ground diamond | 64x48 | 64 | center | Point | 2:1-ish bevel, outline darker |
| Bridge/Ruin/Water/Blight diamond | 64x48 | 64 | center | Point | water/blight need 2 frames (`...0`/`...1`) |
| Tree | 48x64 | 48 | center | Point | anchor sits on tile |
| Rock | 40x32 | 40 | center | Point | – |
| Wall block | 64x64 | 64 | center | Point | stands 1 tile tall |
| Tuft | 24x16 | 32 | center | Point | transparent bg |
| Shadow | 32x12 | 32 | center | Point | black 35% ellipse |
| Unit | 32x48 | 32 | center | Point | feet at bottom; weapon right |
| Portrait | 128x128 | – | – | Bilinear ok | UI only (M6 UI task) |

Unity import: Texture Type Sprite (2D), Mesh Single, Compression None,
Filter Point (except portraits), Max Size 2048. Sprite Mode Single.

## 4. Override file names (exact — case-sensitive)

Ground: `dia_<HTML>` per tile color, e.g. `dia_59774D`; special:
`bridge ruin water0 water1 blight0 blight1`.
Decor: `tree rock wall tuft shadow`.
Units: `char_<Weapon>_<TrimHTML>`, e.g. `char_Sword_4A6B8A` (player),
`char_Axe_8B2500` (enemy). Weapons: Sword Axe Spear Bow Staff Tome Dagger.
Trims: player `4A6B8A`, enemy `8B2500`.

Examples: redraw ONE file `Assets/Resources/Art/char_Sword_4A6B8A.png`
(Kael) and he changes in-game next Play; everything else stays placeholder.
Mix-and-match freely — missing names fall back silently.

## 5. Acceptance checklist per asset

- [ ] Palette-only colors (no off-palette strays — check with palette lock)
- [ ] Transparent background, no halo/fringe
- [ ] Reads at 1x in-game size (squint test at 100% zoom)
- [ ] Team trim color present on units (blue/red pixels visible)
- [ ] Water/blight frames differ ONLY in ripple/vein pixels (no popping)
- [ ] PNG < 200KB each (no giant sheets in repo)

## 6. Cost control (from PRD §13)

$0 route: Kenney/OpenGameArt recolor + Bfxr + Suno free = $0.
Hybrid: AI base + 1 commissioned hero sheet as style anchor ≈ $100–300.
Full commission ≈ $5,750 (breakdown in ART_BRIEF §6).
