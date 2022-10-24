using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataValue;
using static DateContent;

public class DataPaneController : MonoBehaviour
{
    public GameObject dataManagerObject;

    private DataManager dataManager;

    private DataValue infection;

    private DataValue death;

    private DataValue satisfaction;

    public GameObject dateObject;

    private DateContent date;
    
    // Start is called before the first frame update
    void Start()
    {
        dataManager = dataManagerObject.GetComponent<DataManager>();
        infection = gameObject.transform.GetChild(2).GetChild(1).gameObject.GetComponent<DataValue>();
        death = gameObject.transform.GetChild(3).GetChild(1).gameObject.GetComponent<DataValue>();
        satisfaction = gameObject.transform.GetChild(4).GetChild(1).gameObject.GetComponent<DataValue>();
        date = dateObject.GetComponent<DateContent>();
    }

    // Update is called once per frame
    void Update()
    {
        var infectionValue = dataManager.getTotalInfection();
        infection.UpdateFigure(infectionValue, 0.0f, 0);

        var deathValue = dataManager.getTotalDeath();
        death.UpdateFigure(deathValue, 0.0f, 1);
        
        var satisfactionValue = dataManager.getAverageSatisfaction();
        satisfaction.UpdateFigure(0, satisfactionValue, 2);

        date.UpdateDate(dataManager.getDateYear(), dataManager.getDateMonth(), dataManager.getDateDay());
    }
}
