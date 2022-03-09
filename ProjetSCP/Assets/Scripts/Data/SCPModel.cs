namespace SCP.Data
{
    public class SCPModel
    {
        public SCPData Data;
        public SCPObject Object;

        public SCPModel(SCPData data, SCPObject obj)
        {
            Data = data;
            Object = obj;
        }
    }

}
