using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestInfoBlock
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestInfoBlock_InfoIsCorrect()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("SampleScene");
        yield return null;
        var ShanghaiButton = GameObject.Find("Shanghai");
        var EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        var infoBlock = GameObject.FindObjectsOfType<InfoBlock>(true)[0];
        var name = infoBlock.transform.GetChild(0).GetChild(0).gameObject;
        var description = infoBlock.transform.GetChild(0).GetChild(1).gameObject;
        var density = infoBlock.transform.GetChild(0).GetChild(3).gameObject;
        var gdp = infoBlock.transform.GetChild(0).GetChild(5).gameObject;

        PointerEventData eventData = new PointerEventData(EventSystem);
        eventData.position = ShanghaiButton.transform.position;
        ExecuteEvents.Execute(ShanghaiButton, eventData, ExecuteEvents.pointerEnterHandler);

        Assert.AreEqual("Shanghai", name.GetComponent<Text>().text);
        Assert.AreEqual("Shanghai is one of the four direct-administered municipalities of China (PRC), located on the southern estuary of the Yangtze River."
                        , description.GetComponent<Text>().text);
        Assert.AreEqual("3923", density.GetComponent<Text>().text);
        Assert.AreEqual("3870.1", gdp.GetComponent<Text>().text);



    }
}
