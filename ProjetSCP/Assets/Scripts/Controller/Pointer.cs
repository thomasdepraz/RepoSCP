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
        }
    }

    public Vector2 GetGridPosition(Vector3 mousePositionOnScreen)//le truc chiant
    {
        Vector3 viewportPoint = cam.ScreenToViewportPoint(new Vector3(mousePositionOnScreen.x, mousePositionOnScreen.y, cam.nearClipPlane));

        Debug.Log(new Vector2(viewportPoint.x * gridSize.x, viewportPoint.y * gridSize.y));

        return Vector2.zero;
    }

}
