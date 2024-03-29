using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Model : MonoBehaviour
{

    private int Sum(int[] vals) {
        var result = 0;
        for (var i = 0; i < vals.Length; i++) {
            result += vals[i];
        }
        return result;
    }

    public int calculateNewSatisfaction(double stringency_index, int lastIncrease, double GDP, int lastDeath, int policy_changing_frequency, int lastSatisfaction) {
        int dailyChange = 0;
        // print("Stringency index = " + stringency_index);
        // Calculate daily change based on stringency index
        if (stringency_index > 40) {
            dailyChange -= (int)Math.Pow((stringency_index - 40), 0.6); // Non-linear increase
            // print("A");
        } else {
            dailyChange += (int)Math.Pow((40 - stringency_index), 0.6); // Non-linear decrease
            // print("B");
        }

        // Calculate daily change based on last increase and death
        if (lastIncrease > 0){
            if (lastIncrease > 500) {
                dailyChange -= (int)(Math.Log(lastIncrease, 10)); // Non-linear decrease with log
                // print("C");
            }else{
                dailyChange += (int)(Math.Log(lastIncrease, 8));
                // print("D");
            }
        }
        if (lastDeath > 20) {
            dailyChange -= (int)(Math.Log(lastDeath, 5)); // Non-linear decrease with log
        }
        // Calculate daily change based on GDP
        // if (GDP > 1000) {
        //     dailyChange += (int)(GDP / 1000.0); // Non-linear increase
        // }

        // Cap daily change between -10 and 10
        dailyChange = Math.Max(Math.Min(dailyChange, 10), -10);
        // print("daily Change = " + dailyChange);

        // Calculate new satisfaction level
        int newSatisfaction = lastSatisfaction + dailyChange - policy_changing_frequency;

        // Return new satisfaction level, capped at 0 and 100
        return Math.Max(Math.Min(newSatisfaction, 100), 0);
    }

    public int calculateNewDailyDeath(int lastIncrease, int lastDeath, int totalInfection)
    {
        // Define the base death rate as a random number between 0.6 and 1.4
        double baseDeathRate = new System.Random().NextDouble() * (1.5 - 0.5) + 0.5;

        // Define the death rate multiplier based on the last increase in infection cases
        double increaseMultiplier = 1.0;
        if (lastIncrease > 5000)
        {
            increaseMultiplier = 1.5;
        }
        else if (lastIncrease > 500)
        {
            increaseMultiplier = 1.2;
        }
        else if (lastIncrease > 50)
        {
            increaseMultiplier = 1.1;
        }

        // Define the death rate multiplier based on the last death cases
        double deathMultiplier = 1.0;
        if (lastDeath > 100)
        {
            deathMultiplier = 1.5;
        }
        else if (lastDeath > 50)
        {
            deathMultiplier = 1.2;
        }
        else if (lastDeath > 10)
        {
            deathMultiplier = 1.1;
        }

        // Calculate the predicted death cases for the new day
        int predictedDeath = (int)Math.Round(lastIncrease * deathMultiplier * increaseMultiplier * baseDeathRate)/1000;
        if (totalInfection > 0)
            predictedDeath = predictedDeath + new System.Random().Next(0, 1);

        return predictedDeath;
    }

    public int calculateNewDailyInfection(double stringency_index, int lastIncrease, double populationDensity, int population, int p)
    {
        population /= 3000;
        double baseInfectionRate = 0.05; // set the base infection rate to 5%
        double stringencyFactor = 1 - stringency_index / 200.0; // calculate the factor based on stringency index
        double densityFactor = Math.Pow(populationDensity / 9000.0, 0.5); // calculate the factor based on population density
        double lastIncreaseFactor = 1 + Math.Log10(lastIncrease + 1); // calculate the factor based on the last increase

        double randomFactor = 0.9 + 0.2 * new System.Random().NextDouble(); // generate a random factor between 0.9 and 1.1

        double newInfectionRate = baseInfectionRate * stringencyFactor * densityFactor * lastIncreaseFactor * randomFactor; // calculate the new infection rate
        double newDailyInfection = newInfectionRate * population; // calculate the new daily infection cases 

        int result = (int)Math.Round(newDailyInfection);
        if (p == 1) {
            print("stringency index is " + stringency_index + ", last increase is " + lastIncrease + ", population density is " + populationDensity + 
                ", population is " + population + 
                ", predicted new infection cases is " + result);
        }

        return result;
    }

    public double stringencyIndex(int[] policies)
    {
        double index = (double)policies[0] / (double)3 + 
                        (double)policies[1] / (double)3 + 
                        (double)policies[2] / (double)2 + 
                        (double)policies[3] / (double)4 + 
                        (double)policies[4] / (double)2 + 
                        (double)policies[5] / (double)3 + 
                        (double)policies[6] / (double)2;
        index /= (double)7.0;
        index *= (double)100.0;
        return index;
    }

    public int policyChangingFrequency(int[,] pastPolicies) {
        int policyCount = pastPolicies.GetLength(0);
        int traceDays = pastPolicies.GetLength(1);
        int[,] differences = new int[policyCount, traceDays - 1];
        int totalChange = 0;
        for (var i = 0; i < policyCount; i++) {
            for (var j = 0; j < traceDays - 1; j++) {
                differences[i, j] = Math.Abs(pastPolicies[i, j + 1] - pastPolicies[i, j]);
                totalChange += differences[i, j];
            }
        }
        return totalChange / traceDays;

    }

}