using System.Threading.Tasks;
using CloudServices;
using Photon.Realtime;

namespace DataLayer
{
    using HashTable = ExitGames.Client.Photon.Hashtable;

    /// <summary>
    /// This is the data that is stored on the network (PUN Room, PUN Players or Firebase Database)
    /// </summary>
    public partial class NetworkData : SingletonBehaviour<NetworkData>
    {
        private const string LogClassName = "NetworkData";

        public void SetTeamCount(int teamCount)
        {
            var roomProps = new ExitGames.Client.Photon.Hashtable { { "TeamCount" , teamCount } };
            PUNRoomHandler.SetRoomProperties(roomProps);
        }
        public int GetTeamCount()
        {
            return (int) PUNRoomHandler.GetRoomProperty("TeamCount" , 0);
        }

        public bool IsMatchConcluded()
        {
            return (bool) PUNRoomHandler.GetRoomProperty("IsConcluded" , false);
        }
        public void SetMatchConcluded(bool value)
        {
            var roomProps = new ExitGames.Client.Photon.Hashtable { { "IsConcluded" , value } };
            PUNRoomHandler.SetRoomProperties(roomProps);
        }

        public void SetTotalGameTime(int seconds)
        {
            var roomProps = new ExitGames.Client.Photon.Hashtable { { "GameTime" , seconds } };
            PUNRoomHandler.SetRoomProperties(roomProps);
        }
        public int GetTotalGameTime()
        {
            return (int) PUNRoomHandler.GetRoomProperty("GameTime" , DefaultData.Instance.gameSettings.totalMatchTimeInHighScoreMode);
        }

        public void SetPlayerUserId(string value)
        {
            LocalData.Instance.SetPlayerUserId_Persistent(value);
            NetworkData.Instance.UpdateToUserDatabaseEntryAsync("userId",value);
        }
        public async Task<string> GetPlayerUserIdAsync()
        {
            string playerUserId = (string) await NetworkData.Instance.FetchFromUserDatabaseEntryAsync("userId", "");
            LocalData.Instance.SetPlayerUserId_Persistent(playerUserId);
            return playerUserId;
        }

        public void SetIsEaten(bool isEaten)
        {
            var playerProps = new ExitGames.Client.Photon.Hashtable { { "IsEaten" , isEaten } };
            PUNRoomHandler.SetLocalPlayerProperties(playerProps);
        }
        public bool GetIsEaten()
        {
            return (bool) PUNRoomHandler.GetLocalPlayerProperty("IsEaten" , false);
        }

        public void SetTeamNumber(int teamNumber, Player player = null)
        {
            var playerProps = new ExitGames.Client.Photon.Hashtable { { "TeamNumber" , teamNumber } };

            if (player == null)
            {
                PUNRoomHandler.SetLocalPlayerProperties(playerProps);
            }
            else
            {
                PUNRoomHandler.SetPlayerProperties(player, playerProps);
            }
        }
        public int GetTeamNumber(Player player = null)
        {
            if (player == null)
            {
                return (int) PUNRoomHandler.GetLocalPlayerProperty("TeamNumber" , 0);
            }
            else
            {
                return (int) PUNRoomHandler.GetPlayerProperty(player ,"TeamNumber" , 0);
            }
        }

        public void SetHoleSkinId(string holeSkinId)
        {
            PUNRoomHandler.SetLocalPlayerProperties(new ExitGames.Client.Photon.Hashtable(){ {"HoleSkinId",holeSkinId} });
        }
        public string GetHoleSkinId(Player player = null)
        {
            if (player == null)
            {
                return PUNRoomHandler.GetLocalPlayerProperty("HoleSkinId" , "0").ToString();
            }
            else
            {
                return PUNRoomHandler.GetPlayerProperty(player ,"HoleSkinId" , "0").ToString();
            }
        }
    }
}