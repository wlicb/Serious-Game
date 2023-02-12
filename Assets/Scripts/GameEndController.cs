using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndController : MonoBehaviour
{

    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;

    private float zoomLerpSpeed = 10f;

    public GameObject ChinaMap;
    
    public GameObject gameEndCanvas;

    public GameObject mainCanvas;

    private Dictionary<int, string> nameMapping = new Dictionary<int, string>();





    // Start is called before the first frame update
    void Start()
    {
        makeMapping();
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showGameEndScene(int index, int days, int type, int threshold) {
        print(index + "Game End");
        if (type == 0) {
            gameEndCanvas.SetActive(true);
            gameEndCanvas.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = 
                        "Game Over";
            gameEndCanvas.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = 
                        "Citizen satisfaction dropped to " + threshold + "% at " + nameMapping[index];
            gameEndCanvas.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Text>().text = 
                        "You survived " + days + " days in the outbreak of COVID-19";
        } else if (type == 1) {
            gameEndCanvas.SetActive(true);
            gameEndCanvas.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = 
                        "Game Over";
            gameEndCanvas.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = 
                        "Total Death exceeds " + threshold + " at " + nameMapping[index];
            gameEndCanvas.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Text>().text = 
                        "You survived " + days + " days in the outbreak of COVID-19";
        } else {
            gameEndCanvas.SetActive(true);
            gameEndCanvas.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = 
                        "Congratulations";
            gameEndCanvas.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = 
                        "";
            gameEndCanvas.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Text>().text = 
                        "You managed to survive " + days + " days in the outbreak of COVID-19";
        }


        
    }

    public void reload() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.UnloadScene(scene.buildIndex); 
        SceneManager.SetActiveScene(scene);
        SceneManager.LoadScene(scene.name);
        print("reloading");
    }

    public void returnMain() {
        // Scene startScene = SceneManager.GetSceneByName("StartScene");
        // Scene scene = SceneManager.GetActiveScene();
        // SceneManager.UnloadScene(scene.buildIndex); 
        // print(startScene.name);
        SceneManager.LoadScene("StartScene");
        // SceneManager.SetActiveScene(startScene);
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
}
