using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using static DataManager;

public class City : MonoBehaviour
{


    private int infection;

    public GameObject dataManagerObject;

    private DataManager dataManager;

    public int cityIndex;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = dataManagerObject.GetComponent<DataManager>();
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        gameObject.transform.localScale = new Vector3(0, 0, 1);
        // For now, we just initialize the infection to 0, while in reality we will real from json data
        // infection = dataManager.getInfection(cityIndex);

        // coroutine = updateInfection(2.0f);
        // StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        // update the circle
        // var newCase = new System.Random().Next(100);
        // infection += newCase;
        infection = dataManager.getInfection(cityIndex);
        // print(infection);
        var scale = infection / 100;
        gameObject.transform.localScale = new Vector3(scale, scale, 1);
    }
}
