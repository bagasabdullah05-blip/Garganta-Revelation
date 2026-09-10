# Art Brief — Garganta Revelation

Dokumen ini adalah acuan visual untuk semua aset. Bisa dipakai langsung sebagai prompt AI image generator, brief untuk artist commission, atau referensi DIY pixel art.

## 0. Art Direction Overview

| Aspek | Detail |
|---|---|
| **Style** | HD-2D Pixel Art (base sprite 32x48, display 64x96+) |
| **Palette** | Desaturated, muted tones — dark fantasy |
| **Lighting** | Heavy contrast, dramatic shadows, glowing Aether |
| **Referensi** | Octopath Traveler 2D, FFT WotL, Fire Emblem GBA |
| **Mood** | Gritty, war-torn, tiap faksi beda visual |

### Global Color Palette

```
PRIMARY:
  Ashen Blue:    #4A6B8A (Garganta Alliance)
  Blood Red:     #8B2500 (violence, Ironhold accent)
  Bone White:    #E8DCC8 (Holy Dominion)
  Shadow Purple: #3D1F5C (dark magic, corruption)
  Forest Green:  #3A5F3A (Valenwood)

SECONDARY:
  Steel Grey:    #6B7B8D
  Gold:          #C5A345
  Blight Teal:   #2A7A6E (Blight / Aether tercemar)
  Warm Brown:    #6B4226
  Deep Black:    #1A1A2E

HIGHLIGHTS:
  Aether Glow:   #7FFFD4
  Fire Glow:     #FF6B35
  Ice Glow:      #87CEEB
  Holy Glow:     #FFD700
```

### Asset Pipeline

```
PROTOTYPE -> placeholder (colored squares, Unity sprites)
ALPHA     -> free assets (itch.io, OpenGameArt, Kenney.nl)
BETA      -> AI-generated base + manual cleanup di Aseprite
LAUNCH    -> commissioned hero assets + polished final
```

Sumber gratis: itch.io free, OpenGameArt.org, Kenney.nl, 0x72 dungeon tileset.
AI: PixelLab.ai, Leonardo.ai, Stable Diffusion lokal, Midjourney untuk concept.
DIY: Aseprite ($20), Piskel (free), LibreSprite (free).

---

## 1. Karakter Playable (15)

### 1.1 Kael — Protagonist, Ashwalker Squire

```
AGE: 22, Male, stoic, determined, haunted
Hair: pendek messy ash-grey, bekas luka di kulit kepala kiri
Eyes: pale blue, lelah tapi tegas
Skin: tan weathered, burn scar kecil di leher
Build: medium lean, 175cm

Tier 1 Squire:
- worn leather chestpiece dark brown scratched
- chainmail terlihat di collar dan sleeves
- tattered dark grey cloak, bahu kiri robek
- iron bracers dented, heavy leather boots berlumpur
- utility belt dengan pouches kecil

Tier 2 Paladin:
- blessed silver plate dengan simbol Ashwalker
- white cape gold trim, holy pauldrons motif sayap kiri
- bracers dengan rune engraving, boots bersih buckle silver

Tier 3 Holy Knight:
- full divine plate silver-white ornate
- glowing blue runes di edge armor
- majestic white cape flowing, winged helmet optional
- radiant sword dengan holy aura

Warna: ash grey -> silver -> white (bangkit dari abu)
Senjata: chipped iron short sword -> blessed silver longsword -> Excalibur glowing
Ekspresi portrait: normal determined slight frown, battle intense yell,
sad menunduk mata tertutup, happy senyum kecil langka,
corruption mata glow teal urat terlihat
Animasi: jalan steady purposeful, attack horizontal slash,
special Holy Blade vertical strike + light burst,
hit terhuyung pegang dada, death berlutut lihat tangan (Blight marks)
```

Prompt AI:
`2d pixel art sprite, male squire age 22, short messy ash-grey hair, pale blue eyes, worn dark brown leather chestpiece over chainmail, tattered dark grey cloak torn left shoulder, iron bracers, muddy boots, dark fantasy, muted desaturated palette, HD-2D style, 6-direction sprite sheet, idle walk attack hit death frames`

### 1.2 Briar — Ironhold Warrior

