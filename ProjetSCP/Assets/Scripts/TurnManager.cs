using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SCP.Data;

public class TurnManager : MonoBehaviour
{
    public UnityEvent callNextTurn;
    public int dayCount = 1;
    public List 

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

    public void CreateIncident(SCPData scp)
    {
        Debug.Log("Incident !");
    }
}
