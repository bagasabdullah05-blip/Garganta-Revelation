# Feedback Playtest — catat 13 Sep 2026 (lanjut besok)

Sumber: user, setelah buka game. Target: **1080p**.

## Keluhan

1. Resolusi & ukuran menu tidak pas — menu terlalu kecil.
2. Font tidak cocok + pilihan warna aneh.
3. Menu battle kecil sekali — perbesar.
4. Optimasi untuk 1080p gameplay.

## Sudah dikerjakan kilat (<5 mnt, push ini)

- Font menu 16→22, body 14→18, speaker 15→20, judul 26→44.
- Warna tombol: teks Bone (terbaca), hover gold (sebelumnya gold-on-grey aneh).
- Panel ActionMenu 162px→254px, submenu 242px→344px, info unit 320px→460px + font tema.
- Test UITheme ikut update (22/18/20).

## Besok (belum — butuh >5 mnt / keputusan user)

- [ ] Reskin panel penuh: style box custom gelap-transparan (ganti `"box"` default abu-abu) di semua UI.
- [ ] Pass warna global: definisikan palet UI final (teks, aksen, HP/MP bar) — user nilai "aneh", ajukan 2 opsi.
- [ ] Font: user bilang "tidak cocok" — siapkan alternatif (Spectral→ganti? Alegreya Sans? test render) + vote.
- [ ] Layout 1080p: audit semua Rect (Title/Map/Base/Battle/Dialogue/tutorial hint) di 1920x1080, pastikan tidak overlap/overflow; instruksi set Game View 1920x1080.
- [ ] Skala otomatis: `GUI.matrix` scale factor mengikuti tinggi layar (720p vs 1080p vs 4K).
- [ ] Battle HUD: turn order bar + action menu + info unit diperbesar + ikon.
- [ ] (Opsional) ganti IMGUI → uGUI CanvasScaler: kerja besar, putuskan setelah reskin.
