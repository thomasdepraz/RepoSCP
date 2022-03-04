using SCP.Data;
using SCP.Ressources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionManager : MonoBehaviour
{
    [Header("Level 1")]
    public int humanNumber1;
    public int commonRarity1;
    public int rareRarity1;
    public int epicRarity1;
    
    public Text humanNumberText1;
    public Button launchMissionButton1;

    private Mission mission1;
    public SCPStatue SCPStatue1;

    [Header("Level 2")]
    public int humanNumber2;
    public int commonRarity2;
    public int rareRarity2;
    public int epicRarity2;

    public Text humanNumberText2;
    public Button launchMissionButton2;
    public SCPStatue SCPStatue2;

    private Mission mission2;

    [Header("Level 3")]
    public int humanNumber3;
    public int commonRarity3;
    public int rareRarity3;
    public int epicRarity3;

    public Text humanNumberText3;
    public Button launchMissionButton3;
    public SCPStatue SCPStatue3;

    private Mission mission3;

    [Header("Other")]

    public GameObject missionSelectionPanel;
    public GameObject SCPSelectionPanel;


    public SCPData[] allSCP;
    List<SCPData> commonSCP = new List<SCPData>();
    List<SCPData> rareSCP = new List<SCPData>();
    List<SCPData> epicSCP = new List<SCPData>();

    void Start()
    {
        var manager = new RessourcesManager();
        new Registry().Register< MissionManager > (this);
        humanNumberText1.text = humanNumber1.ToString();
        humanNumberText2.text = humanNumber2.ToString();
        humanNumberText3.text = humanNumber3.ToString();

        foreach (SCPData SCP in allSCP)
        {
            if(SCP.rarity == Rarity.COMMON)
            {
                commonSCP.Add(SCP);
            }else if(SCP.rarity == Rarity.RARE)
            {
                rareSCP.Add(SCP);
            }
            else
            {
                epicSCP.Add(SCP);
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("a"))
        {
            Debug.Log("initialise panel");
            OpenMissionPanel();
        }
    }
    public void CreateMission()
    {
       mission1 = new Mission(humanNumber1, commonRarity1,rareRarity1,epicRarity1);
       mission2 = new Mission(humanNumber2, commonRarity2, rareRarity2, epicRarity2);
       mission3 = new Mission(humanNumber3, commonRarity3, rareRarity3, epicRarity3);
    }

    public void IsMissionPerformable()
    {
        if (mission1.IsPerformable())
        {
            launchMissionButton1.interactable = true;
        }
        if (mission2.IsPerformable())
        {
            launchMissionButton2.interactable = true;
        }
        if (mission3.IsPerformable())
        {
            launchMissionButton3.interactable = true;
        }
    }
    public void PlayMission(int missionNumber)
    {
        Mission selectedMission;
        if(missionNumber == 1)
        {
            selectedMission = mission1;

        } 
        else if(missionNumber == 2)
        {
            selectedMission = mission2;
        }
        else
        {
            selectedMission = mission3;
        }
        //Workers en off selon la mission
        //calculs probabilité ==> 3 tirage

        SCPData SCP1 = DrawSCP(selectedMission.CommonRarity, selectedMission.RareRarity, selectedMission.EpicRarity);
        SCPData SCP2 = DrawSCP(selectedMission.CommonRarity, selectedMission.RareRarity, selectedMission.EpicRarity);
        SCPData SCP3 = DrawSCP(selectedMission.CommonRarity, selectedMission.RareRarity, selectedMission.EpicRarity);

        SCPStatue1.UpdateData(SCP1);
        SCPStatue2.UpdateData(SCP2);
        SCPStatue3.UpdateData(SCP3);

        missionSelectionPanel.SetActive(false);
        SCPSelectionPanel.SetActive(true);
    }

    public void OpenMissionPanel()
    {
        CreateMission();
        IsMissionPerformable();
        missionSelectionPanel.SetActive(true);

    }

    SCPData DrawSCP(int commonRarity, int rareRarity, int epicRarity)
    {

        int rarityScore = Random.Range(0, 99);
        Debug.Log(rarityScore);
        SCPData foundSCP;

        if (rarityScore <= commonRarity)
        {
            Debug.Log("commonRarity");
            foundSCP = commonSCP[(int)Random.Range(0, commonSCP.Count)];
           
        }
        else if (commonRarity < rarityScore && rarityScore <= commonRarity + rareRarity)
        {
            Debug.Log("RareRarity");
            foundSCP = rareSCP[(int)Random.Range(0, rareSCP.Count)];
            
        }
        else 
        {
            Debug.Log("EpicRarity");
            foundSCP = epicSCP[(int)Random.Range(0, epicSCP.Count)];
            
        }

        return foundSCP;
    }

}
