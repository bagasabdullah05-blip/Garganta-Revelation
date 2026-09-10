using NUnit.Framework;
using Garganta.Combat;

public class DamageFormulaTests
{
    [Test]
    public void PhysicalBase_MatchesPRD()
    {
        // ATK * Weapon * Class - DEF * 0.5 ; Kael 12*1*1 - 10*0.5 = 7
        Assert.AreEqual(7, CombatManager.PhysicalBase(12, 1f, 1f, 10));
        Assert.AreEqual(1, CombatManager.PhysicalBase(5, 1f, 1f, 50)); // min 1
    }

    [Test]
    public void Elevation_HighGroundBonus()
    {
        Assert.AreEqual(1.1f, CombatManager.ElevationMult(2, 0), 0.001f);
        Assert.AreEqual(0.9f, CombatManager.ElevationMult(0, 2), 0.001f);
        Assert.AreEqual(1.0f, CombatManager.ElevationMult(1, 1), 0.001f);
    }

    [Test]
    public void Finalize_StacksMultipliers()
    {
        // 10 * 1.1 * 1.1 * 1.0(no crit) * 1.0 = 12.1 -> 12
        Assert.AreEqual(12, CombatManager.Finalize(10, 1.1f, 1.1f, false, 1.0f));
        // crit 1.5x: 10 * 1 * 1 * 1.5 * 1 = 15
        Assert.AreEqual(15, CombatManager.Finalize(10, 1f, 1f, true, 1.0f));
    }

    [Test]
    public void HitChance_Clamped()
    {
        Assert.AreEqual(99f, CombatManager.HitChance(120, 0, 10f, 10f), 0.001f);
        Assert.AreEqual(20f, CombatManager.HitChance(50, 90, -10f, -10f), 0.001f);
        Assert.AreEqual(80f, CombatManager.HitChance(90, 10, 0f, 0f), 0.001f);
    }
}
