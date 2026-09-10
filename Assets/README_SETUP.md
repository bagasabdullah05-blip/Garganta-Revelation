# Setup & Play — Garganta Revelation M1 (Unity 6000.6.0f1, 2D URP-ready)

Packages (Unity 6): `feature.2d 2.0.1`, `2d.tilemap.extras 4.0.2`,
`render-pipelines.universal 17.2.1`, `inputsystem 1.11.2`, `cinemachine 3.1.2`,
`textmeshpro 3.0.9`, `addressables 2.2.2`, `test-framework 1.4.5`.
M1 logic is RP-agnostic (placeholder squares); URP 2D Renderer asset assignment
menyusul di M5 bersama HD-2D art.

## Buka proyek
1. Unity Hub → Add project from disk → `K:\unity\Garganta-Revelation`
2. Tunggu import + resolve packages (butuh internet sekali).
3. Jika diminta Safe Mode / API update → Continue.

## Buat scene TestBattle
1. File → New Scene (Basic Empty).
2. Menu **Garganta → Setup TestBattle Scene**.
3. File → Save As → `Assets/Scenes/TestBattle.unity`.
4. Press Play.

## Kontrol Play (M1)
- Klik unit biru (punyamu, yang giliran `*` di bar atas) → highlight biru = move range.
- Klik tile highlight → unit jalan (A* hex-offset, cost Plains1/Forest2/Mountain3).
- Action menu kanan bawah: Attack (highlight merah = range) → klik musuh merah, atau Wait.
- CTB: SPD tinggi jalan lebih sering. High ground (mountain) +10% dmg/acc.
- Menang = semua musuh mati. Kalah = party wipe.

## Tests
Window → General → Test Runner → EditMode → Run All.
Harus hijau: PathfinderTests (3), CTBTests (3), DamageFormulaTests (4), WeaponTriangleTests (6).
Bisa juga headless:
`Unity.exe -batchmode -quit -projectPath "K:\unity\Garganta-Revelation" -runTests -testPlatform EditMode -testResults results.xml`
