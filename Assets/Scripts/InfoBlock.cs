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
        gameObject.transform.localPosition = ChinaMap.transform.GetChild(index + 4).transform.localPosition;
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
        briefDescription.Add(0, "Shanghai is one of the four direct-administered municipalities of China (PRC), located on the southern estuary of the Yangtze River.");
        briefDescription.Add(1, "Beijing is the capital of China. It is the center of power and development of the country.");
        briefDescription.Add(2, "Tianjin is a municipality and a coastal metropolis in Northern China on the shore of the Bohai Sea.");
        briefDescription.Add(3, "Guangzhou is the capital and largest city of Guangdong province in southern China.");
        briefDescription.Add(4, "Shenzhen is a major sub-provincial city and one of the special economic zones of China.");
        briefDescription.Add(5, "Chengdu is a sub-provincial city which serves as the capital of the Chinese province of Sichuan.");
        briefDescription.Add(6, "Chongqing is the only municipality in China located deep inland.");
        briefDescription.Add(7, "Wuhan is the largest city in Hubei and the most populous city in Central China, with a population of over eleven million.");
        briefDescription.Add(8, "Nanchang has become a major railway hub in Southern China in recent decades.");
        briefDescription.Add(9, "Shenyang is a major Chinese sub-provincial city and the provincial capital of Liaoning province.");
        briefDescription.Add(10, "Changchun is the capital and largest city of Jilin Province, comprising 7 districts, 1 county and 3 county-level cities.");
        briefDescription.Add(11, "Harbin is a sub-provincial city and the provincial capital and the largest city of Heilongjiang province.");
        briefDescription.Add(12, "Hohhot is the capital of Inner Mongolia in the north of the People's Republic of China, serving as the region's administrative, economic and cultural center.");
        briefDescription.Add(13, "Urumqi is the capital of the Xinjiang Uyghur Autonomous Region in the far northwest of China.");
        briefDescription.Add(14, "Lhasa is the urban center of the prefecture-level Lhasa City and the administrative capital of Tibet Autonomous Region in Southwest China.");
        briefDescription.Add(15, "Xining is the capital of Qinghai province in western China and the largest city on the Tibetan Plateau.");
        briefDescription.Add(16, "Lanzhou is a key regional transportation hub, connecting areas further west by rail to the eastern half of the country.");
        briefDescription.Add(17, "Yinchuan is the capital of the Ningxia Hui Autonomous Region, China.");
        briefDescription.Add(18, "Xi'an is a sub-provincial city on the Guanzhong Plain. the city is the most populous city in Northwest China.");
        briefDescription.Add(19, "Taiyuan is the capital and largest city of Shanxi Province.");
        briefDescription.Add(20, "Zhengzhou is one of the National Central Cities in China, located in north-central Henan.");
        briefDescription.Add(21, "Jinan is the capital of Shandong province in Eastern China.");
        briefDescription.Add(22, "Qingdao is a major city with the highest GDP in eastern Shandong Province.");
        briefDescription.Add(23, "Nanjing is he capital of Jiangsu province, a sub-provincial city, a megacity, and the third largest city in the East China region.");
        briefDescription.Add(24, "Hefei is the capital and largest city, and political, economic, and cultural center of Anhui Province.");
        briefDescription.Add(25, "Hangzhou is the capital and most populous city of Zhejiang, China.");
        briefDescription.Add(26, "Fuzhou is the capital and one of the largest cities in Fujian province.");
        briefDescription.Add(27, "Changsha is the capital and the largest city of Hunan Province.");
        briefDescription.Add(28, "Guiyang, historically rendered as Kweiyang, is the capital of Guizhou province");
        briefDescription.Add(29, "Kunming is the capital and largest city of Yunnan province. It is the political, economic, communications, and cultural center of the province.");
        briefDescription.Add(30, "Shijiazhuang is the capital and largest city of North China's Hebei Province.");
        briefDescription.Add(31, "Nanning is the capital and largest city by population of the Guangxi Zhuang Autonomous Region in Southern China.");
        briefDescription.Add(32, "Haikou is the capital and most populous city of the Chinese province of Hainan.");
    }
}
