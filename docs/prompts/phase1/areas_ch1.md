# Phase 1 — Ch.1 Ashes: area, props, skill, menu (SUPERSET chapter ini)

Fokus: semua yang terlihat dari Title → Ch.1 selesai. Selain file ini,
Ch.1 pakai: characters (Kael), enemies (Goblin), tiles (ter-wire),
icons (Attack/Talk/PowerStrike + potion/ether), backgrounds (title/map).

## Props Ashfield 64x64 (kecuali disebut), pivot center-bawah untuk rumah/pohon

| Target | Isi | Ditaruh |
|---|---|---|
| `deco_house.png` 64x96 | burnt house: collapsed timber + stone chimney + ember glow | tile (3,2) |
| `deco_debris0.png` 48x32 | broken cart wheel + planks | ruins, `(x*3+y*5)%3==0` |
| `deco_debris1.png` 48x32 | rubble stones + ash pile | ruins, `%3==1` |
| `deco_debris2.png` 48x32 | fallen beam + cloth scrap | ruins, `%3==2` |
| `deco_deadtree.png` 48x64 | leafless black tree, crow optional | plains, `(x*7+y*13)%11==0` |

Prompt pola: `burnt war-torn village prop, MOTIF, dark fantasy, muted palette,
isometric game asset, transparent background, <STYLE>`.
File hilang = fallback diam (tile polos / tuft) — game tidak rusak.

## Skill Ch.1 (Kael Lv2: Attack, Talk, PowerStrike)

- Ikon: `icon_Attack.png`, `icon_Talk.png`, `icon_PowerStrike.png` (lihat icons.md).
- Efek: tanpa art — engine kasih hit-flash + damage number + shake (sudah ada).
  Proyektil sihir (Fireball dkk) = Ch.2+, spec menyusul.

## Menu & kota/hub yang tersentuh alur Ch.1

- Title → `bg_title.png` (layar pertama!).
- World Map (muncul usai New Game) → `bg_map.png`.
- Base/Bastion (belum dikunjungi di Ch.1, tapi siapkan) → `bg_base.png`.
- Dialogue Ch.1: Kael + Briar + `???"` → `portrait_Kael.png`, `portrait_Briar.png`
  (Goblin tidak bicara — fallback kotak huruf).
- ActionMenu/UnitInfo: thumbnail `portrait_<RosterId>` (Kael), fallback kotak huruf.
- Ikon tombol: Attack/Skill/Item/Wait + daftar skill + daftar item.
