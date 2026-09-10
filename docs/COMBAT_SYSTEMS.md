# Combat Systems — Garganta Revelation

## 1. Grid & Terrain

Grid isometric hex-offset, size 8x8 sampai 16x16.

| Tile | Move Cost | DEF Bonus | Special |
|---|---|---|---|
| Plains | 1 | +0 | - |
| Forest | 2 | +2 | block ranged |
| Mountain | 3 | +4 | high ground bonus |
| Water | X | - | impassable |
| Ruins | 1 | +1 | Aether veins heal |
| Blight | 1 | -2 | corruption per turn |
| Wall | X | - | impassable |
| Bridge | 1 | +0 | choke point |

Elevation 3 level: Low 0, Mid 1, High 2.
Attack bawah->atas: -10% acc -10% dmg.
Attack atas->bawah: +10% acc +10% dmg.
Ranged dari high: +20% dmg.

## 2. CTB Turn System

```
CTB += Speed * (1 + buff - debuff)
if CTB >= 100 -> act, CTB = 0
```

Display: horizontal bar atas, portrait urut.
Instant skill bisa nyela giliran.

Contoh:
SPD 5 vs SPD 10 -> unit cepat act 2x lebih sering.
Haste +50% -> increment 1.5x.
Slow -50% -> increment 0.5x.

## 3. Damage Formula

```
Base Physical = ATK * Weapon_Mult * Class_Mult - DEF * 0.5
Base Magical  = MAG * Spell_Mult - MDEF * 0.5
True = fixed ignore defense

Final = Base * Triangle * Element * Elevation * Crit * Variance * Corruption

Triangle: advantage 1.1, disadvantage 0.9, netral 1.0
Element: 0.8 - 1.3 sesuai weakness
Elevation: 0.9 / 1.0 / 1.1
Crit: 1.5x jika roll < CritRate
Variance: random 0.9 - 1.1
Corruption ATK bonus 1.1 - 1.3 tapi DEF turun
```

Hit rate:
```
Acc = Attacker_Acc - Defender_Eva + Triangle_Bonus + Elevation_Bonus
Clamp 20% - 99%
```

Tipe damage: Physical reduce DEF, Magical reduce MDEF, True ignore.

## 4. Weapon Triangle

```
Sword > Axe > Spear > Sword (1.1x / 0.9x)
Bow > Staff > Bow
Dagger netral tapi first strike + high crit
Tome vs Staff: advantage sesuai elemen
```

## 5. AI

Tipe behavior:
- Aggressive: kejar nearest, attack jika range
- Defensive: hold position, attack jika masuk range
- Support: heal / buff ally terendah HP
- Skirmisher: hit and run, jaga jarak
- Boss: phased, summon, AoE優先

Tactics:
1. Cari target lowest HP dalam move+range
2. Pilih tile dengan DEF tertinggi yang masih bisa attack
3. Prioritas healer / mage musuh jika dalam jangkauan
4. Mundur jika HP < 25% kecuali aggressive / boss

## 6. Victory / Defeat

Victory: semua musuh mati / boss mati / objective (survive X turn, escape, defend).
Defeat: semua player mati / protagonist mati di Classic / turn limit habis.
Result: XP, loot roll, mastery, bond, corruption check.

## 7. Mobile vs PC Input

Mobile: tap select, tap target confirm, pinch zoom, swipe pan, long-press detail.
PC: left select, right cancel, WASD pan, wheel zoom, space wait, tab cycle unit.
Min tap target 44px.
