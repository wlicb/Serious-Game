using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static DataManager;
using static InfoBlock;
using TMPro;

public class SocialMediaMessageController : MonoBehaviour
{
    public GameObject[] messagePrefab;
    
    private IEnumerator coroutine;
    
    private int messageCount;

    public GameObject dataManagerObject;

    private DataManager dataManager;

    private int positive_comment_rate;

    private Dictionary<int, string> nameMapping = new Dictionary<int, string>();


    // values of 0 means that no need to replace. 1 meas need to replace
    private IDictionary<string, int> positive_comment = new Dictionary<string, int>()
    {
        {"我们要相信政府啊", 0},
        {"坚定支持政府一切措施", 0},
        {"我们能想到的政府肯定能想到", 0},
        {"{0}的疫情已经在好转啦 大家要乐观", 1},
        {"只要大家团结一致我们肯定能度过难关的", 0}
    };

    private IDictionary<string, int> neutural_comment = new Dictionary<string, int>()
    {
        {"大家先不要着急评判 等一段时间再看", 0},
        {"虽然大家现在很难受但也不要被带节奏" , 0},
        {"目前的政策有利有弊", 0 }
    };

    private IDictionary<string, int> negative_comment = new Dictionary<string, int>()
    {
        {"这{0}政府就是啥事都做不好 任由疫情扩散", 1},
        {"为什么{0}政府一点人文关怀都没有啊", 1 },
        {"{0}政府可以给市民们最基本的尊重吗", 1 },
        {"疫情这么严重 连门都不敢出了555", 0 },
        {"没做啥事就被封在家里 无妄之灾", 0 },
        {"{0}口号喊得那么响却一点实事都不做", 1 },
        {"这{0}疫情也太可怕了吧",1 }
    };



