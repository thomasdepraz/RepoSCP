using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCP.Data;

public class IncidentManager : MonoBehaviour
{
    [SerializeField] public SCPData containedSCP;
    public TurnManager turnManager;

    [Header("TestingValues")]
    public int emptySlots;
    public int activeWorkers;
    public bool classRequirement;
    public int SCPDamage;

    void Start()
    {
        turnManager.callNextTurn.AddListener(VerifyIncident);
    }

    private void VerifyIncident()
    {
        // ((ClassValue)+(EmptySlots*5)+(MissingWorkers*5)+(ClassRequirementFailed))/100

        //KETER = Base 40 / Requirement +20.
        //EUCLIDE = Base 20 / Requirement +20.
        //SÛR = Base 10 / Requirement +10.

        float incidentChance = 0.0f;
        int typeValue = 0;
        int requirementValue = 0;
        int missingWorkers = 0;
        int workerFactor = 0;

        switch (containedSCP.type)
        {
            case SCPType.SAFE:
                typeValue = 10;
                workerFactor = 1;
                if (classRequirement == false)
                {
                    requirementValue = 10;
                }
                break;

            case SCPType.EUCLIDE:
                typeValue = 20;
                workerFactor = 2;
                if (classRequirement == false)
                {
                    requirementValue = 20;
                }
                break;

            case SCPType.KETER:
                typeValue = 40;
                workerFactor = 4;
                if (classRequirement == false)
                {
                    requirementValue = 20;
                }
                break;
        }

        missingWorkers = ((containedSCP.size / 4) * workerFactor) - activeWorkers;

        incidentChance = (typeValue + (emptySlots * 5) + (missingWorkers * 5) + requirementValue) * 0.01f;

        float random = Random.Range(0, 100) * 0.01f;

        if (random <= incidentChance)
        {
            TriggerIncident();
        }
    }

    public void TriggerIncident()
    {
        //Put here Incident effects.

        turnManager.CreateIncident(containedSCP.incident);
    }
}