```
AGE 28 Female fierce loyal secretly caring
Hair: panjang braided dark red auburn, Eyes amber fierce
Skin fair scar di pipi kanan, Build strong muscular 170cm
Tier1: Ironhold military armor dark grey steel red accents,
fur collar wolf fur, heavy steel pauldrons,
leather gauntlets iron plates, combat boots metal plating,
sword belt insignia Ironhold
Tier2 Paladin: silver + red trim, holy pendant curian dari Dominion,
red cape pendek praktis, decorated shield crest Ironhold
Senjata: war sword heavy -> blessed war blade silver edge + shield
Ekspresi: normal smirk confident, battle aggressive shout,
sad alih pandang rahang mengeras, happy tawa genuine langka
Anim: jalan berat armored, attack overhead swing kuat,
shield block angkat shield + efek clang metal
```

Prompt AI:
`2d pixel art, fierce female warrior age 28, long braided auburn hair, amber eyes, scar right cheek, dark grey steel armor red accents, fur collar, heavy pauldrons, war sword and shield, Ironhold military style, dark fantasy pixel art sprite sheet`

### 1.3 Sera — Holy Dominion Acolyte

```
AGE 19 Female gentle faithful questioning beliefs
Hair panjang lurus platinum blonde, Eyes soft violet,
Skin pale luminous, slender graceful 165cm
Tier1: white robes gold trim Dominion standard,
holy symbol bordir dada, wooden staff sederhana,
white headdress nutup sebagian rambut, sandals, healing pouch
Tier2 White Mage: robes flowing lebih panjang bordir ornate,
crystal-tipped staff, headdress dilepas, healer gloves fingerless white
Tier3 Seraph: angelic white robes ethereal glow,
transparent wing-like cape holographic, divine staff crystal melayang,
subtle halo particle, feathered shoulder
Senjata: wooden staff -> materia staff crystal orb -> seraph staff floating crystal
Ekspresi: normal warm gentle smile, battle fokus chanting,
sad tearful compassionate, happy bright hopeful
Anim: jalan graceful sedikit melayang, heal staff angkat particles hijau emas swirl,
attack staff strike lemah reluctant, special Arch of Light sayap muncul light burst besar
```

Prompt AI:
`2d pixel art, gentle female healer age 19, long straight platinum blonde hair, violet eyes, white robes gold trim holy symbol, wooden staff, white headdress, dark fantasy holy mage, soft glow, pixel sprite sheet`

### 1.4 Voss — Valenwood Ranger Archer

```
AGE 25 Male quiet observant sarcastic
Hair shoulder dark green-brown forest camo, Eyes sharp green,
Skin tanned weathered, lean agile 180cm
Outfit: green leather jerkin pola Valenwood, hooded cloak forest green motif daun,
bracers dengan bowstring grooves, soft boots silent, bandolier arrows, hidden pockets
Senjata: composite longbow Valenwood crafted, quiver varied arrows
Ekspresi: normal netral slightly bored, battle fokus tajam,
sarcastic alis naik smirk, serious mata menyipit
Anim: jalan ringan hati-hati ranger, attack quick draw smooth release,
special Piercing Shot aim arrow glowing impact besar
```

Prompt AI:
`2d pixel art ranger male 25, shoulder dark green-brown hair, sharp green eyes, green leather jerkin, hooded forest cloak leaf pattern, longbow, quiver, silent boots, Valenwood elf style, dark fantasy sprite`

### 1.5 Lyra — Shadow Court Mage

```
AGE 24 Female cunning ambitious secretly conflicted
Hair pendek asymmetrical deep purple, Eyes dark purple glow saat cast,
Skin olive, slender 168cm
Tier1: dark purple robe black trim, sigil Shadow Court spider di punggung,
fingerless gloves rune arcane, high collar, dark boots, floating tome
Tier2 Black Mage: robes elaborate arcane circles di kain,
spellbook ornate chained, eye mask optional agen look
Tier3 Spellblade: hybrid magic-armor, gauntlets channel spells,
tome embedded di chest armor, arcane wings saat combat
Senjata: grimoire chained -> void grimoire floating pages dark energy -> bahamut tome living book dark flames
Ekspresi: normal cool calculating, battle manic grin,
conflicted alih pandang tangan di dada, happy warmth genuine langka
Anim: jalan sedikit melayang tome orbit, cast arcane circles tangan glow,
special Meteor panggil meteor gelap dari langit
```

