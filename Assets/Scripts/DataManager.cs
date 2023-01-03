using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
// using static PopupController;

public class DataManager : MonoBehaviour
{
    public int[] dailyCaseRanges;

    public GameObject popupObject;

    private PopupController popupController;

    public GameObject gameEndControllerObject;
    
    private GameEndController gameEndController;

    public GameObject socialMediaMessageControllerObject;
    
    private SocialMediaMessageController socialMediaMessageController;

    private int satisfactionPopupValue = 5;

    private int deathPopupValue = 10;

    private int numCities;

    private int traceDays = 7;

    private int policyCount = 7;

    // may add deathCaseRanges, satisfactionRanges here, but for demo purposes we are just having variation on dailyCaseRange

    private int[] infections;

    // private Dictionary<int, int[]> increaseHistory = new Dictionary<int, int[]>();


    private int[,] increaseHistory;

    private float[,] policyValue;

    private int[,] deathHistory;

    private int[] lastIncrease;

    // private int dayPassed;

    private int[] deaths;

    private int[] deathIncrease;
    private int[] satisfactions;

    private DateTime date;

    private DateTime startingDate;

    private IEnumerator coroutine;

    private DateTime startDate;


    // Start is called before the first frame update
    void Start()
    {
        popupController = popupObject.GetComponent<PopupController>();

        gameEndController = gameEndControllerObject.GetComponent<GameEndController>();

        socialMediaMessageController = socialMediaMessageControllerObject.GetComponent<SocialMediaMessageController>();

        startDate = new DateTime(2022, 3, 1);
        date = new DateTime(2022, 3, 1);
        startingDate = new DateTime(2022, 3, 1);
        numCities = dailyCaseRanges.Length;
        infections = new int[numCities];
        lastIncrease = new int[numCities];
        deaths = new int[numCities];
        deathIncrease = new int[numCities];
        satisfactions = new int[numCities];
        increaseHistory = new int[numCities, traceDays];
        deathHistory = new int[numCities, traceDays];
        policyValue = new float[numCities, policyCount];
        for (var i = 0; i < numCities; i++) {
            infections[i] = 0;
            deaths[i] = 0;
            deathIncrease[i] = 0;
            lastIncrease[i] = 0;
            satisfactions[i] = 100;
            for (var j = 0; j < traceDays; j++) {
                increaseHistory[i,j] = 0;
                deathHistory[i,j] = 0;
            }
            for (var k = 0; k < policyCount; k++) {
                policyValue[i,k] = 0;
            }
        }
        // dayPassed = 0;
        // increaseHistory[dayPassed] = lastIncrease;
        coroutine = updateData(5.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator updateData(float waitTime)
    {
        // The main game loop
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            // dayPassed++;
            // update data: for now use random data, will use model to predict data in the future
            for (var i = 0; i < numCities; i++) {
                lastIncrease[i] = new System.Random().Next(dailyCaseRanges[i]);
                infections[i] += lastIncrease[i];
                double factor = new System.Random().NextDouble()  * 0.1;
                deathIncrease[i] = (int)(lastIncrease[i] * factor);
                deaths[i] += deathIncrease[i];
                satisfactions[i] -= new System.Random().Next(10);
                if (satisfactions[i] <= 0) {
                    satisfactions[i] = 0;
                }
                for (var j = 0; j < traceDays; j++) {
                    if (j == (traceDays - 1)) {
                        increaseHistory[i,j] = lastIncrease[i];
                        deathHistory[i,j] = deaths[i];
                    } else {
                        increaseHistory[i,j] = increaseHistory[i,j+1];
                        deathHistory[i,j] = deathHistory[i,j+1];
                    }

                }
                // print(increaseHistory[1]);
                // increaseHistory.Add(dayPassed, lastIncrease);
                // increaseHistory[dayPassed] = lastIncrease;
            }
            date = date.AddDays(1);

            // check end condition
            for (var i = 0; i < numCities; i++) {
                if (satisfactions[i] == 0) {
                    gameEndController.showGameEndScene(i, getDaysPassed());
                    socialMediaMessageController.endCoroutine();
                    yield break;
                }
            }

            // show satisfaction popup
            for (var i = 0; i < numCities; i++) {
                if (satisfactions[i] < satisfactionPopupValue) {
                    popupController.showPopup(i, 0);
                } else {
                    popupController.hidePopup(i, 0);
                }
            }

            // show death popup
            for (var i = 0; i < numCities; i++) {
                if (deathIncrease[i] > deathPopupValue) {
                    popupController.showPopup(i, 1);
                } else {
                    popupController.hidePopup(i, 1);
                }
            }
        }
    }


    public void updatePolicyIndex(int cityIndex, int policyIndex, float value) {
        if (cityIndex == -1) {
            return;
        }
        policyValue[cityIndex, policyIndex] = value;
        print("city " + cityIndex + " policy " + policyIndex + " is updated to " + value); 
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

    public int getMaxInfectionCity() {
        int city_index = 0;
        int max = infections[0];
        for (var i = 1; i < numCities; i++)
        {
            if (infections[i] > max)
            {
                city_index = i;
                max = infections[i];
            }
        }
        return city_index;
    }

    public int getTotalInfection() {
        int total = 0;
        for (var i = 0; i < numCities; i++) {
            total += infections[i];
        }
        return total;
    }

    public int getTotalIncrease() {
        int total = 0;
        for (var i = 0; i < numCities; i++) {
            total += lastIncrease[i];
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

    public int[] getIncreaseHistory(int cityIndex) {
        int[] result = new int[traceDays];
        for (int j = 0; j < traceDays; j++) {
            result[j] = increaseHistory[cityIndex, j];
        }
        return result;
    }

    
    public int[] getTotalIncreaseHistory() {
        int[] result = new int[traceDays];
        for (int j = 0; j < traceDays; j++) {
            result[j] = 0;
            for (int i = 0; i < numCities; i++) {
                result[j] += increaseHistory[i,j];
            }
        }
        return result;
    }

    public int[] getDeathHistory(int cityIndex) {
        int[] result = new int[traceDays];
        for (int j = 0; j < traceDays; j++) {
            result[j] = deathHistory[cityIndex, j];
        }
        return result;
    }

    public int[] getTotalDeathHistory() {
        int[] result = new int[traceDays];
        for (int j = 0; j < traceDays; j++) {
            result[j] = 0;
            for (int i = 0; i < numCities; i++) {
                result[j] += deathHistory[i,j];
            }
        }
        return result;
    }

    public DateTime getCurrentDate() {
        return date;
    }

    public int getDaysPassed() {
        return (int)((date - startDate).TotalDays);
    }

    public float getPolicyIndex(int cityIndex, int policyIndex) {
        return policyValue[cityIndex, policyIndex];
    }

    public float getAveragePolicyIndex(int policyIndex) {
        float result = 0.0f;
        for (var i = 0; i < numCities; i++) {
            result += policyValue[i,policyIndex];
        }
        result /= numCities;
        return result;
    }
}
