using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCP.Data;
using TMPro;
using UnityEngine.UI;

public class Catalog : MonoBehaviour
{
    List<CatalogButton> catalogButtons;

    [Header("BigOverview")]
    public TextMeshProUGUI bigOverviewName;
    public TextMeshProUGUI bigOverviewID;
    public TextMeshProUGUI bigOverviewRarity;
    public Image bigOverviewVisual;
    public GameObject scrollViewPanel;
    public GameObject bigOverviewPanel;

    [Header("FullOverview")]
    public TextMeshProUGUI SCPDangerosity;
    public TextMeshProUGUI SCPClass;
    public TextMeshProUGUI SCPBigDescription;
    public TextMeshProUGUI SCPSize;
    public GameObject fullOverviewPanel;

    [Header("Other")]
    public GameObject helpPanel;
    public List<SCPData> allSCPS;
    public RectTransform catalogButtonsPanelTransform;
    public CatalogButton catalogButton;
    public GameObject Panel;
    public Transform scrollViewContentTransform;

    private void Start()
    {
        catalogButtons = new List<CatalogButton>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("b") == true)
        {
            InitializeSCPCatalog();
        }
    }
    public void InitializeSCPCatalog()
    {
        Panel.SetActive(true);

        foreach (CatalogButton button in catalogButtons)
        {
            Destroy(button.gameObject);
        }

        catalogButtons.Clear();

        for (int i = 0; i < allSCPS.Count; i++)
        {
            CatalogButton newCatalogButton = Instantiate(catalogButton, catalogButtonsPanelTransform);
            newCatalogButton.UpdateDisplay(allSCPS[i]);
            catalogButtons.Add(newCatalogButton);
            newCatalogButton.catalog = this;
        }
    }

    public void Back()
    {
        if(fullOverviewPanel.activeInHierarchy == true)
        {
            fullOverviewPanel.SetActive(false);
            scrollViewPanel.SetActive(true);
        }
        else if(helpPanel.activeInHierarchy == true)
        {
            helpPanel.SetActive(false);
            scrollViewPanel.SetActive(true);
            bigOverviewPanel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }

    public void OpenHelpPanel()
    {
        helpPanel.SetActive(true);
        fullOverviewPanel.SetActive(false);
        scrollViewPanel.SetActive(false);
        bigOverviewPanel.SetActive(false);
    }

   
}
