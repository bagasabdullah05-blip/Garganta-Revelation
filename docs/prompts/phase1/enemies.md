# Phase 1 — Enemies Ch.1–Ch.2b (3 file sprite, dipakai 5 musuh)

Canvas **32x48** (Wolf 48x32, wide). Detail di `docs/ART_BRIEF.md §2`.
Trim musuh SELALU blood-red accent. Target: `Assets/Resources/Art/<nama>.png`.

> Wolf & Skeleton & Bandit & Goblin: hanya 3 file karena Wolf/Goblin share
> `char_Dagger_8B2500`, Bandit/Skeleton share `char_Sword_8B2500`.

## 1. Bandit + Skeleton → `char_Sword_8B2500.png`

```
2d pixel art game sprite, desperate hooded bandit, mismatched dented armor
brown grey, rusty short sword, red sash trim, full body front-facing
menacing idle stance,
<STYLE>
```

## 2. Goblin + Wolf → `char_Dagger_8B2500.png`

```
2d pixel art game sprite, small green goblin, big ears, hunched, sharp teeth,
crude knife, brown rags, red war-paint trim, full body front-facing
feral idle stance,
<STYLE>
```
CATATAN: Wolf placeholder sementara pakai sprite ini sampai P3 (butuh
`char_Wolf_8B2500.png` 48x32 + code mapping — task P3).

## 3. Cultist → `char_Tome_8B2500.png`

```
2d pixel art game sprite, hooded cultist, dark purple robe black trim,
spider sigil on chest, chained floating grimoire beside hand, glowing
purple eyes in shadow, red trim, full body front-facing chanting pose,
<STYLE>
```
