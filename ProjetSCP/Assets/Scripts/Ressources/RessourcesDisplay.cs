using UnityEngine;
using TMPro;

namespace SCP.Ressources.Display
{
    public class RessourcesDisplay : MonoBehaviour
    {
        public RessourcesManager manager;

        public void Start()
        {
            //Get Manager and inject
            manager = Registry.Get<RessourcesManager>();
            manager.display = this;
        }


        public static void UpdateHumanRessourcesDisplay()
        {

        }

        public static void UpdateMoneyDisplay()
        {

        }
    }
}
