using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;
using System;
using System.Threading;
using System.Linq;

public class TestDataManager
{
    [Test]
    public void ChangeWaitTime_WaitTime_IsSetCorrectly()
    {
        // Arrange
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        var expectedWaitTime = 10.0f;

        // Act
        dataManager.changeWaitTime(expectedWaitTime);

        // Assert
        Assert.AreEqual(expectedWaitTime, dataManager.getWaitTime());
    }

    [Test]
    public void UpdatePolicyIndex_PolicyValue_IsUpdatedCorrectly()
    {
        // Arrange
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        var cityIndex = 2;
        var policyIndex = 0;
        var expectedPolicyValue = 0.5f;
        var expectedAverage = 0.01515f;

        // Act
        dataManager.updatePolicyIndex(cityIndex, policyIndex, expectedPolicyValue);
        var resultPolicyValue = dataManager.getPolicyIndex(cityIndex, policyIndex);
        var resultAverage = dataManager.getAveragePolicyIndex(policyIndex);

        // Assert
        Assert.AreEqual(expectedPolicyValue, resultPolicyValue);
        Assert.AreEqual(expectedAverage, resultAverage, 0.001f);
    }

    [Test]
    public void GetMaxInfectionCity_CityIndex_IsCorrect()
    {
        // Arrange
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        dataManager.updateInfection(new int[] { 50, 100, 75, 80, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
        var rank = 2;
        var expectedCityIndex = 3;

        // Act
        var result = dataManager.getMaxInfectionCity(rank);

        // Assert
        Assert.AreEqual(expectedCityIndex, result);
    }

    [Test]
    public void GetInfection_InfectionCount_IsCorrect()
    {
        // Arrange
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        dataManager.updateInfection(new int[] { 50, 100, 75, 80, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
        var expectedTotalInfection = 365;
        var expectedCityInfection = 50;
        var expectedTotalIncrease = 365;
        var expectedCityIncrease = 50;
        var expectedCityHistoryLast = 50;
        var expectedTotalHistory = 365;

        // Act
        var resultTotal = dataManager.getTotalInfection();
        var resultCity = dataManager.getInfection(0);
        var resultChangeTotal = dataManager.getTotalIncrease();
        var resultChangeCity = dataManager.getIncrease(0);
        var resultCityHistoryLast = dataManager.getIncreaseHistory(0)[6];
        var resultTotalHistory = dataManager.getTotalIncreaseHistory()[6];

        // Assert
        Assert.AreEqual(expectedTotalInfection, resultTotal);
        Assert.AreEqual(expectedCityInfection, resultCity);
        Assert.AreEqual(expectedCityIncrease, resultChangeCity);
        Assert.AreEqual(expectedTotalInfection, resultChangeTotal);
        Assert.AreEqual(expectedCityHistoryLast, resultCityHistoryLast);
        Assert.AreEqual(expectedTotalHistory, resultTotalHistory);
    }

    [Test]
    public void GetDeath_DeathCount_IsCorrect()
    {
        // Arrange
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        dataManager.updateDeath(new int[] { 50, 100, 75, 80, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
        var expectedTotalDeath = 365;
        var expectedCityDeath = 50;
        var expectedCityHistoryLast = 50;
        var expectedTotalHistory = 365;

        // Act
        var resultTotal = dataManager.getTotalDeath();
        var resultCity = dataManager.getDeath(0);
        var resultCityHistoryLast = dataManager.getDeathHistory(0)[6];
        var resultTotalHistory = dataManager.getTotalDeathHistory()[6];

        // Assert
        Assert.AreEqual(expectedTotalDeath, resultTotal);
        Assert.AreEqual(expectedCityDeath, resultCity);
        Assert.AreEqual(expectedCityHistoryLast, resultCityHistoryLast);
        Assert.AreEqual(expectedTotalHistory, resultTotalHistory);
    }

    [Test]
    public void GetSatisfaction_SatisfactionCount_IsCorrect()
    {
        // Arrange
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        dataManager.updateSatisfaction(new int[] { 50, 100, 75, 80, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
        var expectedAverage = 11;
        var expectedCity = 50;

        // Act
        var resultAverage = dataManager.getAverageSatisfaction();
        var resultCity = dataManager.getSatisfaction(0);

        // Assert
        Assert.AreEqual(expectedAverage, resultAverage);
        Assert.AreEqual(expectedCity, resultCity);
    }

    [Test]
    public void GetDate_DateValue_IsCorrect()
    {
        // Arrange
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        var expectedDay = 1;
        var expectedMonth = 3;
        var expectedYear = 2022;
        var expectedDayPassed = 0;

        // Act
        var resultDay = dataManager.getDateDay();
        var resultMonth = dataManager.getDateMonth();
        var resultYear = dataManager.getDateYear();
        var resultDayPassed = dataManager.getDaysPassed();

        // Assert
        Assert.AreEqual(expectedDay, resultDay);
        Assert.AreEqual(expectedMonth, resultMonth);
        Assert.AreEqual(expectedYear, resultYear);
        Assert.AreEqual(expectedDayPassed, resultDayPassed);
    }
}
