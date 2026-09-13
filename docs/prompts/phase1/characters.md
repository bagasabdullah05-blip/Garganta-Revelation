# Phase 1 — Heroes (4 sprite + 4 portrait)

Canvas sprite **32x48** (kaki di bawah, senjata kanan). Portrait **128x128**
(bust, Bilinear OK). Detail full di `docs/ART_BRIEF.md §1`.
Target: `Assets/Resources/Art/<nama>.png`.

## 1. Kael → `char_Sword_4A6B8A.png`

```
2d pixel art game sprite, male squire age 22, short messy ash-grey hair,
pale blue tired eyes, burn scar on neck, worn dark brown leather chestpiece
over chainmail collar, tattered dark grey cloak torn left shoulder, dented
iron bracers, muddy boots, holding chipped iron short sword down-right,
steel-blue shoulder trim, full body front-facing idle battle stance,
<STYLE>
```

## 2. Briar → `char_Axe_4A6B8A.png`

```
2d pixel art game sprite, fierce female warrior age 28, long braided auburn
hair, amber eyes, scar right cheek, dark grey steel armor red accents,
wolf fur collar, heavy pauldrons, war axe resting on shoulder, steel-blue
shoulder trim, full body front-facing idle battle stance,
<STYLE>
```

## 3. Sera → `char_Staff_4A6B8A.png`

```
2d pixel art game sprite, gentle female healer age 19, long straight platinum
blonde hair, soft violet eyes, white robes gold trim holy chest symbol,
wooden staff with soft glow, white headdress, healing pouch, steel-blue
sash trim, full body front-facing idle gentle pose,
<STYLE>
```

## 4. Voss → `char_Bow_4A6B8A.png`

```
2d pixel art game sprite, quiet male ranger age 25, shoulder dark
green-brown hair, sharp green eyes, green leather jerkin Valenwood pattern,
hooded forest cloak leaf motif, composite longbow held left, quiver arrows
on back, silent boots, steel-blue arrow fletching trim, full body
front-facing idle stance,
<STYLE>
```

## Portraits (dialogue, bust shot, painterly boleh)

Prompt pola (ganti nama + ciri, target `portrait_<Nama>.png` 128x128):

```
2d pixel art portrait bust, <ciri wajah persis ART_BRIEF §1>,
dark fantasy, dramatic rim light, muted desaturated palette,
dialogue portrait, centered face, 3/4 view,
<STYLE minus "transparent background" → pakai "dark vignette background">
```

- `portrait_Kael.png` — 22 male, ash-grey messy hair, pale blue tired eyes, neck burn scar, stoic frown
- `portrait_Briar.png` — 28 female, braided auburn hair, amber fierce eyes, cheek scar, confident smirk
- `portrait_Sera.png` — 19 female, platinum straight hair, violet gentle eyes, warm compassionate smile
- `portrait_Voss.png` — 25 male, green-brown shoulder hair, sharp green eyes, neutral bored look
