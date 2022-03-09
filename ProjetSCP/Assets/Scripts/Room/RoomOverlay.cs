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

    float lastClickTime = 1;
    private CameraController camController;

    private void Start()
    {
        overlay = GetComponent<Image>();
        camController = Registry.Get<CameraController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (Registry.Get<GameManager>().gameState != GameState.GAME) return;

        if(Input.GetMouseButtonUp(1))
        {
            if (camController.camState != CameraState.TWEENING)
            {
                camController.ChangeState(transform.position);
            }
            else if (camController.camState == CameraState.FOCUSED && (Vector2)transform.position != (Vector2)camController.transform.position)
            {
                camController.FocusTarget(transform.position);
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
        MoneyCost = 100;

        Building = building;

        OnSelectCallback += OnSelect;
    }

    public void OnSelect()
    {
        if(state == HouseState.EMPTY)
        {
            var ressourceManager = Registry.Get<RessourcesManager>();
            if(ressourceManager.Money >= 100)
            {
                ressourceManager.AddWorker(this);
                ressourceManager.RemoveMoney(100);
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
                break;
            case HouseState.OCCUPIED:
                break;
            default:
                break;
        }
    }

 }

public class Warehouse : Room
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

        }
        else if(!IsEmpty())
        {
            ressourcesManager.selectedSCP = occupant.Data;  
            //Todo feedback

            GameObject.Destroy(occupant.Object.gameObject);
            occupant = null;
            //feedback
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

        GameObject prefab = Registry.Get<GameManager>().scpObjectPrefab;
        GameObject go = GameObject.Instantiate(prefab, Building.occupantOriginTransform.position, prefab.transform.rotation);
        SCPObject obj = go.GetComponent<SCPObject>();
        obj.UpdateRenderer(occupantData);
        this.occupant = new SCPModel(occupantData, obj); 
    }
}

public class ScpContainer : Room
{
    public SCPModel occupant { get; private set; }
    public List<Worker> assignedWorkers = new List<Worker>();

    public ScpContainer(Vector2 size, int moneyCost, Building building)
    {
        Size = size;
        MoneyCost = moneyCost;
        Building = building;
        OnSelectCallback += OnSelect;
    }

    public void OnSelect()
    {
        var ressourcesManager = Registry.Get<RessourcesManager>();
        //Select Scp if any + feedback
        if (IsEmpty() && ressourcesManager.selectedSCP != null)
        {
            Populate(ressourcesManager.selectedSCP);
            ressourcesManager.selectedSCP = null;

        }
        else if (!IsEmpty())
        {
            ressourcesManager.selectedSCP = occupant.Data;
            //Todo feedback

            GameObject.Destroy(occupant.Object.gameObject);
            occupant = null;
            //feedback
        }
    }

    public void InitBuidingOverlay()
    {
        var container = Building.transform.Find("Canvas").Find("Container");
        var addObj = container.transform.Find("AddButton");
        var remObj = container.transform.Find("RemoveButton");
       

        var addButton = addObj.GetComponent<Button>();
        var removeButton = remObj.GetComponent<Button>();
        
        addButton.onClick.AddListener(AssignWorker);
        removeButton.onClick.AddListener(RemoveAssignedWorker);

        UpdateCounterText();
    }


    public bool IsEmpty() => occupant == null;

    public bool CorrectRoomSize(SCPData data)
    {
        return data.requiredSize == Size;
    }

    public void UpdateCounterText()
    {
        var coutObj = Building.transform.Find("Canvas").Find("Container").Find("Counter").Find("CounterText");
        var counterText = coutObj.GetComponent<TextMeshProUGUI>();
        counterText.text = $"{assignedWorkers.Count} / {Size.x * Size.y}";
    }

    public void Populate(SCPData occupantData)
    {
        if (!CorrectRoomSize(occupantData)) return;

        if (!IsEmpty()) return;
        

        GameObject prefab = Registry.Get<GameManager>().scpObjectPrefab;
        GameObject go = GameObject.Instantiate(prefab, Building.occupantOriginTransform.position, prefab.transform.rotation);
        SCPObject obj = go.GetComponent<SCPObject>();
        obj.UpdateRenderer(occupantData);
        this.occupant = new SCPModel(occupantData, obj);
    }

    public void AssignWorker()
    {
        if (assignedWorkers.Count == Size.x * Size.y) return;

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

