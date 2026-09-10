"""Procedural placeholder audio for Garganta Revelation (M6 temp assets).
User will replace with Suno/Freesound files later. Stdlib only -> WAV 44.1k mono."""
import math, os, random, struct, wave

SR = 44100
random.seed(7)
OUT = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out")
os.makedirs(OUT, exist_ok=True)

def midi(m):
    return 440.0 * (2.0 ** ((m - 69) / 12.0))

def adsr(n, a, r):
    env = [0.0] * n
    ai, ri = max(1, int(a * SR)), max(1, int(r * SR))
    for i in range(n):
        v = 1.0
        if i < ai:
            v = i / ai
        if i > n - ri:
            v = min(v, (n - i) / ri)
        env[i] = v
    return env

def tone(freq, dur, kind="sine", vol=0.5, a=0.01, r=0.08, slide_to=None, vib=0.0):
    n = int(dur * SR)
    out = [0.0] * n
    ph = 0.0
    env = adsr(n, a, r)
    for i in range(n):
        k = i / n
        f = freq + (slide_to - freq) * k if slide_to else freq
        if vib:
            f *= 1.0 + vib * math.sin(2 * math.pi * 5.5 * i / SR)
        ph += 2 * math.pi * f / SR
        if kind == "sine":
            v = math.sin(ph)
        elif kind == "square":
            v = 1.0 if math.sin(ph) > 0 else -1.0
            v *= 0.6
        elif kind == "saw":
            v = 2.0 * ((ph / (2 * math.pi)) % 1.0) - 1.0
            v *= 0.5
        else:  # triangle
            v = 2.0 * abs(2.0 * ((ph / (2 * math.pi)) % 1.0) - 1.0) - 1.0
            v *= 0.7
        out[i] = v * vol * env[i]
    return out

def noise(dur, vol=0.5, a=0.005, r=0.05, lowpass=0.0):
    n = int(dur * SR)
    out, last = [0.0] * n, 0.0
    env = adsr(n, a, r)
    for i in range(n):
        w = random.uniform(-1, 1)
        if lowpass > 0:
            last += lowpass * (w - last)
            w = last
        out[i] = w * vol * env[i]
    return out

def mix(*tracks):
    n = max(len(t) for t in tracks)
    out = [0.0] * n
    for t in tracks:
        for i, v in enumerate(t):
            out[i] += v
    peak = max(1e-6, max(abs(v) for v in out))
    g = min(1.0, 0.89 / peak)
    return [v * g for v in out]

def place(buf, track, at):
    n = int(at * SR)
    if len(buf) < n + len(track):
        buf += [0.0] * (n + len(track) - len(buf))
    for i, v in enumerate(track):
        buf[n + i] += v
    return buf

def edge(buf, ms=12):
    k = int(ms / 1000 * SR)
    for i in range(min(k, len(buf))):
        buf[i] *= i / k
        buf[-1 - i] *= i / k
    return buf

def save(name, samples):
    samples = edge(mix(samples))
    path = os.path.join(OUT, name)
    with wave.open(path, "wb") as w:
        w.setnchannels(1)
        w.setsampwidth(2)
        w.setframerate(SR)
        w.writeframes(struct.pack("<%dh" % len(samples),
                                  *[max(-32768, min(32767, int(v * 32767))) for v in samples]))
    print("wrote", name, "%.1fs" % (len(samples) / SR))

def pad_chord(midis, dur, vol=0.16, kind="sine", a=1.0):
    parts = [tone(midi(m), dur, kind, vol, a=a, r=min(1.5, dur / 2)) for m in midis]
    return mix(*parts)

def arp(midis, start, step, dur, kind="triangle", vol=0.22):
    buf = []
    t = start
    i = 0
    while t < start + dur:
        buf = place(buf, tone(midi(midis[i % len(midis)]), step * 1.8, kind, vol, a=0.005, r=0.09), t)
        t += step
        i += 1
    return buf

def kick(at_buf=None, vol=0.7):
    return tone(110, 0.28, "sine", vol, a=0.002, r=0.24, slide_to=38)

def drum_pattern(bpm, bars, heavy=False):
    beat = 60.0 / bpm
    total = beat * 4 * bars
    buf = []
    steps = bars * 8
    for s in range(steps):
        t = s * beat / 2
        if s % 4 == 0:
            buf = place(buf, kick(), t)
        if heavy and s % 8 == 6:
            buf = place(buf, kick(), t)
        if s % 4 == 2:
            buf = place(buf, noise(0.09, 0.25, r=0.08), t)
        buf = place(buf, noise(0.03, 0.06, r=0.025), t + beat / 4)
    return buf, total

# ---------------- MUSIC ----------------
# title: Am F C G pads + arp, 16s
chords = [[57, 60, 64], [53, 57, 60], [48, 55, 64], [55, 59, 62]]
song = []
for ch in chords:
    song = place(song, pad_chord(ch, 4.2, a=1.2), len(song) / SR)
song = mix(song, arp([69, 72, 76, 79, 76, 72], 0, 0.5, 16, "triangle", 0.10))
save("music_title.wav", song)

# map: Dm Bb F C plucks, 8s
song = []
prog = [[50, 53, 57], [46, 50, 53], [41, 45, 48], [43, 47, 50]]
for ch in prog:
    song = place(song, pad_chord(ch, 2.1, vol=0.10, kind="triangle", a=0.4), len(song) / SR)
