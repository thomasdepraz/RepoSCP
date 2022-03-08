using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncidentButton : MonoBehaviour
{
    public GameObject descriptionScreen;
    public TextMeshProUGUI incidentTitle;
    private string incidentDescription;
    private int incidentDamage;

    public void SetupButton(SCPIncident incidentType)
    {
        int random = Random.Range(1, 4);

        switch (random) 
        {
            case 1:
                incidentTitle.text = incidentType.incident1Title;
                incidentDescription = incidentType.incident1Description;
                break;

            case 2:
                incidentTitle.text = incidentType.incident2Title;
                incidentDescription = incidentType.incident2Description;
                break;

            case 3:
                incidentTitle.text = incidentType.incident3Title;
                incidentDescription = incidentType.incident3Description;
                break;
        }

        incidentDamage = incidentType.damage;
    }

    public void UpdateDescriptionScreen()
    {
        descriptionScreen.SetActive(true);
        descriptionScreen.GetComponent<IncidentReport>().SetupIncidentReport(incidentTitle.text, incidentDescription, incidentDamage);
    }
}
