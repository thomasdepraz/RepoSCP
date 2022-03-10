using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncidentReport : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI descriptionText; 

    public void SetupIncidentReport(string title, string text, int damage)
    {
        titleText.text = title;
        damageText.text = "Employé(s) perdu(s) : " + damage;
        descriptionText.text = text;
    }
}
