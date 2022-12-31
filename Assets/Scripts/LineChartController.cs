using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;



public class LineChartController : MonoBehaviour
{

    public GameObject line;

    public GameObject coordinates;

    public GameObject x;

    public void UpdateX(DateTime currentDate) {
        int numDays = 7;
        var days = new DateTime[numDays];
        for (int i = 0; i < numDays; i++) {
            days[i] = currentDate.AddDays(i-6);
        }
        var texts = new string[numDays];
        for (int i = 0; i < numDays; i++) {
            texts[i] = days[i].Month + "/" + days[i].Day;
        }
        for (int i = 0; i < numDays; i++) {
            x.transform.GetChild(i).gameObject.GetComponent<Text>().text = texts[i];
        }

    }

    public void UpdateLine(int[] values) {
        UpdatePoints(values, line, coordinates);
    }

    public void UpdatePoints(int[] values, GameObject line, GameObject coorindates) {
        GameObject[] points = new GameObject[line.transform.childCount];
        GameObject[] labels = new GameObject[coorindates.transform.childCount];
        for (int i = 0; i < line.transform.childCount; i++)
            points[i] = line.transform.GetChild(i).gameObject;
        for (int i = 0; i < coorindates.transform.childCount; i++) {
            labels[i] = coorindates.transform.GetChild(i).gameObject;
        }
        int[] scales = new int[coorindates.transform.childCount];
        int min = Min(values);
        int max = Max(values);
        int range = max - min;
        for (int i = 0; i < coorindates.transform.childCount; i++) {
            if (range == 0) {
                scales[i] = min;
            } else {
                scales[i] = min + range * i / coorindates.transform.childCount;
            }
            labels[i].GetComponent<Text>().text = scales[i].ToString();
        }
        int[] positions = new int[line.transform.childCount];
        for (int i = 0; i < line.transform.childCount; i++) {
            if (range == 0) {
                positions[i] = 0;
            } else {
                positions[i] = (values[i] - min) * 300 / range;
            }
            points[i].transform.localPosition = new Vector3(125 * i, positions[i], 0);
        }

    }

    // Below are some helper functions
    private int Min(int[] values) {
        int min = values[0];
        for (int i = 0; i < values.Length; i++) {
            if (values[i] < min) {
                min = values[i];
            }
        }
        return min;
    }

    private int Max(int[] values) {
        int max = values[0];
        for (int i = 0; i < values.Length; i++) {
            if (values[i] > max) {
                max = values[i];
            }
        }
        return max;
    }
}
