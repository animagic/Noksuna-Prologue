using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestPlayerStats {

    [Test]
	public void TestPlayerStatsValues() {
        // Use the Assert class to test conditions.
        Player p = GameObject.FindObjectOfType<Player>();
        for (int i = 1; i <= p.GetMaxLevel(); i++)
        {
            p.Test_SetBaseStatValues(i);
        }
    }

    [Test]
    public void TestPlayerExperienceTNLValues()
    {
        Player p = GameObject.FindObjectOfType<Player>();
        int totalTNL = 0;
        for (int i = 1; i <= p.GetMaxLevel(); i++)
        {
            totalTNL += p.SetCurrentLevelMaxExperience(i);
        }
        Debug.Log(totalTNL);
    }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator TestPlayerStatsWithEnumeratorPasses() {
        
		yield return null;
	}

    [Test]
    public void TestTakeDamage()
    {
        Enemy e = GameObject.FindObjectOfType<Enemy>();
        for (int lincoln = 0; lincoln <= 500; lincoln += 20)
        {
            e.TakeDamage(lincoln);
        }
    }
}
