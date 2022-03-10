using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelpPannel : MonoBehaviour
{
    [TextArea(3,10)]
    public string argentDescription;
    [TextArea(3, 10)]
    public string sallesDescription;
    [TextArea(3, 10)]
    public string missionsDescription;
    [TextArea(3, 10)]
    public string personnelDescription;
    [TextArea(3, 10)]
    public string placementDescription;
    [TextArea(3, 10)]
    public string incidentsDescription;

    public TextMeshProUGUI description;


    public void OpenDescription(int number)
    {
        switch (number)
        {
            case 0:
                description.text = argentDescription;
                break;
            case 1:
                description.text = sallesDescription;
                break;
            case 2:
                description.text = missionsDescription;
                break;
            case 3:
                description.text = personnelDescription;
                break;
            case 4:
                description.text = placementDescription;
                break;
            case 5:
                description.text = incidentsDescription;
                break;
            default:
                break;
        }
    }
}
