using System.Collections.Generic;
using NUnit.Framework;
using Garganta.Data;

public class CraftingTests
{
    int goldBefore;
    readonly Dictionary<string, int> stockBefore = new Dictionary<string, int>();
    readonly Dictionary<string, int> matsBefore = new Dictionary<string, int>();
    List<string> ownedBefore;

    [SetUp]
    public void Backup()
    {
        goldBefore = Inventory.Gold;
        foreach (var kv in Inventory.Stock()) stockBefore[kv.Key] = kv.Value;
        foreach (var kv in Inventory.Mats()) matsBefore[kv.Key] = kv.Value;
        ownedBefore = new List<string>(Inventory.OwnedEquip);
    }

    [TearDown]
    public void Restore()
    {
        Inventory.Gold = goldBefore;
        Inventory.Clear();
        foreach (var kv in stockBefore) Inventory.Add(kv.Key, kv.Value);
        Inventory.ClearMats();
        foreach (var kv in matsBefore) Inventory.AddMat(kv.Key, kv.Value);
        Inventory.OwnedEquip.Clear();
        Inventory.OwnedEquip.AddRange(ownedBefore);
    }

    [Test]
    public void Recipes_DataValid()
    {
        Assert.AreEqual(8, Crafting.Recipes.Count);
        foreach (var r in Crafting.Recipes)
        {
            if (!string.IsNullOrEmpty(r.ResultEquip))
                Assert.NotNull(EquipmentData.FindAny(r.ResultEquip).Id, r.Name);
            if (!string.IsNullOrEmpty(r.NeedEquip))
                Assert.NotNull(EquipmentData.FindAny(r.NeedEquip).Id, r.Name);
            foreach (var m in r.NeedMats)
                Assert.IsTrue(Crafting.MatPrices.ContainsKey(m.id), m.id);
        }
    }

    [Test]
    public void CanCraft_Gates()
    {
        var steel = Crafting.Recipes[0]; // steel_sword
        Inventory.OwnedEquip.Clear();
        Inventory.ClearMats();
        Assert.IsFalse(Crafting.CanCraft(steel, Inventory.OwnedEquip));
        Inventory.OwnedEquip.Add("iron_sword");
        Inventory.AddMat("iron_ore", 2);
        Assert.IsFalse(Crafting.CanCraft(steel, Inventory.OwnedEquip));
        Inventory.AddMat("iron_ore", 1);
        Assert.IsTrue(Crafting.CanCraft(steel, Inventory.OwnedEquip));
    }

    [Test]
    public void Craft_ConsumesAndProduces()
    {
        var steel = Crafting.Recipes[0];
        Inventory.OwnedEquip.Clear();
        Inventory.OwnedEquip.Add("iron_sword");
        Inventory.ClearMats();
        Inventory.AddMat("iron_ore", 3);
        var saveOwned = new List<string> { "iron_sword" };
        Assert.IsTrue(Crafting.Craft(steel, Inventory.OwnedEquip, saveOwned));
        Assert.Contains("steel_sword", Inventory.OwnedEquip);
        Assert.IsFalse(Inventory.OwnedEquip.Contains("iron_sword"));
        Assert.AreEqual(0, Inventory.MatCount("iron_ore"));
        Assert.Contains("steel_sword", saveOwned);
        Assert.IsFalse(Crafting.Craft(steel, Inventory.OwnedEquip, saveOwned)); // base gone
    }

    [Test]
    public void Elixir_FromConsumables()
    {
        var elixir = Crafting.Recipes.Find(r => r.ResultConsum == "elixir");
        Assert.IsNotNull(elixir.Name);
        Inventory.Add("potion", 1);
        Inventory.Add("ether", 1);
        int p = Inventory.Count("potion"), e = Inventory.Count("ether"), x = Inventory.Count("elixir");
        var owned = new List<string>(Inventory.OwnedEquip);
        Assert.IsTrue(Crafting.Craft(elixir, owned, new List<string>()));
        Assert.AreEqual(p - 1, Inventory.Count("potion"));
        Assert.AreEqual(e - 1, Inventory.Count("ether"));
        Assert.AreEqual(x + 1, Inventory.Count("elixir"));
    }
}