    // Start is called before the first frame update
    void Start()
    {
        messageCount = 0;
        coroutine = updateMessage(1.0f);
        StartCoroutine(coroutine);
        dataManager = dataManagerObject.GetComponent<DataManager>();
        makeMapping();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    private IEnumerator updateMessage(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            // update data: for now use random data, will use model to predict data in the future
            positive_comment_rate = (int)dataManager.getAverageSatisfaction() - 10;
            positive_comment_rate = System.Math.Max(0, positive_comment_rate);
            int x = UnityEngine.Random.Range(0, 101);
            
            var numMessages = gameObject.transform.childCount;
            if (numMessages < 3) {
                var newMessage = Instantiate(messagePrefab[messageCount], new Vector3(transform.position.x, transform.position.y, transform.position.z) , Quaternion.identity);
                newMessage.transform.SetParent(gameObject.transform);
                newMessage.transform.localScale = new Vector3(1, 1, 1);

                if (x < positive_comment_rate)
                {
                    //show positive comment
                    List<string> keyList = new List<string>(positive_comment.Keys);
                    System.Random rand = new System.Random();
                    string randomKey = keyList[rand.Next(keyList.Count)];
                    print(randomKey);
                    if(positive_comment[randomKey] == 0)
                    {
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    }
                    else
                    {
                        int city_index = dataManager.getMaxInfectionCity();
                        string city = nameMapping[city_index];
                        var s = string.Format(randomKey, city);
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    }
                }
                else if (x > 90)
                {
                    //show neutural comment
                    List<string> keyList = new List<string>(neutural_comment.Keys);
                    System.Random rand = new System.Random();
                    string randomKey = keyList[rand.Next(keyList.Count)];
                    if (neutural_comment[randomKey] == 0)
                    {
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    }
                    else
                    {
                        int city_index = dataManager.getMaxInfectionCity();
                        string city = nameMapping[city_index];
                        var s = string.Format(randomKey, city);
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    }
                }
                else
                {
                    //show negative comment
                    List<string> keyList = new List<string>(negative_comment.Keys);
                    System.Random rand = new System.Random();
                    string randomKey = keyList[rand.Next(keyList.Count)];
                    if (negative_comment[randomKey] == 0)
                    {
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    } 
                    else
                    {
                        int city_index = dataManager.getMaxInfectionCity();
                        string city = nameMapping[city_index];
                        var s = string.Format(randomKey, city);
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    }
                }
                var y = newMessage.transform.position.y;
                if (numMessages == 0) {
                    y += newMessage.transform.localScale.y;
                } else if (numMessages >= 2) {
                    y -= newMessage.transform.localScale.y;
                }
                newMessage.transform.position = new Vector3(newMessage.transform.position.x, y, newMessage.transform.localScale.z);
                newMessage.transform.SetParent(gameObject.transform);
            } else {
                var message1 = gameObject.transform.GetChild(0).gameObject;
                var message2 = gameObject.transform.GetChild(1).gameObject;
                var message3 = gameObject.transform.GetChild(2).gameObject;
                var newMessage = Instantiate(messagePrefab[messageCount % 3], message3.transform.position, Quaternion.identity);
                newMessage.transform.SetParent(gameObject.transform);
                newMessage.transform.localScale = new Vector3(1, 1, 1);


                if (x < positive_comment_rate)
                {
                    //show positive comment
                    List<string> keyList = new List<string>(positive_comment.Keys);
                    System.Random rand = new System.Random();
                    string randomKey = keyList[rand.Next(keyList.Count)];
                    print(randomKey);
                    if(positive_comment[randomKey] == 0)
                    {
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    }
                    else
                    {
                        int city_index = dataManager.getMaxInfectionCity();
                        string city = nameMapping[city_index];
                        var s = string.Format(randomKey, city);
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    }
                }
                else if (x > 90)
                {
                    //show neutural comment
                    List<string> keyList = new List<string>(neutural_comment.Keys);
                    System.Random rand = new System.Random();
                    string randomKey = keyList[rand.Next(keyList.Count)];
                    if (neutural_comment[randomKey] == 0)
                    {
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    }
                    else
                    {
                        int city_index = dataManager.getMaxInfectionCity();
                        string city = nameMapping[city_index];
                        var s = string.Format(randomKey, city);
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    }
                }
                else
                {
                    //show negative comment
                    List<string> keyList = new List<string>(negative_comment.Keys);
                    System.Random rand = new System.Random();
                    string randomKey = keyList[rand.Next(keyList.Count)];
                    if (negative_comment[randomKey] == 0)
                    {
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    } 
                    else
                    {
                        int city_index = dataManager.getMaxInfectionCity();
                        string city = nameMapping[city_index];
                        var s = string.Format(randomKey, city);
                        newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    }
                }
                message3.transform.position = message2.transform.position;
                message2.transform.position = message1.transform.position;
                Destroy(message1);
            }

            messageCount++;
            
        }
    }
    private void makeMapping()
    {
        nameMapping.Add(0, "上海");
        nameMapping.Add(1, "北京");
        nameMapping.Add(2, "天津");
        nameMapping.Add(3, "广州");
        nameMapping.Add(4, "深圳");
        nameMapping.Add(5, "成都");
        nameMapping.Add(6, "重庆");
        nameMapping.Add(7, "武汉");
        nameMapping.Add(8, "南昌");
        nameMapping.Add(9, "沈阳");
        nameMapping.Add(10, "长春");
        nameMapping.Add(11, "哈尔滨");
        nameMapping.Add(12, "呼和浩特");
        nameMapping.Add(13, "乌鲁木齐");
        nameMapping.Add(14, "拉萨");
        nameMapping.Add(15, "西宁");
        nameMapping.Add(16, "兰州");
        nameMapping.Add(17, "银川");
        nameMapping.Add(18, "西安");
        nameMapping.Add(19, "太原");
        nameMapping.Add(20, "郑州");
        nameMapping.Add(21, "济南");
        nameMapping.Add(22, "青岛");
        nameMapping.Add(23, "南京");
        nameMapping.Add(24, "合肥");
        nameMapping.Add(25, "杭州");
        nameMapping.Add(26, "福州");
        nameMapping.Add(27, "长沙");
        nameMapping.Add(28, "贵阳");
        nameMapping.Add(29, "昆明");
        nameMapping.Add(30, "石家庄");
        nameMapping.Add(31, "南宁");
        nameMapping.Add(32, "海口");
    }


}
