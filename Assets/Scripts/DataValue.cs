using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataValue : MonoBehaviour
{
    public void UpdateFigure(int value, float valueFloat, int type) {
        if (type == 0) {
            // var value = dataManager.getTotalInfection();
            gameObject.GetComponent<Text>().text = value.ToString();
        }
        else if (type == 1) {
            // var value = dataManager.getTotalDeath();
            gameObject.GetComponent<Text>().text = value.ToString();
        }
        else {
            // var valueFloat = (float) value;
            gameObject.GetComponent<Text>().text = valueFloat.ToString() + "%";
        }
    }
}
