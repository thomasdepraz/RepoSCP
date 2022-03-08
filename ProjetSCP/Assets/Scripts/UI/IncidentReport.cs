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
        damageText.text = "Membre(s) du personnel perdu : " + damage;
        descriptionText.text = text;
    }
}
