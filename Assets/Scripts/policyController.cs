using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataManager;

public class policyController : MonoBehaviour
{
    public GameObject datamanagerObj;

    public Slider[] sliders;

    private DataManager dataManager;

    private int cityIndex;


    

    // Start is called before the first frame update
    void Start()
    {
        dataManager = datamanagerObj.GetComponent<DataManager>();
        cityIndex = -1;
        for (var i = 0; i < sliders.Length; i++) {
            sliders[i].enabled = false;
        }

    }


    public void changeCity(int idx) {
        cityIndex = idx;
        if (cityIndex == -1) {
            for (var i = 0; i < sliders.Length; i++) {
                sliders[i].wholeNumbers = false;
                sliders[i].enabled = false;
                sliders[i].value = dataManager.getAveragePolicyIndex(i);
            }
        } else {
            for (var i = 0; i < sliders.Length; i++) {
                sliders[i].value = dataManager.getPolicyIndex(cityIndex, i);
                sliders[i].wholeNumbers = true;
                sliders[i].enabled = true;
            }
        }
    }

    public void updatePolicy(int policyIndex, float value) {
        dataManager.updatePolicyIndex(cityIndex, policyIndex, value);
    }

    public int getCityIndex() {
        return cityIndex;
    }

}
