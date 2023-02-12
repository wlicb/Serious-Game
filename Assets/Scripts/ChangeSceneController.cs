using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneController : MonoBehaviour
{

    public Animator changeSceneAnimator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGame() {
        var coroutine = loadGameHelper();
        StartCoroutine(coroutine);
    }

    private IEnumerator loadGameHelper() {
        changeSceneAnimator.SetTrigger("Start");
        print(123);
        yield return new WaitForSeconds(2f);
        // Thread.Sleep(1200);
        SceneManager.LoadScene("SampleScene");
    }

}