Prompt AI:
`2d pixel art female dark mage 24, short asymmetrical deep purple hair, glowing purple eyes, dark purple robe black trim spider sigil, floating chained grimoire, arcane runes, Shadow Court style, dark fantasy`

### 1.6 Renn — Ashen Thief

```
AGE 20 Male street-smart joker hides trauma
Hair messy black grey streak blight scar, Eyes brown mischievous,
Skin dark brown, small wiry 170cm
Outfit: dark leather vest open front, baggy pants tucked boots,
multiple hidden blade holsters, mask gantung di leher,
fingerless gloves, belt lockpicks tools
Senjata: dual curved thief blades + hidden blade di bracer
Ekspresi: normal grinning confident, battle feral focused,
scared mata lebar rare genuine, sad silent menunduk
Anim: jalan ringan swaying, attack quick dual slash combo,
special Shadow Strike hilang dalam smoke muncul di belakang
```

### 1.7 Dawn — Holy Dominion Paladin

```
AGE 32 Female righteous noble rigid but learning
Hair panjang blonde braided crown, Eyes blue stern,
Skin fair, tall powerful 180cm
Outfit: full white plate Dominion standard, gold trim holy symbols,
white cape panjang flowing, winged pauldrons, tower shield white gold, cross pendant
Senjata: holy blade glowing + divine shield reflect light
Ekspresi: normal stoic authoritative, battle divine fury,
conflicted kernyit tangan di pendant, happy warm maternal
Anim: jalan regal armored, attack shield bash -> holy slash,
special Divine Guard shield glow protection aura ke allies
```

### 1.8-1.15 Ringkasan

```
KORR Spearman->Dragoon: veteran Ironhold berparut, heavy grey armor red plume,
giant spear, dragon helmet later, gruff protective father-figure
ASHA Archer hidden: Valenwood liar camouflaged, green-brown leather leaf cloak,
composite bow, wild bicara teka-teki
ZARA Rune Knight: wanderer netral heritage campur, mismatched armor dari travel,
rune-etched sword, wise melancholic philosopher
THORNE Black Mage anti-hero: defector Shadow Court, dark robes wajah setengah terbakar,
void grimoire, bitter redeemed protective
VAEL Dragoon: muda cocky Ironhold, light armor dragon scale accents,
spear spesialis jump, hot-headed honor-bound
NYX Assassin faction choice: agen Shadow Court, full face mask dark cloak,
twin daggers, cold efficient secretly kind
EOS White Mage: elder wise Dominion, ornate white robes crystal staff,
glowing eyes Aether-touched, grandmotherly fierce saat perlu
GRIM Berserker battle recruit: massive scarred mantan musuh,
minimal armor tribal markings, giant axe, simple honest kutip peribahasa
```

---

## 2. Musuh

### Common (12)

```
BANDIT: hooded human mismatched armor, rusty weapons desperate,
brown grey kotor, normal human. Varian leader bigger better gear
GOBLIN: kecil green skin telinga besar, bungkuk gigi tajam,
crude clubs knives, green skin brown rags, 60cm
WOLF: besar menacing grey fur, red eyes saat corrupted,
pack behavior, grey dark grey, large dog
SKELETON: animated bones glowing eyes, rusty weapons,
parts missing satu lengan skull retak, bone white blue glow
ZOMBIE: shambling corpse torn flesh, greenish skin exposed bones,
slow tanky, grey-green dark
SLIME: gelatinous blob semi-transparent, absorbs split saat hit,
blue water red fire green poison, size variasi
BAT: winged red eyes, swooping attack, gantung di dungeon ceiling,
dark purple black, small-medium
SPIDER: giant hairy multiple eyes, web slow musuh, poison bite,
black dark red, dog-sized
CULTIST: hooded robed insignia faksi, dark magic, chanting anim,
dark purple black
ROGUE KNIGHT: fallen knight corrupted armor, dented plate cape robek,
broken sword, dark grey rust
IMP: small demon bat wings, fire magic cackling, red orange, 40cm
ORC: large green-brown skin, heavy war axe club, brutish, 200cm
```

### Elite (10)

