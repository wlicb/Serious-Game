using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{

    public int calculateNewDailyInfection(int cityIndex, int lastIncrease, int[] policies) {
        var val = 0;
        if (lastIncrease == 0) {
            val = new System.Random().Next(500);
        } else {
            val = new System.Random().Next(500) * (20 - Sum(policies)) / 20 * lastIncrease / 500;
        } 
        // print(val);
        return val;
    }

    public int calculateNewDailyDeath(int cityIndex, int dailyInfection) {
        double factor = new System.Random().NextDouble()  * 0.1;
        int val = (int)(dailyInfection * factor);
        return val;
    }

    public int calculateNewSatisfaction(int cityIndex, int infection, int death, int[] policies, int lastSatisfaction) {
        var val = lastSatisfaction;
        if (Sum(policies) >= 10) {
            val -= new System.Random().Next(10);
        } else {
            val += new System.Random().Next(10);
        }

        if (death >= 10) {
            val -= new System.Random().Next(10);
        }

        if (val < 0) {
            val = 0;
        } else if (val > 100) {
            val = 100;
        }
        return val;
    }

    private int Sum(int[] vals) {
        var result = 0;
        for (var i = 0; i < vals.Length; i++) {
            result += vals[i];
        }
        return result;
    }
}