using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class News : MonoBehaviour
{
    private int value;

    public GameObject dataManagerObject;

    public int cityIndex;

    private DataManager dataManager;

    public string cityName;

    void Start()
    {
        value = 0;
        dataManager = dataManagerObject.GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        value = dataManager.getIncrease(cityIndex);
        gameObject.GetComponent<Text>().text = cityName + " reported " + value.ToString() + " daily cases of COVID-19 yesterday.";
    }
}
