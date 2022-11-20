using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static DataManager;
using static InfoBlock;

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
        {"����Ҫ����������", 0},
        {"�ᶨ֧������һ�д�ʩ", 0},
        {"�������뵽�������϶����뵽", 0},
        {"{0}�������Ѿ��ں�ת�������Ҫ�ֹ�", 1},
        {"ֻҪ����Ž�һ�����ǿ϶��ܶȹ��ѹص�", 0}
    };

    private IDictionary<string, int> neutural_comment = new Dictionary<string, int>()
    {
        {"����Ȳ�Ҫ�ż����У���һ��ʱ���ٿ�", 0},
        {"��Ȼ������ں����ܵ�Ҳ��Ҫ��������" , 0},
        {"Ŀǰ�����������б�", 0 }
    };

    private IDictionary<string, int> negative_comment = new Dictionary<string, int>()
    {
        {"��{0}��������ɶ�¶������ã�����������ɢ", 1},
        {"Ϊʲô{0}����һ�����Ĺػ���û�а�", 1 },
        {"{0}�������Ը��������������������", 1 },
        {"������ô���� ���Ŷ����ҳ���555", 0 },
        {"û��ɶ�¾ͱ����ڼ������֮��", 0 },
        {"{0}�ںź�����ô��ȴһ��ʵ�¶�����", 1 },
        {"��{0}����Ҳ̫�����˰�",1 }
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
                        newMessage.GetComponent<TextMesh>().text = randomKey;
                    }
                    else
                    {
                        int city_index = dataManager.getMaxInfectionCity();
                        string city = nameMapping[city_index];
                        var s = string.Format(randomKey, city);
                        newMessage.GetComponent<TextMesh>().text = s;
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
                        newMessage.GetComponent<TextMesh>().text = randomKey;
                    }
                    else
                    {
                        int city_index = dataManager.getMaxInfectionCity();
                        string city = nameMapping[city_index];
                        var s = string.Format(randomKey, city);
                        newMessage.GetComponent<TextMesh>().text = s;
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
                        newMessage.GetComponent<TextMesh>().text = randomKey;
                    } 
                    else
                    {
                        int city_index = dataManager.getMaxInfectionCity();
                        string city = nameMapping[city_index];
                        var s = string.Format(randomKey, city);
                        newMessage.GetComponent<TextMesh>().text = s;
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
                message3.transform.position = message2.transform.position;
                message2.transform.position = message1.transform.position;
                Destroy(message1);
            }

            messageCount++;
            
        }
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
