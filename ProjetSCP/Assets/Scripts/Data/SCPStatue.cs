using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SCP.Data;
using TMPro;

public class SCPStatue : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI classe;
    public TextMeshProUGUI rarity;
    public TextMeshProUGUI danger;
    public TextMeshProUGUI description;
    public TextMeshProUGUI size;
    public Image sizeSprite;
    public TextMeshProUGUI ID;

    public void UpdateData(SCPData scpData)
    {
        Name.text = scpData.Name;
        ID.text = scpData.ID;
        if (scpData.type == SCPType.SAFE)
        {
            classe.text = "Sûr";
        }
        else if (scpData.type == SCPType.EUCLIDE)
        {
            classe.text = "Euclide";
        }
        else if (scpData.type == SCPType.KETER)
        {
            classe.text = "Keter";
        }
        //classe.text = scpData.type.ToString();
        if (scpData.rarity == Rarity.COMMON)
        {
            rarity.text = "Commun";
        }
        else if (scpData.rarity == Rarity.RARE)
        {
            rarity.text = "Rare";
        }
        else if (scpData.rarity == Rarity.EPIC)
        {
            rarity.text = "Epique";
        }
        //rarity.text = scpData.rarity.ToString();
        if (scpData.dangerLevel == DangerLevel.GREEN)
        {
            danger.text = "Vert";
        }
        else if (scpData.dangerLevel == DangerLevel.YELLOW)
        {
            danger.text = "Jaune";
        }
        else if (scpData.dangerLevel == DangerLevel.ORANGE)
        {
            danger.text = "Orange";
        }
        else if (scpData.dangerLevel == DangerLevel.RED)
        {
            danger.text = "Rouge";
        }
        else if (scpData.dangerLevel == DangerLevel.BLACK)
        {
            danger.text = "Noir";
        }
        //danger.text = scpData.dangerLevel.ToString();
        description.text = scpData.descriptionShort;
        image.sprite = scpData.smallVisual;
        sizeSprite.sprite = scpData.sizeSprite;
    }
}
