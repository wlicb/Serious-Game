using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataValue : MonoBehaviour
{
    // Start is called before the first frame update

    private int value;

    public GameObject dataManagerObject;

    public int type;

    private DataManager dataManager;

    void Start()
    {
        value = 0;
        dataManager = dataManagerObject.GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (type == 0)
            value = dataManager.getTotalInfection();
        else if (type == 1)
            value = dataManager.getTotalDeath();
        else
            value = dataManager.getAverageSatisfaction();
        gameObject.GetComponent<Text>().text = value.ToString();
        if (type == 2)
            gameObject.GetComponent<Text>().text += "%";
    }
}
