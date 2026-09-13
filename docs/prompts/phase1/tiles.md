# Phase 1 — Tiles & Decor (14 file)

Diamond **64x48**, decor ikut spec ART_TASKS §3. Isometric diamond 2:1-ish,
bevel terang atas + outline gelap (contoh visual: placeholder game sekarang).
Target: `Assets/Resources/Art/<nama>.png`.

> REKOMENDASI CEPAT: tiles dari Kenney.nl isometric pack + recolor ke palet
> (30 menit) hasilnya lebih konsisten dari AI. AI hanya bila mau custom total.

## Diamond AI prompt (pola, ganti MOTIF + PALET per tile)

```
isometric diamond game tile, top-down 2:1, MOTIF, beveled edge light top
dark outline, seamless edges with same tiles, <STYLE>
```

| Target | MOTIF |
|---|---|
| `dia_59724D`.png | dull green worn grass, dirt patches, tiny flowers → **cek nama exact di bawah** |
| `dia_3B5E3B`.png | dense mossy forest floor, dark soil |
| `dia_6B6B72`.png | grey rock slabs, snow dust top |
| `bridge.png` | wooden planks over stone, side rails |
| `ruin.png` | cracked stone slabs, overgrown debris |
| `water0.png` / `water1.png` | deep blue water, frame1 ripple bergeser 4px |
| `blight0.png` / `blight1.png` | sickly teal corrupted ground, glowing veins; frame1 vein lebih tebal |
| `tree.png` 48x64 | ancient tall tree, dense canopy, moss trunk |
| `rock.png` 40x32 | grey boulder, teal moss crack |
| `wall.png` 64x64 | dark stone block wall, brick lines, mossy top |
| `tuft.png` 24x16 | small grass tuft cluster |
| `shadow.png` 32x12 | soft black ellipse 35% |

**Nama diamond plains/forest/mountain** (dari `Balance.TileColor`, truncate byte):
plains `dia_59724C`, forest `dia_3A5E3A`, mountain `dia_6B6B72`.
Bila file tidak keganti saat Play → nama salah 1 digit: cek cepat dengan
sementara rename varian terdekat (engine fallback diam-diam = nama tidak match).
