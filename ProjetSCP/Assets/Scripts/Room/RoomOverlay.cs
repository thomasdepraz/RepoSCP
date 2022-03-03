using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SCP.Camera;
using SCP.Data;
using System;

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
        print(camController);
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
    public Vector2 Position { get; set; }
    public Vector2 FocusPosition { get; }
    public Vector2 Size { get; private set; }

    public readonly RoomOverlay overlay;

    protected Action OnSelectCallback;

    protected Action OnDeselectCallback;
}

public class CommandPost : Room
{
    public CommandPost()
    {

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

    }

    public void PopulateHouse()
    {

    }

 }

