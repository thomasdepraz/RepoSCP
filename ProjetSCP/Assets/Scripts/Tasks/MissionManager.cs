using SCP.Data;
using SCP.Ressources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SCP.Building;

public class MissionManager : MonoBehaviour
{
    [Header("Level 1")]
    public int humanNumber1;
    public int commonRarity1;
    public int rareRarity1;
    public int epicRarity1;

    public int workerLossProbability1;

    public Text humanNumberText1;
    public Button launchMissionButton1;

    private Mission mission1;
    public SCPStatue SCPStatue1;

    [Header("Level 2")]
    public int humanNumber2;
    public int commonRarity2;
    public int rareRarity2;
    public int epicRarity2;

    public int workerLossProbability2;

    public Text humanNumberText2;
    public Button launchMissionButton2;
    public SCPStatue SCPStatue2;

    private Mission mission2;

    [Header("Level 3")]
    public int humanNumber3;
    public int commonRarity3;
    public int rareRarity3;
    public int epicRarity3;

    public int workerLossProbability3;

    public Text humanNumberText3;
    public Button launchMissionButton3;
    public SCPStatue SCPStatue3;

    private Mission mission3;

    [Header("Other")]

    public GameObject missionSelectionPanel;
    public GameObject SCPSelectionPanel;
    public TextMeshProUGUI numberOfDeadWorkersText;
    public GameObject workersLossPanel;
    public TextMeshProUGUI deadWorkersDescription;
    RessourcesManager ressourceManager;
    public GameObject glowingShader1;
    public GameObject glowingShader2;
    public GameObject glowingShader3;
    public Image selectedSCPSPriteSize;
    public Image selectedSCPSpriteImage;

    SCPData temporarySelectedSCP;
    SCPData drawnSCP1;
    SCPData drawnSCP2;
    SCPData drawnSCP3;
    SCPData chosenSCP; //SCP � r�cup�rer pour placer dans le stockage
    int numberOfDeadWorkers;

    public SCPData[] allSCP;
    List<SCPData> commonSCP = new List<SCPData>();
    List<SCPData> rareSCP = new List<SCPData>();
    List<SCPData> epicSCP = new List<SCPData>();

    private void Awake()
    {
        new Registry().Register< MissionManager > (this);
    }

    void Start()
    {
        ressourceManager = Registry.Get<RessourcesManager>();
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

    private void Update()
    {
        //debug
        if(Input.GetKeyDown("x"))
        {
            OpenMissionPanel();
        }
    }
    public void CreateMission()
    {
       mission1 = new Mission(humanNumber1, commonRarity1,rareRarity1,epicRarity1, workerLossProbability1);
       mission2 = new Mission(humanNumber2, commonRarity2, rareRarity2, epicRarity2, workerLossProbability2);
       mission3 = new Mission(humanNumber3, commonRarity3, rareRarity3, epicRarity3, workerLossProbability3);
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
        SoundManager.instance.PlaySound("MissionCollectCommon");
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

        drawnSCP1 = DrawSCP(selectedMission.CommonRarity, selectedMission.RareRarity, selectedMission.EpicRarity);
        drawnSCP2 = DrawSCP(selectedMission.CommonRarity, selectedMission.RareRarity, selectedMission.EpicRarity);
        drawnSCP3 = DrawSCP(selectedMission.CommonRarity, selectedMission.RareRarity, selectedMission.EpicRarity);

        SCPStatue1.UpdateData(drawnSCP1);
        SCPStatue2.UpdateData(drawnSCP2);
        SCPStatue3.UpdateData(drawnSCP3);

       
        numberOfDeadWorkers = WorkersLoss(selectedMission);
        numberOfDeadWorkersText.text = numberOfDeadWorkers.ToString();

        missionSelectionPanel.SetActive(false);
        SCPSelectionPanel.SetActive(true);
    }

    public void OpenMissionPanel()
    {
        SoundManager.instance.PlaySound("UIMenuButton");
        CreateMission();
        IsMissionPerformable();
        missionSelectionPanel.SetActive(true);
        chosenSCP = null;
    }

    SCPData DrawSCP(int commonRarity, int rareRarity, int epicRarity)
    {

        int rarityScore = Random.Range(0, 99);
        SCPData foundSCP;

        if (rarityScore <= commonRarity)
        {
            foundSCP = commonSCP[(int)Random.Range(0, commonSCP.Count)];
        }
        else if (commonRarity < rarityScore && rarityScore <= commonRarity + rareRarity)
        {
            foundSCP = rareSCP[(int)Random.Range(0, rareSCP.Count)];
        }
        else 
        {
            foundSCP = epicSCP[(int)Random.Range(0, epicSCP.Count)];
        }

        return foundSCP;
    }

    public void SelectSCP(int statueNumber)
    {
        if(statueNumber == 1)
        {
            temporarySelectedSCP = drawnSCP1;
            ValidateSelectedSCP();
        }
        else if (statueNumber == 2)
        {
            temporarySelectedSCP = drawnSCP2;
            ValidateSelectedSCP();
        }
        else
        {
            temporarySelectedSCP = drawnSCP3;
            ValidateSelectedSCP();
        }
    }

    public void ValidateSelectedSCP()
    {
        chosenSCP = temporarySelectedSCP;
        if (chosenSCP != null)
        {
            SoundManager.instance.PlaySound("MissionCollectEpic");
            WorkersLossUpdateDescription();
            SCPSelectionPanel.SetActive(false);
            workersLossPanel.SetActive(true);
            glowingShader3.SetActive(false);
            glowingShader2.SetActive(false);
            glowingShader1.SetActive(false);

            //Placer le scp dans la salle de stockage
            Warehouse w = Registry.Get<BuildingManager>().warehouseRoom.room as Warehouse;
            w.Populate(chosenSCP);
        }   
    }

    int WorkersLoss(Mission mission)
    {
        int numberofdeadworkers = 0;
        for (int i = 0; i < mission.HumanNumber; i++)
        {
            if ( Random.Range(0, 100) > 100 - mission.WorkersLossProbability)
            {
                ressourceManager.RemoveWorker();
                numberofdeadworkers++;
                //Ici remove uniquement les workers qui sont partis dans la mission
            }
        }
        return numberofdeadworkers;
    }

    void WorkersLossUpdateDescription() 
    {
        if (numberOfDeadWorkers == 0)
        {
            deadWorkersDescription.text = chosenSCP.missionIncident.lowWorkersDeathIncident;

        }
        else if (numberOfDeadWorkers >= 1 && numberOfDeadWorkers <= 6)
        {
            deadWorkersDescription.text = chosenSCP.missionIncident.mediumWorkersDeathIncident;
        }
        else
        {
            deadWorkersDescription.text = chosenSCP.missionIncident.highWorkersDeathIncident;
        }

        selectedSCPSpriteImage.sprite = chosenSCP.smallVisual;
        selectedSCPSPriteSize.sprite = chosenSCP.sizeSprite;
    }

    public void MissionOver()
    {
        workersLossPanel.SetActive(false);
        numberOfDeadWorkers = 0;
        SoundManager.instance.PlaySound("UIMenuButtonReturn");
    }

    public void BackToMainMenu()
    {
        missionSelectionPanel.SetActive(false);
        SoundManager.instance.PlaySound("UIMenuButtonReturn");
    }
}
