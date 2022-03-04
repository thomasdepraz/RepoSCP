using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public UnityEvent callNextTurn;
    public int dayCount = 1;

    private void Start()
    {
        callNextTurn.AddListener(DebugNewDay);
    }

    // For testing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NewTurn();
        }
    }

    public void NewTurn()
    {
        dayCount++;
        callNextTurn.Invoke();
    }

    private void DebugNewDay()
    {
        Debug.Log("Day " + dayCount);
    }
}