song = mix(song, arp([62, 65, 69, 65, 62, 58, 62, 65], 0, 0.25, 8, "triangle", 0.14))
save("music_map.wav", song)

# base: warm C Am F G pads, 12s
song = []
for ch in [[48, 52, 55], [45, 52, 57], [41, 48, 53], [43, 47, 50]]:
    song = place(song, pad_chord(ch, 3.2, vol=0.14, a=1.5), len(song) / SR)
save("music_base.wav", song)

# battle: Dm drive 140bpm
dr, total = drum_pattern(140, 4)
bass = []
for s in range(32):
    bass = place(bass, tone(midi(38), 0.16, "triangle", 0.30, a=0.004, r=0.1), s * (60 / 140) / 2)
stabs = []
for b in range(4):
    for off in (0.5, 1.5, 2.5, 3.25):
        stabs = place(stabs, mix(*[tone(midi(m), 0.14, "saw", 0.10, a=0.004, r=0.12) for m in (62, 65, 69)]),
                      b * (60 / 140) * 4 + off * (60 / 140))
save("music_battle.wav", mix(dr, bass, stabs))

# boss: E root 150bpm, heavier
dr, total = drum_pattern(150, 4, heavy=True)
bass = []
for s in range(32):
    bass = place(bass, tone(midi(40 if s % 2 == 0 else 38), 0.15, "saw", 0.20, a=0.004, r=0.1), s * (60 / 150) / 2)
choir = pad_chord([52, 55, 59], total, vol=0.07, kind="saw", a=1.0)
save("music_boss.wav", mix(dr, bass, choir))

# victory: C major fanfare arp
song = arp([72, 76, 79, 84, 88], 0, 0.22, 1.4, "triangle", 0.25)
song = place(song, pad_chord([48, 52, 55, 60], 2.5, vol=0.12, a=0.3), 1.0)
save("music_victory.wav", song)

# defeat: descending cello
song = []
for i, m in enumerate([69, 67, 65, 64]):
    song = place(song, tone(midi(m), 1.3, "triangle", 0.30, a=0.15, r=0.7, vib=0.004), i * 1.25)
song = place(song, pad_chord([45, 52], 5.5, vol=0.10, a=1.0), 0)
save("music_defeat.wav", song)

# ending: Am F C G + shimmer, 12s
song = []
for ch in chords:
    song = place(song, pad_chord(ch, 3.2, a=1.2), len(song) / SR)
song = mix(song, arp([69, 72, 76, 79, 84, 79, 76, 72], 0, 0.375, 12, "sine", 0.10))
save("music_ending.wav", song)

# ---------------- SFX ----------------
save("sfx_hit.wav", mix(noise(0.12, 0.5, r=0.1, lowpass=0.4), tone(90, 0.15, "sine", 0.6, a=0.002, r=0.13, slide_to=45)))
save("sfx_skill_magic.wav", mix(tone(300, 0.5, "saw", 0.25, a=0.05, r=0.3, slide_to=1200), noise(0.4, 0.12, a=0.1, r=0.3)))
save("sfx_miss.wav", noise(0.28, 0.3, a=0.08, r=0.2))
save("sfx_heal.wav", mix(*[place([], tone(midi(m), 0.4, "sine", 0.3, a=0.01, r=0.35), i * 0.12) for i, m in enumerate([72, 76, 79, 84])]))
save("sfx_ui.wav", tone(880, 0.07, "square", 0.25, a=0.003, r=0.05))
save("sfx_gold.wav", mix(tone(1320, 0.12, "sine", 0.3, a=0.003, r=0.1), place([], tone(1760, 0.2, "sine", 0.3, a=0.003, r=0.18), 0.09)))
save("sfx_levelup.wav", mix(*[place([], tone(midi(m), 0.35, "triangle", 0.3, a=0.005, r=0.3), i * 0.13) for i, m in enumerate([72, 76, 79, 84, 88])]))
save("sfx_explosion.wav", mix(noise(1.0, 0.6, a=0.005, r=0.9, lowpass=0.15), tone(70, 0.7, "sine", 0.5, a=0.003, r=0.6, slide_to=30)))
save("sfx_bow.wav", mix(tone(220, 0.1, "triangle", 0.3, a=0.002, r=0.08, slide_to=440), noise(0.2, 0.15, a=0.02, r=0.15)))
save("sfx_step.wav", noise(0.07, 0.2, a=0.003, r=0.05, lowpass=0.5))
# batch 2 (M6 hooks)
save("sfx_talk.wav", mix(tone(660, 0.25, "sine", 0.3, a=0.01, r=0.2), place([], tone(990, 0.3, "sine", 0.3, a=0.01, r=0.25), 0.12)))
save("sfx_death.wav", mix(tone(300, 0.5, "saw", 0.3, a=0.005, r=0.4, slide_to=60), noise(0.3, 0.25, a=0.01, r=0.25, lowpass=0.3)))
save("sfx_revive.wav", mix(*[place([], tone(m, 0.3, "sine", 0.28, a=0.01, r=0.25), i * 0.09) for i, m in enumerate([880, 1174, 1568, 2093])]))
save("sfx_stun.wav", mix(tone(150, 0.15, "square", 0.35, a=0.003, r=0.12), noise(0.1, 0.2, a=0.003, r=0.08)))
print("DONE")
