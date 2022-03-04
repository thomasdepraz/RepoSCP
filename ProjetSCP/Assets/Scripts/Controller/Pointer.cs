using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [HideInInspector] public bool checkPosition;
    public Vector2 currentGridPosition = Vector2.zero;
    public Action UpdateGridCallback;
    private Camera cam;

    public Vector2 gridSize;

    private void Awake()
    {
        new Registry().Register<Pointer>(this);
    }

    private void Start()
    {
        cam = Camera.main;
    }

    public void Update()
    {
        Vector2 newPos = GetGridPosition(Input.mousePosition);
        if(newPos != currentGridPosition)
        {
            currentGridPosition = newPos;
            UpdateGridCallback?.Invoke();
        }
        if(checkPosition)
        {
            //Move code back in here

        }
    }

    public Vector2 GetGridPosition(Vector3 mousePositionOnScreen)//le truc chiant
    {
        Vector3 viewportPoint = cam.ScreenToViewportPoint(new Vector3(mousePositionOnScreen.x, mousePositionOnScreen.y, cam.nearClipPlane));
        Vector2 viewportScaledPoint = new Vector2(Mathf.FloorToInt(viewportPoint.x * gridSize.x), Mathf.FloorToInt(viewportPoint.y * gridSize.y));

        //offset point
        Vector2 newPoint = new Vector2(viewportScaledPoint.x, gridSize.y - (viewportScaledPoint.y + 1) - 3);

        return newPoint;
    }

}

public class Grid
{
    public Vector2 size { get; private set; }
    public bool[,] gridSlots;


    public Grid(Vector2 size)
    {
        gridSlots = new bool[(int)size.x, (int)size.y];
    }



    public void Build(Vector2 position, Vector2 size)
    {

    }

    public bool CanBuild(Vector2 position, Vector2 size)
    {
        return false;
    }




}
