using SCP.Ressources.Display;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCP.Ressources
{
    public class RessourcesManager 
    {
        public int Money { get; private set; }
        public List<Worker> HumanRessources { get; private set; } = new List<Worker>();

        public RessourcesDisplay display;

        public RessourcesManager()
        {
            new Registry().Register<RessourcesManager>(this);
            for (int i = 0; i < 12; i++)
            {
                HumanRessources.Add(new Worker());
            }
        }

        public void AddWorker()
        {
            HumanRessources.Add(new Worker());
        }

        public void RemoveWorker(Worker toRemove = null)
        {
            if (HumanRessources.Count == 0) return;

            if (toRemove == null) HumanRessources.Remove(toRemove);
            else HumanRessources.RemoveAt(0);
        }


        public void AddMoney(int quantity)
        {
            Money += quantity;
        }

        public void RemoveMoney(int quantity)
        {
            Money -= quantity;
        }
    }
}
