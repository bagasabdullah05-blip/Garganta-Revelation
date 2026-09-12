# Asset Generation Guide — Garganta Revelation

Panduan lengkap: apa saja yang harus di-generate, prompt siap copy-paste yang konsisten dengan cerita, dan cara pasang di Unity.

## 0. Style Block Global (wajib ditempel di akhir SEMUA prompt image)

```
STYLE: dark fantasy 2d pixel art, muted desaturated palette, HD-2D style like Octopath Traveler, clean single object, transparent background, no blur, no watermark, no text
```

Palette acuan: Ashen Blue #4A6B8A, Blood Red #8B2500, Bone White #E8DCC8, Shadow Purple #3D1F5C, Forest Green #3A5F3A, Steel Grey #6B7B8D, Gold #C5A345, Blight Teal #2A7A6E, Aether Glow #7FFFD4.

## 1. Cara Generate (workflow)

1. Pakai AI image (Midjourney / Stable Diffusion / Leonardo / PixelLab).
2. Tempel prompt per aset di bawah + STYLE block.
3. Untuk konsistensi: jadikan 1 hasil terbaik sebagai image reference untuk generate berikutnya (fitur image prompt / character reference).
4. Upscale maksimal 2x, lalu rapikan di Aseprite: hapus blur, samakan outline gelap 1px, samakan palette dengan daftar di atas.
5. Simpan sesuai nama file di Section 4, masukkan ke folder `Assets/` sesuai struktur, push.

## 2. Daftar Generate + Prompt

### 2.1 Tileset (10 file, canvas diamond 64x32, referensi: tile prototype di `Assets/Sprites/Tiles/Prototype/`)

```
TILE PLAINS: isometric game tile diamond 64x32, worn green grass with small flowers and dirt patches, war-torn but alive, STYLE
TILE FOREST: isometric game tile diamond 64x32, single ancient tree with dense green canopy and mossy roots blocking the tile, STYLE
TILE MOUNTAIN: isometric game tile diamond 64x32, grey rock peak with snow cap, high ground look, STYLE
TILE WATER: isometric game tile diamond 64x32, deep blue water with light ripple lines, STYLE (generate 2x dengan seed beda untuk frame animasi)
TILE RUINS: isometric game tile diamond 64x32, broken stone floor with cracks and debris, overgrown, STYLE
TILE BLIGHT: isometric game tile diamond 64x32, corrupted dark purple ground with glowing teal veins and toxic mist, sickly, STYLE
TILE WALL: isometric game tile, tall grey stone wall block rising above diamond base, brick lines, STYLE
TILE BRIDGE: isometric game tile diamond 64x32, wooden plank bridge with side rails over water, STYLE
```

### 2.2 Karakter Playable (15, sprite 64x64 per frame + portrait 256x256)

Template prompt sprite (ganti [DESKRIPSI] per karakter):

```
2d pixel art game sprite, [DESKRIPSI], idle standing pose facing down, full body, STYLE
```

Deskripsi per karakter (detail penuh ada di `docs/ART_BRIEF.md`):

```
KAEL: male squire age 22, short messy ash-grey hair, pale blue eyes, worn dark brown leather chestpiece over chainmail, tattered dark grey cloak torn left shoulder, iron bracers, chipped short sword
BRIAR: fierce female warrior age 28, long braided auburn hair, amber eyes, scar right cheek, dark grey steel armor red accents, fur collar, war sword and shield
SERA: gentle female healer age 19, long straight platinum blonde hair, violet eyes, white robes gold trim holy symbol, wooden staff, white headdress
VOSS: male ranger age 25, shoulder dark green-brown hair, sharp green eyes, green leather jerkin, hooded forest cloak leaf pattern, longbow and quiver
LYRA: female dark mage age 24, short asymmetrical deep purple hair, glowing purple eyes, dark purple robe black trim spider sigil, floating chained grimoire
RENN: male thief age 20, messy black hair grey streak, brown mischievous eyes, dark leather open vest, baggy pants, dual curved daggers, lockpick belt
DAWN: female paladin age 32, long blonde braided crown hair, stern blue eyes, full white plate gold trim, long white cape, holy blade and tower shield
KORR: scarred veteran spearman, heavy grey Ironhold armor red plume, giant spear, father-figure look
ASHA: wild Valenwood archer, green-brown camouflage leather, leaf cloak, composite bow, riddle-smile
ZARA: neutral wanderer rune knight, mismatched collected armor, rune-etched sword, melancholic wise
THORNE: bitter Shadow Court defector black mage, dark robes, half-burned face, void grimoire
VAEL: young cocky dragoon, light armor dragon scale accents, spear, hot-headed grin
NYX: cold Shadow Court assassin, full face mask, dark cloak, twin daggers
EOS: elderly wise white mage, ornate white robes, crystal staff, glowing Aether eyes, grandmotherly
GRIM: massive scarred berserker, minimal armor tribal markings, giant axe, honest simple face
```

