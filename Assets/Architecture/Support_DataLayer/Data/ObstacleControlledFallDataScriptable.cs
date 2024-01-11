using System;
using UnityEngine;

namespace DataLayer
{
    [CreateAssetMenu(menuName = "Custom/ObstacleControlledFallData", fileName = "ObstacleControlledFallData")]
    public class ObstacleControlledFallDataScriptable : ScriptableObject
    {
        public FallingItemSettings smallestFallingObstacleSettings;
        public FallingItemSettings smallFallingObstacleSettings;
        public FallingItemSettings mediumFallingObstacleSettings;
        public FallingItemSettings largeFallingObstacleSettings;
        public FallingItemSettings largestFallingObstacleSettings;
    }

    [Serializable]
    public class FallingItemSettings
    {
        public FallingItemSettings()
        {
        }

        public FallingItemSettings(FallingItemSettings other)
        {
            this.fallingDownSpeed = other.fallingDownSpeed;
            this.fallingDownItemLifetime = other.fallingDownItemLifetime;
            this.fallTowardsCenter = other.fallTowardsCenter;
            this.fallingCenterSpeed = other.fallingCenterSpeed;
        }

        public float fallingDownSpeed = 6.0f;
        public float fallingDownItemLifetime = 1.0f;

        public bool fallTowardsCenter = true;
        public float fallingCenterSpeed=  3.5f;

        public void Reset()
        {
            fallingDownSpeed = 6.0f;
            fallingDownItemLifetime = 1.0f;
            fallTowardsCenter = true;
            fallingCenterSpeed=  3.5f;
        }
    }
}