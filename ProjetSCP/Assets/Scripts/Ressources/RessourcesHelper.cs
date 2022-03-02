using System.Collections.Generic;

namespace SCP.Ressources.Helper
{
    public class RessourcesHelper
    {
        public static int GetAvailableWorkersCount(IEnumerable<Worker> workers)
        {
            int count = 0;
            foreach(var worker in workers)
            {
                if (worker.State == Worker.WorkerState.IDLE)
                    count++;
            }
            return count;
        }
    }
}
