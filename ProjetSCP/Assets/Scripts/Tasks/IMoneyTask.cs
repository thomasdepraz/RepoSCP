using SCP.Tasks;


namespace SCP.Tasks
{
    public interface IMoneyTask : ITask
    {
        public void FinanceTask(int cost);
    }
}