Prompt portrait (ganti [DESKRIPSI] sama seperti di atas):

```
pixel art character portrait bust 256x256, [DESKRIPSI], face close-up, expressive eyes, dark background rim light, STYLE
```

Butuh 5 ekspresi per karakter: normal, battle (yelling), sad, happy, corruption (teal glowing veins). Tambahkan `, [EKSPRESI] expression` di prompt.

### 2.3 Musuh (sprite 48x48-128x128 sesuai ukuran)

```
BANDIT: hooded human bandit, mismatched armor, rusty sword, desperate look, STYLE
GOBLIN: small green goblin 60cm, big ears, hunched, sharp teeth, crude club, STYLE
WOLF: large menacing grey wolf, red corrupted eyes, STYLE
SKELETON: animated skeleton warrior, glowing blue eyes, rusty weapon, cracked skull, STYLE
ZOMBIE: shambling corpse, grey-green torn flesh, exposed bones, STYLE
SLIME: gelatinous blob semi-transparent blue, STYLE (varian red fire, green poison)
BAT: dark purple winged bat, red eyes, STYLE
SPIDER: giant black hairy spider, multiple red eyes, STYLE
CULTIST: hooded robed cultist, dark purple, chanting pose, STYLE
ROGUE KNIGHT: fallen knight, dented dark plate, tattered cape, broken sword, STYLE
IMP: small red demon, bat wings, fire in hand, cackling, STYLE
ORC: large green-brown orc 200cm, heavy war axe, brutish, STYLE
DARK KNIGHT: fallen paladin, full black plate with red glowing runes, cursed greatsword, STYLE
DRAGON: classic four-legged dragon, wings spread, fire breath, red scales, massive, STYLE
WYVERN: two-legged wyvern mount, dark grey, fast look, STYLE
ARCHMAGE: elderly floating mage, arcane purple aura, orbiting tomes, STYLE
LICH: undead skeletal sorcerer, crown, tattered royal robes, soul fire hands, STYLE
GOLEM: massive stone construct, teal Aether cracks, 2-tile size, STYLE
CHIMERA: three-headed beast lion goat snake, STYLE
BANSHEE: ghostly floating female, transparent pale white blue, screaming, STYLE
SHADOW BEAST: amorphous living darkness, red eyes, shadow tendrils, STYLE
BLIGHT WALKER: corrupted human monster, grey flesh, glowing teal blight veins, STYLE
BOSS GARRICK: massive bandit lord, stolen armor, war hammer, missing eye, STYLE
BOSS MARCUS: fallen allied knight, damaged faction armor, dual swords, STYLE
BOSS VEX: elite Ironhold general, pristine dark armor, commanding aura, STYLE
BOSS CORRUPTOR: massive amorphous blight entity, shifting form, teal core, STYLE
BOSS USURPER: dark fantasy final boss, [faksi: corrupted high priest / shadow king / blight lord], multi-form, STYLE
BOSS PRIMEVAL DRAGON: ancient colossal dragon, crystalline scales, all elements aura, STYLE
```

### 2.4 UI (1920x1080 bg, button 400x100, panel 9-slice, ikon 48x48)

```
MAIN MENU BG: dark fantasy landscape 1920x1080, ruined castle silhouette, floating ash particles, distant fire horizon, overcast, no text, STYLE
TITLE LOGO: ornate dark fantasy game title text "Garganta Revelation", metallic with ember glow, transparent background
BUTTON NORMAL: stone tablet button 400x100, carved border, empty center, dark grey stone gold trim, STYLE
BUTTON HOVER: same stone tablet button with teal glow from behind, STYLE
PANEL: dark stone panel 800x600 with gold border, empty center for content, 9-slice friendly plain edges, STYLE
HP BAR: green health bar asset 200x24 with dark outline, STYLE
PORTRAIT FRAME: circular portrait frame 96x96, blue border player version, STYLE (varian red border enemy)
ICON ATTACK: pixel icon 48x48 crossed sword, STYLE
ICON SKILL: pixel icon 48x48 magic spark, STYLE
ICON ITEM: pixel icon 48x48 potion bottle, STYLE
ICON WAIT: pixel icon 48x48 hourglass, STYLE
DIALOGUE BOX: medieval dialogue box 1200x300, parchment dark, portrait slot left, STYLE
WORLD MAP: aged parchment continental map, 5 regions, ink drawing, compass rose, no labels, STYLE
```

### 2.5 Skill VFX (sprite sheet / particle PNG transparan)

```
FIREBALL: fire magic projectile sprite, orange red flames, STYLE
ICE SHARD: ice magic crystal blue white, STYLE
LIGHTNING: yellow white lightning bolt, STYLE
HEAL: green gold rising sparkles, STYLE
HOLY BURST: gold white radiant wings burst, STYLE
DARK VOID: purple black shadow tendrils, STYLE
SLASH: white sword slash arc effect, STYLE
IMPACT: orange impact burst star, STYLE
```

