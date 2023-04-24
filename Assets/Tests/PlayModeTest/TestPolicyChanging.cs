using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestPolicyChanging
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestPolicyChanging_PolicyChanging_IsCorrect()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("SampleScene");
        yield return null;
        // Arrange
        var ShanghaiButton = GameObject.Find("Shanghai").GetComponent<Button>();
        var SchoolClosingSlider = GameObject.Find("School closing").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var DataManager = GameObject.Find("DataManager").GetComponent<DataManager>();

        ShanghaiButton.onClick.Invoke();
        SchoolClosingSlider.value = 1;
        var expected = 1;
        var actual = DataManager.getPolicyIndex(0, 0);

        Assert.AreEqual(expected, actual);

    }
}
