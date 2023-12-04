using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DataLayer
{
    [CreateAssetMenu(menuName = "Custom/HoleMovementData", fileName = "HoleMovementData")]
    [System.Serializable]
    public class HoleMovementScriptable : ScriptableObject
    {
        [SerializeField]
        private AnimationCurve holeSpeedCurve;
        [SerializeField]
        private int holeDataCount = 20;
        [SerializeField]
        private List<HoleMovementData> holeMovementDataList = new();
    
        [Button]
        public void UpdateValues()
        {
            holeMovementDataList.Clear();

            for (int i = 0; i < holeDataCount; i++)
            {
                holeMovementDataList.Add(new HoleMovementData());
            }
        
            for (var i = 0; i < holeMovementDataList.Count; i++)
            {
                var holeGrowthData = holeMovementDataList[i];
                var remappedValue = MathX.ClampedRemap(0, holeMovementDataList.Count,
                    0.0f, 1.0f,
                    i);

                holeGrowthData.holeSpeed = holeSpeedCurve.Evaluate(remappedValue);
                holeGrowthData.growthLevel = i;
            }
        }

        [CanBeNull]
        public HoleMovementRecord GetHoleMovementRecordByGrowthLevel(int growthLevel)
        {
            for (int i = holeMovementDataList.Count - 1; i >= 0; i--)
            {
                var item = holeMovementDataList[i];
                if (item.growthLevel == growthLevel)
                {
                    return new HoleMovementRecord()
                    {
                        growthLevel = item.growthLevel,
                        holeSpeed = item.holeSpeed,
                    };
                }
            }

            return null;
        }
 
        public int GetRecordCount()
        {
            return holeMovementDataList.Count;
        }
 
        public record HoleMovementRecord
        {
            public int growthLevel;
            public float holeSpeed;
        }

        [System.Serializable]
        private class HoleMovementData
        {
            public int growthLevel;
            public float holeSpeed;
        }
    }
}