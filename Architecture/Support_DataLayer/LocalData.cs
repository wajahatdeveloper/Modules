using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DataLayer
{
    /// <summary>
    /// This is the data that is stored only locally
    /// </summary>
    public class LocalData : SingletonBehaviour<LocalData>
    {
        private const string LogClassName = "LocalData";

        [ReadOnly] public string CurrentRoomName;

        [ReadOnly] public string PrivateRoomPassKey;
        [ReadOnly] public bool IsPrivateLobbyUsed;

        [ReadOnly] public bool IsPlayerRejoining = false;

        [ShowInInspector, ReadOnly] public int staged_inGameTime;     // temp variable do not use generally

        [ShowInInspector, ReadOnly] private int killCount = 0;
        [ShowInInspector, ReadOnly] private int maxPlayerCount = 20;
        [ShowInInspector, ReadOnly] private int minPlayerCount = 2;
        [ShowInInspector, ReadOnly] private int inGameScore = 0;

        #region WeaponInventory

        public void AddWeapon_Persistent(WeaponType weaponType, int amount)
        {
            var key = weaponType.ToString();
            if (PersistentDataHandler.ContainsKey(key))
            {
                int value = PersistentDataHandler.GetData<int>(key, 0);
                value += amount;
                PersistentDataHandler.SetData<int>(key, value);
            }
            else
            {
                PersistentDataHandler.SetData<int>(key, 1);
            }
        }
        public void RemoveWeapon_Persistent(WeaponType weaponType, int amount)
        {
            var key = weaponType.ToString();
            if (PersistentDataHandler.ContainsKey(key))
            {
                int value = PersistentDataHandler.GetData<int>(key, 0);
                value -= amount;
                PersistentDataHandler.SetData<int>(key, Mathf.Max(0, value));
            }
        }
        public int GetWeaponCount(WeaponType weaponType)
        {
            return PersistentDataHandler.GetData<int>(weaponType.ToString(), 0);
        }

        #endregion

        public int GetSelectedTeamSize_Persistent() => PersistentDataHandler.GetData<int>("SelectedTeamSize", 2);
        public void SetSelectedSelectedTeamSize_Persistent(int value) => PersistentDataHandler.SetData<int>("SelectedTeamSize", value);

        public string GetSelectedModeName_Persistent() => PersistentDataHandler.GetData<string>("SelectedModeName", "Default");
        public void SetSelectedModeName_Persistent(string value) => PersistentDataHandler.SetData<string>("SelectedModeName", value);

        public string GetSelectedMapName_Persistent() => PersistentDataHandler.GetData<string>("SelectedMapName", "Default");
        public void SetSelectedMapName_Persistent(string value) => PersistentDataHandler.SetData<string>("SelectedMapName", value);


        public string GetSelectedHoleSkinId_Persistent() => PersistentDataHandler.GetData<string>("SelectedHoleSkinId", "0");
        public void SetSelectedHoleSkinId_Persistent(string value) => PersistentDataHandler.SetData<string>("SelectedHoleSkinId", value);

        public int GetInGameScore() => inGameScore;
        public void SetInGameScore(int value) => this.inGameScore = value;

        public string GetPlayerUserId_Persistent()
        {
            return PersistentDataHandler.GetData<string>("PlayerUserId", "");
        }
        public void SetPlayerUserId_Persistent(string value) => PersistentDataHandler.SetData<string>("PlayerUserId", value);

        public bool GetIsRejoinAvailable_Persistent() => PersistentDataHandler.GetData<bool>("IsRejoinAvailable", false);
        public void SetIsRejoinAvailable_Persistent(bool value) => PersistentDataHandler.SetData<bool>("IsRejoinAvailable", value);

        public string GetRejoinRoomId_Persistent() => PersistentDataHandler.GetData<string>("RejoinRoomId", "");
        public void SetRejoinRoomId_Persistent(string value) => PersistentDataHandler.SetData<string>("RejoinRoomId", value);

        public int GetMaxPlayerCount()
        {
            var gameSettings = DefaultData.Instance.gameSettings;

            if (IsPrivateLobbyUsed)
            {
                return maxPlayerCount;
            }

            if (GetSelectedModeName_Persistent() == gameSettings.mode1Name)
            {
                return gameSettings.maximumPlayersInHighScoreMode;
            }
            else
            {
                return gameSettings.maximumPlayersInBattleRoyaleMode;
            }
        }
        public void SetMaxPlayerCount(int value) => this.maxPlayerCount = value;

        public int GetMinPlayerCount()
        {
            var gameSettings = DefaultData.Instance.gameSettings;

            if (IsPrivateLobbyUsed)
            {
                return minPlayerCount;
            }

            if (GetSelectedModeName_Persistent() == gameSettings.mode1Name)
            {
                return gameSettings.minimumPlayersInHighScoreMode;
            }
            else
            {
                return GetSelectedTeamSize_Persistent() * 2;    // minimum two teams should be present
            }
        }
        public void SetMinPlayerCount(int value) => this.minPlayerCount = value;

        public bool IsBattleRoyaleMode()
        {
            return GetSelectedModeName_Persistent() == DefaultData.Instance.gameSettings.mode2Name;
        }

        #region HoleKillCount

        private event Action<int> killCountUpdateAction;

        public void SetKillCount(int value)
        {
            this.killCount = value;
            //* NetworkData.Instance.SendToDatabaseAsync("KillCount",value);
            killCountUpdateAction?.Invoke(value);
        }

        public int GetKillCount()
        {
            return killCount;
        }

        public void SubscribeToKillCountUpdates(Action<int> handler)
        {
            killCountUpdateAction += handler;
        }

        public void UnsubscribeToKillCountUpdates(Action<int> handler)
        {
            killCountUpdateAction -= handler;
        }

        #endregion
    }
}