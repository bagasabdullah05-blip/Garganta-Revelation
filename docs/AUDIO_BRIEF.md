# Audio Brief — Garganta Revelation (M6 Production)

Target: 18 music tracks (loopable) + ~40 SFX. Budget route: Suno/Udio (AI music)
+ Freesound/CC0 (SFX) + Bfxr (retro/UI) = $0–50. Commission route in §5.

Drop files (no code changes needed — AudioManager auto-plays when present):

```
Assets/Resources/Audio/Music/<id>.ogg   (vorbis, 44.1kHz, loop)
Assets/Resources/Audio/SFX/<id>.wav     (44.1kHz mono, trimmed)
```

Music IDs used in code: `title map base battle boss victory defeat ending`.
SFX IDs used in code: `hit skill_magic miss heal`.
Extra IDs for M6 wiring (drop + one-line hook on request): listed per track below.

## 1. Music tracks (Suno/Udio prompts — copy-paste ready)

Global style suffix for every prompt: `, dark fantasy tactical RPG, orchestral with war drums and strings, loopable, no vocals`.

| # | ID | Use | Prompt core |
|---|---|---|---|
| 1 | `title` | Title screen | `somber piano and cello, ash falling, distant choir, slow 70BPM` |
| 2 | `map` | World map | `adventurous strings and lute, hopeful but wary, 100BPM` |
| 3 | `base` | Bastion hub | `warm tavern strings, crackling fire ambience, calm 80BPM` |
| 4 | `battle` | Normal battle | `driving war drums, staccato strings, brass stabs, tense 130BPM` |
| 5 | `boss` | Boss / final | `massive taiko drums, choir chants, pipe organ, epic 140BPM` |
| 6 | `victory` | Victory stinger | `triumphant brass fanfare resolving to soft strings, 12 seconds` |
| 7 | `defeat` | Defeat stinger | `mournful solo cello descending, 10 seconds` |
| 8 | `ending` | Ending screen | `full orchestra + choir, bittersweet resolve, 90BPM` |
| 9 | `dungeon` | Blight tunnels | `dripping caves, low drones, teal-crystal shimmer synths, 60BPM` |
| 10 | `town_burn` | Ch.1 Ashes | `burning village, sparse guitar, crying violin, 65BPM` |
| 11 | `forest` | Valenwood | `ancient forest, wooden flutes, birds, dappled strings, 85BPM` |
| 12 | `fortress` | Ironhold/siege | `military snare, iron horns, marching brass, 120BPM` |
| 13 | `court` | Shadow Court | `harpsichord, whispering choir, uneasy minor waltz 3/4` |
| 14 | `sorrow` | Betrayal scene | `rain piano, solo violin, grief, 60BPM` |
| 15 | `hope` | Unity/recruit | `rising strings, dawn chorus, major lift at 0:45, 95BPM` |
| 16 | `blight` | Corruption peak | `dissonant detuned strings, heartbeat drum, dread 70BPM` |
| 17 | `skirmish` | Training | `light percussion practice drums, playful strings, 110BPM` |
| 18 | `primeval` | Superboss | `primal roars as percussion, avalanche brass, 150BPM` |

Loop notes: trim to zero-crossing, 2–4 bar loop minimum; test with loop crossfade
0.5s. Keep each ogg < 4MB (quality ~0.5 vorbis is fine for loop beds).

## 2. SFX (Freesound search terms + fallback synth)

Naming = file name. Already hooked (play automatically): `hit skill_magic miss heal`.

| ID | Use | Freesound search | Bfxr fallback |
|---|---|---|---|
| `hit` | sword/axe impact | `sword hit metal` | square, short decay |
| `skill_magic` | generic spell | `magic fireball whoosh` | saw sweep up |
| `miss` | evade | `whoosh dodge` | noise hp sweep |
| `heal` | heal/buff | `heal chime sparkle` | sine arp up |
| `bow` | arrow release | `bow release arrow` | – |
| `step` | unit move tick | `footstep grass` (quiet, 0.15s) | – |
| `ui` | menu click | `ui click blip` | square blip |
| `gold` | loot/shop | `coin pickup` | sine coin arp |
| `levelup` | level fanfare | `level up fanfare` | – |
| `talk` | recruit success | `quest complete chime` | – |
| `stun` | stun thud | `dizzy bonk` | – |
| `explosion` | bomb/meteor | `explosion boom` | noise boom |
| `ice` | blizzard | `ice shatter` | noise lp crackle |
| `bolt` | thunder | `thunder crack` | noise burst |
| `dark` | void/umbral | `dark pulse` | saw sweep down |
| `holy` | holy/seraph | `holy choir hit` | – |
| `death` | unit falls | `body fall thud` | – |
| `revive` | phoenix down | `phoenix rebirth shimmer` | – |
| `blight` | corruption tick | `toxic bubble` | – |
| `victory` | (uses music stinger) | – | – |

License rule: only CC0 / CC-BY (credit in Credits screen, M6 polish) / direct synth.
Normalize to -12 LUFS-ish, mono, strip silence at head (<5ms).

## 3. Wiring more SFX (one line each, on request)

`FindAnyObjectByType<Garganta.Audio.AudioManager>()?.PlaySfx("<id>")` from:
bow (UnitMovement arrival), ui (ActionMenu buttons), gold (AwardVictory loot),
levelup (GainXP true), talk/recruit (Recruit branch), stun, explosion (Bomb),
ice/bolt/dark/holy per skill id, death (TakeDamage death), revive, blight
(corruption tier-up). Say the word and I wire a batch.

## 4. Unity import settings

- Music ogg: Load Type Streaming, Vorbis, Quality 50, Preload off.
- SFX wav: Load Type Decompress on Load, Preload on, 3D off (2D game).
- Keep `Assets/Resources/Audio/` out of version control bloat: ogg/wav ARE
  committed (small), but never commit `.wav` > 5MB — convert to ogg.

## 5. Commission route (if budget opens)

Track $100–500, SFX pack $50–200, full OST 15–20 tracks $2000–5000.
Give the composer this file + `docs/PRD.md §3` (factions/moods) + 3 reference
tracks (Octopath battle, FFT formation screen, Fire Emblem map theme).
