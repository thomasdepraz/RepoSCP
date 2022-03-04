using UnityEngine;


[CreateAssetMenu(fileName = "New SCP Mission Incident", menuName = "Data/SCPMissionIncident")]
public class SCPMissionIncident : ScriptableObject
{
    [TextArea(3,20)]
    public string lowWorkersDeathIncident;
    [TextArea(3, 20)]
    public string mediumWorkersDeathIncident;
    [TextArea(3, 20)]
    public string highWorkersDeathIncident;
}
