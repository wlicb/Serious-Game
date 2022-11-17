using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBlock : MonoBehaviour
{
    private Dictionary<int, string> nameMapping = new Dictionary<int, string>();
    private Dictionary<int, double> populationDensity = new Dictionary<int, double>();
    private Dictionary<int, double> GDP = new Dictionary<int, double>();
    private Dictionary<int, string> briefDescription = new Dictionary<int, string>();

    public GameObject name;

    public GameObject description;

    public GameObject density;

    public GameObject gdp;

    public GameObject ChinaMap;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        makeMapping();
        addPopulationDensity();
        addGDP();
        addDescription();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hide() {
        gameObject.SetActive(false);
        
    }

    public void show(int index) {
        gameObject.SetActive(true);
        // gameObject.transform.SetParent(ChinaMap.transform.GetChild(index + 1));
        gameObject.transform.localPosition = ChinaMap.transform.GetChild(index + 2).transform.localPosition;
        updateContent(index);
    }

    public void updateContent(int index) {
        name.GetComponent<Text>().text = nameMapping[index];
        description.GetComponent<Text>().text = briefDescription[index];
        density.GetComponent<Text>().text = populationDensity[index].ToString();
        gdp.GetComponent<Text>().text = GDP[index].ToString();
    }

    private void makeMapping() {
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

    private void addPopulationDensity() {
        populationDensity.Add(0, 3923);
        populationDensity.Add(1, 1334);
        populationDensity.Add(2, 1158);
        populationDensity.Add(3, 2511);
        populationDensity.Add(4, 8793);
        populationDensity.Add(5, 1462);
        populationDensity.Add(6, 389);
        populationDensity.Add(7, 1452);
        populationDensity.Add(8, 844);
        populationDensity.Add(9, 695);
        populationDensity.Add(10, 368);
        populationDensity.Add(11, 188);
        populationDensity.Add(12, 198);
        populationDensity.Add(13, 294);
        populationDensity.Add(14, 27);
        populationDensity.Add(15, 320);
        populationDensity.Add(16, 332);
        populationDensity.Add(17, 321);
        populationDensity.Add(18, 1281);
        populationDensity.Add(19, 767);
        populationDensity.Add(20, 1678);
        populationDensity.Add(21, 898);
        populationDensity.Add(22, 910);
        populationDensity.Add(23, 1414);
        populationDensity.Add(24, 814);
        populationDensity.Add(25, 719);
        populationDensity.Add(26, 703);
        populationDensity.Add(27, 850);
        populationDensity.Add(28, 575);
        populationDensity.Add(29, 394);
        populationDensity.Add(30, 732);
        populationDensity.Add(31, 391);
        populationDensity.Add(32, 1240);
    }

    private void addGDP() {
        GDP.Add(0, 3870.1);
        GDP.Add(1, 3610.3);
        GDP.Add(2, 1408.4);
        GDP.Add(3, 2501.9);
        GDP.Add(4, 2767.0);
        GDP.Add(5, 1771.7);
        GDP.Add(6, 2500.3);
        GDP.Add(7, 1561.6);
        GDP.Add(8, 574.6);
        GDP.Add(9, 657.2);
        GDP.Add(10, 663.8);
        GDP.Add(11, 518.4);
        GDP.Add(12, 280.1);
        GDP.Add(13, 333.7);
        GDP.Add(14, 67.8);
        GDP.Add(15, 137.3);
        GDP.Add(16, 288.7);
        GDP.Add(17, 196.4);
        GDP.Add(18, 1002.0);
        GDP.Add(19, 415.3);
        GDP.Add(20, 1200.4);
        GDP.Add(21, 1014.1);
        GDP.Add(22, 1240.1);
        GDP.Add(23, 1481.8);
        GDP.Add(24, 1004.6);
        GDP.Add(25, 1610.6);
        GDP.Add(26, 1002.0);
        GDP.Add(27, 1214.3);
        GDP.Add(28, 431.2);
        GDP.Add(29, 673.4);
        GDP.Add(30, 593.5);
        GDP.Add(31, 472.6);
        GDP.Add(32, 179.2);
    }

    private void addDescription() {
        briefDescription.Add(0, "");
        briefDescription.Add(1, "");
        briefDescription.Add(2, "");
        briefDescription.Add(3, "");
        briefDescription.Add(4, "");
        briefDescription.Add(5, "");
        briefDescription.Add(6, "");
        briefDescription.Add(7, "");
        briefDescription.Add(8, "");
        briefDescription.Add(9, "");
        briefDescription.Add(10, "");
        briefDescription.Add(11, "");
        briefDescription.Add(12, "");
        briefDescription.Add(13, "");
        briefDescription.Add(14, "");
        briefDescription.Add(15, "");
        briefDescription.Add(16, "");
        briefDescription.Add(17, "");
        briefDescription.Add(18, "");
        briefDescription.Add(19, "");
        briefDescription.Add(20, "");
        briefDescription.Add(21, "");
        briefDescription.Add(22, "");
        briefDescription.Add(23, "");
        briefDescription.Add(24, "");
        briefDescription.Add(25, "");
        briefDescription.Add(26, "");
        briefDescription.Add(27, "");
        briefDescription.Add(28, "");
        briefDescription.Add(29, "");
        briefDescription.Add(30, "");
        briefDescription.Add(31, "");
        briefDescription.Add(32, "");
    }
}
