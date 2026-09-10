using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Garganta.Combat;
using Garganta.Data;
using Garganta.Units;

public class CorruptionTests
{
    [Test]
    public void Tier_Boundaries()
    {
        Assert.AreEqual(0, CombatManager.CorruptionTier(0));
        Assert.AreEqual(0, CombatManager.CorruptionTier(24));
        Assert.AreEqual(1, CombatManager.CorruptionTier(25));
        Assert.AreEqual(2, CombatManager.CorruptionTier(50));
        Assert.AreEqual(3, CombatManager.CorruptionTier(75));
        Assert.AreEqual(3, CombatManager.CorruptionTier(99));
        Assert.AreEqual(4, CombatManager.CorruptionTier(100));
    }

    [Test]
    public void Mods_RiskReward()
    {
        CombatManager.CorruptionMods(0, out var a0, out var d0);
        Assert.AreEqual(1f, a0);
        Assert.AreEqual(1f, d0);
        CombatManager.CorruptionMods(50, out var a50, out var d50);
        Assert.AreEqual(1.2f, a50);
        Assert.AreEqual(0.8f, d50);
        CombatManager.CorruptionMods(80, out var a80, out var d80);
        Assert.AreEqual(1.3f, a80);
        Assert.AreEqual(0.8f, d80);
    }

    [Test]
    public void LoseTurn_Chances()
    {
        Assert.IsFalse(CombatManager.LoseTurnRoll(10, 0f));
        Assert.IsTrue(CombatManager.LoseTurnRoll(60, 0.05f));
        Assert.IsFalse(CombatManager.LoseTurnRoll(60, 0.5f));
        Assert.IsTrue(CombatManager.LoseTurnRoll(80, 0.29f));
        Assert.IsFalse(CombatManager.LoseTurnRoll(80, 0.31f));
    }
}

public class RecruitTests
{
    readonly List<GameObject> trash = new List<GameObject>();

    Unit Make(string classId, Vector2Int c, bool player, int hp = -1)
    {
        var go = new GameObject("r" + c);
        trash.Add(go);
        var u = go.AddComponent<Unit>();
        u.Init("r", classId, player, ClassDatabase.Get(classId).Base, c);
        u.SetClass(classId);
        u.RosterId = classId;
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
    public void Talk_ConvertsWeakenedMarkedFoe()
    {
        var talker = Make("Squire", new Vector2Int(0, 0), true);
        var korr = Make("Spearman", new Vector2Int(1, 0), false, 20);
        korr.RecruitId = "Korr";
        var allies = new List<Unit> { talker };
        var foes = new List<Unit> { korr };
        Mgr().ResolveSkill(talker, SkillDatabase.Get("Talk"), korr, allies, foes);
        Assert.AreEqual(0, foes.Count);
        Assert.AreEqual(2, allies.Count);
        Assert.IsTrue(korr.IsPlayer);
        Assert.AreEqual("Korr", korr.RosterId);
    }

    [Test]
    public void Talk_FailsWhenHealthyOrUnmarked()
    {
        var talker = Make("Squire", new Vector2Int(0, 0), true);
        var healthy = Make("Spearman", new Vector2Int(1, 0), false, 9999);
        healthy.RecruitId = "Korr";
        var plain = Make("Thief", new Vector2Int(0, 1), false, 5);
        var allies = new List<Unit> { talker };
        var foes = new List<Unit> { healthy, plain };
        var mgr = Mgr();
        mgr.ResolveSkill(talker, SkillDatabase.Get("Talk"), healthy, allies, foes);
        mgr.ResolveSkill(talker, SkillDatabase.Get("Talk"), plain, allies, foes);
        Assert.AreEqual(2, foes.Count);
        Assert.IsFalse(healthy.IsPlayer);
    }
}

public class RepTests
{
    [Test]
    public void AddGet_PersistThroughSerialize()
    {
        var save = new GameSave();
        Reputation.Add(save, "Shadow", 2);
        Reputation.Add(save, "Shadow", 3);
        Assert.AreEqual(5, Reputation.Get(save, "Shadow"));
        Assert.AreEqual(0, Reputation.Get(save, "Ironhold"));
        var back = SaveSystem.Deserialize(SaveSystem.Serialize(save));
        Assert.AreEqual(5, Reputation.Get(back, "Shadow"));
        Assert.IsTrue(Reputation.Summary(back).Contains("Shadow:5"));
    }
}
