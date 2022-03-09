using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCP.Data;
using SCP.Ressources;

public class IncidentManager : MonoBehaviour
{
    public static RessourcesManager ressourceManager;
    public TurnManager turnManager;
    public List<ScpContainer> scpRooms;
    private List<SCPData> scpDatas;
    private List<int> assignedWorkersList;

    void Start()
    {
        ressourceManager = Registry.Get<RessourcesManager>();
        turnManager.callNextTurn.AddListener(GetAllSCPIncidents);

        scpDatas = new List<SCPData>();
        assignedWorkersList = new List<int>();
    }

    private void GetAllSCPIncidents()
    {
        scpDatas.Clear();
        assignedWorkersList.Clear();
        scpRooms = ressourceManager.scpRooms;

        for (int a = 0; a < scpRooms.Count; a++)
        {
            if (!scpRooms[a].IsEmpty())
            {
                scpDatas.Add(scpRooms[a].occupant.Data);
                assignedWorkersList.Add(scpRooms[a].assignedWorkers.Count);
            }
        }

        for (int b = 0; b < scpDatas.Count; b++)
        {
            VerifySCPIncident(scpDatas[b], assignedWorkersList[b]);
        }
    }

    private void VerifySCPIncident(SCPData containedSCP, int activeWorkers) // Ajouter la préférence de classe;
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
                /*if (classRequirement == false)
                {
                    requirementValue = 10;
                }*/
                break;

            case SCPType.EUCLIDE:
                typeValue = 20;
                workerFactor = 2;
                /*if (classRequirement == false)
                {
                    requirementValue = 20;
                }*/
                break;

            case SCPType.KETER:
                typeValue = 40;
                workerFactor = 4;
                /*if (classRequirement == false)
                {
                    requirementValue = 20;
                }*/
                break;
        }

        missingWorkers = ((containedSCP.size / 4) * workerFactor) - activeWorkers;

        incidentChance = (typeValue + (missingWorkers * 5) /*+ requirementValue*/) * 0.01f;

        float random = Random.Range(0, 100) * 0.01f;

        if (random <= incidentChance)
        {
            TriggerIncident(containedSCP);
        }
    }

    public void TriggerIncident(SCPData scpData)
    {
        //Put here Incident effects.

        turnManager.CreateIncident(scpData.incident);
    }
}
