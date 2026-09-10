using NUnit.Framework;
using System.Collections.Generic;
using Garganta.Combat;

public class CTBTests
{
    [Test]
    public void Gain_ScalesWithSpeed()
    {
        Assert.AreEqual(10f, TurnManager.ComputeGain(10f, 0f), 0.001f);
        Assert.AreEqual(15f, TurnManager.ComputeGain(10f, 0.5f), 0.001f); // haste
        Assert.AreEqual(5f, TurnManager.ComputeGain(10f, -0.5f), 0.001f); // slow
    }

    [Test]
    public void SelectNext_PicksHighestReady()
    {
        Assert.AreEqual(1, TurnManager.SelectNext(new List<float> { 100f, 120f, 30f }));
        Assert.AreEqual(-1, TurnManager.SelectNext(new List<float> { 10f, 50f, 99f }));
    }

    [Test]
    public void FastUnit_ActsMoreOften()
    {
        float fast = 0f, slow = 0f;
        int fastActs = 0, slowActs = 0;
        for (int i = 0; i < 40; i++)
        {
            fast += TurnManager.ComputeGain(10f, 0f);
            slow += TurnManager.ComputeGain(5f, 0f);
            if (fast >= 100f) { fastActs++; fast = 0f; }
            if (slow >= 100f) { slowActs++; slow = 0f; }
        }
        Assert.Greater(fastActs, slowActs);
        Assert.AreEqual(4, fastActs);
        Assert.AreEqual(2, slowActs);
    }
}
