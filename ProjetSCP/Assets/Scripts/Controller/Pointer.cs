using SCP.Building;
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

    private Vector2 gridSize;

    private void Awake()
    {
        new Registry().Register<Pointer>(this);
    }

    private void Start()
    {
        cam = Camera.main;
        gridSize = Registry.Get<BuildingManager>().gridSize;
    }

    public void Update()
    {
        if(checkPosition)
        {
            //Move code back in here
            Vector2 newPos = GetGridPosition(Input.mousePosition);
            if(newPos != currentGridPosition)
            {
                currentGridPosition = newPos;
                UpdateGridCallback?.Invoke();
            }
        }
    }

    public Vector2 GetGridPosition(Vector3 mousePositionOnScreen)//le truc chiant
    {
        Vector3 viewportPoint = cam.ScreenToViewportPoint(new Vector3(mousePositionOnScreen.x, mousePositionOnScreen.y, cam.nearClipPlane));
        Vector2 viewportScaledPoint = new Vector2(Mathf.FloorToInt(viewportPoint.x * gridSize.x), Mathf.FloorToInt(viewportPoint.y * gridSize.y));

        Vector2 newPoint = new Vector2(Mathf.Clamp(viewportScaledPoint.x, 2, 14), Mathf.Clamp(gridSize.y - (viewportScaledPoint.y + 1) - 2, 0,5));

        return newPoint;
    }

}
