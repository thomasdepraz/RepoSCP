using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour
{
    public GameObject wheel;
    public MissionManager missionManager;
   public void GoBuildMode()
    {
        wheel.SetActive(false);
    }

    public void GoMission()
    {
        missionManager.OpenMissionPanel();
        wheel.SetActive(false);
    }

    public void OpenWheel()
    {
        wheel.SetActive(true);
    }

    public void CloseWheel()
    {
        wheel.SetActive(false);
    }
}
