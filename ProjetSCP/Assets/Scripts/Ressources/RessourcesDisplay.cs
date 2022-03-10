using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SCP.Ressources.Helper;
using SCP.Data;

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
        public TextMeshProUGUI[] modifTexts;
        public float feedbackDuration;
        public List<ScpContainer> scpRooms;

        [Header("Testing")]
        private TurnManager turnManager;
        public GameObject reportPlaceholder;

        public void Start()
        {
            //Get Manager and inject
            manager = Registry.Get<RessourcesManager>();
            turnManager = Registry.Get<TurnManager>();

            manager.display = this;

            scpRooms = manager.scpRooms;

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
            int income = 0;

            reportPlaceholder.SetActive(true);

            scpRooms = manager.scpRooms;

            for (int x = 0; x < scpRooms.Count; x++)
            {
                if (!scpRooms[x].IsEmpty())
                {
                    income += CountSCPIncome(scpRooms[x].occupant.Data);
                }
            }

            income = income * 100;

            manager.AddMoney(income);

            reportPlaceholder.GetComponent<TurnReport>().UpdateReport(income);
        }

        private int CountSCPIncome(SCPData data)
        {
            int rarityValue = 0;
            int typeValue = 0;
            int dangerValue = 0;

            switch (data.rarity)
            {
                case Rarity.COMMON:
                    rarityValue = 1;
                    break;
                case Rarity.RARE:
                    rarityValue = 2;
                    break;
                case Rarity.EPIC:
                    rarityValue = 3;
                    break;
                case Rarity.UNIQUE:
                    rarityValue = 5;
                    break;
            }

            switch (data.type)
            {
                case SCPType.SAFE:
                    typeValue = 1;
                    break;

                case SCPType.EUCLIDE:
                    typeValue = 2;
                    break;

                case SCPType.KETER:
                    typeValue = 4;
                    break;
            }

            switch (data.dangerLevel)
            {
                case DangerLevel.GREEN:
                    dangerValue = 1;
                    break;

                case DangerLevel.YELLOW:
                    dangerValue = 2;
                    break;

                case DangerLevel.ORANGE:
                    dangerValue = 3;
                    break;

                case DangerLevel.RED:
                    dangerValue = 4;
                    break;

                case DangerLevel.BLACK:
                    dangerValue = 5;
                    break;
            }

            int result = rarityValue + typeValue + dangerValue;

            return (result);
        }


        public IEnumerator IndicateValueChange(int modificationValue, int modifType, bool negative)
        {
            int value = Mathf.Abs(modificationValue);

            switch (modifType)
            {
                case 0:
                    modifTexts[0].gameObject.SetActive(true);
                    if (negative == true)
                    {
                        modifTexts[0].text = "- " + value.ToString();
                    }
                    else
                    {
                        modifTexts[0].text = "+ " + value.ToString();
                    }
                    break;

                case 1:
                    modifTexts[1].gameObject.SetActive(true);
                    if (negative == true)
                    {
                        modifTexts[1].text = "- " + value.ToString();
                    }
                    else
                    {
                        modifTexts[1].text = "+ " + value.ToString();
                    }
                    break;

                case 2:
                    modifTexts[2].gameObject.SetActive(true);
                    if (negative == true)
                    {
                        modifTexts[2].text = "- " + value.ToString();
                    }
                    else
                    {
                        modifTexts[2].text = "+ " + value.ToString();
                    }
                    break;
            }

            yield return new WaitForSeconds(feedbackDuration);

            modifTexts[modifType].gameObject.SetActive(false);
        }
    }
}
