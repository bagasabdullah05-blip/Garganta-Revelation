# Setup & Play — Garganta Revelation M6 (Unity 6000.6.0f1, 2D URP-ready)

Packages (Unity 6): `feature.2d 2.0.1`, `2d.tilemap.extras 4.0.2`,
`render-pipelines.universal 17.2.1`, `inputsystem 1.20.0`, `cinemachine 3.1.2`,
`textmeshpro 3.0.9`, `addressables 2.11.2`, `test-framework 1.4.5`.
Visual 2D prosedural (diamond tiles, sprite karakter, Y-sort); URP 2D Renderer
asset + HD-2D art final menyusul di M5.

## Buka proyek
1. Unity Hub → Add project from disk → `K:\unity\Garganta-Revelation`
2. Tunggu import + resolve packages (butuh internet sekali).
3. Jika diminta Safe Mode / API update → Continue.

## Buat scene TestBattle
Scene resmi sudah di repo (`Assets/Scenes/TestBattle.unity`) — langsung buka + Play.
Regenerasi bila perlu: New Scene → menu **Garganta → Setup TestBattle Scene**
(menambah Battle + UI + Flow) → Save As ke path yang sama.

## Alur main (M3)
- **Title**: New Game / Continue / Load slot 1-3.
- **World Map**: node Ch.1 → Ch.2a → Ch.2b terbuka berurutan; klik node → dialogue
  intro (Space/klik = lanjut, Skip >> tersedia) → battle.
- **Battle**: sama seperti M2 + hint tutorial di Ch.1. Menang → XP/loot + dialogue
  penutup + rekrut baru + auto-save → kembali ke Map. Kalah → ulang tanpa penalti.
- **Bastion (Base)**: Party (lihat stats), Equip (ganti gear milik), Shop
  (beli potion/gear/material), Workshop (craft 8 resep: Steel Sword, Flametongue,
  Partisan, Dragon Mail, Materia Staff, Hunter Bow, Blight Edge, Elixir),
  Save (slot 1-3). Material (iron ore, herbs, leather-hide, crystal, essence, scale,
  ichor) drop dari musuh sesuai class-nya.

## Kontrol Play (M2)
- Klik unit biru (punyamu, yang giliran `*` di bar atas) → highlight biru = move range.
- Klik tile highlight → unit jalan (A* hex-offset, cost Plains1/Forest2/Mountain3).
- Action menu kanan: **Attack** (highlight merah) → klik musuh; **Skill** → pilih skill
  (MP, unlock per level: Squire PowerStrike L1 / ShieldBash-stun L3 / WarCry-buff L5) →
  klik target (kuning = range, merah = AoE); **Item** → Potion/Ether/dll, klik kawan
  (Bomb & Phoenix Down langsung jalan); **Wait**.
- Sera bisa heal (Healing Light/Cure, klik kawan hijau). Musuh Cultist nge-heal,
  Wolf hit-and-run, Skeleton bertahan, unit sekarat non-agresif kabur.
- CTB: SPD tinggi jalan lebih sering. High ground +10% dmg/acc. Stun = giliran lewat.
- Menang → layar hasil: XP + level-up + gold + loot. Kalah = party wipe.

## Sistem M4 (Ch.3–Ch.7)
- **Tier2 jobs**: Paladin/Dragoon/Assassin/BlackMage/WhiteMage/RuneKnight via tab
  **Jobs** di Bastion — syarat job level ala FFT (mis. Paladin: Squire J10 + Acolyte J5);
  job level +1 per battle. Reclass simpan skill lama + gear.
- **Talk recruit**: skill Talk (range 1) ke musuh bertanda HP <30% (mis. Korr di Ch.5)
  → gabung dan terbawa ke roster.
- **Corruption**: tile Blight +10/turn; tier 25/50/75 = ATK+/DEF-/giliran hilang;
  100% = mati dimakan Blight. Cure/Aether Drop/Chapel membersihkan.
- **Choices & reputation**: Ch.3/4/7 ada pilihan A/B (pengaruh faksi + rekrutmen);
  standing tampil di World Map, dipakai 5 endings (M6).
- **Support bonds**: bertarung bersama/berdekatan menaikkan bond (C/B/A/S) →
  aura HP/ATK + bonus XP.
- **Classic permadeath**: gugur (kecuali Kael) = keluar roster.

## Sistem M5 (Ch.8–Ch.11 + endgame)
- **Tier3 master jobs**: HolyKnight/Spellblade/Shadowblade/Seraph (syarat job J12+J12)
  + 12 master skill (Judgment, ElementalSurge AoE, VoidStep, Grand Benediction...).
- **Ch.8 All-Out War** (6v6 + wave bala bantuan, Asha & Vael gabung) → **Ch.9 Blight
  Heart** (+Thorne, pilihan Nyx, boss BlightHeart 2 wave) → **Ch.10 Unity** (+Eos,
  rekrut Grim via Talk) → **Ch.11 Final Stand** (boss Usurper lv12 + wave).
- **5 endings**: True (15 rekrut) / Blight (corruption Kael) / Shadow / Sacrifice /
  Light (faksi tertinggi); layar ending + stats.
- **Skirmish** (grinding repeatable, scaling level party), **Primeval Lair**
  (superboss post-ending), **NG+** (roster kept, musuh +2).
- Roster final 15/15 sesuai PRD.

## Produksi M6 (art & audio final — tanpa ubah code)
- Musik: 18 prompt siap-copy di `docs/AUDIO_BRIEF.md` (Suno/Udio) → taruh ogg di
  `Assets/Resources/Audio/Music/<id>.ogg` → otomatis bunyi (title/map/base/
  battle/boss/victory/defeat/ending).
- SFX: daftar + keyword Freesound + resep Bfxr di `docs/AUDIO_BRIEF.md` → taruh wav
  di `Assets/Resources/Audio/SFX/<id>.wav` (`hit skill_magic miss heal` sudah bunyi).
- Pixel art: palet `docs/PALETTE.gpl` + checklist + nama file exact di
  `docs/ART_TASKS.md` → taruh PNG di `Assets/Resources/Art/` → otomatis ganti
  placeholder. Prompt karakter sudah ada di `docs/ART_BRIEF.md`.

## Tests
Window → General → Test Runner → EditMode → Run All.
Harus hijau 93: Pathfinder(3), CTB(3), Damage(4), Triangle(6), Leveling(5),
Skill(6), Equipment(6), Save(3), Chapter(5), Shop(5), Job(5), Bond(4),
Corruption(3), Recruit(2), Rep(1), Tier3(4), Ending(5), Skirmish(3), Wave(3),
ArtAudio(5), Difficulty(5), AudioAssets(2), Crafting(4) + 1 stub Addressables.
Bisa juga headless:
`Unity.exe -batchmode -quit -projectPath "K:\unity\Garganta-Revelation" -runTests -testPlatform EditMode -testResults results.xml`
