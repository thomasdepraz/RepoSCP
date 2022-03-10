using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SCP.Camera;
using SCP.Data;
using System;
using SCP.Building;
using SCP.Ressources;
using System.Collections.Generic;
using TMPro;

public class RoomOverlay : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    private Image overlay;
    public Room linkedRoom { private get; set; }

    private CanvasGroup canvasGroup;

    float lastClickTime = 1;
    private CameraController camController;



    private void Start()
    {
        overlay = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        camController = Registry.Get<CameraController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Registry.Get<GameManager>().gameState == GameState.BUILDING) return;
        //throw new System.NotImplementedException();
        print(linkedRoom.GetType().ToString());
        var tooltip = Registry.Get<TooltipManager>();
        tooltip.gameObject.SetActive(true);
        switch (linkedRoom.GetType().ToString())
        {
            case "CommandPost":
                tooltip.UpdateTooltip("PC");
                break;
                
            case "Warehouse":
                tooltip.UpdateTooltip("Stock Room");
                break;
            case "House":
                tooltip.UpdateTooltip("House");
                break;
            case "ScpContainer":
                tooltip.UpdateTooltip("SCP Room");
                break;

        }


        tooltip.AppendText(tooltip.overlayDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Registry.Get<TooltipManager>().gameObject.SetActive(false);

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (Registry.Get<GameManager>().gameState != GameState.GAME) return;

        if(Input.GetMouseButtonUp(1))
        {
            if (camController.camState != CameraState.TWEENING)
            {
                if (linkedRoom is IInfo)
                    (linkedRoom as IInfo).SetInfo(camController.camState != CameraState.FOCUSED);

                camController.ChangeState(linkedRoom.Building.occupantOriginTransform.position, canvasGroup);
                Registry.Get<TooltipManager>().gameObject.SetActive(false);
            }
        }
        else if( Input.GetMouseButtonUp(0))
        {
            linkedRoom.OnSelectCallback?.Invoke();
        }
    }

    public void setEnabled()
    {

    }

    public void setDisabled()
    {

    }

}

public abstract class Room
{
    public Vector2 Position { get; protected set; }
    public Vector2 FocusPosition { get; protected set; }
    public Vector2 Size { get; protected set; }
    public int MoneyCost { get; protected set; }

    public Building Building { get; set; }

    private readonly RoomOverlay overlay;

    public Action OnSelectCallback;

    public Action OnDeselectCallback;

    public virtual void SetPosition(Vector2 position)
    {
        Position = position;
        //calculate focus position
    }

}

public class CommandPost : Room
{
    public CommandPost(Building building)
    {
        Size = new Vector2(3, 2);

        OnSelectCallback = OnSelect;

        Building = building;
        Building.room = this;
    }

    public void OnSelect()
    {
        Registry.Get<ControlButton>().ToggleWheel();
    }
}

public class House : Room
{
    public enum HouseState
    {
        EMPTY,
        OCCUPIED
    }

    public HouseState state;

    public House(Building building)
    {
        //recenser les maisons qqpart après leur création;
        Size = new Vector2(1, 1);
        MoneyCost = 200;

        Building = building;

        OnSelectCallback += OnSelect;
    }

    public void OnSelect()
    {
        if(state == HouseState.EMPTY)
        {
            var ressourceManager = Registry.Get<RessourcesManager>();
            if(ressourceManager.Money >= 200)
            {
                ressourceManager.AddWorker(this);
                ressourceManager.RemoveMoney(200);
                SetState(HouseState.OCCUPIED);
            }
        }
    }

    public void SetState(HouseState state)
    {
        this.state = state;
        switch (state)
        {
            case HouseState.EMPTY:
                Building.roomContent.SetActive(false);
                break;
            case HouseState.OCCUPIED:
                Building.roomContent.SetActive(true);
                break;
            default:
                break;
        }
    }

 }

public class Warehouse : Room, IInfo
{
    public SCPModel occupant { get; private set; }

    public Warehouse(Building building)
    {
        Size = new Vector3(3, 3);
        OnSelectCallback += OnSelect;
        Building = building;
        Building.room = this;
    }

    public void OnSelect()
    {
        var ressourcesManager = Registry.Get<RessourcesManager>();
        //Select Scp if any + feedback
        if (IsEmpty() && ressourcesManager.selectedSCP != null)
        {
            Populate(ressourcesManager.selectedSCP);
            ressourcesManager.selectedSCP = null;

            //Resets the cursor to the default  
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        }
        else if(!IsEmpty())
        {
            ressourcesManager.selectedSCP = occupant.Data;
            //Todo feedback
            Cursor.SetCursor(occupant.Data.smallVisual.texture, Vector3.zero, CursorMode.Auto);

            GameObject.Destroy(occupant.Object.gameObject);
            occupant = null;
            //feedback
        }
    }

