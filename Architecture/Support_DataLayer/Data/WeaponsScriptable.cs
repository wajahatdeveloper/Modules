using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DataLayer
{
    [CreateAssetMenu(menuName = "Custom/WeaponsData", fileName = "WeaponsData")]
    [System.Serializable]
    public class WeaponsScriptable : ScriptableObject
    {
        [SerializeField]
        private AnimationCurve priceCurve;

        [Header("Weapon Data")]
        [SerializeField]
        private int weaponDataCount = 7;
        [SerializeField]
        private List<WeaponData> weaponDataList = new();

        #if UNITY_EDITOR

        [Space]
        [FoldoutGroup("Generation")]
        [FolderPath]
        public string weaponEnumPath;

        [FoldoutGroup("Generation")]
        [Button]
        public void RebuildWeaponEnum()
        {
            if (weaponEnumPath.Trim().Length <= 0)
            {
                Debug.LogError("Must have folder path for generation");
                return;
            }

            var list = weaponDataList.Select(x => x.name).ToArray();
            File.WriteAllText(weaponEnumPath + "/" + "WeaponTypeEnum" + ".cs", GetClassContent("WeaponType", list));
            AssetDatabase.ImportAsset(weaponEnumPath + "/" + "WeaponTypeEnum" + ".cs", ImportAssetOptions.ForceUpdate);

            Debug.Log("Generation Complete");
        }

        private static string GetClassContent(string className, string[] labelsArray)
        {
            string output = "";
            output += "//This class is auto-generated do not modify (WeaponsScriptable.cs)\n";
            output += "public enum " + className + "\n";
            output += "{\n";
            foreach (string label in labelsArray)
            {
                output += "\t"+ label + ",\n";
            }
            output += "}";
            return output;
        }

        #endif


        [Button]
        public void UpdateValues()
        {
            for (int i = weaponDataList.Count; i < weaponDataCount; i++)
            {
                weaponDataList.Add(new WeaponData());
            }

            for (var i = 0; i < weaponDataList.Count; i++)
            {
                var weaponData = weaponDataList[i];
                var remappedValue = MathX.ClampedRemap(0, weaponDataList.Count,
                    0.0f, 1.0f,
                    i);

                var evaluatedValue = (int)priceCurve.Evaluate(remappedValue);

                weaponData.price = evaluatedValue;
            }
        }

        [CanBeNull]
        public WeaponRecord GetWeaponRecordByName(string weaponName)
        {
            var weaponData = weaponDataList.FirstOrDefault(x => x.name == weaponName);
            if (weaponData == null)
            {
                return null;
            }

            int index = weaponDataList.IndexOf(weaponData);
            var weaponRecord = new WeaponRecord(weaponData.sprite)
            {
                weaponIndex = index,
                name = weaponData.name,
                price = weaponData.price,
            };

            return weaponRecord;
        }

        [CanBeNull]
        public WeaponRecord GetWeaponRecordByIndex(int index)
        {
            if (index < weaponDataList.Count)
            {
                var weaponData = weaponDataList[index];
                var weaponRecord = new WeaponRecord(weaponData.sprite)
                {
                    weaponIndex = index,
                    name = weaponData.name,
                    price = weaponData.price,
                };
                return weaponRecord;
            }

            return null;
        }

        public int GetRecordCount()
        {
            return weaponDataList.Count;
        }

        public record WeaponRecord
        {
            internal WeaponRecord(Sprite spr)
            {
                sprite = spr;
            }

            public int weaponIndex;
            public string name;
            public string description;
            public Sprite sprite;
            public int price;
        }

        [System.Serializable]
        private class WeaponData
        {
            public string name;
            public string description;
            public Sprite sprite;
            public int price;
        }
    }
}