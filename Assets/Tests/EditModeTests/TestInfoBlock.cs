using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestInfoBlock
{
    [Test]
    public void GetGDP_IsCorrect()
    {
        // Arrange
        var infoBlock = new GameObject().AddComponent<InfoBlock>();
        var expected = 3870.1;

        // Act
        var result = infoBlock.GetGDP(0);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void GetName_IsCorrect()
    {
        // Arrange
        var infoBlock = new GameObject().AddComponent<InfoBlock>();
        var expected = "Shanghai";

        // Act
        var result = infoBlock.returnCityName(0);

        // Assert
        Assert.IsTrue(expected == result);
    }



    [Test]
    public void GetPopulation_IsCorrect()
    {
        // Arrange
        var infoBlock = new GameObject().AddComponent<InfoBlock>();
        var expected = 24183300;

        // Act
        var result = infoBlock.GetPopulation(0);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void GetDensity_IsCorrect()
    {
        // Arrange
        var infoBlock = new GameObject().AddComponent<InfoBlock>();
        var expected = 3923;

        // Act
        var result = infoBlock.GetDensity(0);

        // Assert
        Assert.AreEqual(expected, result);
    }

}
