using NUnit.Framework;
using UnityEngine;
using Garganta.Data;

public class DifficultyTests
{
    [Test]
    public void Names_AllFour()
    {
        Assert.AreEqual("Story", GameBalance.Name(0));
        Assert.AreEqual("Normal", GameBalance.Name(1));
        Assert.AreEqual("Hard", GameBalance.Name(2));
        Assert.AreEqual("Nightmare", GameBalance.Name(3));
    }

    [Test]
    public void EnemyMult_Scales()
    {
        Assert.AreEqual(0.8f, GameBalance.EnemyStatMult(0));
        Assert.AreEqual(1f, GameBalance.EnemyStatMult(1));
        Assert.AreEqual(1.2f, GameBalance.EnemyStatMult(2));
        Assert.AreEqual(1.5f, GameBalance.EnemyStatMult(3));
    }

    [Test]
    public void XpMult_Tradeoff()
    {
        Assert.AreEqual(1.2f, GameBalance.XpMult(0));
        Assert.AreEqual(0.6f, GameBalance.XpMult(3));
    }

    [Test]
    public void Permadeath_OffOnStory()
    {
        Assert.IsFalse(GameBalance.Permadeath(0));
        Assert.IsTrue(GameBalance.Permadeath(1));
        Assert.IsTrue(GameBalance.Permadeath(3));
    }

    [Test]
    public void NewGame_StoresDifficulty()
    {
        var before = SaveSystem.Current;
        try
        {
            Assert.AreEqual(2, SaveSystem.NewGame(2).difficulty);
            Assert.AreEqual(0, SaveSystem.NewGame(0).difficulty);
        }
        finally { SaveSystem.Current = before; }
    }
}

public class AudioAssetsTests
{
    [Test]
    public void Music_AllTracksPresent()
    {
        foreach (var id in new[] { "title", "map", "base", "battle", "boss", "victory", "defeat", "ending" })
            Assert.NotNull(Resources.Load<AudioClip>("Audio/Music/" + id), id);
    }

    [Test]
    public void Sfx_AllHookedPresent()
    {
        foreach (var id in new[] { "hit", "skill_magic", "miss", "heal", "bow", "gold", "levelup", "talk", "stun", "explosion", "death", "revive" })
            Assert.NotNull(Resources.Load<AudioClip>("Audio/SFX/" + id), id);
    }
}
