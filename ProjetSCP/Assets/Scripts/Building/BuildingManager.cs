using SCP.Ressources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SCP.Building
{
    [System.Serializable]
    public enum BuildingType
    {
        NONE,
        HOUSING,
        POWERPLANT,
        COMMANDPOST,
        WAREHOUSE,
        SCP2_1,
        SCP2_2,
        SCP4_2
    }
    public class BuildingManager : MonoBehaviour
    {
        public GameObject roomParents;

        [Header("Prebuilt Rooms")]
        public Building commandRoom;
        public Building warehouseRoom;

        [Header("Room Prefabs")]
        public GameObject housePrefab;
        public GameObject powerPlantPrefab;
        public GameObject SCP2_1Prefab;
        public GameObject SCP2_2Prefab;
        public GameObject SCP4_2Prefab;

        public BuildingType selectedBuildingType;
        public Grid grid;
        public Vector2 gridSize;

        GameManager gameManager;
        Pointer pointer;

        private Building selectedObject;

        [Header("UI")]
        public Image background;

        private void Awake()
        {
            new Registry().Register<BuildingManager>(this);
        }

        private void Start()
        {
            gameManager = Registry.Get<GameManager>();
            pointer = Registry.Get<Pointer>();

            pointer.UpdateGridCallback += MoveObject;

            grid = new Grid(new Vector2(13,6));

            //Create pre built rooms
            var warehouse = new Warehouse();
            var commandPost = new CommandPost();

            warehouse.SetPosition(new Vector2(0,0));
            commandPost.SetPosition(new Vector2(3,0));

            grid.Build(Vector2.zero, warehouse.Size);
            grid.Build(new Vector2(3, 0), commandPost.Size);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleBuildingMode(gameManager.gameState != GameState.BUILDING ? true : false);
            }

            if(gameManager.gameState==GameState.BUILDING && selectedObject != null)
            {
                if (Input.GetMouseButtonDown(0))
                    PlaceObject();
                else if (Input.GetMouseButtonDown(1))
                {
                    Destroy(selectedObject.gameObject);
                    ToggleBuildingMode(false);
                }
            }

        }

        public void SelectBuildingType(string type)//link to button
        {
            if (selectedObject != null) return;

            switch (type)
            {
                case "NONE":
                    break;
                case "HOUSING":
                    GameObject go = Instantiate(housePrefab, Vector3.zero, housePrefab.transform.rotation);
                    selectedObject = go.GetComponent<Building>();
                    selectedObject.type = BuildingType.HOUSING;
                    selectedObject.room = new House();
                    selectedObject.overlay.linkedRoom = selectedObject.room;
                    break;
                case "POWERPLANT":
                    break;
                case "SCP2_1":
                    GameObject room2x1 = Instantiate(SCP2_1Prefab, Vector3.zero, SCP2_1Prefab.transform.rotation);
                    selectedObject = room2x1.GetComponent<Building>();
                    selectedObject.type = BuildingType.SCP2_1;
                    selectedObject.room = new ScpContainer(new Vector2(2, 1), 100);
                    selectedObject.overlay.linkedRoom = selectedObject.room;
                    break;
                case "SCP2_2":
                    GameObject room2x2 = Instantiate(SCP2_2Prefab, Vector3.zero, SCP2_2Prefab.transform.rotation);
                    selectedObject = room2x2.GetComponent<Building>();
                    selectedObject.type = BuildingType.SCP2_1;
                    selectedObject.room = new ScpContainer(new Vector2(2, 2), 200);
                    selectedObject.overlay.linkedRoom = selectedObject.room;
                    break;
                case "SCP4_2":
                    GameObject room4x2 = Instantiate(SCP4_2Prefab, Vector3.zero, SCP4_2Prefab.transform.rotation);
                    selectedObject = room4x2.GetComponent<Building>();
                    selectedObject.type = BuildingType.SCP2_1;
                    selectedObject.room = new ScpContainer(new Vector2(4, 2), 400);
                    selectedObject.overlay.linkedRoom = selectedObject.room;
                    break;
                default:
                    break;
            }
        }

        public void ToggleBuildingMode(bool on)
        {
            if(on)
            {
                gameManager.gameState = GameState.BUILDING;
                pointer.checkPosition = true;

                //Show UI
                background.gameObject.SetActive(true);

            }
            else
            {
                pointer.checkPosition = false;
                gameManager.gameState = GameState.GAME;

                //Hide UI
                background.gameObject.SetActive(false);
            }
        }

        public void MoveObject()
        {
            if(selectedObject!=null)
                selectedObject.transform.position = GetWorldPosition(pointer.currentGridPosition);
        }

        public void PlaceObject()
        {
            Vector2 gridPos = GetRealGridPos(pointer.currentGridPosition);
            if (grid.CanBuild(gridPos, selectedObject.room.Size) && EnoughMoney(selectedObject.room.MoneyCost))
            {
                selectedObject.room.SetPosition(gridPos);
                grid.Build(gridPos, selectedObject.room.Size);
                Registry.Get<RessourcesManager>().RemoveMoney(selectedObject.room.MoneyCost);
                selectedObject = null;
            }
        }

        public bool EnoughMoney(int cost)
        {
            return Registry.Get<RessourcesManager>().Money >= cost;
        }

        public Vector3 GetWorldPosition(Vector2 gridPosition)
        {
            int x = (int)gridPosition.x * 20 - (20 * 8) - 10;
            int y = (int)gridPosition.y * 20 - 50 ;

            return new Vector3(x, -y, 0);
        }

        public Vector2 GetRealGridPos(Vector2 currentGridPos)
        {
            return new Vector2(currentGridPos.x - 2, currentGridPos.y);
        }
    }

}
