using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace DataLayer
{
    [CreateAssetMenu(menuName = "Custom/MapManagerData", fileName = "MapManagerData")]
    [System.Serializable]
    public class MapManagerDataScriptable : ScriptableObject
    {
        [SerializeField]
        private List<MapData> mapList = new();

        public int GetRecordCount()
        {
            return mapList.Count;
        }
 
        [CanBeNull]
        public MapRecord GetMapRecordByIndex(int index)
        {
            if (index < 0 || index > mapList.Count - 1)
            {
                return null;
            }
        
            var data = mapList[index];
            var record = new MapRecord()
            {
                mapName = data.mapName,
                mapPrefab = data.mapPrefab,
            };

            return record;
        }
    
        [CanBeNull]
        public MapRecord GetMapRecordByName(string mapName)
        {
            var data = mapList.FirstOrDefault(x => x.mapName == mapName);
            if (data == null) { return null; }
        
            var record = new MapRecord()
            {
                mapName = data.mapName,
                mapPrefab = data.mapPrefab,
            };

            return record;
        }
    
        public record MapRecord
        {
            public string mapName;
            public GameObject mapPrefab;
        }

        [System.Serializable]
        private class MapData
        {
            public string mapName;
            public GameObject mapPrefab;
        }
    }
}