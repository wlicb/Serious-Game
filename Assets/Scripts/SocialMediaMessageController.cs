using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static DataManager;
using static InfoBlock;
using TMPro;
using System.Threading;

public class SocialMediaMessageController : MonoBehaviour
{
    public GameObject messagePrefab;
    
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
        {"{0}的疫情已经在好转啦，大家要乐观", 1},
        {"政府的行动快速而有效，我们有信心度过难关", 0},
        {"疫情严峻，但是政府已经采取了很多有力措施，我们支持他们！", 0},
        {"我们应该感谢政府在疫情中给予我们的支持", 0},
        {"奉劝大家，保持冷静，积极配合政府的疫情防控措施", 0},
        {"疫情难关，我们一起顶住", 0},
        {"我对{0}的未来充满信心！疫情必定能早日消失", 1},
        {"感谢{0}政府对疫情防控的刻苦付出!", 1},
        {"为了让{0}安全，我们要配合政府的措施", 1},
        {"因为大家的努力，{0}疫情已经得到了控制", 0},
        {"感谢{0}政府的无私奉献，努力控制疫情", 1},
        {"大家一起努力，{0}就一定能迎刃而解", 1},
        {"在这段时间里，大家都为{0}的安全做了努力", 1},
        {"让我们把所有的力量都用在抗击疫情上，给{0}带来更多的希望", 1},
        {"看到疫情的积极变化，大家要继续保持信心", 0},
        {"通过我们的共同努力，{0}的疫情终将得以控制", 0},
        {"我们是{0}最坚强的人民，疫情不会打倒我们", 1},
        {"把信心寄托在政府身上是对我们责任的体现，也是对{0}未来的期望", 1},
        {"只要大家团结一致我们肯定能度过难关的", 0}
    };

    private IDictionary<string, int> neutural_comment = new Dictionary<string, int>()
    {
        {"大家先不要着急评判，等一段时间再看", 0},
        {"虽然大家现在很难受但也不要被带节奏" , 0},
        {"目前的政策有利有弊", 0 },
        {"对于{0}的疫情，政府的措施还在试验中", 1},
        {"对于政府的决策，我们需要更多的信息" , 0},
        {"我们应该等待更多的信息，然后再做出评价", 0 },
        {"疫情是人类共同面临的挑战，大家都要有责任感", 0},
        {"我们需要更多的实际行动来应对疫情" , 0},
        {"在疫情期间，大家也要注意自己的行为", 0 },
        {"疫情防控需要团结一致，也要有耐心", 0},
        {"疫情是没有颜色的，每个人都要承担责任" , 0},
        {"有些事情我们不能左右，只能接受", 0 },
        {"不同的人对于疫情的评价也不同，我们要尊重每一种意见", 0},
        {"当前的情况不容乐观，但也不容悲观" , 0},
        {"在疫情面前，每个人都有责任与义务", 0 }
    };

    private IDictionary<string, int> negative_comment = new Dictionary<string, int>()
    {
        {"这{0}政府就是啥事都做不好，任由疫情扩散\U0001F605", 1},
        {"为什么{0}政府一点人文关怀都没有啊", 1 },
        {"{0}政府可以给市民们最基本的尊重吗", 1 },
        {"疫情这么严重，连门都不敢出了555", 0 },
        {"没做啥事就被封在家里，无妄之灾", 0 },
        {"{0}口号喊得那么响却一点实事都不做", 1 },
        {"政府对于{0}疫情的应对不力，令人担忧", 1},
        {"为什么{0}政府还不能控制疫情？", 1 },
        {"{0}市民应该有更多的权利和保护，但政府却不采取措施", 1 },
        {"真不敢相信{0}会爆发这么严重的疫情", 1 },
        {"疫情让我们感受到了政府的无力和无能", 0 },
        {"{0}政府没有对疫情采取有效措施，这真是不可原谅", 1 },
        {"{0}政府完全不顾市民的感受，再也不相信它们了", 1},
        {"真的受够了{0}政府对疫情的不作为，真的没有人性了吗", 1 },
        {"希望有一天{0}不再被疫情困扰，但政府的态度真的不太乐观", 1 },
        {"疫情的影响太大了，政府的措施真的有效吗", 0 },
        {"疫情带给了我们太多的困扰，{0}政府毫无人性地对待了我们", 1 },
        {"市民的声音得到了{0}政府的忽视，真的很沮喪", 1 },
        {"疫情对我们的生活带来了太大的影响，{0}政府的应对方案不够理想", 1},
        {"{0}的政府只是为了自己的利益，完全不关心市民的困境", 1 },
        {"这{0}疫情也太可怕了吧",1 }
    };

    private string[] nickname_list;

    public Sprite[] avatars;


    // Start is called before the first frame update
    void Start()
    {
        // Thread.Sleep(2000);

        messageCount = 0;
        coroutine = updateMessage(2.0f);
        StartCoroutine(coroutine);
        dataManager = dataManagerObject.GetComponent<DataManager>();
        makeMapping();
        nickname_list = new string[35];
        nicknameMapping();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    void generateComment(List<string> keyList, GameObject newMessage, IDictionary<string, int> comment)
    {
        System.Random rand = new System.Random();
        string randomKey = keyList[rand.Next(keyList.Count)];
        print(comment[randomKey]);
        if (comment[randomKey] == 0)
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
            if (numMessages < 4) {
                var newMessage = Instantiate(messagePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z) , Quaternion.identity);
                newMessage.transform.SetParent(gameObject.transform);
                newMessage.transform.localScale = new Vector3(1, 1, 1);

                if (x < positive_comment_rate)
                {
                    //show positive comment
                    List<string> keyList = new List<string>(positive_comment.Keys);
                    generateComment(keyList, newMessage, positive_comment);
                    // System.Random rand = new System.Random();
                    // string randomKey = keyList[rand.Next(keyList.Count)];
                    // // print(randomKey);
                    // if(positive_comment[randomKey] == 0)
                    // {
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    // }
                    // else
                    // {
                    //     int city_index = dataManager.getMaxInfectionCity();
                    //     string city = nameMapping[city_index];
                    //     var s = string.Format(randomKey, city);
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    // }
                }
                else if (x > 90)
                {
                    //show neutural comment
                    List<string> keyList = new List<string>(neutural_comment.Keys);
                    generateComment(keyList, newMessage, neutural_comment);
                    // System.Random rand = new System.Random();
                    // string randomKey = keyList[rand.Next(keyList.Count)];
                    // if (neutural_comment[randomKey] == 0)
                    // {
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    // }
                    // else
                    // {
                    //     int city_index = dataManager.getMaxInfectionCity();
                    //     string city = nameMapping[city_index];
                    //     var s = string.Format(randomKey, city);
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    // }
                }
                else
                {
                    //show negative comment
                    List<string> keyList = new List<string>(negative_comment.Keys);
                    generateComment(keyList, newMessage, negative_comment);
                    // System.Random rand = new System.Random();
                    // string randomKey = keyList[rand.Next(keyList.Count)];
                    // if (negative_comment[randomKey] == 0)
                    // {
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    // } 
                    // else
                    // {
                    //     int city_index = dataManager.getMaxInfectionCity();
                    //     string city = nameMapping[city_index];
                    //     var s = string.Format(randomKey, city);
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    // }
                }
                var y = -(newMessage.transform.localScale.y) * numMessages - 0.2f * numMessages;
                newMessage.transform.position = new Vector3(newMessage.transform.position.x, y, newMessage.transform.localScale.z);
                newMessage.transform.SetParent(gameObject.transform);


                System.Random randname = new System.Random();
                var nameIdx = randname.Next(nickname_list.Length);
                System.Random randAvatar = new System.Random();
                var avatarIdx = randAvatar.Next(avatars.Length);
                newMessage.transform.GetChild(1).GetComponent<Text>().text = nickname_list[nameIdx];
                newMessage.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = avatars[avatarIdx];


            } else {
                var message1 = gameObject.transform.GetChild(0).gameObject;
                var message2 = gameObject.transform.GetChild(1).gameObject;
                var message3 = gameObject.transform.GetChild(2).gameObject;
                var message4 = gameObject.transform.GetChild(3).gameObject;

                StartCoroutine(moveMessageHelper(message1, message2, message3, message4, x));
                
                
            }

            messageCount++;
            
        }
    }

    public IEnumerator moveMessageHelper(GameObject message1, GameObject message2, GameObject message3, GameObject message4, int x) {
        message1.GetComponent<Animator>().Play("Message Hide");
        yield return new WaitForSeconds(1f);
        var newPos = message4.transform.position;
        float step = 119.03f;
        message4.transform.position = Vector3.MoveTowards(message4.transform.position, message3.transform.position, step);
        // message4.transform.position = message3.transform.position;
        message3.transform.position = Vector3.MoveTowards(message3.transform.position, message2.transform.position, step);
        message2.transform.position = Vector3.MoveTowards(message2.transform.position, message1.transform.position, step);
        Destroy(message1);
        var newMessage = Instantiate(messagePrefab, newPos, Quaternion.identity);
                newMessage.transform.SetParent(gameObject.transform);
                newMessage.transform.localScale = new Vector3(1, 1, 1);


                if (x < positive_comment_rate)
                {
                    //show positive comment
                    List<string> keyList = new List<string>(positive_comment.Keys);
                    generateComment(keyList, newMessage, positive_comment);
                    // System.Random rand = new System.Random();
                    // string randomKey = keyList[rand.Next(keyList.Count)];
                    // // print(randomKey);
                    // if(positive_comment[randomKey] == 0)
                    // {
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    // }
                    // else
                    // {
                    //     int city_index = dataManager.getMaxInfectionCity();
                    //     string city = nameMapping[city_index];
                    //     var s = string.Format(randomKey, city);
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    // }
                }
                else if (x > 90)
                {
                    //show neutural comment
                    List<string> keyList = new List<string>(neutural_comment.Keys);
                    generateComment(keyList, newMessage, neutural_comment);
                    // System.Random rand = new System.Random();
                    // string randomKey = keyList[rand.Next(keyList.Count)];
                    // if (neutural_comment[randomKey] == 0)
                    // {
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    // }
                    // else
                    // {
                    //     int city_index = dataManager.getMaxInfectionCity();
                    //     string city = nameMapping[city_index];
                    //     var s = string.Format(randomKey, city);
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    // }
                }
                else
                {
                    //show negative comment
                    List<string> keyList = new List<string>(negative_comment.Keys);
                    generateComment(keyList, newMessage, negative_comment);
                    // System.Random rand = new System.Random();
                    // string randomKey = keyList[rand.Next(keyList.Count)];
                    // if (negative_comment[randomKey] == 0)
                    // {
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = randomKey;
                    // } 
                    // else
                    // {
                    //     int city_index = dataManager.getMaxInfectionCity();
                    //     string city = nameMapping[city_index];
                    //     var s = string.Format(randomKey, city);
                    //     newMessage.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = s;
                    // }
                }
                



                System.Random randname = new System.Random();
                var nameIdx = randname.Next(nickname_list.Length);
                System.Random randAvatar = new System.Random();
                var avatarIdx = randAvatar.Next(avatars.Length);
                newMessage.transform.GetChild(1).GetComponent<Text>().text = nickname_list[nameIdx];
                newMessage.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = avatars[avatarIdx];
    }

    public void endCoroutine() {
        StopAllCoroutines();
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

    private void nicknameMapping() {
        nickname_list[0] = "爱笑一生";
        nickname_list[1] = "热爱冒险";
        nickname_list[2] = "心驰神往";
        nickname_list[3] = "自在自由";
        nickname_list[4] = "璀璨星尘"; 
        nickname_list[5] = "热爱冒险";
        nickname_list[6] = "开心每一天";
        nickname_list[7] = "胡闹天王";
        nickname_list[8] = "欢乐小鬼";
        nickname_list[9] = "小猪萌";
        nickname_list[10] = "狗狗爱好者";
        nickname_list[11] = "猫咪控";
        nickname_list[12] = "狗仔队长";
        nickname_list[13] = "充满正能量";
        nickname_list[14] = "快乐无限";
        nickname_list[15] = "笑颜常开";
        nickname_list[16] = "活力四溢";
        nickname_list[17] = "点燃者";
        nickname_list[18] = "愿景家";
        nickname_list[19] = "梦想家";
        nickname_list[20] = "不进前五十不改名";
        nickname_list[21] = "不瘦十斤不改名";
        nickname_list[22] = "有生一定要环游世界";
        nickname_list[23] = "山峦秀美";
        nickname_list[24] = "JoyBoy";
        nickname_list[25] = "BiBi_handsome";
        nickname_list[26] = "Never";
        nickname_list[27] = "Dreamer";
        nickname_list[28] = "YH_Walker";
        nickname_list[29] = "Mamba_forever";
        nickname_list[30] = "love3000";
        nickname_list[31] = "backstreetboy";
        nickname_list[32] = "狗狗";
        nickname_list[33] = "kitty";
        nickname_list[34] = "阿巴阿巴";
    }


}
