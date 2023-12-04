using System.Collections;
using System.Collections.Generic;
using DataLayer;
using UnityEngine;

namespace GameServices
{
    public class GlobalAccess : MonoBehaviour
    {
        private void OnEnable()
        {
            GlobalEvents.OnEnableRejoin += Handle_OnEnableRejoin;
            GlobalEvents.OnDisableRejoin += Handle_OnDisableRejoin;
        }

        private void OnDisable()
        {
            GlobalEvents.OnEnableRejoin -= Handle_OnEnableRejoin;
            GlobalEvents.OnDisableRejoin -= Handle_OnDisableRejoin;
        }

        private void Handle_OnEnableRejoin(string roomId)
        {
            DebugX.Log("Rejoining Enabled.",LogFilters.None,gameObject);
            LocalData.Instance.SetRejoinRoomId_Persistent(roomId);
            LocalData.Instance.SetIsRejoinAvailable_Persistent(true);
        }

        private void Handle_OnDisableRejoin()
        {
            DebugX.Log("Rejoining Disabled.",LogFilters.None,gameObject);
            LocalData.Instance.SetRejoinRoomId_Persistent("");
            LocalData.Instance.SetIsRejoinAvailable_Persistent(false);
        }
    }
}