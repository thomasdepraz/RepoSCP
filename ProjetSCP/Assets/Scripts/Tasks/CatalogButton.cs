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
    public Image smallVisual;

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
        smallVisual.sprite = mySCP.smallVisual;
    }

    public void OpenSCPCaracteristics()
    {
        catalog.SCPDangerosity.text = "Danger " + mySCP.dangerLevel.ToString(); 
        catalog.SCPBigDescription.text = mySCP.fullDescription;
        catalog.SCPClass.text = "Classe " + mySCP.type.ToString();
        catalog.SCPSize.sprite = mySCP.sizeSprite;
        catalog.fullOverviewPanel.SetActive(true);
        catalog.scrollViewPanel.SetActive(false);
        LayoutRebuilder.ForceRebuildLayoutImmediate(catalog.scrollViewContentTransform as RectTransform);
    }

    public void ShowBigSCP()
    {

        catalog.bigOverviewName.text = mySCP.Name;
        catalog.bigOverviewID.text = mySCP.ID.ToString();
        catalog.bigOverviewRarity.text = mySCP.rarity.ToString();
        catalog.bigOverviewVisual.sprite = mySCP.bigVisual;
        catalog.bigOverviewVisual.gameObject.SetActive(true);
        if(mySCP.rarity == Rarity.RARE || mySCP.rarity == Rarity.EPIC)
        {
            catalog.starsParticles.Play();
        }
        else
        {
            catalog.starsParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        if (mySCP.rarity == Rarity.EPIC)
        {
            catalog.glowingShader.SetActive(true);
        }
        else
        {
            catalog.glowingShader.SetActive(false);
        }
    }

}
