using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Garganta.Combat;
using Garganta.Data;
using Garganta.Units;

public class SkillTests
{
    readonly List<GameObject> trash = new List<GameObject>();

    Unit Make(string classId, Vector2Int c, int hp = -1)
    {
        var go = new GameObject("u" + c);
        trash.Add(go);
        var u = go.AddComponent<Unit>();
        u.Init("u", classId, true, ClassDatabase.Get(classId).Base, c);
        u.SetClass(classId);
        if (hp >= 0) u.HP = hp;
        return u;
    }

    CombatManager Mgr()
    {
        var go = new GameObject("cm");
        trash.Add(go);
        return go.AddComponent<CombatManager>();
    }

    [TearDown]
    public void Cleanup()
    {
        foreach (var go in trash) Object.DestroyImmediate(go);
        trash.Clear();
    }

    [Test]
    public void UnlockedSkills_GatedByLevel()
    {
        var l1 = ClassDatabase.UnlockedSkills("Squire", 1);
        Assert.AreEqual(3, l1.Count); // Attack + Talk + PowerStrike
        var l3 = ClassDatabase.UnlockedSkills("Squire", 3);
        Assert.AreEqual(4, l3.Count); // + ShieldBash
        var l10 = ClassDatabase.UnlockedSkills("Mage", 10);
        Assert.AreEqual(6, l10.Count); // Attack + Talk + 4
    }

    [Test]
    public void HealAmount_Math()
    {
        Assert.AreEqual(12, CombatManager.HealAmount(12, 1f, 0));
        Assert.AreEqual(14, CombatManager.HealAmount(12, 1f, 20)); // staff +20%
    }

    [Test]
    public void UnitsInRadius_Filters()
    {
        var a = Make("Squire", new Vector2Int(0, 0));
        var b = Make("Squire", new Vector2Int(1, 0));
        var c = Make("Squire", new Vector2Int(3, 0));
        var dead = Make("Squire", new Vector2Int(0, 1));
        dead.TakeDamage(9999);
        var list = new List<Unit> { a, b, c, dead };
        var hit = CombatManager.UnitsInRadius(new Vector2Int(0, 0), 1, list);
        Assert.AreEqual(2, hit.Count);
        Assert.Contains(a, hit);
        Assert.Contains(b, hit);
    }

    [Test]
    public void UseMP_BlocksWhenInsufficient()
    {
        var u = Make("Mage", new Vector2Int(0, 0));
        u.MP = 5;
        Assert.IsFalse(u.UseMP(10));
        Assert.AreEqual(5, u.MP);
        Assert.IsTrue(u.UseMP(5));
        Assert.AreEqual(0, u.MP);
    }

    [Test]
    public void Cure_HealsAndCleanses()
    {
        var caster = Make("Acolyte", new Vector2Int(0, 0));
        var target = Make("Squire", new Vector2Int(1, 0), 10);
        target.StunTurns = 2;
        int mpBefore = caster.MP;
        Mgr().ResolveSkill(caster, SkillDatabase.Get("Cure"), target,
            new List<Unit> { target }, new List<Unit>());
        Assert.AreEqual(0, target.StunTurns);
        Assert.Greater(target.HP, 10);
        Assert.AreEqual(mpBefore - 8, caster.MP);
    }

    [Test]
    public void BuffSkill_RaisesStat()
    {
        var caster = Make("Squire", new Vector2Int(0, 0));
        Mgr().ResolveSkill(caster, SkillDatabase.Get("WarCry"), caster,
            new List<Unit> { caster }, new List<Unit>());
        Assert.AreEqual(3, caster.BuffAtk);
        Assert.AreEqual(3, caster.BuffTurns);
    }
}
