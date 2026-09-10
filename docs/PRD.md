# Product Requirements Document — Garganta Revelation

## 1. Executive Summary

| Field | Detail |
|---|---|
| **Title** | Garganta Revelation |
| **Genre** | Turn-Based Tactical RPG |
| **Platform** | PC (Steam) + Mobile (Android/iOS) |
| **Engine** | Unity 2D |
| **Target Audience** | Fans FFT, Fire Emblem, SRPG enthusiasts, 18-35 |
| **Tone** | Dark Fantasy, morally grey, political intrigue |
| **Estimasi Main** | 40-60 jam (story), 80+ jam (100%) |
| **Monetization** | Premium (one-time purchase) |
| **Art Style** | 2D Pixel Art (HD-2D inspired) |

## 2. Unique Selling Points

1. **Hybrid Grid System** — Isometric dengan zoom control, depth FE + visual FFT
2. **Aether Corruption System** — Units bisa terinfeksi, memberi risk/reward
3. **Deep Job System** — 16 kelas dengan 3 tier progression
4. **Faction Reputation** — Pilihan cerita menentukan ending (5 endings)
5. **Meaningful Permadeath** — Setiap karakter punya story, mati = cerita hilang
6. **Recruitment Variety** — 5 cara rekrut karakter berbeda

## 3. World Lore

### Premise

Dunia **Garganta** hidup dari energi **Aether** — energi primordial yang mengalir di veins tanah. Selama ribuan tahun, 5 kerajaan hidup damai berkat Aether. Tapi 10 tahun lalu, **The Blight** muncul — wabah yang mengubah Aether yang murni menjadi toksik, mengubah makhluk menjadi monster, dan membunuh tanah.

Setiap kerajaan menyalahkan yang lain. Perang diam-diam mulai terjadi. Di tengah konflik ini, protagonist kita — seorang **Ashwalker** (manusia yang selamat dari Blight) — menemukan rahasia bahwa The Blight bukan alami, tapi diciptakan.

### 5 Factions

| Faction | Philosophy | Color | Capital |
|---|---|---|---|
| **Holy Dominion** | Blight harus dibasmi via perang suci | Gold/White | Sanctum |
| **Ironhold Federation** | Militer kuat = survival | Grey/Red | Citadel |
| **Valenwood Concord** | Alam harus dilindungi, Blight = balasan | Green/Brown | Elderwood |
| **Shadow Court** | Kontrol Aether = kontrol dunia | Purple/Black | Obsidian |
| **Garganta Alliance** | Semua faksi harus bersatu | Ash Blue | The Bastion |

## 4. Gameplay Systems

### 4.1 Grid Combat

**Grid Type**: Isometric (hex-offset coordinates)
**Grid Size**: 8x8 hingga 16x16 tergantung encounter

**Tile Types**:

| Tile | Movement Cost | Defense Bonus | Special |
|---|---|---|---|
| Plains | 1 | +0 | - |
| Forest | 2 | +2 | Blocks ranged |
| Mountain | 3 | +4 | High ground bonus |
| Water | Impassable | - | - |
| Ruins | 1 | +1 | Aether veins (heal) |
| Blight | 1 | -2 | Corruption per turn |
| Wall | Impassable | - | - |
| Bridge | 1 | +0 | Choke point |

**Elevation System**:
- 3 levels: Low (0), Mid (1), High (2)
- Attacking downward: +10% accuracy, +10% damage
- Attacking upward: -10% accuracy, -10% damage
- Shooting from high: +20% damage

### 4.2 Turn System (CTB — Charge Turn Battle)

```
CTB Gauge += Speed * (1 + Buff/Debuff modifiers)
When gauge >= 100 → Unit takes turn, gauge resets
Higher Speed = More turns over time
```

**Turn Order Display**: Horizontal bar di atas layar, urut dari paling depan

**Interrupt**: Beberapa skill bisa "instant" — mengambil giliran saat ini juga

### 4.3 Weapon Triangle

```
Sword → Axe → Spear → Sword
Bow → Staff → Bow
```

### 4.4 Aether Corruption System

**Corruption Level**: 0% → 100%

