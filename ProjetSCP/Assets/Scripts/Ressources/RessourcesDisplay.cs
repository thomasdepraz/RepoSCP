using System.Collections;
using UnityEngine;
using TMPro;
using SCP.Ressources.Helper;

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
        private TurnManager turnManager;
        public GameObject reportPlaceholder;

        public void Start()
        {
            //Get Manager and inject
            manager = Registry.Get<RessourcesManager>();
            turnManager = Registry.Get<TurnManager>();
            
            manager.display = this;

            moneyDisplay = moneyDisplayObject;
            workerDisplay = workerDisplayObject;

            turnManager.callNextTurnLate.AddListener(TurnIncomeTrigger);

            reportPlaceholder.SetActive(false);

            UpdateHumanRessourcesDisplay();
            UpdateMoneyDisplay();
        }

        //For Testing Only
        public void Update()
        {
           if (Input.GetKeyDown(KeyCode.T))
           {
                manager.AddMoney(10);
           } 

           if (Input.GetKeyDown(KeyCode.Y))
           {
                manager.AddWorker();
           }

           if (Input.GetKeyDown(KeyCode.G))
           {
                manager.RemoveMoney(10);
           }

           if (Input.GetKeyDown(KeyCode.H))
           {
                manager.RemoveWorker();
           }
        }


        public static void UpdateHumanRessourcesDisplay()
        {
            if (manager == null) return;
            workerDisplay.text = RessourcesHelper.GetAvailableWorkersCount(manager.HumanRessources) + "/" + manager.HumanRessources.Count;
        }

        public static void UpdateMoneyDisplay()
        {
            if (manager == null) return;
            moneyDisplay.text = manager.Money + " k";
        }

        private void TurnIncomeTrigger()
        {
            reportPlaceholder.SetActive(true);
            reportPlaceholder.GetComponent<TurnReport>().UpdateReport();  
            manager.AddMoney(10);
        }
    }
}
