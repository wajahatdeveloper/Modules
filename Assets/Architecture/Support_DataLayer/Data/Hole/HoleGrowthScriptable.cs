using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DataLayer
{
    [CreateAssetMenu(menuName = "Custom/HoleGrowthData", fileName = "HoleGrowthData")]
    [System.Serializable]
    public class HoleGrowthScriptable : ScriptableObject
    {
        [SerializeField]
        private AnimationCurve holeScoreCurve;
        [SerializeField]
        private AnimationCurve holeScaleCurve;
        [SerializeField]
        private int holeDataCount = 20;
        [SerializeField]
        private List<HoleGrowthData> holeGrowthDataList = new();
    
        [Button]
        public void UpdateValues()
        {
            holeGrowthDataList.Clear();

            for (int i = 0; i < holeDataCount; i++)
            {
                holeGrowthDataList.Add(new HoleGrowthData());
            }
        
            for (var i = 0; i < holeGrowthDataList.Count; i++)
            {
                var holeGrowthData = holeGrowthDataList[i];
                var remappedValue = MathX.ClampedRemap(0, holeGrowthDataList.Count,
                    0.0f, 1.0f,
                    i);

                holeGrowthData.scoreRequired = (int)holeScoreCurve.Evaluate(remappedValue);
                holeGrowthData.holeScale = holeScaleCurve.Evaluate(remappedValue);
                holeGrowthData.growthLevel = i;
            }
        }

        public int GetHoleGrowthRecordIndex(HoleGrowthRecord record)
        {
            for (int i = 0; i < holeGrowthDataList.Count; i++)
            {
                var item = holeGrowthDataList[i];
                if (item.scoreRequired == record.scoreRequired)
                {
                    return i;
                }
            }

            return -1;
        }

        [CanBeNull]
        public HoleGrowthRecord GetHoleGrowthRecordByScore(int score)
        {
            for (int i = holeGrowthDataList.Count - 1; i >= 0; i--)
            {
                var item = holeGrowthDataList[i];
                if (item.scoreRequired <= score)
                {
                    return new HoleGrowthRecord()
                    {
                        scoreRequired = item.scoreRequired,
                        growthLevel = item.growthLevel,
                        holeScale = item.holeScale,
                    };
                }
            }

            return null;
        }
    
        [CanBeNull]
        public HoleGrowthRecord GetHoleGrowthRecordByGrowthLevel(int growthLevel)
        {
            for (int i = holeGrowthDataList.Count - 1; i >= 0; i--)
            {
                var item = holeGrowthDataList[i];
                if (item.growthLevel == growthLevel)
                {
                    return new HoleGrowthRecord()
                    {
                        scoreRequired = item.scoreRequired,
                        growthLevel = item.growthLevel,
                        holeScale = item.holeScale,
                    };
                }
            }

            return null;
        }
    
        public int GetRecordCount()
        {
            return holeGrowthDataList.Count;
        }

        public record HoleGrowthRecord
        {
            public int scoreRequired;
            public int growthLevel;
            public float holeScale;
        }

        [System.Serializable]
        private class HoleGrowthData
        {
            public int scoreRequired;
            public int growthLevel;
            public float holeScale;
        }
    }
}