using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Garganta.Core;
using Garganta.Data;
using Garganta.Units;

public class EquipmentTests
{
    readonly List<GameObject> trash = new List<GameObject>();

    Unit Make(string classId)
    {
        var go = new GameObject("eq");
        trash.Add(go);
        var u = go.AddComponent<Unit>();
        u.Init("eq", classId, true, ClassDatabase.Get(classId).Base, new Vector2Int(0, 0));
        u.SetClass(classId);
        return u;
    }

    [TearDown]
    public void Cleanup()
    {
        foreach (var go in trash) Object.DestroyImmediate(go);
        trash.Clear();
    }

    [Test]
    public void Sword_AddsAttack()
    {
        var u = Make("Squire");
        Assert.AreEqual(12, u.Stats.ATK);
        u.Equipped[EquipSlot.Weapon] = EquipmentData.FindWeapon("iron_sword");
        u.RefreshStats();
        Assert.AreEqual(22, u.Stats.ATK);
        Assert.AreEqual(WeaponType.Sword, u.Stats.Weapon);
    }

    [Test]
    public void Tome_AddsMagic()
    {
        var u = Make("Mage");
        u.Equipped[EquipSlot.Weapon] = EquipmentData.FindWeapon("fire_tome");
        u.RefreshStats();
        Assert.AreEqual(25, u.Stats.MAG); // 14 + 11
    }

    [Test]
    public void ShortBow_ReducesRange()
    {
        var u = Make("Archer");
        u.Equipped[EquipSlot.Weapon] = EquipmentData.FindWeapon("short_bow");
        u.RefreshStats();
        Assert.AreEqual(3, u.Stats.Range); // 4 - 1
    }

    [Test]
    public void Accessory_MoveAndSpeed()
    {
        var u = Make("Thief");
        u.Equipped[EquipSlot.Accessory] = EquipmentData.FindAccessory("agility_boots");
        u.RefreshStats();
        Assert.AreEqual(7, u.Stats.Move); // 6 + 1
        Assert.AreEqual(17, u.Stats.SPD); // 12 + 5
    }

    [Test]
    public void Range_NeverBelowOne()
    {
        var u = Make("Squire");
        u.Equipped[EquipSlot.Weapon] = EquipmentData.FindWeapon("short_bow");
        u.RefreshStats();
        Assert.AreEqual(1, u.Stats.Range); // 1 - 1 -> clamped to 1
    }

    [Test]
    public void Inventory_TakeAndRefund()
    {
        int before = Inventory.Count("potion");
        Inventory.Add("potion", 2);
        Assert.AreEqual(before + 2, Inventory.Count("potion"));
        Assert.IsTrue(Inventory.Take("potion", 2));
        Assert.AreEqual(before, Inventory.Count("potion"));
        Assert.IsFalse(Inventory.Take("phoenix_down", 999));
    }
}
