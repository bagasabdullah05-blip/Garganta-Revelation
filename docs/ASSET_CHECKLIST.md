# Asset Checklist — Garganta Revelation

Centang `[x]` jika aset sudah ada / sudah dikirim. File ini acuan apa saja yang perlu kamu kirim agar bisa saya integrasikan ke Unity.

## 0. Spesifikasi Teknis (berlaku semua aset)

- [ ] Format: PNG transparan (karakter, item, UI), PNG/JPG (background)
- [ ] Pixel art: tanpa anti-aliasing blur, warna solid
- [ ] Penamaan: `tipe_nama_aksi_arah.png` contoh `char_kael_walk_down.png`
- [ ] Ukuran konsisten per kategori (lihat tiap seksi)
- [ ] Origin/pivot: kaki tengah untuk karakter, center untuk tile isometric

## 1. Karakter Playable (15)

Per karakter butuh:

- [ ] Sprite sheet: 6 arah (bawah, atas, kiri, kanan, atas-kiri, atas-kanan)
- [ ] Animasi per arah: idle (2f), walk (4f), attack (4-6f), skill (6f), hit (2f), death (4f)
- [ ] Ukuran frame: 64x64 (disarankan, sebutkan jika beda)
- [ ] Portrait: 256x256, 5 ekspresi (normal, battle, sad, happy, corruption)
- [ ] Icon kecil 48x48 untuk turn order bar

Daftar:

- [ ] 01 Kael (Squire -> Paladin -> Holy Knight)
- [ ] 02 Briar (Warrior)
- [ ] 03 Sera (Acolyte -> White Mage -> Seraph)
- [ ] 04 Voss (Archer)
- [ ] 05 Lyra (Mage -> Black Mage)
- [ ] 06 Renn (Thief -> Assassin)
- [ ] 07 Dawn (Paladin)
- [ ] 08 Korr (Spearman -> Dragoon)
- [ ] 09 Zara (Rune Knight)
- [ ] 10 Asha (Ranger, hidden)
- [ ] 11 Vael (Dragoon, hidden)
- [ ] 12 Thorne (Black Mage anti-hero)
- [ ] 13 Nyx (Assassin)
- [ ] 14 Eos (White Mage)
- [ ] 15 Grim (Berserker)

**Minimal prototype (bisa jalan dulu):** Kael, Briar, Sera, Voss + 1 musuh.

## 2. Musuh

- [ ] Common (12): Bandit, Goblin, Wolf, Skeleton, Zombie, Slime, Bat, Spider, Cultist, Rogue Knight, Imp, Orc
- [ ] Tiap common: 4 arah, idle + attack + hit + death (2-4 frame cukup)
- [ ] Elite (10): Dark Knight, Dragon, Wyvern, Archmage, Lich, Golem, Chimera, Banshee, Shadow Beast, Blight Walker
- [ ] Boss (6): Garrick, Marcus, Vex, Corruptor, Usurper, Primeval Dragon
- [ ] Boss besar (Dragon/Golem): frame 128x128, ukuran 2x2 tile
- [ ] Minimal prototype: Bandit, Skeleton, Slime saja

## 3. Tiles & Map

- [ ] Base tile isometric 64x32 (diamond)
- [ ] Tipe: Plains, Forest, Mountain, Water, Ruins, Blight, Wall, Bridge
- [ ] Tiap tipe: base + edge 4 arah + corner + transisi
- [ ] Varian: Forest (single/double/dense), Mountain (small/large/cliff + snow)
- [ ] Water animasi 4-frame ripple
- [ ] Blight: glow teal pulsing + mist
- [ ] Props: pohon, batu, reruntuhan, tenda, spanduk faksi, stalaktit
- [ ] Minimal prototype: 1 set Plains + Wall + 1 dekor pohon/batu

## 4. UI

- [ ] Background main menu (1920x1080)
- [ ] Logo/title "Garganta Revelation"
- [ ] Buttons: New, Continue, Load, Settings, Credits (normal + hover)
- [ ] Panel: unit info, action menu, inventory, equipment, dialogue box
- [ ] Health bar, MP bar, CTB gauge
- [ ] Ikon aksi: Attack, Skill, Item, Wait
- [ ] Minimap frame
- [ ] Font pixel yang dipakai (TTF/OTF + lisensi)
- [ ] Minimal prototype: 1 button, 1 panel, HP bar saja

## 5. Skill & Item Icons

- [ ] Ikon skill: 112 (bisa bertahap, minimal 10 untuk prototype)
- [ ] Ukuran: 48x48 PNG
- [ ] Ikon equipment: senjata (38), armor (9), helm (6), aksesoris (11)
- [ ] Ikon consumable: Potion, Hi-Potion, Elixir, Ether, Antidote, Phoenix Down, Tent, Aether Drop, Bomb, Smoke Bomb
- [ ] Minimal prototype: 4 ikon skill + Potion + Phoenix Down

## 6. VFX (boleh dari kode dulu jika belum ada)

- [ ] Fire, Ice, Lightning, Heal, Holy, Dark, Blight (sprite sheet / particle PNG)
- [ ] Slash arc, impact burst, arrow trail
- [ ] Damage numbers font (atau pakai TextMeshPro dulu)
- [ ] Screen transition / fade

## 7. Audio (opsional tahap visual, tapi catat)

- [ ] BGM: main theme, battle, boss, town, world map, victory, defeat (OGG)
- [ ] SFX: slash, bow, cast, heal, hit, crit, miss, death, menu select/confirm/cancel, footstep
- [ ] Minimal prototype: boleh silent dulu

## 8. Cara Kirim ke Saya

1. ZIP per kategori: `chars.zip`, `enemies.zip`, `tiles.zip`, `ui.zip`, `icons.zip`
2. Sertakan `info.txt` tiap ZIP berisi: ukuran frame, jumlah frame per animasi, arah yang tersedia
3. Upload / lampirkan file di chat ini
4. Saya masukkan ke `Assets/Sprites/...`, buatkan prefab + animator + tilemap, update checklist ini jadi `[x]`, lalu push ke GitHub

## 9. Status Integrasi (diisi saya)

- [ ] Folder `Assets/` dibuat di repo
- [ ] Sprite karakter masuk + prefab unit
- [ ] Tileset masuk + tilemap test scene
- [ ] UI masuk + HUD jalan
- [ ] Icons masuk + action menu tampil
- [ ] Test build PC jalan
