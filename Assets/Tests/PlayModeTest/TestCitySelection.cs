using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestCitySelection
{
    // A Test behaves as an ordinary method
    [UnityTest]
    public IEnumerator CitySelection_IsCorrect()
    {
        // yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SampleScene");

        yield return null;
        // Arrange
        var ShanghaiButton = GameObject.Find("Shanghai").GetComponent<Button>();
        var ChinaMap = GameObject.Find("China").GetComponent<Button>();
        var citySelectController = GameObject.Find("CitySelector").GetComponent<CitySelectController>();
        var PolicyController = GameObject.Find("PolicyController").GetComponent<policyController>();
        var DataController = GameObject.Find("Data Pane").GetComponent<DataPaneController>();


        // Case 1
        var expected = 0;
        ShanghaiButton.onClick.Invoke();
        var policyResult = PolicyController.getCityIndex();
        var dataResult = DataController.getCityIndex();

        Assert.AreEqual(expected, policyResult);
        Assert.AreEqual(expected, dataResult);

        // Case 2
        expected = -1;
        ChinaMap.onClick.Invoke();
        policyResult = PolicyController.getCityIndex();
        dataResult = DataController.getCityIndex();

        Assert.AreEqual(expected, policyResult);
        Assert.AreEqual(expected, dataResult);
    }
}
