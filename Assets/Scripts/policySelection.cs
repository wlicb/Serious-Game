using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static policyController;

public class policySelection : MonoBehaviour
{
    // Start is called before the first frame update
    public int policyIndex;

    public GameObject policyControllerObject;

    private policyController pc;

    void Start()
    {
        pc = policyControllerObject.GetComponent<policyController>();
    }


    public void updatePolicy(float value) {
        pc.updatePolicy(policyIndex, value);
    }
}
