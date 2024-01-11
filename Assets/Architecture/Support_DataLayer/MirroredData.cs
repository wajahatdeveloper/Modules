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
        [ShowInInspector, ReadOnly] private string countryCode = "UN";

        public string GetCountryCode() => countryCode;
        public void SetCountryCode(string value)
        {
            countryCode = value;
            NetworkData.Instance.SendUserCountryCodeToDatabaseAsync(LocalData.Instance.GetPlayerUserId_Persistent(), value);
        }

        public bool GetIsInMatch() => isInMatch;
        public void SetIsInMatch(bool value)
        {
            isInMatch = value;
            NetworkData.Instance.UpdateToUserDatabaseEntryAsync("IsInMatch",value);
        }
        public async Task<bool> GetIsInMatchRealtimeAsync()
        {
            var val = await NetworkData.Instance.FetchFromUserDatabaseEntryAsync("IsInMatch", false);
            return bool.Parse(val.ToString());
        }

        public string GetPlayerNickName()
        {
            return playerNickName;
        }
        public void SetPlayerNickName(string nickName = "")
        {
            playerNickName = nickName == "" ? DefaultData.Instance.GenerateRandomGuestName() : nickName;
            NetworkData.Instance.UpdateToUserDatabaseEntryAsync("NickName",nickName);
        }
    }
}