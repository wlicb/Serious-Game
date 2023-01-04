using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartController : MonoBehaviour
{

    public GameObject mainCanvas;

    public GameObject ruleCanvas;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGame() {
        mainCanvas.GetComponent<Animator>().Play("Start Main Cavnas Hide");
        SceneManager.LoadScene("SampleScene");
    }

    public void loadRule() {
        mainCanvas.SetActive(false);
        ruleCanvas.SetActive(true);
    }

    public void returnMain() {
        ruleCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}
