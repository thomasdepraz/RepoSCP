using UnityEngine;

namespace SCP.Camera
{
     [CreateAssetMenu(fileName ="New Camera Profile", menuName = "Camera/Profile")]
    public class CameraProfile : ScriptableObject
    { 
        public int unfocusedZPos;
        public int focusedZPos;

        public float unfocusedFOV;
        public float focusedFOV;

        public float movementSpeed;
    }
}
