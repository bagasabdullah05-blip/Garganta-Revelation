# Phase 1 — Heroes: full set (sprite + 6 klip anim + 4 portrait)

Setiap hero = **1 idle sprite + 6 strip anim + 4 portrait = 11 file**.
Strip = PNG horizontal, frame 32x48, nama `anim_<Id>_<clip>.png`
(Id = Kael/Briar/Sera/Voss, clip = idle/walk/attack/skill/hit/death).
Engine putar otomatis; strip hilang = fallback sprite diam (game tetap jalan).
Target: `Assets/Resources/Art/` (strip di `Anims/`).

Aturan strip: pose & desain HARUS konsisten antar frame (pakai seed sama /
img2img dari idle). Kiri-kanan cukup 1 arah — engine flip otomatis.

## Base prompt (tempel <STYLE> dari STYLE.md di tiap prompt)

Ganti [KLIP] dengan baris klip di bawah. Base per hero sama dengan file
sebelumnya (32x48, kaki bawah, senjata kanan, trim steel-blue).

## Kael — Squire (`char_Sword_4A6B8A.png` + `anim_Kael_*.png`)

Base: male squire 22, messy ash-grey hair, pale blue tired eyes, neck burn
scar, brown leather + chainmail, tattered grey cloak, iron bracers, chipped
short sword.

| Klip | Frame | Prompt delta |
|---|---|---|
| `idle` | 2 | standing guard, subtle breath, sword lowered |
| `walk` | 4 | walk cycle left-to-right, cloak sway, sword bob |
| `attack` | 4 | horizontal slash, wind-up → strike → recover |
| `skill` | 4 | overhead holy slash + light burst (boleh glow) |
| `hit` | 2 | flinch back, hand to chest |
| `death` | 4 | kneel → collapse, staring at hand (Blight marks) |

## Briar — Warrior (`char_Axe_4A6B8A.png` + `anim_Briar_*.png`)

Base: female warrior 28, braided auburn hair, amber eyes, cheek scar,
grey-red steel armor, fur collar, war axe.

| Klip | Frame | Prompt delta |
|---|---|---|
| `idle` | 2 | confident stance, axe butt on ground |
| `walk` | 4 | heavy armored march |
| `attack` | 4 | overhead axe swing, full body twist |
| `skill` | 4 | shield-less war cry slash + red burst |
| `hit` | 2 | stagger, teeth grit |
| `death` | 4 | fall to knees, axe planted |

## Sera — Acolyte (`char_Staff_4A6B8A.png` + `anim_Sera_*.png`)

Base: female healer 19, platinum straight hair, violet eyes, white-gold robes,
wooden staff, headdress.

| Klip | Frame | Prompt delta |
|---|---|---|
| `idle` | 2 | gentle hover-sway, staff upright |
| `walk` | 4 | graceful glide walk, robes flow |
| `attack` | 2 | reluctant staff poke (lemah, sesuai lore) |
| `skill` | 4 | staff raised, green-gold swirl particles |
| `hit` | 2 | startled step back |
| `death` | 4 | sink down, staff falling |

## Voss — Archer (`char_Bow_4A6B8A.png` + `anim_Voss_*.png`)

Base: male ranger 25, green-brown shoulder hair, sharp green eyes, green
jerkin + hooded leaf cloak, longbow, quiver.

| Klip | Frame | Prompt delta |
|---|---|---|
| `idle` | 2 | relaxed alert, bow lowered |
| `walk` | 4 | light careful ranger stride |
| `attack` | 4 | quick draw → aim → release |
| `skill` | 4 | piercing shot, arrow glow, big impact pose |
| `hit` | 2 | sidestep wince |
| `death` | 4 | crumple, quiver spilling arrows |

## Portraits 128x128 (`portrait_<Nama>[_<expr>].png`, painterly boleh)

Ekspresi per hero: normal (fallback `portrait_<Nama>.png`), `battle`,
`sad`, `happy`. Stretch: `corruption` (mata glow teal + urat — tampil saat
Blight ≥50%, wiring P2).

Pola: `2d pixel art portrait bust, [CIRI ART_BRIEF §1 + EKSPRESI],
dramatic rim light, muted palette, centered 3/4 view, dark vignette, no text`
- Kael: normal stoic frown / battle yell / sad eyes closed / rare small smile
- Briar: normal smirk / battle shout / sad jaw clenched / genuine laugh
- Sera: normal warm smile / battle chanting focus / tearful / bright hopeful
- Voss: normal bored neutral / battle sharp focus / sarcastic smirk / serious squint
