using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using NUnit.Framework;

public class TestModel
{
    Model model;

    [SetUp]
    public void Setup()
    {
        model = GameObject.Find("Model").GetComponent<Model>();
    }

    [Test]
    public void CalculateNewSatisfaction_ReturnsCorrectSatisfaction()
    {
        double stringencyIndex = 50;
        int lastIncrease = 100;
        double GDP = 1000;
        int lastDeath = 10;
        int policyChangingFrequency = 2;
        int lastSatisfaction = 80;

        // Case 1

        int expectedSatisfaction = 77;

        int actualSatisfaction = model.calculateNewSatisfaction(stringencyIndex, lastIncrease, GDP, 
                            lastDeath, policyChangingFrequency, lastSatisfaction);

        Assert.AreEqual(expectedSatisfaction, actualSatisfaction);

        // Case 2
        stringencyIndex = 0;

        actualSatisfaction = model.calculateNewSatisfaction(stringencyIndex, lastIncrease, GDP, 
                            lastDeath, policyChangingFrequency, lastSatisfaction);

        expectedSatisfaction = 88;

        Assert.AreEqual(expectedSatisfaction, actualSatisfaction);

        // Case 3
        lastIncrease = 0;

        actualSatisfaction = model.calculateNewSatisfaction(stringencyIndex, lastIncrease, GDP, 
                            lastDeath, policyChangingFrequency, lastSatisfaction);

        expectedSatisfaction = 87;

        Assert.AreEqual(expectedSatisfaction, actualSatisfaction);

        // Case 4
        lastIncrease = 1000;

        actualSatisfaction = model.calculateNewSatisfaction(stringencyIndex, lastIncrease, GDP, 
                            lastDeath, policyChangingFrequency, lastSatisfaction);

        expectedSatisfaction = 85;

        Assert.AreEqual(expectedSatisfaction, actualSatisfaction);


        // Case 5
        lastIncrease = 0;

        lastDeath = 50;

        actualSatisfaction = model.calculateNewSatisfaction(stringencyIndex, lastIncrease, GDP, 
                            lastDeath, policyChangingFrequency, lastSatisfaction);

        expectedSatisfaction = 85;

        Assert.AreEqual(expectedSatisfaction, actualSatisfaction);

        // Case 6
        lastSatisfaction = 100;

        actualSatisfaction = model.calculateNewSatisfaction(stringencyIndex, lastIncrease, GDP, 
                            lastDeath, policyChangingFrequency, lastSatisfaction);

        expectedSatisfaction = 100;

        Assert.AreEqual(expectedSatisfaction, actualSatisfaction);

        // Case 7
        lastSatisfaction = 0;

        stringencyIndex = 100;
        lastIncrease = 1000;
        lastDeath = 50;

        actualSatisfaction = model.calculateNewSatisfaction(stringencyIndex, lastIncrease, GDP, 
                            lastDeath, policyChangingFrequency, lastSatisfaction);

        expectedSatisfaction = 0;

        Assert.AreEqual(expectedSatisfaction, actualSatisfaction);
        
    }

    [Test]
    public void CalculateNewDailyDeath_ReturnsCorrectDeaths()
    {
        int totalInfection = 1000;
        // Case 1
        int lastIncrease = 10000;
        int lastDeath = 200;

        double expectedDeaths = 23.5;
        // int expectedDeaths = 2;

        double actualDeaths = model.calculateNewDailyDeath(lastIncrease, lastDeath, totalInfection);

        Assert.AreEqual(expectedDeaths, actualDeaths, 9.5);

        // Case 2
        lastIncrease = 1000;
        lastDeath = 80;

        expectedDeaths = 2;

        actualDeaths = model.calculateNewDailyDeath(lastIncrease, lastDeath, totalInfection);

        Assert.AreEqual(expectedDeaths, actualDeaths, 1);

        // Case 3
        lastIncrease = 100;
        lastDeath = 20;

        expectedDeaths = 0.5;

        actualDeaths = model.calculateNewDailyDeath(lastIncrease, lastDeath, totalInfection);

        Assert.AreEqual(expectedDeaths, actualDeaths, 0.5);

        // Case 4
        lastIncrease = 0;
        lastDeath = 0;

        expectedDeaths = 0.5;

        actualDeaths = model.calculateNewDailyDeath(lastIncrease, lastDeath, totalInfection);

        Assert.AreEqual(expectedDeaths, actualDeaths, 0.5);

        // Case 5
        totalInfection = 0;
        int actual = model.calculateNewDailyDeath(lastIncrease, lastDeath, totalInfection);
        Assert.AreEqual(0, actual);
        
    }

    [Test]
    public void CalculateNewDailyInfection_ReturnsCorrectInfections()
    {
        double stringencyIndex = 50;
        int lastIncrease = 100;
        double populationDensity = 5000;
        int population = 100000;
        int p = 0;

        int expectedInfections = 3;

        int actualInfections = model.calculateNewDailyInfection(stringencyIndex, lastIncrease, populationDensity, population, p);

        Assert.AreEqual(expectedInfections, actualInfections);
    }

    [Test]
    public void StringencyIndex_ReturnsCorrectIndex()
    {
        int[] policies = { 1, 2, 1, 2, 1, 2, 1 };

        double expectedIndex = 52.3809524;

        double actualIndex = model.stringencyIndex(policies);

        Assert.AreEqual(expectedIndex, actualIndex, 0.00001);
    }

    [Test]
    public void PolicyChangingFrequency_ReturnsCorrectFrequency()
    {
        int[,] pastPolicies = { { 2, 3, 4, 5 }, { 1, 2, 3, 4 }, { 3, 4, 5, 6 } };

        int expectedFrequency = 2;

        int actualFrequency = model.policyChangingFrequency(pastPolicies);

        Assert.AreEqual(expectedFrequency, actualFrequency);
    }
}