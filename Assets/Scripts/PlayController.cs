using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayController : MonoBehaviour
{
    private bool paused;

    public Sprite pauseSprite;

    public Sprite playSprite;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggle() {
        if (!paused) {
            paused = true;
            Time.timeScale = 0;
            gameObject.GetComponent<Image>().sprite = playSprite;
        } else {
            paused = false;
            Time.timeScale = 1;
            gameObject.GetComponent<Image>().sprite = pauseSprite;
        }

    }

}
