# Roadmap — Garganta Revelation (revisi: prototype → visual dulu)

Status 10 Sep 2026: **P0 selesai** — semua sistem playable Title→ending + 99 tests.
Visual masih placeholder prosedural. Mulai sekarang: art dulu, baru tuning.

## P0 — PROTOTYPE ✅ DONE (commit `df17885`)

Grid/CTB/combat, 6+6+4 jobs, 74 equipment, 15 chars, 12 chapters, bonds,
recruit, corruption, reputation, 5 endings, NG+, skirmish, superboss,
crafting, difficulty, save 3 slot, musik/SFX temp, 99 tests hijau.

## P1 — VISUAL FOUNDATION (sekarang) 🎯

**Goal: main Title → Ch.2b tanpa SATU PUN placeholder di layar.**
Scope = semua yang terlihat di ~2 jam pertama. Total **±56 file**
(rincian di `docs/prompts/phase1/`):

| Kelompok | Isi | File |
|---|---|---|
| Heroes | Kael, Briar, Sera, Voss (sprite + portrait) | 8 |
| Enemies | Bandit, Goblin, Wolf, Skeleton, Cultist | 5 |
| Tiles | 7 diamond (+frame 2 water/blight) + tree/rock/wall/tuft/shadow | 14 |
| Background | title, world-map, bastion | 3 |
| Icons | 11 skill + 8 consumable + 8 gear | 27 |

Exit criteria:
- [ ] 56 file di `Assets/Resources/Art/` dengan nama exact (§4 ART_TASKS)
- [ ] Play Ch.1→Ch.2b: tidak ada kotak prosedural tersisa
- [ ] Batchmode import bersih + 99 tests tetap hijau
- [ ] Ikon tampil di UI (wiring kecil, lihat P1 task list di bawah)

P1 tasks (code, kecil): tampilkan `icon_<id>` di ActionMenu/ItemMenu/skill list;
tampilkan `portrait_<Nama>` di DialogueUI; `bg_title/bg_map/bg_base` di
Title/WorldMap/Base. Estimasi ±150 baris, tanpa ubah sistem.

## P2 — FEEL & BALANCE

Animasi code-only (idle bob, lunge attack, hit flash, damage pop yang sudah ada),
sisa wiring SFX (§3 AUDIO_BRIEF), playtest tuning XP/job-curve/boss,
bugfix dari playthrough manusia Ch.1→ending.

## P3 — CONTENT ART

Sisa cast Ch.3–Ch.11 (11 heroes + 8 enemies + 3 boss + 11 portrait) ikut tabel
§2 ART_TASKS (P2–P4). Prompt menyusul di `docs/prompts/phase2/` (template sama).

## P4 — LAUNCH

Musik final (timpa ogg), SFX final, mobile profile, lokalisasi EN/ID,
store page + trailer. Zero code wajib (kecuali temuan QA).
