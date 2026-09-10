using NUnit.Framework;
using Garganta.Core;
using Garganta.Combat;

public class WeaponTriangleTests
{
    [Test]
    public void SwordBeatsAxe() => Assert.AreEqual(1.1f, WeaponTriangle.GetMultiplier(WeaponType.Sword, WeaponType.Axe), 0.001f);
    [Test]
    public void AxeLosesToSword() => Assert.AreEqual(0.9f, WeaponTriangle.GetMultiplier(WeaponType.Axe, WeaponType.Sword), 0.001f);
    [Test]
    public void SpearCycle()
    {
        Assert.AreEqual(1.1f, WeaponTriangle.GetMultiplier(WeaponType.Axe, WeaponType.Spear), 0.001f);
        Assert.AreEqual(1.1f, WeaponTriangle.GetMultiplier(WeaponType.Spear, WeaponType.Sword), 0.001f);
    }
    [Test]
    public void BowStaff() => Assert.AreEqual(1.1f, WeaponTriangle.GetMultiplier(WeaponType.Bow, WeaponType.Staff), 0.001f);
    [Test]
    public void DaggerNeutral() => Assert.AreEqual(1.0f, WeaponTriangle.GetMultiplier(WeaponType.Dagger, WeaponType.Sword), 0.001f);
    [Test]
    public void SameNeutral() => Assert.AreEqual(1.0f, WeaponTriangle.GetMultiplier(WeaponType.Sword, WeaponType.Sword), 0.001f);
}
