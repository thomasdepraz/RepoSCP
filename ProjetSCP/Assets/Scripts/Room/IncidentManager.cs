using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCP.Data;
using SCP.Ressources.Display;
using SCP.Ressources;

public class IncidentManager : MonoBehaviour
{
    public static RessourcesManager ressourceManager;
    public RessourcesDisplay display;
    public TurnManager turnManager;
    public TurnReport turnReport;
    public List<ScpContainer> scpRooms;
    private List<SCPData> scpDatas;
    private List<int> assignedWorkersList;
    private List<ScpContainer> damagedScpRooms;
    public int totalWorkersLost = 0;

    void Start()
    {
        ressourceManager = Registry.Get<RessourcesManager>();
        turnManager.callNextTurn.AddListener(GetAllSCPIncidents);

        scpDatas = new List<SCPData>();
        assignedWorkersList = new List<int>();
        damagedScpRooms = new List<ScpContainer>();
    }

    private void GetAllSCPIncidents()
    {
        totalWorkersLost = 0;
        scpDatas.Clear();
        assignedWorkersList.Clear();
        damagedScpRooms.Clear();
        scpRooms = ressourceManager.scpRooms;

        for (int a = 0; a < scpRooms.Count; a++)
        {
            if (!scpRooms[a].IsEmpty())
            {
                scpDatas.Add(scpRooms[a].occupant.Data);
                assignedWorkersList.Add(scpRooms[a].assignedWorkers.Count);
                damagedScpRooms.Add(scpRooms[a]);
            }
        }

        for (int b = 0; b < scpDatas.Count; b++)
        {
            VerifySCPIncident(scpDatas[b], assignedWorkersList[b], damagedScpRooms[b]);
        }

        if (totalWorkersLost != 0)
        {
            display.StartCoroutine(display.IndicateValueChange(totalWorkersLost, 0, true));
            display.StartCoroutine(display.IndicateValueChange(totalWorkersLost, 1, true));
        }
    }

    private void VerifySCPIncident(SCPData containedSCP, int activeWorkers, ScpContainer damagedRoom) // Ajouter la préférence de classe;
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
                if (containedSCP.optimalState == false)
                {
                    requirementValue = 10;
                }
                break;

            case SCPType.EUCLIDE:
                typeValue = 20;
                workerFactor = 2;
                requirementValue = 5;
                break;

            case SCPType.KETER:
                typeValue = 40;
                workerFactor = 4;
                if (containedSCP.optimalState == false)
                {
                    requirementValue = 20;
                }
                break;
        }

        missingWorkers = ((containedSCP.size / 4) * workerFactor) - activeWorkers;

        incidentChance = (typeValue + (missingWorkers * 5) /*+ requirementValue*/) * 0.01f;

        float random = Random.Range(0, 100) * 0.01f;

        if (random <= incidentChance)
        {
            TriggerIncident(containedSCP, damagedRoom);
        }
    }

    public void TriggerIncident(SCPData scpData, ScpContainer damagedRoom)
    {
        //Put here Incident effects.
        //Get Assigned worker 0.
        //Remove assigned worker 0.
        //Remove Worker.

        int damage = scpData.incident.damage;

        if (damage <= damagedRoom.assignedWorkers.Count)
        {
            for (int c = 0; c < damage; c++)
            {
                Worker workerMemory = damagedRoom.assignedWorkers[0];
                damagedRoom.assignedWorkers.RemoveAt(0);
                ressourceManager.RemoveWorker(workerMemory);
                totalWorkersLost++;
            }
        }
        else if (damage > damagedRoom.assignedWorkers.Count)
        {
            int differenceToKill = damage - damagedRoom.assignedWorkers.Count;

            for (int d = 0; d < damagedRoom.assignedWorkers.Count; d++)
            {
                Worker workerMemory = damagedRoom.assignedWorkers[0];
                damagedRoom.assignedWorkers.RemoveAt(0);
                ressourceManager.RemoveWorker(workerMemory);
                totalWorkersLost++;
            }

            for (int e = 0; e < differenceToKill; e++)
            {
                ressourceManager.RemoveWorker();
                totalWorkersLost++;
            }
        }

        turnManager.CreateIncident(scpData.incident);
    }
}
