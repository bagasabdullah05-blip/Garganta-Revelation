# Phase 1 — Enemies: full set (3 file sprite + strip anim)

Aturan strip sama dengan heroes (`anim_<Id>_<clip>.png`, frame 32x48,
seed sama/img2img dari idle). File sprite dishare (lihat tabel).
Target: `Assets/Resources/Art/` (strip di `Anims/`).

> Wolf & Skeleton sementara share file (lihat tabel). File khusus
> `anim_Wolf_*` 48x32 + mapping code = task P3.

## 1. Bandit + Skeleton — base `char_Sword_8B2500.png`, strip `anim_Bandit_*`

Base: desperate hooded bandit, mismatched dented armor brown grey, rusty
short sword, red sash trim, menacing idle.

| Klip | Frame | Prompt delta |
|---|---|---|
| `idle` | 2 | shifting weight, blade twitch |
| `walk` | 4 | skulking advance, hunched |
| `attack` | 4 | wild slash combo |
| `hit` | 2 | knocked back, hood slips |
| `death` | 4 | collapse, sword clatter |
(`skill` strip opsional — fallback attack.)

## 2. Goblin (+Wolf/Skeleton temp) — base `char_Dagger_8B2500.png`, strip `anim_Goblin_*`

Base: small green goblin, big ears, hunched, sharp teeth, crude knife,
brown rags, red war-paint.

| Klip | Frame | Prompt delta |
|---|---|---|
| `idle` | 2 | bouncing, ear twitch |
| `walk` | 4 | scampering waddle |
| `attack` | 4 | leaping stab |
| `hit` | 2 | squashed flatten |
| `death` | 4 | deflate + poof |

## 3. Cultist — base `char_Tome_8B2500.png`, strip `anim_Cultist_*`

Base: hooded cultist, dark purple robe, spider sigil, chained floating
grimoire, purple glow eyes.

| Klip | Frame | Prompt delta |
|---|---|---|
| `idle` | 2 | chanting sway, tome orbit |
| `walk` | 4 | gliding steps, robes drag |
| `attack` | 4 | dark bolt cast, hand glow |
| `skill` | 4 | both hands up, void tendrils |
| `hit` | 2 | recoil, hood shadow flicker |
| `death` | 4 | crumple to ash, tome drops |
