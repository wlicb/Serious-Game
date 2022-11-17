using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Math;

public class RandomNumber : MonoBehaviour
{
    private Text numText;

    public int digit;

    // public double value;
    // Start is called before the first frame update
    void Start()
    {
        // initInterval = Random.Range(0.2f, 0.5f);
        numText = GetComponent<Text>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // numText.text = (value % (Math.Pow(10, digit)));

    }

    public void UpdateFigure(int value) {
        // print(value);
        // numText.text = (value / (System.Math.Pow(10, digit))).ToString();
        var stringValue = value.ToString();
        if (digit >= stringValue.Length) {
            numText.text = "0";
        } else {
            numText.text = ((int)(stringValue[stringValue.Length -  digit - 1]) - 48).ToString();
        }
    }
}
