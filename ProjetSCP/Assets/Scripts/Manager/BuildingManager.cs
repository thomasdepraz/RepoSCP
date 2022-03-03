using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCP.Building
{
    public enum BuildingType
    {
        NONE,
        HOUSING,
        POWERPLANT,
        COMMANDPOST,
        SCP2_1
    }
    public class BuildingManager : MonoBehaviour
    {
        [Header("Room Prefabs")]
        public GameObject housePrefab;
        public GameObject powerPlantPrefab;
        public GameObject SCP2_1Prefab;

        public BuildingType selectedBuildingType;

        GameManager gameManager;

        private void Awake()
        {
            new Registry().Register<BuildingManager>(this);
        }

        private void Start()
        {
            gameManager = Registry.Get<GameManager>();
        }

        public void SelectBuildingType(BuildingType type)//link to button
        {
            selectedBuildingType = type;
        }

        public void ToggleBuildingMode(bool on)
        {
            if(on)
            {
                gameManager.gameState = GameState.BUILDING;
                Registry.Get<Pointer>().checkPosition = true;
                //Show UI
            }
            else
            {
                Registry.Get<Pointer>().checkPosition = false;
                gameManager.gameState = GameState.GAME;
                //Hide UI
            }
        }

    }

}