### 2.6 Audio (pakai Suno AI, prompt siap pakai)

```
MAIN THEME: epic dark fantasy orchestral theme, choir, melancholic, 90 BPM
BATTLE: intense tactical RPG battle music, war drums, strings, 120 BPM, loopable
BOSS: dramatic boss battle, heavy orchestra, choir, 140 BPM, escalating
TOWN: peaceful medieval town, lute and flute, warm, 80 BPM, loopable
WORLD MAP: melancholic adventure, solo violin, strings, 90 BPM, hopeful
DEFEAT: somber slow piano strings, 60 BPM, sad
VICTORY: triumphant brass fanfare, 1 minute
```

SFX gratis dari Freesound.org: slash, bow, cast, heal, hit, crit, miss, death, menu select/confirm/cancel, footstep.

## 3. Struktur Folder & Nama File (wajib diikuti)

```
Assets/
  Sprites/
    Tiles/Prototype/      <- tileset v1 sudah ada, timpa file yang dipoles dengan nama SAMA
    Characters/Kael/      <- kael_idle_down_0.png, kael_walk_down_0..3.png, ...
    Characters/Briar/     <- dst
    Enemies/Common/       <- bandit.png, goblin.png, ...
    Enemies/Elite/        <- ...
    Enemies/Bosses/       <- ...
    Portraits/            <- kael_normal.png, kael_battle.png, ...
    UI/                   <- btn_normal.png, panel.png, hp_bar.png, ...
    Icons/Skills/         <- skill_power_strike.png, ...
    Icons/Items/          <- item_potion.png, ...
    VFX/                  <- vfx_fireball.png, ...
  Audio/
    Music/                <- main_theme.ogg, battle.ogg, ...
    SFX/                  <- sfx_slash.wav, ...
```

## 4. Cara Pasang di Unity (per tipe aset)

### 4.1 Import setting (semua sprite)

1. Klik file PNG di Project window.
2. Inspector: Texture Type = `Sprite (2D and UI)`, Filter Mode = `Point (no filter)`, Compression = `None`, Mesh Type = `Tight`.
3. Pixel Per Unit = `32` (karakter 64px = 2 unit) atau `32` untuk tile 64x32.
4. Apply.

### 4.2 Tileset -> Tilemap

1. Buka `Window > 2D > Tile Palette`, buat palette baru.
2. Drag 8 tile PNG ke palette -> Create Tile otomatis.
3. Buat GameObject `Grid > Tilemap`, assign tileset.
4. Paint map sesuai layout di `docs/CONTENT_PLAN.md`.
5. Tambah `TilemapCollider2D` untuk Wall/Water (impassable).

### 4.3 Karakter -> Prefab + Animator

1. Sprite sheet multi-frame: Sprite Mode = `Multiple`, buka Sprite Editor, Slice Grid 64x64.
2. Buat `Animator Controller` per karakter: state Idle, Walk, Attack, Hit, Death (transisi via parameter `state` int).
3. Buat Prefab `Unit`: SpriteRenderer + Animator + BoxCollider2D + script `Unit.cs` (nanti dari programmer).
4. Sorting Layer: `Units`, Order in Layer = `posisi Y` (script atur otomatis).
5. Portrait/portrait ekspresi: Texture Type Sprite, taruh di `Portraits/`, referensikan dari `UnitDatabase`.

### 4.4 UI

1. Canvas: Render Mode Screen Space, Canvas Scaler = `Scale With Screen Size`, referensi 1920x1080 (PC) / 1080x1920 (mobile portrait menu).
2. Button: Image Type Sliced (butuh border 9-slice dari panel asset), tambah TextMeshPro di atasnya.
3. HP bar: Slider dengan Fill image dari `hp_bar.png`.
4. Turn order portraits: Horizontal Layout Group + prefab lingkaran 96x96.

### 4.5 Audio

1. Music: Load Type `Streaming`, format OGG Vorbis, centang Loop.
2. SFX: Load Type `Decompress On Load`, format WAV.
3. Taruh di AudioMixer: grup Music / SFX.
4. Referensikan dari `AudioManager` (nanti dari programmer).

### 4.6 VFX

1. Import PNG transparan, buat Prefab dengan Animator (play sekali lalu destroy via script).
2. Atau pakai ParticleSystem 2D dengan texture dari `VFX/`.
3. Sorting Layer: `Effects` (di atas Units).

## 5. Checklist Generate (ringkas, detail di ASSET_CHECKLIST.md)

- [ ] 10 tileset (8 tipe + 2 frame air + atlas)
- [ ] 15 karakter (sprite + 5 portrait + ikon turn order)
- [ ] 12 common + 10 elite + 6 boss enemies
- [ ] UI kit (bg, logo, button x2, panel, hp bar, frame x2, 4 ikon aksi, dialogue, world map)
- [ ] 8 VFX dasar + slash + impact
- [ ] 112 ikon skill (bertahap, minimal 10 prototype)
- [ ] 7 BGM + 10 SFX dasar
