using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DataLayer
{
    [CreateAssetMenu(menuName = "Custom/HoleCameraData", fileName = "HoleCameraData")]
    [System.Serializable]
    public class HoleCameraScriptable : ScriptableObject
    {
        [SerializeField]
        private AnimationCurve holeFovCurve;
        [SerializeField]
        private AnimationCurve holeDistanceCurve;
        [SerializeField]
        private int holeDataCount = 20;
        [SerializeField]
        private List<HoleCameraData> holeCameraDataList = new();
    
        [Button]
        public void UpdateValues()
        {
            holeCameraDataList.Clear();

            for (int i = 0; i < holeDataCount; i++)
            {
                holeCameraDataList.Add(new HoleCameraData());
            }
        
            for (var i = 0; i < holeCameraDataList.Count; i++)
            {
                var holeGrowthData = holeCameraDataList[i];
                var remappedValue = MathX.ClampedRemap(0, holeCameraDataList.Count,
                    0.0f, 1.0f,
                    i);

                holeGrowthData.cameraFov = (int) holeFovCurve.Evaluate(remappedValue);
                holeGrowthData.cameraDistance = (int) holeDistanceCurve.Evaluate(remappedValue);
                holeGrowthData.growthLevel = i;
            }
        }
    
        public int GetRecordCount()
        {
            return holeCameraDataList.Count;
        }
 
        [CanBeNull]
        public HoleCameraRecord GetHoleCameraRecordByGrowthLevel(int growthLevel)
        {
            for (int i = holeCameraDataList.Count - 1; i >= 0; i--)
            {
                var item = holeCameraDataList[i];
                if (item.growthLevel == growthLevel)
                {
                    return new HoleCameraRecord()
                    {
                        growthLevel = item.growthLevel,
                        cameraFov = item.cameraFov,
                        cameraDistance = item.cameraDistance,
                    };
                }
            }

            return null;
        }
    
        public record HoleCameraRecord
        {
            public int growthLevel;
            public float cameraFov;
            public float cameraDistance;
        }

        [System.Serializable]
        private class HoleCameraData
        {
            public int growthLevel;
            public float cameraFov;
            public float cameraDistance;
        }
    }
}