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
        print(infection);
        var scale = infection / 100;
        gameObject.transform.localScale = new Vector3(scale, scale, 1);
    }

    // private IEnumerator updateInfection(float waitTime)
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(waitTime);
    //         // update data: for now use random data, will use model to predict data in the future
    //         var newCase = new System.Random().Next(dailyCaseRange);
    //         infection += newCase;
    //         print(infection);
    //         var scale = (float)infection / 100;
    //         gameObject.transform.localScale = new Vector3(scale, scale, 1);
    //     }
    // }

    // not used up to now: to load some data from json file
    // void getInfectionData() {
    //     var path = @"E:\Unity\serious-game\Assets\Data\data_temp.json";
    //     Console.WriteLine(path);
    //     var json = System.IO.File.ReadAllText(path);

    //     // var objects = Newtonsoft.Json.Linq.JArray.Parse(json);
    //     // foreach(Newtonsoft.Json.Linq.JObject root in objects)
    //     // {
    //     //     foreach(KeyValuePair<String, Newtonsoft.Json.Linq.JToken> city in root)
    //     //     {
    //     //         var cityName = city.Key;
    //     //         var infection = city.Value["infection"];
    //     //         Console.WriteLine(cityName);
    //     //         Console.WriteLine("\n");
    //     //     }
    //     // }
    // }
}
