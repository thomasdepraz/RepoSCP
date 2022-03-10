using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SCP.Data;

public class TurnReport : MonoBehaviour
{
    public GameObject incidentDetails;
    public TurnManager turnManager;
    public TextMeshProUGUI moneyGainedText;
    public TextMeshProUGUI dayCountText;
    public Transform incidentsContent;
    public GameObject incidentButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateReport(int income)
    {
        moneyGainedText.text = "Revenu : " + income + " k";
        dayCountText.text = "Jour : " + turnManager.dayCount;

        for (int i = 0; i < turnManager.incidents.Count; i++)
        {
            AddIncidentToReport(turnManager.incidents[i]);
        }
    }

    public void ExitReport()
    {
        incidentDetails.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void AddIncidentToReport(SCPIncident incidentType)
    {
        Debug.Log("GoButton");

        GameObject currentIncident = Instantiate(incidentButton, incidentsContent);

        currentIncident.GetComponent<IncidentButton>().descriptionScreen = incidentDetails;
        currentIncident.GetComponent<IncidentButton>().SetupButton(incidentType);
    }

    public void ClearIncidentButtons()
    {
        for (int g = 0; g < incidentsContent.childCount; g++)
        {
            Destroy(incidentsContent.GetChild(0).gameObject);
        }
    }
}
