using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DateContent;
using static Wavecircle;
using static UpdateInfection;
using static LineChartController;

public class DataPaneController : MonoBehaviour
{
    public GameObject dataManagerObject;

    private DataManager dataManager;

    public GameObject infectionFigure;

    private UpdateInfection infectionScript;
    
    public GameObject deathFigure;

    private UpdateInfection deathScript;

    public GameObject satisfactionFigure;

    private Wavecircle satisfactionScript;

    public GameObject lineChartCases;

    public GameObject lineChartDeath;

    private LineChartController lineChartControllerCases;

    private LineChartController lineChartControllerDeath;

    public GameObject dateObject;

    private DateContent date;

    private int CityIndex;

    private int k = 7;

    
    // Start is called before the first frame update
    void Start()
    {
        CityIndex = -1;
        dataManager = dataManagerObject.GetComponent<DataManager>();
        infectionScript = infectionFigure.GetComponent<UpdateInfection>();
        deathScript = deathFigure.GetComponent<UpdateInfection>();
        satisfactionScript = satisfactionFigure.GetComponent<Wavecircle>();
        date = dateObject.GetComponent<DateContent>();
        lineChartControllerCases = lineChartCases.GetComponent<LineChartController>();
        lineChartControllerDeath = lineChartDeath.GetComponent<LineChartController>();
    }

    // Update is called once per frame
    void Update()
    {
        var infectionValue = 0;
        if (CityIndex == -1)
            infectionValue = dataManager.getTotalIncrease();
        else
            infectionValue = dataManager.getIncrease(CityIndex);
        infectionScript.UpdateNumber(infectionValue);

        var deathValue = 0;
        if (CityIndex == -1)
            deathValue = dataManager.getTotalDeath();
        else
            deathValue = dataManager.getDeath(CityIndex);
        deathScript.UpdateNumber(deathValue);
        

        var satisfactionValue = 100.0f;
        if (CityIndex == -1)
            satisfactionValue = dataManager.getAverageSatisfaction();
        else
            satisfactionValue = dataManager.getSatisfaction(CityIndex);
        satisfactionScript.UpdatePercent(satisfactionValue);

        date.UpdateDate(dataManager.getDateYear(), dataManager.getDateMonth(), dataManager.getDateDay());

        var infectionHistory = new int[k];
        if (CityIndex == -1)
            infectionHistory = dataManager.getTotalIncreaseHistory();
        else
            infectionHistory = dataManager.getIncreaseHistory(CityIndex);
        lineChartControllerCases.UpdateLine(infectionHistory);

        var deathHistory = new int[k];
        if (CityIndex == -1)
            deathHistory = dataManager.getTotalDeathHistory();
        else
            deathHistory = dataManager.getDeathHistory(CityIndex);
        lineChartControllerDeath.UpdateLine(deathHistory);

        lineChartControllerCases.UpdateX(dataManager.getCurrentDate());
        lineChartControllerDeath.UpdateX(dataManager.getCurrentDate());
        
    }

    public void changeCity(int i) {
        CityIndex = i;
    }

    public int getCityIndex() {
        return CityIndex;
    }
}
