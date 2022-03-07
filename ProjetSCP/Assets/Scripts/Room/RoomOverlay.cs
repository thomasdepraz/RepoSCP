using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SCP.Camera;
using SCP.Data;
using System;
using SCP.Building;

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

        if(Time.time - lastClickTime < 0.3 && camController.camState != CameraState.TWEENING)
        {
            camController.ChangeState(transform.position);
        }
        else if(camController.camState == CameraState.FOCUSED && (Vector2)transform.position!= (Vector2)camController.transform.position)
        {
            camController.FocusTarget(transform.position);
        }
        lastClickTime = Time.time;
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

    public readonly RoomOverlay overlay;

    protected Action OnSelectCallback;

    protected Action OnDeselectCallback;

    public virtual void SetPosition(Vector2 position)
    {
        Position = position;
        //calculate focus position
        
    }

}

public class CommandPost : Room
{
    public CommandPost()
    {
        Size = new Vector2(3, 2);

        OnSelectCallback = OnSelect;
        OnDeselectCallback = OnDeselect;
    }

    public void OnSelect()
    {
        Registry.Get<BuildingManager>().ToggleBuildingMode(true);
    }

    public void OnDeselect()
    {
        Registry.Get<BuildingManager>().ToggleBuildingMode(false);
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

    public House()
    {
        //recenser les maisons qqpart après leur création;
        Size = new Vector2(1, 1);
        MoneyCost = 50;

    }

    public void PopulateHouse()
    {

    }

 }

public class Warehouse : Room
{
    public SCPModel occupant;

    public Warehouse()
    {
        Size = new Vector3(3, 3);
    }

    public bool IsEmpty() => occupant == null;
}

public class ScpContainer : Room
{
    public SCPModel occupant { get; private set; }

    public ScpContainer(Vector2 size, int moneyCost)
    {
        Size = size;
        MoneyCost = moneyCost;
    }

    public void PopulateRoom()
    {

    }
}

