using UnityEngine;

namespace SCP.Data
{
    public class SCPModel
    {
        public SCPData Data;
        public GameObject Object;

        public SCPModel(SCPData data, GameObject obj)
        {
            Data = data;
            Object = obj;
        }
    }

}
