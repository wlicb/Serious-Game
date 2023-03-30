using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wavecircle : MonoBehaviour
{
    [Range(0, 100)]
    public float no1;

    public Transform wave;
    public Transform s, e;

    public Text theText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // UpdatePercent(no1);
    }

    public void UpdatePercent(float f)
    {
        wave.localPosition = new Vector3(wave.localPosition.x, s.localPosition.y + (e.localPosition.y - s.localPosition.y) * f / 100, wave.localPosition.z);

        theText.text = Mathf.RoundToInt(f) + "%";
        // print(wave.localPosition.y);
    }
}
