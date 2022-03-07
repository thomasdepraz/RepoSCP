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

        public RessourcesManager(int defaultWorkersCount, int defaultMoneyQuantity)
        {
            new Registry().Register<RessourcesManager>(this);

            //Init base values
            for (int i = 0; i < defaultWorkersCount; i++)
            {
                HumanRessources.Add(new Worker());
            }

            Money = defaultMoneyQuantity;
        }

        public void AddWorker()
        {
            HumanRessources.Add(new Worker());
            RessourcesDisplay.UpdateHumanRessourcesDisplay();
            display.StartCoroutine(display.IndicateValueChange(1, 0, false));
            display.StartCoroutine(display.IndicateValueChange(1, 1, false));
        }

        public void RemoveWorker(Worker toRemove = null)
        {
            if (HumanRessources.Count == 0) return;

            if (toRemove == null) HumanRessources.Remove(toRemove);
            else HumanRessources.RemoveAt(0);

            RessourcesDisplay.UpdateHumanRessourcesDisplay();
            display.StartCoroutine(display.IndicateValueChange(1, 0, true));
            display.StartCoroutine(display.IndicateValueChange(1, 1, true));
        }


        public void AddMoney(int quantity)
        {
            Money += quantity;
            RessourcesDisplay.UpdateMoneyDisplay();
            display.StartCoroutine(display.IndicateValueChange(quantity, 2, false));
        }

        public void RemoveMoney(int quantity)
        {
            Money -= quantity;
            RessourcesDisplay.UpdateMoneyDisplay();
            display.StartCoroutine(display.IndicateValueChange(quantity, 2, true));
        }
    }
}
