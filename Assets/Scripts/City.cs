using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using static DataManager;
using static InfoBlock;

public class City : MonoBehaviour
{


    private int infection;

    public GameObject dataManagerObject;

    private DataManager dataManager;

    public int cityIndex;

    private InfoBlock infoBlock;


    // Start is called before the first frame update
    void Start()
    {
        dataManager = dataManagerObject.GetComponent<DataManager>();
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        gameObject.transform.localScale = new Vector3(0, 0, 1);
        infoBlock = gameObject.transform.parent.GetChild(0).gameObject.GetComponent<InfoBlock>();

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
