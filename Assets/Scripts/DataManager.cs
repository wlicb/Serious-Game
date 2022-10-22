using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using System;

public class DataManager : MonoBehaviour
{
    public int[] dailyCaseRanges;

    public int numCities;

    // may add deathCaseRanges, satisfactionRanges here, but for demo purposes we are just having variation on dailyCaseRange

    private int[] infections;

    private int[] deaths;
    private int[] satisfactions;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        infections = new int[numCities];
        deaths = new int[numCities];
        satisfactions = new int[numCities];
        for (var i = 0; i < numCities; i++) {
            infections[i] = 0;
            deaths[i] = 0;
            satisfactions[i] = 100;
        }
        coroutine = updateInfection(2.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator updateInfection(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            // update data: for now use random data, will use model to predict data in the future
            
            for (var i = 0; i < numCities; i++) {
                infections[i] += new System.Random().Next(dailyCaseRanges[i]);
                deaths[i] += new System.Random().Next(10);
                satisfactions[i] -= new System.Random().Next(10);
                if (satisfactions[i] <= 0) {
                    satisfactions[i] = 0;
                }
            }
            // print(infections);
        }
    }

    public int getInfection(int i) {
        return infections[i];
    }

    public int getDeath(int i) {
        return deaths[i];
    }

    public int getSatisfaction(int i) {
        return satisfactions[i];
    }

    public int getTotalInfection() {
        int total = 0;
        for (var i = 0; i < numCities; i++) {
            total += infections[i];
        }
        return total;
    }

    public int getTotalDeath() {
        int total = 0;
        for (var i = 0; i < numCities; i++) {
            total += deaths[i];
        }
        return total;
    }

    public int getAverageSatisfaction() {
        int total = 0;
        for (var i = 0; i < numCities; i++) {
            total += satisfactions[i];
        }
        return total / 10;

    }

}