```
DARK KNIGHT: fallen paladin full dark plate, black armor red glowing runes,
cursed greatsword, large human boss-level
DRAGON: classic 4 legs wings fire breath, red fire blue ice green poison,
massive setengah grid
WYVERN: 2 legs mount rider, faster less HP dari dragon, dark grey black large
ARCHMAGE: elderly floating arcane aura, multiple tomes orbit,
lightning fire, dark robes purple glow levitating
LICH: undead sorcerer skeletal, crown royal robes robek, floating soul fire,
dark purple ghostly green
GOLEM: massive stone construct slow devastating,
cracks Aether veins teal glow, grey stone, 2 tiles
CHIMERA: three-headed lion goat snake, tiap kepala attack beda,
brown grey green large
BANSHEE: ghostly female floating transparent, screaming AoE debuff,
pale white blue ethereal
SHADOW BEAST: amorphous darkness red eyes shadow tendrils,
phase through terrain, pure black red highlights variable size
BLIGHT WALKER: corrupted human monstrous, blight veins teal glow,
half-human half-monster, grey flesh teal corruption large human
```

### Boss (6)

```
CH4 BANDIT LORD GARRICK: massive man stolen armor, war hammer heavy plate,
scars missing eye, arena mountain pass, P1 normal, P2 berserk red eyes
CH6 TRAITOR MARCUS: mantan ally armor faksi sama, gear rusak cerminan broken trust,
dual swords, arena castle interior, P1 honorable duel, P2 dirty tricks smoke bombs
CH8 GENERAL VEX Ironhold: elite pristine dark armor, strategic summon reinforcements,
commanding aura buff allies, arena fortress siege, P1 command troops, P2 personal combat
CH10 BLIGHT HEART CORRUPTOR: massive amorphous shifting,
multiple patterns, arena blight core toxic, P1 tentacles, P2 summon walkers, P3 core exposed desperate
CH12 USURPER faction-dependent: Dominion=corrupted high priest,
Shadow=shadow king, neutral=blight lord, multi-phase multi-form, arena aether core reality warping
POST-GAME PRIMEVAL DRAGON: ancient massive 4x4 tiles, all elements, arena dragon lair
```

---

## 3. Map

### M1 Ashfield Ruins 10x10 tutorial plains

```
Tema: desa hancur awal journey
Visual: ruined buildings stone wood scattered, burning debris fire particles,
dirt paths, dull green grass patches, villager corpses background,
smoke rising, overcast grey sky
Tiles: plains worn grass, ruins broken stone walls, bridges wooden damaged,
elevated hills rubble piles, impassable collapsed buildings
Mood: desolation loss beginning darkness
```

### M2 Valenwood Approach 12x12 forest

```
Visual: dense tall ancient trees, dappled sunlight canopy,
moss rocks, small stream impassable water, fallen logs cover elevated,
mushrooms flowers, mist bawah
Tiles: forest cost2, plains clearings, water stream impassable,
elevation hilltops logs, bridge stone
Mood: mysterious ancient alive but wary
```

### M3 Ironhold Citadel Gate 14x14 fortress siege

```
Visual: massive stone walls impassable, gatehouse choke,
guard towers elevation archer advantage, drawbridge destructible,
banners red grey Ironhold, siege rams ladders, mud rain
Tiles: stone floor interior, walls impassable, towers high elevation,
gates destructible jadi passable, mud penalty
Mood: intense military strategic
```

### M4 Blight Tunnels 12x12 underground

```
Visual: dark caves visibility terbatas, Aether veins teal glow di dinding,
toxic pools blight tiles, stalactites, abandoned mining equipment,
monster nests webs bones, flickering lights
Tiles: cave floor dark stone, aether veins heal tapi corruption risk,
blight pools corruption per turn, impassable walls stalagmites,
elevation rocky platforms
Mood: claustrophobic dangerous otherworldly
```

### M5 Obsidian Spire 14x14 Shadow Court

```
Visual: dark obsidian architecture, purple magical barriers,
floating platforms magical, arcane circles di lantai,
shadowy figures background, mirrors reflect magic shatterable,
chandeliers purple flames
Tiles: dark stone polished elegant, barriers impassable dispellable,
platforms elevation magical, circles buff debuff zones, mirrors reflect projectiles
Mood: elegant treacherous magical
```

