using UnityEngine;
using SCP.Data;

[CreateAssetMenu(fileName = "New SCP Incident", menuName = "Data/SCPIncident")]
public class SCPIncident : ScriptableObject
{
    public int damage;
    public string incident1Title;
    [TextArea(3, 20)]
    public string incident1Description;
    public string incident2Title;
    [TextArea(3, 20)]
    public string incident2Description;
    public string incident3Title;
    [TextArea(3, 20)]
    public string incident3Description;
}
