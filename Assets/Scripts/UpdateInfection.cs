using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RandomNumber;

public class UpdateInfection : MonoBehaviour
{
    public GameObject[] digits;
    public void UpdateNumber(int value) {
        for (int i = 0; i < digits.Length; i++) {
            digits[i].GetComponent<RandomNumber>().UpdateFigure(value);
        }
    }

}
