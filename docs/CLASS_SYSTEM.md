# Class System — Garganta Revelation

## 1. Leveling

```
Level cap 50 (main), 99 (NG+)
XP Need = floor(100 * Level^1.5)
1->2: 100, 5->6: 559, 10->11: 1662, 20->21: 5238, 30->31: 10435
Total ke 50: ~350k XP
```

Sumber XP:
- kill enemy 50-200 sesuai level diff
- boss 500-1000 fixed
- side quest 200-500
- discovery 25-100
- support bonus +20% jika dekat bonded ally
- class leveling bonus +10%

Scaling:
- enemy level > player: enemy +5% ATK/DEF per level diff
- player > enemy: XP -20% per level diff (anti overgrind)

Stat growth per level:

| Class | HP | ATK | DEF | MAG | MDEF | SPD |
|---|---|---|---|---|---|---|
| Squire | +8 | +2 | +1 | +0 | +0 | +1 |
| Spearman | +9 | +1 | +2 | +0 | +0 | +1 |
| Archer | +6 | +2 | +1 | +0 | +0 | +2 |
| Acolyte | +5 | +0 | +1 | +2 | +2 | +1 |
| Mage | +4 | +0 | +0 | +3 | +1 | +1 |
| Thief | +5 | +1 | +0 | +0 | +0 | +3 |
| Paladin | +10 | +2 | +2 | +1 | +1 | +1 |
| Dragoon | +11 | +2 | +2 | +0 | +0 | +1 |
| Black Mage | +5 | +0 | +0 | +4 | +2 | +1 |
| White Mage | +6 | +0 | +1 | +3 | +3 | +1 |
| Assassin | +6 | +2 | +0 | +0 | +0 | +3 |
| Rune Knight | +8 | +2 | +1 | +2 | +1 | +1 |

Cap: HP 999, ATK/DEF/MAG/MDEF 99, SPD 50.

## 2. Tier 1 Base (6)

| Class | HP | ATK | DEF | MAG | SPD | Weapon | Role |
|---|---|---|---|---|---|---|---|
| Squire | 100 | 12 | 10 | 3 | 5 | Sword | balanced melee |
| Spearman | 110 | 11 | 12 | 3 | 5 | Spear | tank 2-tile |
| Archer | 80 | 13 | 7 | 3 | 7 | Bow | ranged |
| Acolyte | 70 | 5 | 6 | 12 | 6 | Staff | healer |
| Mage | 65 | 4 | 5 | 14 | 8 | Tome | magic dps |
| Thief | 75 | 10 | 6 | 5 | 12 | Dagger | fast utility |

## 3. Tier 2 Advanced (6)

| Class | Requires | Weapon | Special |
|---|---|---|---|
| Paladin | Squire10+Acolyte5 | Sword+Staff | Holy Blade magic dmg + heal |
| Dragoon | Spearman10+Squire5 | Spear | Jump ignore DEF 70% hit |
| Assassin | Thief10+Archer5 | Dagger | Shadow Strike crit pertama |
| Black Mage | Mage10+Archer5 | Tome | Meteor AoE |
| White Mage | Acolyte10+Mage5 | Staff | Reraise auto-revive |
| Rune Knight | Squire10+Mage5 | Sword | Runic Blade absorb reflect |

## 4. Tier 3 Master (4)

| Class | Requires | Special |
|---|---|---|
| Holy Knight | Paladin+Dragoon | Divine Guard protect all 1 turn |
| Spellblade | BlackMage+RuneKnight | Elemental Surge attacks jadi AoE |
| Shadowblade | Assassin+BlackMage | Void Step teleport + attack |
| Seraph | WhiteMage+Paladin | Arch of Light full heal + buff |

## 5. Mastery & Reclass

Mastery 0-100%:
- battle dengan class: +5%
- pakai skill class: +10%
- kill dengan skill class: +15%
- boss kill: +25%
Mastered 100% -> syarat Tier2/3 kebuka.

Reclass di Bastion kapan saja jika mastered:
- keep semua skill lama
- stats recalc growth baru
- appearance ganti

## 6. Skill Trees contoh

Squire:
L1 Power Strike 5MP 1.5x ATK
L3 Shield Bash stun 1 turn
L5 War Cry ATK allies +10%
L7 Counter counter saat hit
L10 Limit Break 3x ATK full MP -> unlock Paladin / RuneKnight

Mage:
L1 Fireball 10MP 1.2x MATK single
L3 Blizzard 1.0x AoE 3x3
L5 Thunder 1.4x partial ignore MDEF
L7 Spell Surge next +50%
L10 Meteor 25MP 2.0x AoE 5x5 -> unlock BlackMage / RuneKnight

Acolyte:
L1 Healing Light 8MP 30% HP ally
L5 Cure cleanse + heal
L10 Reraise 20MP auto-revive 30% -> WhiteMage / Paladin

Thief:
L1 Steal, L3 Stealth 6MP invisible 2 turn,
L7 Shadow Step, L10 Assassinate -> Assassin

Total skill ~112: 60 base (10 per base class),
30 advanced (5 per Tier2), 12 master (3 per Tier3), 10 universal.
