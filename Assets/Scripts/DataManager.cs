using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using System;

public class DataManager : MonoBehaviour
{
    public int[] dailyCaseRanges;

    private int numCities;

    // may add deathCaseRanges, satisfactionRanges here, but for demo purposes we are just having variation on dailyCaseRange

    private int[] infections;

    private int[] lastIncrease;

    private int[] deaths;
    private int[] satisfactions;

    private DateTime date;

    private IEnumerator coroutine;


    // Start is called before the first frame update
    void Start()
    {
        date = new DateTime(2022, 3, 1);
        numCities = dailyCaseRanges.Length;
        infections = new int[numCities];
        lastIncrease = new int[numCities];
        deaths = new int[numCities];
        satisfactions = new int[numCities];
        for (var i = 0; i < numCities; i++) {
            infections[i] = 0;
            deaths[i] = 0;
            lastIncrease[i] = 0;
            satisfactions[i] = 100;
        }
        coroutine = updateData(2.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator updateData(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            // update data: for now use random data, will use model to predict data in the future
            for (var i = 0; i < numCities; i++) {
                lastIncrease[i] = new System.Random().Next(dailyCaseRanges[i]);
                infections[i] += lastIncrease[i];
                deaths[i] += new System.Random().Next(10);
                satisfactions[i] -= new System.Random().Next(10);
                if (satisfactions[i] <= 0) {
                    satisfactions[i] = 0;
                }
            }
            date = date.AddDays(1);
            // print(date.Day);
            // print(infections);
        }
    }

    public int getDateDay() {
        return date.Day;
    }

    public int getDateMonth() {
        return date.Month;
    }

    public int getDateYear() {
        return date.Year;
    }

    public int getInfection(int i) {
        return infections[i];
    }

    public int getIncrease(int i) {
        return lastIncrease[i];
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

    public float getAverageSatisfaction() {
        int total = 0;
        for (var i = 0; i < numCities; i++) {
            total += satisfactions[i];
        }
        return (float)(total / numCities);

    }

}