| Level | Effect |
|---|---|
| 0-25% | Normal |
| 25-50% | +10% ATK, -10% DEF (Berserk advantage) |
| 50-75% | +20% ATK, -20% DEF, 10% chance lose turn |
| 75-99% | +30% ATK, random attacks allies, 30% chance lose turn |
| 100% | Unit becomes Blight monster (PERMADEATH) |

**Management**: Heal di Chapel, item khusus, White Mage cleanse

### 4.5 Relationship/Support System

**Bond Points**: +1 per battle bersama, +3 jika dalam support range

| Level | Bond Required | Unlock |
|---|---|---|
| C | 20 | HP+5%, ATK+3% |
| B | 50 | Support conversation + HP+10%, ATK+5% |
| A | 100 | Synergy skill (combo attack) + HP+15%, ATK+8% |
| S | 200 | Dual attack (150% damage combo) + unique ending |

### 4.6 Recruitment System

| Method | How | Example |
|---|---|---|
| **Story Auto** | Scripted join | Protagonist, first companion |
| **Battle Recruit** | Use "Talk" skill when enemy HP < 30% | Enemies with blue glow |
| **Side Quest** | Complete optional quest | Save a village → villager joins |
| **Faction Choice** | Pick one of two characters | Political dilemma |
| **Hidden** | Secret condition met | Win 10 battles without casualty |

## 5. Story Structure

### Act I — The Gathering Storm (Ch 1-4, ~10 jam)

| Ch | Title | Key Events | Recruits | Battles |
|---|---|---|---|---|
| 1 | **Ashes** | Village destroyed, protagonist survives | Kael (Squire) | 1 tutorial |
| 2 | **The Road** | Meet first companion, travel to Bastion | Briar (Warrior) | 2 |
| 3 | **The Bastion** | Join resistance, base opens | Sera (Healer) | 1 + recruitment |
| 4 | **First Blood** | First real battle, moral choice | Voss (Archer, optional) | 2 + choice |

### Act II — The War Begins (Ch 5-8, ~15 jam)

| Ch | Title | Key Events | Recruits | Battles |
|---|---|---|---|---|
| 5 | **Iron & Wood** | Alliance with Valenwood, tension with Ironhold | Dawn (Paladin) | 3 |
| 6 | **Betrayal** | Mole revealed, major character dies (or saved if S-rank) | Korr (Spearman) | 2 |
| 7 | **Underground** | Underground resistance, Blight depths | Renn (Thief) | 3 |
| 8 | **Counter** | Major counter-attack, siege battle | Zara (Rune Knight) | 3 |

### Act III — The Darkest Hour (Ch 9-12, ~15 jam)

| Ch | Title | Key Events | Recruits | Battles |
|---|---|---|---|---|
| 9 | **All-Out War** | 3-front battle, massive scale | Asha (Hidden) + Vael (Hidden) | 4 |
| 10 | **Blight Heart** | Journey to Blight source, corruption peak | Thorne (Anti-hero) + Nyx (Faction choice) | 3 |
| 11 | **Unity** | All factions unite, final preparation | Eos (White Mage) + Grim (Berserker) | 2 |
| 12 | **Final Stand** | Multi-phase final battle, 3 endings | - | 1 epic battle |

### Epilogue
- Character endings based on bond levels
- Faction ending based on reputation
- Post-game: Super bosses, secret dungeon, NG+

## 6. Endings (5)

1. **Light Ending** — Blight purified, all factions unite
2. **Shadow Ending** — Shadow Court wins, protagonist rules
3. **Sacrifice Ending** — Protagonist sacrifice to seal Blight
4. **Blight Ending** — Protagonist becomes Blight lord (bad ending)
5. **True Ending** — Unlocked only with 100% recruitment + all S-rank bonds

## 7. UI/UX Design

### Battle HUD

```
┌──────────────────────────────────────────────────┐
│  [Turn Order Bar]                                 │
│  [P1] [P2] [E1] [P3] [E2] [P4] ...             │
├──────────────────────────────────────────────────┤
│              ┌──── ISOMETRIC GRID ────┐           │
│              │    [Units on tiles]    │           │
│              └────────────────────────┘           │
├──────────────────────────────────────────────────┤
│  [Unit Info]  [Action Menu]  [Mini Map]           │
│  [Phase: Player Turn]                             │
└──────────────────────────────────────────────────┘
```

