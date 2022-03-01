using SCP.Ressources;
using System.Collections.Generic;


namespace SCP.Tasks
{
    public interface IHumanTask : ITask
    {
        public void EngageWorkers(IEnumerable<Worker> humanRessources);

        public IEnumerable<Worker> GetNecessaryWorkers();
    }
}
