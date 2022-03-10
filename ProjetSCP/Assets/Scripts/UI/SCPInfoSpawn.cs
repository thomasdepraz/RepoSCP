using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public TextMeshProUGUI workerText;
    public Image sizeImage;
    public Image scpPhoto;
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
        codeText.text = data.ID;
        nameText.text = data.Name;
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
        switch (data.size)
        {
            case 2:
                workerText.text = "1";
                break;

            case 4:
                workerText.text = "2";
                break;

            case 8:
                workerText.text = "4";
                break;
        }
        scpPhoto.sprite = data.smallVisual;
        sizeImage.sprite = data.sizeSprite;

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
