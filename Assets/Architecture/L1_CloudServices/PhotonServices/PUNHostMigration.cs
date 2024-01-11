using System;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;

namespace CloudServices
{
    [InfoBox("This Script Exposes the Following Events\n" +
             "OnHostMigrated<Player>")]
    public class PUNHostMigration : MonoBehaviourPunCallbacks
    {
        private const string LogClassName = "PUNHostMigration";

        public static event Action<Player> OnHostMigrated;

        public static void SetMasterClient(Player newMasterClient)
        {
            DebugX.Log($"{LogClassName} : Switching Master Client to {newMasterClient.NickName}..", LogFilters.Network, null);
            if (PhotonNetwork.SetMasterClient(newMasterClient))
            {
                DebugX.Log($"{LogClassName} : Master Client Switched to {newMasterClient.NickName}", LogFilters.Network, null);
            }
            else
            {
                DebugX.LogError($"{LogClassName} : Error Switching Master Client to {newMasterClient.NickName}", LogFilters.Network, null);
            }
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            DebugX.Log($"{LogClassName} : Master Client Switched to {newMasterClient.NickName}", LogFilters.Network, gameObject);

            OnHostMigrated?.Invoke(newMasterClient);
        }
    }
}