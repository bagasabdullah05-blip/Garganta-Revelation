using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Garganta.Data;

public class SaveTests
{
    int goldBefore;
    GameSave currentBefore;

    [SetUp]
    public void Backup()
    {
        goldBefore = Inventory.Gold;
        currentBefore = SaveSystem.Current;
    }

    [TearDown]
    public void Restore()
    {
        Inventory.Gold = goldBefore;
        SaveSystem.Current = currentBefore;
        SaveSystem.LastSlot = 1;
    }

    [Test]
    public void NewGame_Defaults()
    {
        var s = SaveSystem.NewGame();
        Assert.AreEqual(2, s.roster.Count);
        Assert.AreEqual("Kael", s.roster[0].rosterId);
        Assert.AreEqual(2, s.roster[0].level);
        Assert.AreEqual(200, s.gold);
        Assert.AreEqual(0, s.progress);
    }

    [Test]
    public void Serialize_Roundtrip()
    {
        var s = SaveSystem.NewGame();
        s.progress = 2;
        s.gold = 999;
        var back = SaveSystem.Deserialize(SaveSystem.Serialize(s));
        Assert.AreEqual(2, back.progress);
        Assert.AreEqual(999, back.gold);
        Assert.AreEqual(2, back.roster.Count);
        Assert.AreEqual("iron_sword", back.roster[0].weaponId);
        Assert.AreEqual(2, back.stock.Count);
    }

    [Test]
    public void Slot_SaveLoadDelete()
    {
        string dir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(dir);
        try
        {
            SaveSystem.NewGame();
            SaveSystem.Current.gold = 321;
            Assert.IsFalse(SaveSystem.SlotExists(7, dir));
            SaveSystem.Save(7, dir);
            Assert.IsTrue(SaveSystem.SlotExists(7, dir));
            SaveSystem.Current.gold = 0;
            Assert.IsTrue(SaveSystem.Load(7, dir));
            Assert.AreEqual(321, SaveSystem.Current.gold);
            Assert.AreEqual(321, Inventory.Gold); // applied to runtime
            SaveSystem.Delete(7, dir);
            Assert.IsFalse(SaveSystem.SlotExists(7, dir));
        }
        finally { Directory.Delete(dir, true); }
    }
}
