using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateContent : MonoBehaviour
{
    public void UpdateDate(int year, int month, int day) {
        gameObject.GetComponent<Text>().text = year + "-" + month + "-" + day;
    }

}
