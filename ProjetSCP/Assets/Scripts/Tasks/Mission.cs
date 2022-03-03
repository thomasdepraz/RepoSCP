using SCP.Ressources;
using SCP.Ressources.Helper;
using SCP.Tasks;
using System.Collections.Generic;

public class Mission : IHumanTask
{
    public int HumanNumber { get; private set; }
    public int CommonRarity { get; private set; }
    public int RareRarity { get; private set; }
    public int EpicRarity { get; private set; }


    public Mission(int humanNumber, int commonRarity, int rareRarity, int epicRarity)
    {
        HumanNumber = humanNumber;
        CommonRarity = commonRarity;
        RareRarity = rareRarity;
        EpicRarity = epicRarity;
        
    }

    public void EngageWorkers(IEnumerable<Worker> humanRessources)
    {
        throw new System.NotImplementedException();
        //Mettre les workers qui font rien dans la liste en on
    }

    public IEnumerable<Worker> GetNecessaryWorkers()
    {
        throw new System.NotImplementedException();
        //trouver les workers qui font rien dans la liste
    }

    public bool IsPerformable()
    {
        var manager = Registry.Get<RessourcesManager>();
        int count = RessourcesHelper.GetAvailableWorkersCount(manager.HumanRessources);
        //Vérifier si c'est possible de faire la tache ( es ce que j'ai assez de workers
        return count>= HumanNumber;
    }

    public void PerformTask()
    {
        throw new System.NotImplementedException();
        //faire la tache : appeler fonction dan sun autre script
    }
}
