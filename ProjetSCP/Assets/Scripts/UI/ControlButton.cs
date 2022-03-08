using SCP.Building;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour
{
    public GameObject wheel;
    private MissionManager missionManager;


    public void Awake()
    {
        new Registry().Register<ControlButton>(this);
    }

    public void Start()
    {
        missionManager = Registry.Get<MissionManager>();
    }

    public void GoBuildMode()
    {
        Registry.Get<BuildingManager>().ToggleBuildingMode(true);
        wheel.SetActive(false);
    }

    public void GoMission()
    {
        missionManager.OpenMissionPanel();
        wheel.SetActive(false);
    }

    public void ToggleWheel()
    {
        wheel.SetActive(!wheel.activeSelf);
    }

    
}