### M6 Aether Heart 16x16 final

```
Visual: crystalline Aether formations, floating terrain chunks,
swirling energy particles, distorted gravity warping,
ancient ruins + crystal, central altar objective,
color shifts normal -> corrupted -> purified
Tiles: crystal floor glowing, floating platforms moving,
aether streams power-up atau hazard, altar center objective,
void instant death jika jatuh
Mood: epic otherworldly climactic
```

---

## 4. UI Art

```
Main Menu: background dark ruined castle silhouette, ash particles,
distant fire horizon, slow parallax. Title ASHEN? no -> GARGANTA REVELATION
ornate dark fantasy font metallic slight glow embers.
Buttons stone tablets carved hover glow behind. New Continue Load Settings Credits.
Warna dark grey stone + gold text + teal glow.

Battle HUD: turn order bar top horizontal dark bg portraits circles 40px,
player blue border enemy red current glowing smooth scroll.
Action menu bottom right stone tablet Attack Skill Item Wait + icons.
Unit info bottom left dark panel portrait HP green-yellow-red MP blue
CTB gauge stats buff debuff icons. Minimap top right small grid
blue dots player red enemy clickable pan.
Warna dark stone + colored indicators + subtle glow.

Health bars floating: di atas sprite width proporsional max HP,
green 100-60 yellow 60-30 red 30-0, dark outline, smooth drain, flash white on hit.

Damage numbers pixel font readable kecil:
physical white bold, magical purple italic, heal green +,
critical red larger shake, miss grey MISS fade,
buff blue float up, debuff orange float down.

Equipment screen: kiri model + stats, tengah 5 slots circular
rarity border white green blue purple gold glow equipped
empty dashed ?. kanan inventory scrollable icon name rarity stats
checkmark equipped sortable. Dark bg rarity colors pop.

World map: parchment hand-drawn 5 regions, marker pulsing,
dotted travel routes, fog of war grey unvisited, continental scale.
Region zoom detail towns dungeons landmarks paths terrain.
Aged parchment ink compass rose.
```

---

## 5. Tiles & Animasi

```
Base tile isometric diamond 64x32, grid max 16x16.
Tiap tipe: base clean, edge 4 arah, corner 4 arah, transisi antar tipe.
Plains green variasi shade flowers random subtle shadow.
Forest tree block sight cost2 varian single double dense,
musim green orange white.
Mountain rock 3D illusion small peak large peak cliff, snow di atas.
Water ripple 4-frame loop reflection, shallow penalty vs deep impassable.
Ruins broken stone cracked overgrown debris.
Blight corrupted teal veins pulsing glow toxic mist dark sickly.
Wall stone vertical block move sight, short bisa lihat atas vs tall block.
Bridge wooden stone over water gap choke indicator.

Sprite sheet karakter: 6 arah, tiap arah 4-8 frames,
anim idle 2 breath, walk 4-6, attack 4-6 weapon-specific,
skill 6-8 effect-specific, hit 2-3 knockback, death 4-6 collapse.
Frame 64x64 base upscale 128/256. Sheet 6 kolom x 8 baris = 48 frames.
Enemy: 4 arah simpler 2-4 frames. Large 2x2 tiles 128x128.

Skill VFX:
fire orange red expanding fireball impact burst shake 0.5-1s
ice blue white icicle shatter freeze 0.5-1s
lightning yellow white bolt sky sparks stun 0.3-0.5s
heal green gold rising soft glow sparkle 1s
holy gold white wings radiant 1-1.5s
dark purple black void tendrils 0.5-1s
blight teal veins spreading mist ongoing ambient
weapon: sword white slash, axe orange burst, spear blue thrust,
bow arrow trail spark, dagger quick flash, staff colored orb,
tome arcane circle spell text
```

## 6. Estimasi Budget Art

```
Commission:
character animated $50-200/sheet, boss $100-400,
UI kit $200-500, tileset $100-300, portrait $30-80, key art $100-500
Total estimasi: 15 char x100=1500 + 6 boss x200=1200 + 30 enemy x50=1500
+ tileset 200 + UI 300 + 15 portrait x50=750 + key art 300 = ~$5,750
Alternatif hemat: free + AI + DIY = $0-100
```
