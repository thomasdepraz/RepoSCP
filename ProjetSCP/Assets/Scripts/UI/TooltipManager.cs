using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public TextMeshProUGUI tooltipText;

    [Header("Tooltip Content")]
    [TextArea(2,15)]
    public string moneyDescription;
    [TextArea(2, 15)]
    public string workersDescription;
    [TextArea(2, 15)]
    public string nextTurnDescription;
    [TextArea(2, 15)]
    public string catalogueDescription;
    [TextArea(2, 15)]
    public string houseDescription;
    [TextArea(2, 15)]
    public string powerDescription;
    [TextArea(2, 15)]
    public string stockDescription;
    [TextArea(2, 15)]
    public string SCPRoomDescription;
    [TextArea(2, 15)]
    public string PCDescription;
    [TextArea(2, 15)]
    public string missionDescription;
    [TextArea(2, 15)]
    public string buildModeDescription;
    [TextArea(2, 15)]
    public string marketDescription;
    [TextArea(2, 15)]
    public string visitDescription;

    [TextArea(2, 15)]
    public string overlayDescription;

    public void Awake()
    {
        new Registry().Register<TooltipManager>(this);
    }

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateTooltip (string UIElement)
    {
        switch (UIElement)
        {
            case "Money":
                tooltipText.text = moneyDescription;
                break;

            case "Workers":
                tooltipText.text = workersDescription;
                break;

            case "Next Turn":
                tooltipText.text = nextTurnDescription;
                break;

            case "Catalogue":
                tooltipText.text = catalogueDescription;
                break;

            case "Power Core":
                tooltipText.text = powerDescription;
                break;

            case "House":
                tooltipText.text = houseDescription;
                break;

            case "SCP Room":
                tooltipText.text = SCPRoomDescription;
                break;

            case "Stock Room":
                tooltipText.text = stockDescription;
                break;

            case "PC":
                tooltipText.text = PCDescription;
                break;

            case "Mission":
                tooltipText.text = missionDescription;
                break;

            case "Build":
                tooltipText.text = buildModeDescription;
                break;

            case "Market":
                tooltipText.text = marketDescription;
                break;

            case "Visit":
                tooltipText.text = visitDescription;
                break;

            case "Overlay":
                tooltipText.text = overlayDescription;
                break;
        }
    }

    public void AppendText(string text)
    {
        tooltipText.text += "<br>" + text;
    }
}
