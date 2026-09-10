using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Garganta.Art;
using Garganta.Audio;

public class ArtAudioTests
{
    [Test]
    public void Override_MissReturnsNull()
    {
        ArtOverride.Clear();
        Assert.IsNull(ArtOverride.Get("dia_ABCDEF"));
        Assert.AreEqual(0, ArtOverride.Count);
    }

    [Test]
    public void Override_RegisterAndGet()
    {
        ArtOverride.Clear();
        var tex = new Texture2D(4, 4);
        var s = Sprite.Create(tex, new Rect(0, 0, 4, 4), new Vector2(0.5f, 0.5f), 32f);
        ArtOverride.Register("char_Sword_4A6B8A", s);
        Assert.AreEqual(1, ArtOverride.Count);
        Assert.AreSame(s, ArtOverride.Get("char_Sword_4A6B8A"));
        ArtOverride.Clear();
        Assert.IsNull(ArtOverride.Get("char_Sword_4A6B8A"));
        Object.DestroyImmediate(tex);
    }

    [Test]
    public void Override_LoadAllEmptyIsSafe()
    {
        ArtOverride.Clear();
        ArtOverride.LoadAll(); // no Resources/Art folder in test project: must not throw
        Assert.IsNull(ArtOverride.Get("anything"));
    }

    [Test]
    public void Audio_ClampVol()
    {
        Assert.AreEqual(0f, AudioManager.ClampVol(-1f));
        Assert.AreEqual(1f, AudioManager.ClampVol(2f));
        Assert.AreEqual(0.5f, AudioManager.ClampVol(0.5f));
    }

    [Test]
    public void Audio_MissingClipsStaySilent()
    {
        var go = new GameObject("am");
        try
        {
            var am = go.AddComponent<AudioManager>();
            Assert.DoesNotThrow(() => am.PlaySfx("does_not_exist"));
            Assert.DoesNotThrow(() => am.PlayMusic("does_not_exist"));
            Assert.DoesNotThrow(() => am.StopMusic());
        }
        finally { Object.DestroyImmediate(go); }
    }
}
