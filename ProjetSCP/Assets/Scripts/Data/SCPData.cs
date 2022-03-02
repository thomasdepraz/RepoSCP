using UnityEngine;

namespace SCP.Data
{
    [CreateAssetMenu(fileName = "New SCP Data", menuName = "Data/SCP")]
    public class SCPData : ScriptableObject
    {

        public string ID;
        public string Name;
        public Sprite sprite; //TEMP ?
        public DangerLevel dangerLevel;
        public SCPType type;
        public Rarity rarity;
        public int size;
    }

}
