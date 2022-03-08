using UnityEngine;

namespace SCP.Data
{
    [CreateAssetMenu(fileName = "New SCP Data", menuName = "Data/SCP")]
    public class SCPData : ScriptableObject
    {
        public string ID;
        public string Name;
        public Sprite sprite;
        public Mesh mesh;
        public Material material;
        public DangerLevel dangerLevel;
        public SCPType type;
        public Rarity rarity;
        public int size;
        public SCPMissionIncident missionIncident;
        [TextArea(2, 6)]
        public string descriptionShort;
        public SCPIncident incident;
        [TextArea(2, 20)]
        public string fullDescription;
    }

}
