using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCP.Building
{
    public class Building : MonoBehaviour
    {
        public BuildingType type;
        public RoomOverlay overlay;
        public Room room;

        public Transform occupantOriginTransform;
        public GameObject roomContent;


        public void Start()
        {

        }

    }
}
