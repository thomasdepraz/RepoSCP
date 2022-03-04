using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SCP.Data;

public class TurnReport : MonoBehaviour
{
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

    public void UpdateReport()
    {
        //Récupérer l'argent gagner et l'afficher.

        dayCountText.text = "Jour : " + turnManager.dayCount;

        for(int i = 0; i < turnManager.incidents.Count; i++)
        {
            AddIncidentToReport(turnManager.incidents[i]);
        }
    }

    public void ExitReport()
    {
        this.gameObject.SetActive(false);
    }

    public void AddIncidentToReport(SCPIncident incidentType)
    {
        GameObject currentIncident = Instantiate(incidentButton, incidentsContent);

        if (incidentsContent.childCount == 1)
        {
            currentIncident.GetComponent<RectTransform>().position = new Vector3(0, 125, 0);
        }
        else
        {
            //currentIncident.GetComponent<RectTransform>().position = new Vector3(0, incidentsContent.GetChild(incidentsContent.transform.GetChildCount).position.y - 35, 0);
        }

        currentIncident.GetComponent<IncidentButton>().SetupButton(incidentType);
    }
}
