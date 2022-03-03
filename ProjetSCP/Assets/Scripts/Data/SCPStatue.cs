using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SCP.Data;
using TMPro;

public class SCPStatue : MonoBehaviour
{
    //public MeshRenderer Mesh;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI classe;
    public TextMeshProUGUI rarity;
    public TextMeshProUGUI danger;
    public TextMeshProUGUI description;
    public TextMeshProUGUI size;
    public TextMeshProUGUI ID;

    public void UpdateData(SCPData scpData)
    {
        Name.text = scpData.name;
        ID.text = scpData.ID;
        classe.text = scpData.type.ToString();
        rarity.text = scpData.rarity.ToString();
        danger.text = scpData.dangerLevel.ToString();
        size.text = scpData.size.ToString(); 
    }
}
