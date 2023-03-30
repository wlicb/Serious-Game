using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestModel
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestStringencyIndex()
    {
        // Use the Assert class to test conditions
        Model model = GameObject.Find("Model").GetComponent<Model>();
        int[] policies = {3, 3, 2, 4, 2, 3, 2};
        Assert.AreEqual(100.0, model.stringencyIndex(policies));
    }
}
