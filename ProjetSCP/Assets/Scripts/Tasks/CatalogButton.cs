using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SCP.Data;
using UnityEngine.Events;

public class CatalogButton : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI ID;
    public TextMeshProUGUI rarity;
    public TextMeshProUGUI classe;
    public TextMeshProUGUI danger;
    public Image visual;

    public SCPData mySCP;
    public Catalog catalog;

    public void UpdateDisplay(SCPData SCP)
    {
        mySCP = SCP;
        Name.text = mySCP.Name;
        ID.text = mySCP.ID;
        rarity.text = mySCP.rarity.ToString();
        classe.text = mySCP.type.ToString();
        danger.text = mySCP.dangerLevel.ToString();
        visual.sprite = mySCP.sprite;
    }

    public void OpenSCPCaracteristics()
    {
        catalog.SCPDangerosity.text = "Danger " + mySCP.dangerLevel.ToString(); 
        catalog.SCPBigDescription.text = mySCP.fullDescription;
        catalog.SCPClass.text = "Classe " + mySCP.type.ToString();
        catalog.SCPSize.text = "Taille " + mySCP.size.ToString();
        catalog.fullOverviewPanel.SetActive(true);
        catalog.scrollViewPanel.SetActive(false);
    }

    public void ShowBigSCP()
    {
        catalog.bigOverviewName.text = mySCP.Name;
        catalog.bigOverviewID.text = mySCP.ID.ToString();
        catalog.bigOverviewRarity.text = mySCP.rarity.ToString();
        catalog.bigOverviewVisual.sprite = mySCP.sprite;
    }

}
