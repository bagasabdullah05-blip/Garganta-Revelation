# Progress — Garganta Revelation

> File ini sumber kebenaran progres. Centang di GitHub web / editor lokal,
> ATAU lewat panel Unity **Garganta → Progress** (dock di samping Inspector,
> klik = file ini ikut berubah). Commit tiap ada yang dicentang.

## P0 — Prototype (code) ✅

- [x] Grid/CTB/combat + 99 tests hijau
- [x] 6+6+4 jobs, ~70 skill, 74 equipment, shop, crafting
- [x] 15 chars, 12 chapters, bonds, recruit, corruption, reputation
- [x] 5 endings, NG+, skirmish, superboss, difficulty, save 3 slot
- [x] Musik/SFX temp (8 loop + 14 SFX, auto-play)
- [x] Automated playtests (sim + PlayMode auto-play Victory)

## P1 — Visual Foundation (Title → Ch.2b)

### Tiles (WIRED ✅ — sudah tampil di game)

- [x] `dia_59724C.png` plains (+`_alt` variasi checker)
- [x] `dia_3A5E3A.png` forest (tree terintegrasi)
- [x] `dia_6B6B72.png` mountain (peak terintegrasi)
- [x] `water0.png` + `water1.png` animasi
- [x] `blight0.png` + `blight1.png` animasi
- [x] `ruin.png` + `bridge.png` + `wall.png`
- [x] Engine pindah layout 2:1 (row 0.5) + sorting depth

### Heroes (sprite 32x48 + 6 klip + 4 portrait — spec: prompts/phase1/characters.md)

- [ ] Kael: sprite+idle, walk(4f)
- [ ] Kael: attack(4f), skill(4f)
- [ ] Kael: hit(2f), death(4f)
- [ ] Kael portraits: normal, battle, sad, happy
- [ ] Briar: sprite+idle, walk(4f)
- [ ] Briar: attack(4f), skill(4f)
- [ ] Briar: hit(2f), death(4f)
- [ ] Briar portraits: normal, battle, sad, happy
- [ ] Sera: sprite+idle, walk(4f)
- [ ] Sera: attack(2f), skill(4f)
- [ ] Sera: hit(2f), death(4f)
- [ ] Sera portraits: normal, battle, sad, happy
- [ ] Voss: sprite+idle, walk(4f)
- [ ] Voss: attack(4f), skill(4f)
- [ ] Voss: hit(2f), death(4f)
- [ ] Voss portraits: normal, battle, sad, happy

### Enemies Ch.1–Ch.2b (spec: prompts/phase1/enemies.md)

- [ ] Bandit: sprite+idle, walk(4f)
- [ ] Bandit: attack(4f), hit(2f), death(4f)
- [ ] Goblin: sprite+idle, walk(4f)
- [ ] Goblin: attack(4f), hit(2f), death(4f)
- [ ] Cultist: sprite+idle, walk(4f)
- [ ] Cultist: attack(4f), skill(4f), hit(2f), death(4f)

### Backgrounds

- [ ] `bg_title.png` 960x540
- [ ] `bg_map.png` 1024x1024
- [ ] `bg_base.png` 960x540

### Icons 32x32 (`Assets/Resources/Art/Icons/`)

- [ ] Skill (11): Attack Talk PowerStrike ShieldBash WarCry Pierce AimedShot QuickShot HealingLight Smite Cure
- [ ] Consumable (8): potion hi_potion ether antidote phoenix_down bomb elixir tent
- [ ] Gear (8): iron_sword hand_axe wooden_staff short_bow rusty_sword leather_armor iron_helm power_band

### Wiring code P1 (saya)

- [ ] `icon_<id>` tampil di tombol Skill/Item/Equip
- [ ] `portrait_<Nama>` tampil di DialogueUI
- [ ] `bg_title/bg_map/bg_base` tampil di Title/Map/Base
- [ ] Play Ch.1→Ch.2b: nol placeholder di layar

Prompt per aset: `docs/prompts/phase1/`. Spec: `docs/ART_TASKS.md`.
Checklist detail 15 chars: `docs/ASSET_CHECKLIST.md`.

## P2 — Feel & Balance

- [ ] Animasi code (idle bob, lunge, hit flash)
- [ ] Sisa wiring SFX (bow/ui/gold/levelup/talk/stun/death/revive/blight)
- [ ] Tuning XP/job-curve/boss dari playtest manusia
- [ ] Bugfix playthrough Ch.1→ending

## P3 — Content Art (Ch.3–Ch.11)

- [ ] Heroes wave 2-3 (11 sprite + portrait)
- [ ] Enemies set 2 + 3 boss
- [ ] Prompt pack `docs/prompts/phase2/`

## P4 — Launch

- [ ] Musik final (timpa ogg) + SFX final
- [ ] Mobile profile + lokalisasi EN/ID + store page
