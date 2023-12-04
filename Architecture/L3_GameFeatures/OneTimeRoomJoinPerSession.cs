using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace GameFeatures
{
    public class OneTimeRoomJoinPerSession : SingletonBehaviour<OneTimeRoomJoinPerSession>
    {
        private static StringCollection _RoomsJoinedThisSession = new();

        public bool IsRoomJoinedThisSession(string roomId)
        {
            return _RoomsJoinedThisSession.Contains(roomId);
        }

        public void AddRoomJoinedThisSession(string roomId)
        {
            _RoomsJoinedThisSession.Add(roomId);
        }
    }
}