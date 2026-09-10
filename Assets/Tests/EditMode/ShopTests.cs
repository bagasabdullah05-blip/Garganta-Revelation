using NUnit.Framework;
using Garganta.Data;

public class ShopTests
{
    int goldBefore;
    int bombBefore;

    [SetUp]
    public void Backup()
    {
        goldBefore = Inventory.Gold;
        bombBefore = Inventory.Count("bomb");
    }

    [TearDown]
    public void Restore()
    {
        Inventory.Gold = goldBefore;
        while (Inventory.Count("bomb") > bombBefore) Inventory.Take("bomb");
        while (Inventory.Count("bomb") < bombBefore) Inventory.Add("bomb");
    }

    [Test]
    public void BuyConsumable_DeductsAndStocks()
    {
        Inventory.Gold = 500;
        int before = Inventory.Count("bomb"); // 200G
        Assert.IsTrue(Inventory.TryBuyConsumable("bomb"));
        Assert.AreEqual(300, Inventory.Gold);
        Assert.AreEqual(before + 1, Inventory.Count("bomb"));
    }

    [Test]
    public void BuyConsumable_BlockedWhenPoor()
    {
        Inventory.Gold = 10;
        Assert.IsFalse(Inventory.TryBuyConsumable("bomb"));
        Assert.AreEqual(10, Inventory.Gold);
    }

    [Test]
    public void BuyEquipment_AddsOwned()
    {
        Inventory.Gold = 1000;
        Inventory.OwnedEquip.Clear();
        var e = EquipmentData.FindWeapon("steel_sword"); // 400G
        var owned = new System.Collections.Generic.List<string>();
        Assert.IsTrue(Inventory.TryBuyEquipment(e, owned));
        Assert.AreEqual(600, Inventory.Gold);
        Assert.Contains("steel_sword", Inventory.OwnedEquip);
        Assert.Contains("steel_sword", owned);
        Inventory.OwnedEquip.Clear();
    }

    [Test]
    public void FindAny_UnknownReturnsEmpty()
    {
        var e = EquipmentData.FindAny("does_not_exist");
        Assert.IsNull(e.Id);
    }

    [Test]
    public void FindAny_CoversAllSlots()
    {
        Assert.AreEqual(EquipSlot.Weapon, EquipmentData.FindAny("iron_sword").Slot);
        Assert.AreEqual(EquipSlot.Armor, EquipmentData.FindAny("chainmail").Slot);
        Assert.AreEqual(EquipSlot.Helmet, EquipmentData.FindAny("bronze_helm").Slot);
        Assert.AreEqual(EquipSlot.Accessory, EquipmentData.FindAny("power_band").Slot);
    }
}
