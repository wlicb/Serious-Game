using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataValue;
using static DateContent;
using static Wavecircle;
using static UpdateInfection;

public class DataPaneController : MonoBehaviour
{
    public GameObject dataManagerObject;

    private DataManager dataManager;

    // private DataValue infection;
    public GameObject infectionFigure;

    private UpdateInfection infectionScript;
    
    public GameObject deathFigure;

    private UpdateInfection deathScript;

    private DataValue death;

    // private DataValue satisfaction;

    // public GameObject satisfactionText;

    public GameObject satisfactionFigure;

    private Wavecircle satisfactionScript;

    public GameObject dateObject;

    private DateContent date;

    private int CityIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        dataManager = dataManagerObject.GetComponent<DataManager>();
        // infection = gameObject.transform.GetChild(2).GetChild(1).gameObject.GetComponent<DataValue>();
        infectionScript = infectionFigure.GetComponent<UpdateInfection>();
        // death = gameObject.transform.GetChild(3).GetChild(1).gameObject.GetComponent<DataValue>();
        deathScript = deathFigure.GetComponent<UpdateInfection>();
        // satisfaction = satisfactionText.GetComponent<DataValue>();
        satisfactionScript = satisfactionFigure.GetComponent<Wavecircle>();
        date = dateObject.GetComponent<DateContent>();
    }

    // Update is called once per frame
    void Update()
    {
        var infectionValue = 0;
        if (CityIndex == -1)
            infectionValue = dataManager.getTotalInfection();
        else
            infectionValue = dataManager.getInfection(CityIndex);
        // infection.UpdateFigure(infectionValue, 0.0f, 0);
        infectionScript.UpdateNumber(infectionValue);

        var deathValue = 0;
        if (CityIndex == -1)
            deathValue = dataManager.getTotalDeath();
        else
            deathValue = dataManager.getDeath(CityIndex);
        // death.UpdateFigure(deathValue, 0.0f, 1);
        deathScript.UpdateNumber(deathValue);
        

        var satisfactionValue = 100.0f;
        if (CityIndex == -1)
            satisfactionValue = dataManager.getAverageSatisfaction();
        else
            satisfactionValue = dataManager.getSatisfaction(CityIndex);
        // satisfaction.UpdateFigure(0, satisfactionValue, 2);
        satisfactionScript.UpdatePercent(satisfactionValue);

        date.UpdateDate(dataManager.getDateYear(), dataManager.getDateMonth(), dataManager.getDateDay());
    }

    public void changeCity(int i) {
        CityIndex = i;
    }
}
