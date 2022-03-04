﻿using System.Collections;
using UnityEngine;
using TMPro;
using SCP.Ressources.Helper;
using UnityEngine.Events;

namespace SCP.Ressources.Display
{
    public class RessourcesDisplay : MonoBehaviour
    {
        public static RessourcesManager manager;

        [Header("RessourceDisplays")]
        public TextMeshProUGUI moneyDisplayObject;
        public TextMeshProUGUI workerDisplayObject;
        private static TextMeshProUGUI moneyDisplay;
        private static TextMeshProUGUI workerDisplay;

        [Header("Testing")]
        public TurnManager turnManager;
        public GameObject reportPlaceholder;
        public TextMeshProUGUI dayNumberReport;

        public void Start()
        {
            //Temporary
            var ressourcesManager = new RessourcesManager();
            //Get Manager and inject
            manager = Registry.Get<RessourcesManager>();
            manager.display = this;

            moneyDisplay = moneyDisplayObject;
            workerDisplay = workerDisplayObject;

            turnManager.callNextTurn.AddListener(TurnIncomeTrigger);

            reportPlaceholder.SetActive(false);
        }

        //For Testing Only
        public void Update()
        {
           if (Input.GetKeyDown(KeyCode.T))
           {
                manager.AddMoney(10);
                UpdateMoneyDisplay();
           } 

           if (Input.GetKeyDown(KeyCode.Y))
           {
                manager.AddWorker();
                UpdateHumanRessourcesDisplay();
           }

           if (Input.GetKeyDown(KeyCode.G))
           {
                manager.RemoveMoney(10);
                UpdateMoneyDisplay();
           }

           if (Input.GetKeyDown(KeyCode.H))
           {
                manager.RemoveWorker();
                UpdateHumanRessourcesDisplay();
           }
        }


        public static void UpdateHumanRessourcesDisplay()
        {
            workerDisplay.text = RessourcesHelper.GetAvailableWorkersCount(manager.HumanRessources) + "/" + manager.HumanRessources.Count;
        }

        public static void UpdateMoneyDisplay()
        {
            moneyDisplay.text = manager.Money + " k";
        }

        private void TurnIncomeTrigger()
        {
            StartCoroutine(TurnIncome());
        }

        private IEnumerator TurnIncome()
        {
            reportPlaceholder.SetActive(true);
            manager.AddMoney(10);
            UpdateMoneyDisplay();
            dayNumberReport.text = "Jour : " + turnManager.dayCount;
            yield return new WaitForSeconds(2f);
            reportPlaceholder.SetActive(false);
        }
    }
}
