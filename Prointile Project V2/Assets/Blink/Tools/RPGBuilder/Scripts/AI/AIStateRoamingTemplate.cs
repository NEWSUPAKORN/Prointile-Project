using System;
using UnityEngine;

namespace BLINK.RPGBuilder.AI
{
    [Serializable, CreateAssetMenu(fileName = "New AI Roaming State Template", menuName = "BLINK/AI/Templates/Roaming Template")]
    public class AIStateRoamingTemplate : AIStateIdleTemplate
    {
        public float PauseDuration = 60;
        public float RoamDistance = 15;
        public float RoamPointTreshold;
        public LayerMask roamGroundLayers;
        public float roamTargetCheckInterval = 0.25f;
        public bool roamAroundSpawner = true;
    }
}
