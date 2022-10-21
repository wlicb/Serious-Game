using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectionValue : MonoBehaviour
{
    // Start is called before the first frame update

    private int value;

    public GameObject dataManagerObject;

    private DataManager dataManager;

    void Start()
    {
        value = 0;
        dataManager = dataManagerObject.GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        value = dataManager.getTotalInfection();
        gameObject.GetComponent<Text>().text = value.ToString();
    }
}