    public void SetInfo(bool on)
    {
        if (!IsEmpty())
        {
            if (on)
            {
                Registry.Get<SCPInfoSpawn>().SpawnSCPInfo(occupant.Data);
            }
            else
            {
                Registry.Get<SCPInfoSpawn>().SCPInfoWindow.SetActive(false);
            }
        }
    }

    public bool IsEmpty() => occupant == null;

    public void Populate(SCPData occupantData)
    {
        if(!IsEmpty())
        {
            GameObject.Destroy(occupant.Object);
            occupant = null;
        }

        GameObject prefab = occupantData.statue;
        GameObject go = GameObject.Instantiate(prefab, Building.occupantOriginTransform.position, prefab.transform.rotation);
        //SCPObject obj = go.GetComponent<SCPObject>();
        //obj.UpdateRenderer(occupantData);
        this.occupant = new SCPModel(occupantData, go); 
    }
}

public interface IInfo
{
    public void SetInfo(bool on);
}

public class ScpContainer : Room , IInfo
{
    public SCPModel occupant { get; private set; }
    public List<Worker> assignedWorkers = new List<Worker>();

    public ScpContainer(Vector2 size, int moneyCost, Building building)
    {
        Size = size;
        MoneyCost = moneyCost;
        Building = building;
        OnSelectCallback += OnSelect;

        Registry.Get<RessourcesManager>().scpRooms.Add(this);
    }

    public void OnSelect()
    {
        var ressourcesManager = Registry.Get<RessourcesManager>();
        var buildingManager = Registry.Get<BuildingManager>();
        //Select Scp if any + feedback
        if (IsEmpty() && ressourcesManager.selectedSCP != null)
        {
            Populate(ressourcesManager.selectedSCP);
            ressourcesManager.selectedSCP = null;

            buildingManager.SetSCPOptimalState();

            //Resets the cursor to the default  
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else if (!IsEmpty())
        {
            ressourcesManager.selectedSCP = occupant.Data;
            //Todo feedback
            Cursor.SetCursor(occupant.Data.smallVisual.texture, Vector3.zero, CursorMode.Auto);

            GameObject.Destroy(occupant.Object.gameObject);
            occupant = null;
            //feedback

            buildingManager.SetSCPOptimalState();

            Building.roomContent.SetActive(false);
        }
    }

    public void InitBuidingOverlay()
    {
        var container = Building.transform.Find("Canvas").Find("Overlay").Find("Container");
        var addObj = container.transform.Find("AddButton");
        var remObj = container.transform.Find("RemoveButton");
       

        var addButton = addObj.GetComponent<Button>();
        var removeButton = remObj.GetComponent<Button>();
        
        addButton.onClick.AddListener(AssignWorker);
        removeButton.onClick.AddListener(RemoveAssignedWorker);

        UpdateCounterText();
    }

    public void SetInfo(bool on)
    {
        if(!IsEmpty())
        {
            if(on)
            {
                Registry.Get<SCPInfoSpawn>().SpawnSCPInfo(occupant.Data);
            }
            else
            {
                Registry.Get<SCPInfoSpawn>().SCPInfoWindow.SetActive(false);
            }
        }
    }


    public bool IsEmpty() => occupant == null;

    public bool CorrectRoomSize(SCPData data)
    {
        return data.requiredSize == Size;
    }

    public void UpdateCounterText()
    {
        var coutObj = Building.transform.Find("Canvas").Find("Overlay").Find("Container").Find("Counter").Find("CounterText");
        var counterText = coutObj.GetComponent<TextMeshProUGUI>();
        counterText.text = $"{assignedWorkers.Count} / {(Size.x * Size.y)/2}";
    }

    public void Populate(SCPData occupantData)
    {
        if (!CorrectRoomSize(occupantData)) return;

        if (!IsEmpty()) return;
        

        GameObject prefab = occupantData.statue;
        GameObject go = GameObject.Instantiate(prefab, Building.occupantOriginTransform.position, prefab.transform.rotation);
        //SCPObject obj = go.GetComponent<SCPObject>();
        //obj.UpdateRenderer(occupantData);
        this.occupant = new SCPModel(occupantData, go);

        var props = Building.transform.Find($"{occupant.Data.ID}_Props");
        Building.roomContent = props.gameObject;
        Building.roomContent.SetActive(true);
    }

    public void AssignWorker()
    {
        if (assignedWorkers.Count == (Size.x * Size.y)/2) return;

        var workers = Registry.Get<RessourcesManager>().HumanRessources;
        foreach (var w in workers)
        {
            if(w.State == Worker.WorkerState.IDLE)
            {
                assignedWorkers.Add(w);
                w.Engage();
                break;
            }
        }
        UpdateCounterText();
    }
    public void RemoveAssignedWorker()
    {
        if (assignedWorkers.Count == 0) return;
        assignedWorkers[0].Disengage();
        assignedWorkers.RemoveAt(0);

        UpdateCounterText();
    }
}

