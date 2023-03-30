using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pace : MonoBehaviour
{
    public GameObject dataManagerObject;

    private DataManager dataManager;

    private bool isFaster;

    public Sprite fasterSprite;

    public Sprite slowerSprite;

    void Start() {
        dataManager = dataManagerObject.GetComponent<DataManager>();
        isFaster = false;
    }

    private void faster() {
        dataManager.changeWaitTime(8.0f);
    }

    private void slower() {
        dataManager.changeWaitTime(16.0f);
    }

    public void toggle() {
        if (!isFaster) {
            faster();
            isFaster = true;
            gameObject.GetComponent<Image>().sprite = slowerSprite;
        } else {
            slower();
            isFaster = false;
            gameObject.GetComponent<Image>().sprite = fasterSprite;
        }
    }
}
