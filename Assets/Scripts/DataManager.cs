using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Threading;
using System.Linq;
// using static PopupController;

public class DataManager : MonoBehaviour
{

    private float waitTime;

    public GameObject modelObj;

    private Model model;

    public GameObject popupObject;

    private PopupController popupController;

    public GameObject gameEndControllerObject;
    
    private GameEndController gameEndController;

    public GameObject socialMediaMessageControllerObject;
    
    private SocialMediaMessageController socialMediaMessageController;

    public GameObject infoBlockObj;

    private InfoBlock infoBlock;

    // define some parameters to be tuned

    private int satisfactionPopupValue = 50;
    // 100 is only for testing purpose, should be 50

    private int deathPopupValue = 5;
    // 0 is only for testing purpose, should be 5

    private int numCities = 33;

    private int traceDays = 7;

    private int policyCount = 7;

    private int satisfactionThreshold = 30;
    // 99 is only for testing purpose, should be 30

    private int deathThreshold = 1;
    // 1 is only for testing purpose, should be 100

    private int successDays = 60;
    // 1 is only for testing purpose, should be 60

    private int numDaysToCountFrequency = 10;

    private int[] infections;

    // private Dictionary<int, int[]> increaseHistory = new Dictionary<int, int[]>();


    private int[,] increaseHistory;

    private float[,] policyValue;

    private float[ , , ] policyHistory;

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

    private void init() {
        startDate = new DateTime(2022, 3, 1);
        date = new DateTime(2022, 3, 1);
        startingDate = new DateTime(2022, 3, 1);
        infections = new int[numCities];
        lastIncrease = new int[numCities];
        deaths = new int[numCities];
        deathIncrease = new int[numCities];
        satisfactions = new int[numCities];
        increaseHistory = new int[numCities, traceDays];
        deathHistory = new int[numCities, traceDays];
        policyValue = new float[numCities, policyCount];
        policyHistory = new float[numCities, policyCount, numDaysToCountFrequency];
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
                for (var m = 0; m < numDaysToCountFrequency; m++) {
                    policyHistory[i,k,m] = 0;
                }
            }
        }
        // dayPassed = 0;
        // increaseHistory[dayPassed] = lastIncrease;

        // coroutine = updateData(8.0f);
        waitTime = 16.0f;
    }

    public DataManager() {
        init();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Thread.Sleep(2000);
        model = modelObj.GetComponent<Model>();

        popupController = popupObject.GetComponent<PopupController>();

        gameEndController = gameEndControllerObject.GetComponent<GameEndController>();

        socialMediaMessageController = socialMediaMessageControllerObject.GetComponent<SocialMediaMessageController>();

        infoBlock = infoBlockObj.GetComponent<InfoBlock>();

        init();
        coroutine = updateData();
        StartCoroutine(coroutine);
    }

    // private IEnumerator updateData(float waitTime)
    private IEnumerator updateData()
    {
        // The main game loop
        while (true)
        {
            print(waitTime);
            yield return new WaitForSeconds(waitTime);
            // dayPassed++;
            // update data: for now use random data, will use model to predict data in the future
            for (var i = 0; i < numCities; i++) {

                // These two arrays are for casting purposes
                var policies = new int[policyCount];
                var pastPolicies = new int[policyCount, numDaysToCountFrequency];
                for (var m = 0; m < policyCount; m++) {
                    policies[m] = (int)policyValue[i, m];
                }

                for (var k = 0; k < policyCount; k++) {
                    for (var n = 0; n < numDaysToCountFrequency; n++) {
                        if (n != numDaysToCountFrequency - 1) {
                            policyHistory[i, k, n] = policyHistory[i, k, n+1];
                        } else {
                            policyHistory[i, k, n] = policyValue[i, k];
                        }
                        pastPolicies[k, n] = (int)policyHistory[i, k, n];
                    }
                }

                int frequency = model.policyChangingFrequency(pastPolicies);
                
                // print(frequency);

                double stringency = model.stringencyIndex(policies);
                // lastIncrease[i] = 0;
                int p = 0;
                if (i == 4)
                    p = 1;
                lastIncrease[i] = model.calculateNewDailyInfection(stringency, lastIncrease[i], infoBlock.GetDensity(i), infoBlock.GetPopulation(i), p);
                infections[i] += lastIncrease[i];

                // print(lastIncrease[i]);

                deathIncrease[i] = model.calculateNewDailyDeath(lastIncrease[i], deathIncrease[i], infections[i]);
                deaths[i] += deathIncrease[i];
                satisfactions[i] = model.calculateNewSatisfaction(stringency, lastIncrease[i], infoBlock.GetGDP(i), deathIncrease[i], frequency, satisfactions[i]);
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
            if (getDaysPassed() >= successDays) {
                gameEndController.showGameEndScene(0, getDaysPassed(), 2, 0);
                socialMediaMessageController.endCoroutine();
                yield break;
            }

            for (var i = 0; i < numCities; i++) {
                if (satisfactions[i] <= satisfactionThreshold) {
                    gameEndController.showGameEndScene(i, getDaysPassed(), 0, satisfactionThreshold);
                    socialMediaMessageController.endCoroutine();
                    yield break;
                }
            }

            for (var i = 0; i < numCities; i++) {
                if (deaths[i] >= deathThreshold) {
                    gameEndController.showGameEndScene(i, getDaysPassed(), 1, deathThreshold);
                    socialMediaMessageController.endCoroutine();
                    yield break;
                }
            }

            // 

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

    public void changeWaitTime(float value) {
        waitTime = value;
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

    public int getMaxInfectionCity(int rank) {
        // int city_index = 0;
        // int max = infections[0];
        // for (var i = 1; i < numCities; i++)
        // {
        //     if (infections[i] > max)
        //     {
        //         city_index = i;
        //         max = infections[i];
        //     }
        // }
        // return city_index;
        List<int> infectionList = new List<int>(infections);
        var numbers = infectionList.OrderByDescending(x => x).Skip(rank - 1).Take(1); 
        var index = infectionList.FindIndex(x => x == numbers.First()); 
        return index;
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
            // print(result[j]);
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

    public float getWaitTime() {
        return waitTime;
    }

    // These three functions are only for testing purposes. Should not be called anywhere in the game.

    public void updateDeath(int[] death) {
        var tmp = deaths;
        deaths = death;
        for (var i = 0; i < numCities; i++) {
            deathIncrease[i] = death[i] - tmp[i];
            for (var j = 0; j < traceDays; j++) {
                if (j == (traceDays - 1)) {
                    // increaseHistory[i,j] = lastIncrease[i];
                    deathHistory[i,j] = deaths[i];
                } else {
                    // increaseHistory[i,j] = increaseHistory[i,j+1];
                    deathHistory[i,j] = deathHistory[i,j+1];
                }

            }
        }
    }

    public void updateInfection(int[] infection) {
        var tmp = infections;
        infections = infection;
        for (var i = 0; i < numCities; i++) {
            lastIncrease[i] = infection[i] - tmp[i];
            for (var j = 0; j < traceDays; j++) {
                if (j == (traceDays - 1)) {
                    increaseHistory[i,j] = lastIncrease[i];
                    // deathHistory[i,j] = deaths[i];
                } else {
                    increaseHistory[i,j] = increaseHistory[i,j+1];
                    // deathHistory[i,j] = deathHistory[i,j+1];
                }
                // print(increaseHistory[i, j] + "   " + i + "   " +  j);

            }
            
        }

    }

    public void updateSatisfaction(int[] satisfaction) {
        satisfactions = satisfaction;
    }


}
