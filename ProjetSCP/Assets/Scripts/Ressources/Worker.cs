using SCP.Ressources.Display;

namespace SCP.Ressources
{
    public class Worker
    {
        public enum WorkerState
        {
            IDLE,
            WORKING
        }

        public WorkerState State { get; private set; }

        public Worker()
        {
            State = WorkerState.IDLE;
        }

        public void Engage()
        {
            State = WorkerState.WORKING;
            RessourcesDisplay.UpdateHumanRessourcesDisplay();
        }

        public void Disengage()
        {
            State = WorkerState.IDLE;
            RessourcesDisplay.UpdateHumanRessourcesDisplay();
        }
    }
}
