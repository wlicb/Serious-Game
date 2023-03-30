using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class News : MonoBehaviour
{
    private int value;

    public GameObject dataManagerObject;

    private int cityIndex;

    private DataManager dataManager;

    private string cityName;

    private Dictionary<int, string> nameMapping = new Dictionary<int, string>();

    void Start()
    {
        value = 0;
        dataManager = dataManagerObject.GetComponent<DataManager>();
        makeMapping();
    }

    // Update is called once per frame
    void Update()
    {
        cityIndex = dataManager.getMaxInfectionCity(1);
        cityName = nameMapping[cityIndex];
        value = dataManager.getIncrease(cityIndex);
        gameObject.GetComponent<Text>().text = cityName + " reported " + value.ToString() + " daily cases of COVID-19 yesterday.";
    }

    private void makeMapping()
    {
        nameMapping.Add(0, "Shanghai");
        nameMapping.Add(1, "Beijing");
        nameMapping.Add(2, "Tianjin");
        nameMapping.Add(3, "Guangzhou");
        nameMapping.Add(4, "Shenzhen");
        nameMapping.Add(5, "Chengdu");
        nameMapping.Add(6, "Chongqing");
        nameMapping.Add(7, "Wuhan");
        nameMapping.Add(8, "Nanchang");
        nameMapping.Add(9, "Shenyang");
        nameMapping.Add(10, "Changchun");
        nameMapping.Add(11, "Harbin");
        nameMapping.Add(12, "Huhhot");
        nameMapping.Add(13, "Urumqi");
        nameMapping.Add(14, "Lhasa");
        nameMapping.Add(15, "Xining");
        nameMapping.Add(16, "Lanzhou");
        nameMapping.Add(17, "Yinchuan");
        nameMapping.Add(18, "Xi'an");
        nameMapping.Add(19, "Taiyuan");
        nameMapping.Add(20, "Zhengzhou");
        nameMapping.Add(21, "Jinan");
        nameMapping.Add(22, "Qingdao");
        nameMapping.Add(23, "Nanjing");
        nameMapping.Add(24, "Hefei");
        nameMapping.Add(25, "Hangzhou");
        nameMapping.Add(26, "Fuzhou");
        nameMapping.Add(27, "Changsha");
        nameMapping.Add(28, "Guiyang");
        nameMapping.Add(29, "Kunming");
        nameMapping.Add(30, "Shijiazhuang");
        nameMapping.Add(31, "Nanning");
        nameMapping.Add(32, "Haikou");
    }

}
