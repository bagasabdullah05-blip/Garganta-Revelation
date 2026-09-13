# Main Menu Sinematik — desain + prompt

## Desain (sudah di-wire di TitleUI)

Layar judul = **slideshow sinematik**: 4 scene cutscene dalam game,
crossfade tiap 8 detik + slow zoom, logo di tengah, menu di bawah,
musik tema mengalun. Tanpa file = tampilan lama (panel saja).

| File | Scene | Tampil |
|---|---|---|
| `bg_title_0.png` 960x540 | Ashfield terbakar (Ch.1) | 0–8 dtk |
| `bg_title_1.png` 960x540 | Gerbang Bastion + panji (Ch.2b) | 8–16 dtk |
| `bg_title_2.png` 960x540 | Blight Heart berdenyut teal (Ch.9) | 16–24 dtk |
| `bg_title_3.png` 960x540 | Siluet party vs horizon | 24–32 dtk, loop |
| `logo_garganta.png` 800x256 transparan | Logo judul emas | tengah atas |

## Prompt scene (painterly, 16:9, no text — logo file terpisah!)

Pola: `cinematic game key visual, MOTIF, dark fantasy, muted desaturated
palette, dramatic lighting, painterly, 16:9 composition, no text no logo no watermark`

1. `bg_title_0`: burning village at dusk, collapsed timber houses, villagers
   fleeing silhouettes, embers and smoke columns, lone swordsman foreground back-view
2. `bg_title_1`: massive fortress gate, blue alliance banners vs grey ironhold
   banners, armies forming lines, overcast dawn light
3. `bg_title_2`: colossal pulsating teal corruption heart in cavern, veins across
   stone, tiny party silhouettes approaching with torches
4. `bg_title_3`: four heroes back-view on cliff (ash-grey swordsman, braided
   warrior, white healer, hooded archer), sunrise over war-torn valley

## Prompt logo (`logo_garganta.png`, transparan)

```
fantasy game logo emblem, text "GARGANTA REVELATION", ornate gothic serif
letters, antique gold with ember glow edges, small sword-and-aether sigil above
text, transparent background, centered, no other elements
```
Catatan: AI sering typo teks — generate 4–6x, pilih yang ejaannya benar.
Alternatif pasti-benar: teks digambar manual (font Cinzel Decorative gold).

## Prompt musik tema (Suno/Udio — timpa `title.ogg`)

```
dark fantasy main theme, 90 seconds, lone cello motif (3 descending notes =
Kael's theme) opening 15s, building war drums + strings + distant choir,
hopeful brass lift at 0:55, resolve to somber piano, orchestral, loopable,
no vocals. Reference mood: Octopath title meets FFT formation screen.
```
Versi battle/map/base yang ada sekarang tetap dipakai di tempatnya masing-masing.
