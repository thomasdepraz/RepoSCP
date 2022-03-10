using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCP.Data;
using TMPro;

public class SCPInfoSpawn : MonoBehaviour
{
    public GameObject SCPInfoWindow;
    [Header("SCP Info Texts")]
    public TextMeshProUGUI codeText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rarityText;
    public TextMeshProUGUI classText;
    public TextMeshProUGUI dangerText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI descriptionText;

    [Header("Testing")]
    public SCPData scpData;

    public void Awake()
    {
        new Registry().Register<SCPInfoSpawn>(this);

    }


    public void SpawnSCPInfo(SCPData data)
    {
        SCPInfoWindow.SetActive(true);
        codeText.text = "Code : " + data.ID;
        nameText.text = "Nom : " + data.Name;
        if (data.rarity == Rarity.COMMON)
        {
            rarityText.text = "Rareté : Commun";
        }
        else if (data.rarity == Rarity.RARE)
        {
            rarityText.text = "Rareté : Rare";
        }
        else if (data.rarity == Rarity.EPIC)
        {
            rarityText.text = "Rareté : Epique";
        }
        //rarityText.text = "Rareté : " + data.rarity.ToString();
        if (data.type == SCPType.SAFE)
        {
            classText.text = "Classe : Sûr";
        }
        else if (data.type == SCPType.EUCLIDE)
        {
            classText.text = "Classe : Euclide";
        }
        else if (data.type == SCPType.KETER)
        {
            classText.text = "Classe : Keter";
        }
        //classText.text = "Classe : " + data.type.ToString();
        if (data.dangerLevel == DangerLevel.GREEN)
        {
            dangerText.text = "Dangerosité : Vert";
        }
        else if (data.dangerLevel == DangerLevel.YELLOW)
        {
            dangerText.text = "Dangerosité : Jaune";
        }
        else if (data.dangerLevel == DangerLevel.ORANGE)
        {
            dangerText.text = "Dangerosité : Orange";
        }
        else if (data.dangerLevel == DangerLevel.RED)
        {
            dangerText.text = "Dangerosité : Rouge";
        }
        else if (data.dangerLevel == DangerLevel.BLACK)
        {
            dangerText.text = "Dangerosité : Noir";
        }
        //dangerText.text = "Dangerosité : " + data.dangerLevel.ToString();
        sizeText.text = "Taille : " + data.size.ToString();
        descriptionText.text = data.descriptionShort;
    }

    //For Testing only 
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    SpawnSCPInfo(scpData);
        //}
    }


}
