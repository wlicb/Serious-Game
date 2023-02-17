using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameRoundValue;

public class TutorialController : MonoBehaviour
{

    public GameObject[] pages;

    public GameObject pauseButton;

    // public GameObject ShanghaiDot;

    public GameObject dataPaneController;

    public GameObject schoolClosingSlider;

    // public GameObject chinaMap;

    private int currentIndex = -1;

    void Awake() {
        if (GameRoundValue.roundCount == 1) {
            initialze();
        }
    }


    public void initialze() {
        // gameObject.SetActive(true);
        pages[0].SetActive(true);
        currentIndex = 0;
        // pauseButton.GetComponent<PlayController>().pause();
        var coroutine = initializeHelper();
        StartCoroutine(coroutine);
    }

    public IEnumerator initializeHelper() {
        yield return new WaitForSeconds(1f);
        pauseButton.GetComponent<PlayController>().pause();
    }

    // public void nextPage() {
    //     pages[currentIndex].SetActive(false);
    //     currentIndex++;
    //     if (currentIndex == pages.Length) {
    //         gameObject.SetActive(false);
    //         currentIndex = -1;
    //         pauseButton.GetComponent<PlayController>().toggle();
    //     } else {
    //         pages[currentIndex].SetActive(true);
    //     }
    // }

    public void lastPage() {
        pages[currentIndex].SetActive(false);
        currentIndex--;
        pages[currentIndex].SetActive(true);
    }

    public void nextPage() {
        pages[currentIndex].SetActive(false);
        currentIndex++;
        var coroutine = nextPageWithInteractionHelper();
        StartCoroutine(coroutine);
    }

    private IEnumerator nextPageWithInteractionHelper() {
        pauseButton.GetComponent<PlayController>().resume();
        // print(currentIndex);
        int idx = currentIndex - 1;

        // Change the interaction forms here
        if (idx == 1) {
            // yield return new WaitForSeconds(9.3f);
        } else if (idx == 2) {
            int seconds = 0;
            while (seconds < 6) {
                yield return new WaitForSeconds(1f);
                if (dataPaneController.GetComponent<DataPaneController>().getCityIndex() == 0) {
                    break;
                } else {
                    seconds++;
                }
            }
            if (seconds == 6) {
                currentIndex--;
                // print(currentIndex);
            }
        } else if (idx == 4) {
            int seconds = 0;
            while (seconds < 6) {
                yield return new WaitForSeconds(1f);
                if (schoolClosingSlider.GetComponent<Slider>().value == 1.0f) {
                    break;
                } else {
                    seconds++;
                }
            }
            if (seconds == 6) {
                currentIndex--;
                // print(currentIndex);
            }
        } else if (idx == 6) {
            int seconds = 0;
            while (seconds < 6) {
                yield return new WaitForSeconds(1f);
                if (dataPaneController.GetComponent<DataPaneController>().getCityIndex() == -1) {
                    break;
                } else {
                    seconds++;
                }
            }
            if (seconds == 6) {
                currentIndex--;
                // print(currentIndex);
            }
        }



        pauseButton.GetComponent<PlayController>().pause();
        if (currentIndex == pages.Length) {
            currentIndex = -1;
            pauseButton.GetComponent<PlayController>().resume();
        } else {
            pages[currentIndex].SetActive(true);
        }
    }
    
}
