# Setup & Play — Garganta Revelation M3 (Unity 6000.6.0f1, 2D URP-ready)

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
1. File → New Scene (Basic Empty).
2. Menu **Garganta → Setup TestBattle Scene** (menambah Battle + UI + Flow).
3. File → Save As → `Assets/Scenes/TestBattle.unity`.
4. Press Play.

## Alur main (M3)
- **Title**: New Game / Continue / Load slot 1-3.
- **World Map**: node Ch.1 → Ch.2a → Ch.2b terbuka berurutan; klik node → dialogue
  intro (Space/klik = lanjut, Skip >> tersedia) → battle.
- **Battle**: sama seperti M2 + hint tutorial di Ch.1. Menang → XP/loot + dialogue
  penutup + rekrut baru + auto-save → kembali ke Map. Kalah → ulang tanpa penalti.
- **Bastion (Base)**: Party (lihat stats), Equip (ganti gear milik), Shop
  (beli potion/gear pakai gold), Save (slot 1-3).

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

## Tests
Window → General → Test Runner → EditMode → Run All.
Harus hijau 47: Pathfinder(3), CTB(3), Damage(4), Triangle(6), Leveling(5),
Skill(6), Equipment(6), Save(3), Chapter(5), Shop(5) + 1 stub Addressables.
Bisa juga headless:
`Unity.exe -batchmode -quit -projectPath "K:\unity\Garganta-Revelation" -runTests -testPlatform EditMode -testResults results.xml`
