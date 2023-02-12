using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public GameObject helpCanvas;

    // public GameObject pauseButton;

    public Button button;



    private int currentContent;
    // Start is called before the first frame update
    void Start()
    {
        currentContent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showHelpScene() {
        var coroutine = showHelpSceneHelper();
        StartCoroutine(coroutine);
    }

    private IEnumerator showHelpSceneHelper() {
        helpCanvas.SetActive(true);
        helpCanvas.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        // pauseButton.GetComponent<PlayController>().toggle();
        button.interactable = false;
        Time.timeScale = 0;
    }


    public void Next() {
        if (currentContent < (helpCanvas.transform.GetChild(0).childCount - 1)) {
            changeContent(currentContent);
            currentContent++;
        } else {
            hideHelpScene(currentContent);
            currentContent = 0;
        }
    }

    public void Skip() {
        hideHelpScene(currentContent);
        currentContent = 0;
    }

    private void hideHelpScene(int index) {
        var coroutine = hideHelpHelper(index);
        StartCoroutine(coroutine);
    }

    private IEnumerator hideHelpHelper(int index) {
        string name = "Hide Content " + (index + 1);
        print(name);
        helpCanvas.transform.GetChild(0).GetChild(index).gameObject.GetComponent<Animator>().Play(name);
        helpCanvas.GetComponent<Animator>().Play("Hide Help Canvas");
        yield return new WaitForSeconds(1f);
        helpCanvas.transform.GetChild(0).GetChild(index).gameObject.SetActive(false);
        helpCanvas.SetActive(false);
        // pauseButton.GetComponent<PlayController>().toggle();
        button.interactable = true;
        Time.timeScale = 1;
    }

    private void changeContent(int index) {
        var coroutine = changeContentHelper(index);
        StartCoroutine(coroutine);
    }

    private IEnumerator changeContentHelper(int index) {
        string name = "Hide Content " + (index + 1);
        print(name);
        // helpCanvas.transform.GetChild(0).GetChild(index).gameObject.GetComponent<Animator>().Play(name);
        yield return new WaitForSeconds(1f);
        // print(123);
        helpCanvas.transform.GetChild(0).GetChild(index).gameObject.SetActive(false);
        helpCanvas.transform.GetChild(0).GetChild(index + 1).gameObject.SetActive(true);
        // yield return new WaitForSeconds(1f);
    }


}
