using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestWarningAndTermination
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestSatisfactionWarning()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("SampleScene");
        yield return null;
        var ShanghaiButton = GameObject.Find("Shanghai").GetComponent<Button>();
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        var SchoolClosingSlider = GameObject.Find("School closing").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var WorkplaceClosingSlider = GameObject.Find("Workplace closing").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var PublicEventsSlider = GameObject.Find("Cancel public events").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var GatheringSizeSlider = GameObject.Find("Restrictions on gathering size").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var PublicTransportSlider = GameObject.Find("Close public transport").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var StayAtHomeSlider = GameObject.Find("Stay-at-home requirements").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var InternalMovementSlider = GameObject.Find("Restrictions on internal movement").transform.GetChild(1).gameObject.GetComponent<Slider>();

        ShanghaiButton.onClick.Invoke();
        SchoolClosingSlider.value = 3;
        WorkplaceClosingSlider.value = 3;
        PublicEventsSlider.value = 2;
        GatheringSizeSlider.value = 4;
        PublicTransportSlider.value = 2;
        StayAtHomeSlider.value = 3;
        InternalMovementSlider.value = 2;

        while (dataManager.getSatisfaction(0) == 100) {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(1, GameObject.Find("Popup Pane").transform.childCount);


    }

    [UnityTest]
    public IEnumerator TestDeathWarning() {
        SceneManager.LoadScene("SampleScene");
        yield return null;
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        while (dataManager.getTotalDeath() == 0) {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        // Assert.IsNotNull(GameObject.Find("Death Popup"));
        Assert.Greater(GameObject.Find("Popup Pane").transform.childCount, 0);
    }

    [UnityTest]
    public IEnumerator TestSatisfactionTermination()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("SampleScene");
        yield return null;
        var ShanghaiButton = GameObject.Find("Shanghai").GetComponent<Button>();
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        var SchoolClosingSlider = GameObject.Find("School closing").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var WorkplaceClosingSlider = GameObject.Find("Workplace closing").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var PublicEventsSlider = GameObject.Find("Cancel public events").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var GatheringSizeSlider = GameObject.Find("Restrictions on gathering size").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var PublicTransportSlider = GameObject.Find("Close public transport").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var StayAtHomeSlider = GameObject.Find("Stay-at-home requirements").transform.GetChild(1).gameObject.GetComponent<Slider>();
        var InternalMovementSlider = GameObject.Find("Restrictions on internal movement").transform.GetChild(1).gameObject.GetComponent<Slider>();

        ShanghaiButton.onClick.Invoke();
        SchoolClosingSlider.value = 3;
        WorkplaceClosingSlider.value = 3;
        PublicEventsSlider.value = 2;
        GatheringSizeSlider.value = 4;
        PublicTransportSlider.value = 2;
        StayAtHomeSlider.value = 3;
        InternalMovementSlider.value = 2;

        while (dataManager.getSatisfaction(0) == 100) {
            yield return null;
        }
        // yield return new WaitForSeconds(1.0f);
        Assert.IsNotNull(GameObject.Find("GameEndCanvas"));
    }

    [UnityTest]
    public IEnumerator TestDeathTermination() {
        SceneManager.LoadScene("SampleScene");
        yield return null;
        var dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        while (dataManager.getTotalDeath() == 0) {
            yield return null;
        }
        // yield return new WaitForSeconds(1.0f);
        Assert.IsNotNull(GameObject.Find("GameEndCanvas"));
    }

    [UnityTest]
    public IEnumerator TestSuccessTermination() {
        SceneManager.LoadScene("SampleScene");
        yield return new WaitForSeconds(16.1f);
        Assert.IsNotNull(GameObject.Find("GameEndCanvas"));
    }
}
