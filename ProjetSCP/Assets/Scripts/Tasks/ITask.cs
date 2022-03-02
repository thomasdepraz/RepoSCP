namespace SCP.Tasks
{
    public interface ITask
    {
        public bool IsPerformable();

        public void PerformTask();
    }
}
