using SCP.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPObject : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer rend;

    public void UpdateRenderer(SCPData data)
    {
        meshFilter.mesh = data.mesh;
        rend.material = data.material;      
    }
}
