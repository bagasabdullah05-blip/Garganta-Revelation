# Phase 1 Prompt Pack — STYLE (tempel di SEMUA prompt)

## Style suffix (copy-paste ke akhir setiap prompt)

```
2d pixel art, HD-2D style, desaturated dark fantasy palette, gritty war-torn mood,
dramatic contrast, clean silhouette readable at 1x size, solid flat colors, no gradients,
no photorealistic details, no blur, no watermark, transparent background
```

Palette lock (pakai hex ini, lihat `docs/PALETTE.gpl`):
`#4A6B8A ash blue, #8B2500 blood red, #E8DCC8 bone, #3D1F5C shadow purple,
#3A5F3A forest green, #6B7B8D steel, #C5A345 gold, #2A7A6E blight teal,
#6B4226 brown, #1A1A2E deep black, glow #7FFFD4 / #FF6B35 / #87CEEB / #FFD700`

## Per-tool settings

**Midjourney:** tambah `--ar 1:1 --style raw --stylize 250 --v 6 --no blur, watermark, text, photorealistic`.
Untuk konsistensi antar aset: generate Kael dulu → pakai `--sref <url-kael>` di semua prompt berikutnya.
**Stable Diffusion (A1112/Forge):** checkpoint pixel-art (mis. `pixel-art-xl`), sampler DPM++ 2M Karras 25 steps,
CFG 6, size sesuai tabel (32/48/64 kelipatan!), Negative: `blurry, photorealistic, watermark, text, gradient, deformed, extra limbs`.
**Leonardo.ai:** Pixel Art pipeline, Alchemy ON leves, Transparency ON (PNG).
**PixelLab.ai:** Sprite mode + upscale 4x, lalu cleanup manual di Aseprite.

## Aturan global (jangan dilanggar)

1. SATU aset = SATU file PNG transparan. Jangan sprite-sheet (engine baca single).
2. Ukuran canvas EXACT ikut tabel tiap file (engine PPU fix, salah ukuran = blur).
3. Water/blight SELALU 2 frame, bedakan hanya piksel ripple/vein (anti popping).
4. Nama file EXACT case-sensitive ikut daftar (pipeline `ArtOverride` match nama).
5. Maks 200KB/file. Kompres PNG (TinyPNG) sebelum commit.
6. Portrait & background BOLEH bilinear/lebih painterly — sisanya Point/pixel crisp.
