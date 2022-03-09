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

        public House House { get; private set; }

        public Worker()
        {
            State = WorkerState.IDLE;
        }

        public Worker(House house)
        {
            State = WorkerState.IDLE;
            House = house;
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