### Controls

**Mobile**: Tap to select, pinch to zoom, swipe to pan, long press for details
**PC**: Left click select, right click cancel, WASD pan, mouse wheel zoom, space end turn

## 8. Difficulty System

| Mode | Enemy Stats | XP Gain | Recruitment | Permadeath |
|---|---|---|---|---|
| **Story** | -20% | +20% | All available | Off |
| **Normal** | Base | Base | Normal | Optional |
| **Hard** | +20% | -20% | Some missable | On by default |
| **Nightmare** | +50% | -40% | Strict conditions | Always on |

## 9. Technical Requirements

### Architecture

```
Design Patterns:
  - Singleton: GameManager, AudioManager
  - Observer: EventBus (decouple systems)
  - State Machine: Unit states, Game states
  - Object Pool: Projectiles, damage numbers
  - Strategy: AI behaviors
  - Factory: Unit creation
```

### Performance Targets

| Platform | FPS | RAM | Build Size |
|---|---|---|---|
| PC (Low-end) | 60 fps | <2GB | <500MB |
| Mobile (Mid) | 30 fps | <1.5GB | <300MB |
| Mobile (Low) | 30 fps | <1GB | <200MB |

### Key Unity Packages

```
com.unity.2d.tilemap
com.unity.2d.tilemap.extras
com.unity.inputsystem
com.unity.cinemachine
com.unity.textmeshpro
com.unity.addressables
```

## 10. Development Milestones

| Milestone | Goal | Duration |
|---|---|---|
| **M1 — Prototype** | Mainable grid combat | Minggu 1-3 |
| **M2 — Core** | Full combat system | Bulan 1-2 |
| **M3 — World** | Playable Chapter 1-2 | Bulan 3-4 |
| **M4 — Content** | All 12 chapters playable | Bulan 5-8 |
| **M5 — Production** | Polish & art integration | Bulan 9-11 |
| **M6 — Launch** | Release ready | Bulan 12 |

## 11. Risk Assessment

| Risk | Impact | Likelihood | Mitigation |
|---|---|---|---|
| Scope creep | High | High | Strict adherence to milestones |
| Art asset delays | Medium | Medium | Use placeholder first, art last |
| Mobile performance | High | Medium | Profile early, optimize often |
| Balancing too hard | Medium | Medium | Playtest at each milestone |
| Burnout | High | Medium | Sustainable pace, celebrate small wins |

## 12. Content Summary

| Content | Quantity |
|---|---|
| Playable Characters | 15 |
| Classes (total) | 16 (6 base + 6 advanced + 4 master) |
| Skills | ~112 |
| Weapons | 38 |
| Armor | 9 |
| Helmets | 6 |
| Accessories | 11 |
| Consumables | 10 |
| Total Equipment | ~74 |
| Enemy Types | 30+ |
| Bosses | 6+ (multi-phase) |
| Maps | 30+ |
| Chapters | 12 + epilogue |
| Side Quests | 15-20 |
| Endings | 5 |
| Music Tracks | 15-20 |
| SFX | 100+ |

## 13. Budget Summary

| Category | Approach | Est. Cost |
|---|---|---|
| Engine | Unity Personal (free) | $0 |
| Art (placeholder) | Code-generated | $0 |
| Art (alpha) | Free assets | $0 |
| Art (production) | Commissioned | $3,000-5,000 |
| Music | AI-generated (Suno) | $0-50 |
| SFX | Free (Freesound) | $0 |
| Tools | Aseprite, Reaper | $80 |
| **TOTAL** | | **$3,080-5,630** |

## 14. Success Metrics

| Metric | Target |
|---|---|
| Prototype playable | Week 3 |
| Chapter 1 complete | Month 2 |
| Steam demo release | Month 6 |
| Full game beta | Month 10 |
| 1.0 Launch | Month 12 |
| Steam rating | 80%+ positive |
| Mobile rating | 4.5+ stars |
