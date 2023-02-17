using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using static GameRoundValue;

public class GameStartController : MonoBehaviour
{

    public GameObject mainCanvas;

    public GameObject ruleCanvas;

    // public Animator changeSceneAnimator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGame() {
        GameRoundValue.roundCount++;
        print(roundCount);
        var coroutine = loadGameHelper();
        StartCoroutine(coroutine);
    }

    private IEnumerator loadGameHelper() {
        mainCanvas.GetComponent<Animator>().Play("Start Main Canvas Hide");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }

    public void loadRule() {
        ruleCanvas.SetActive(true);
        var coroutine = loadRuleHelper();
        StartCoroutine(coroutine);
    }

    private IEnumerator loadRuleHelper() {
        mainCanvas.GetComponent<Animator>().Play("Start Main Canvas Hide");
        yield return new WaitForSeconds(1f);
        mainCanvas.SetActive(false);
    }

    public void returnMain() {
        mainCanvas.SetActive(true);
        var coroutine = returnMainHelper();
        StartCoroutine(coroutine);
    }

    private IEnumerator returnMainHelper() {
        ruleCanvas.GetComponent<Animator>().Play("Rule Canvas Hide");
        yield return new WaitForSeconds(1f);
        ruleCanvas.SetActive(false);

    }

}
