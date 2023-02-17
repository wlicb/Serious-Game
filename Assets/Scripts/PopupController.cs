using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CitySelectController;

public class PopupController : MonoBehaviour
{
    public GameObject satisfactionPrefab;

    public GameObject deathPrefab;

    private GameObject[] satisfactionList;

    private GameObject[] deathList;
    
    public GameObject ChinaMap;

    public GameObject popupPane;

    private int numCities = 33;
    // Start is called before the first frame update
    void Start()
    {
        satisfactionList = new GameObject[numCities];
        deathList = new GameObject[numCities];
    }

    public void showPopup(int index, int type) {
        if (type == 0) {
            if (satisfactionList[index] == null) {
                // print(index);
                satisfactionList[index] = Instantiate(satisfactionPrefab);
                satisfactionList[index].transform.SetParent(popupPane.transform);
                satisfactionList[index].transform.localPosition = ChinaMap.transform.GetChild(index + 6).transform.localPosition;
                satisfactionList[index].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        } else {
            if (deathList[index] == null) {
                // print(index);
                deathList[index] = Instantiate(deathPrefab);
                deathList[index].transform.SetParent(popupPane.transform);
                deathList[index].transform.localPosition = ChinaMap.transform.GetChild(index + 6).transform.localPosition;
                deathList[index].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }

    }

    public void hidePopup(int index, int type) {
        if (type == 0) {
            if (satisfactionList[index] != null) {
                var animator = satisfactionList[index].GetComponent<Animator>();
                animator.Play("Satisfaction Popup Disappear");
                // float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
                // yield return new WaitForSecondsRealtime(animationLength);

                Destroy(satisfactionList[index], 2f);
                satisfactionList[index] = null;
            }
        } else {
            if (deathList[index] != null) {
                var animator = deathList[index].GetComponent<Animator>();
                animator.Play("Death Popup Disappear");
                // float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
                // yield return new WaitForSecondsRealtime(animationLength);

                Destroy(deathList[index], 2f);
                deathList[index] = null;
            }
        }

    }
}
