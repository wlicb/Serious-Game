using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaMessageController : MonoBehaviour
{
    public GameObject[] messagePrefab;
    

    private IEnumerator coroutine;
    
    private int messageCount;


    // Start is called before the first frame update
    void Start()
    {
        messageCount = 0;
        coroutine = updateMessage(1.0f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    private IEnumerator updateMessage(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            // update data: for now use random data, will use model to predict data in the future
            var numMessages = gameObject.transform.childCount;
            if (numMessages < 3) {
                var newMessage = Instantiate(messagePrefab[messageCount], new Vector3(transform.position.x, transform.position.y, transform.position.z) , Quaternion.identity);
                newMessage.transform.SetParent(gameObject.transform);
                newMessage.transform.localScale = new Vector3(1, 1, 1);
                var y = newMessage.transform.position.y;
                if (numMessages == 0) {
                    y += newMessage.transform.localScale.y;
                } else if (numMessages >= 2) {
                    y -= newMessage.transform.localScale.y;
                }
                newMessage.transform.position = new Vector3(newMessage.transform.position.x, y, newMessage.transform.localScale.z);
                newMessage.transform.SetParent(gameObject.transform);
            } else {
                var message1 = gameObject.transform.GetChild(0).gameObject;
                var message2 = gameObject.transform.GetChild(1).gameObject;
                var message3 = gameObject.transform.GetChild(2).gameObject;
                var newMessage = Instantiate(messagePrefab[messageCount % 3], message3.transform.position, Quaternion.identity);
                newMessage.transform.SetParent(gameObject.transform);
                newMessage.transform.localScale = new Vector3(1, 1, 1);
                message3.transform.position = message2.transform.position;
                message2.transform.position = message1.transform.position;
                Destroy(message1);
            }

            messageCount++;
            
        }
    }


}
