using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCP.Data;
using TMPro;
using UnityEngine.UI;

public class Catalog : MonoBehaviour
{
    List<CatalogButton> catalogButtons;
    public List<SCPData> allSCPS;
    public RectTransform catalogButtonsPanelTransform;
    public CatalogButton catalogButton;

    public GameObject Panel;

    [Header("BigOverview")]
    public TextMeshProUGUI bigOverviewName;
    public TextMeshProUGUI bigOverviewID;
    public TextMeshProUGUI bigOverviewRarity;
    public Image bigOverviewVisual;
    public GameObject ScollViewPanel;

    [Header("FullOverview")]
    public TextMeshProUGUI SCPDangerosity;
    public TextMeshProUGUI SCPClass;
    public TextMeshProUGUI SCPBigDescription;
    public TextMeshProUGUI SCPSize;
    public GameObject fullOverviewPanel;

    private void Start()
    {
        catalogButtons = new List<CatalogButton>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("a") == true)
        {
            InitializeSCPCatalog();
        }
    }
    public void InitializeSCPCatalog()
    {
        catalogButtons.Clear();
        //deleteChildrens
        for(int i = 0; i < allSCPS.Count; i++)
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
            ScollViewPanel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }
}
