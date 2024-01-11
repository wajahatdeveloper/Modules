using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif
using UnityEngine;

namespace DataLayer
{
    [CreateAssetMenu(menuName = "Custom/HoleSkinData", fileName = "HoleSkinData")]
    [System.Serializable]
    public class HoleSkinScriptable : ScriptableObject
    {
        [SerializeField]
        private AnimationCurve skinPriceCurve;

        [Space]
        [FoldoutGroup("Full Override")]
        [FolderPath]
        public string skinsTextureFolderPath;
        [FoldoutGroup("Full Override")]
        [FolderPath]
        public string skinsUIFolderPath;

#if UNITY_EDITOR
        [FoldoutGroup("Full Override")]
        [Button]
        public void AutoFetchAssets()
        {
            ResetAll();
            FetchTextures();
            FetchUI();
            UpdateValues();
        }

        private void ResetAll()
        {
            skinDataList.Clear();
        }

        private void FetchUI()
        {
            if (skinsUIFolderPath.Trim().Length <= 0)
            {
                Debug.LogError("Must have folder path for ui skins to fetch");
                return;
            }

            var skinsDirPaths = Directory.GetDirectories(skinsUIFolderPath);

            skinDataCount = skinsDirPaths.Length;
            UpdateValues();

            skinsDirPaths.Sort();

            for (var i = 0; i < skinsDirPaths.Length; i++)
            {
                var skinDirPath = skinsDirPaths[i];
                var skinSpriteObjects = AssetUtilities.GetAllAssetsOfType(typeof(Sprite), skinDirPath);
                var skinSprites = skinSpriteObjects.Select(x => (Sprite)x);
                skinDataList[i].skinSprites = skinSprites.ToList();
            }
        }

        private void FetchTextures()
        {
            if (skinsTextureFolderPath.Trim().Length <= 0)
            {
                Debug.LogError("Must have folder path for skins to fetch");
                return;
            }

            var skinsDirPaths = Directory.GetDirectories(skinsTextureFolderPath);

            skinsDirPaths.Sort();

            skinDataCount = skinsDirPaths.Length;
            UpdateValues();

            for (var i = 0; i < skinsDirPaths.Length; i++)
            {
                var skinDirPath = skinsDirPaths[i];
                var skinTextureObjects = AssetUtilities.GetAllAssetsOfType(typeof(Texture), skinDirPath);
                var skinTextures = skinTextureObjects.Select(x => (Texture)x);
                skinDataList[i].skinTextures = skinTextures.ToList();
                skinDataList[i].skinName = skinDirPath.Split("_").Last();
            }
        }
#endif

        [Header("Skin Data")]
        [SerializeField]
        private int skinDataCount = 10;
        [SerializeField]
        private List<HoleSkinData> skinDataList = new();

        public void UpdateValues()
        {
            for (int i = skinDataList.Count; i < skinDataCount; i++)
            {
                skinDataList.Add(new HoleSkinData());
            }

            for (var i = 0; i < skinDataList.Count; i++)
            {
                var skinData = skinDataList[i];
                var remappedValue = MathX.ClampedRemap(0, skinDataList.Count,
                    0.0f, 1.0f,
                    i);

                var evaluatedValue = (int)skinPriceCurve.Evaluate(remappedValue);

                skinData.requiredCurrency = evaluatedValue;
            }
        }

        [CanBeNull]
        public HoleSkinRecord GetHoleSkinRecordById(string holeSkinId)
        {
            if (int.TryParse(holeSkinId, out int index))
            {
                return GetHoleSkinRecordByIndex(index);
            }
            else
            {
                Debug.LogError("Hole Skin Id Must be a valid number");
                return null;
            }
        }

        [CanBeNull]
        public HoleSkinRecord GetHoleSkinRecordByIndex(int index)
        {
            if (index < skinDataList.Count)
            {
                var skinData = skinDataList[index];
                var skinRecord = new HoleSkinRecord(skinData.skinTextures, skinData.skinSprites)
                {
                    skinName = skinData.skinName,
                    requiredCurrency = skinData.requiredCurrency,
                    skinIndex = index,
                };
                return skinRecord;
            }

            return null;
        }

        public int GetRecordCount()
        {
            return skinDataList.Count;
        }

        public record HoleSkinRecord
        {
            internal HoleSkinRecord(List<Texture> textures, List<Sprite> sprites)
            {
                skinTextures = textures;
                skinSprites = sprites;
            }

            public string skinName;
            public int skinIndex;
            public int requiredCurrency;

            private List<Texture> skinTextures;
            private List<Sprite> skinSprites;

            public List<Texture> GetTextureList()
            {
                return skinTextures;
            }

            public List<Sprite> GetSpriteList()
            {
                return skinSprites;
            }
        }

        [System.Serializable]
        private class HoleSkinData
        {
            public string skinName;
            public int requiredCurrency;
            public List<Texture> skinTextures;
            public List<Sprite> skinSprites;
        }
    }
}