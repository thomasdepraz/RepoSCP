using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SCP.Data;

public class TurnReport : MonoBehaviour
{
    public TurnManager turnManager;
    public TextMeshProUGUI moneyGainedText;
    public TextMeshProUGUI dayCountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateReport()
    {
        //Récupérer l'argent gagner et l'afficher.
        dayCountText.text = "Jour : " + turnManager.dayCount;
    }

    public void ExitReport()
    {
        this.gameObject.SetActive(false);
    }

    public void AddIncident(SCPType type, int damage)
    {

    }
}
