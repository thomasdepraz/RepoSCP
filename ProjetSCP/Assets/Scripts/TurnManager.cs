using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SCP.Data;

public class TurnManager : MonoBehaviour
{
    public UnityEvent callNextTurn;
    public UnityEvent callNextTurnLate;
    public int dayCount = 1;
    public List<SCPIncident> incidents;
    

    void Start()
    {
        callNextTurn.AddListener(DebugNewDay);
    }

    public void NewTurn()
    {
        incidents.Clear();
        dayCount++;
        callNextTurn.Invoke();
        callNextTurnLate.Invoke();
    }

    private void DebugNewDay()
    {
        Debug.Log("Day " + dayCount);
    }

    public void CreateIncident(SCPIncident incidentType)
    {
        Debug.Log("Incident !");

        incidents.Add(incidentType);
    }
}
