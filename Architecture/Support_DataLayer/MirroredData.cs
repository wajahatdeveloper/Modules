using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;

namespace DataLayer
{
    /// <summary>
    /// This is the data that is mirrored locally and on the network (Database)
    /// </summary>
    public class MirroredData : SingletonBehaviour<MirroredData>
    {
        private const string LogClassName = "MirroredData";

        [ShowInInspector, ReadOnly] private bool isInMatch = false;
        [ShowInInspector, ReadOnly] private string playerNickName = "";

        public bool GetIsInMatch() => isInMatch;
        public void SetIsInMatch(bool value)
        {
            isInMatch = value;
            UserDataHandler.Instance.SendToDatabaseAsync("IsInMatch",value);
        }
        public async Task<bool> GetIsInMatchRealtimeAsync()
        {
            var val = await UserDataHandler.Instance.FetchFromDatabase("IsInMatch", false);
            return bool.Parse(val.ToString());
        }

        public string GetPlayerNickName()
        {
            if (playerNickName == "")
            {
                playerNickName = DefaultData.Instance.GenerateRandomGuestName();
            }
            return playerNickName;
        }
        public void SetPlayerNickName(string nickName)
        {
            playerNickName = nickName;
            UserDataHandler.Instance.SendToDatabaseAsync("NickName",nickName);
        }
    }
}