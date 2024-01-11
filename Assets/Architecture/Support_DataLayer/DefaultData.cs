using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace DataLayer
{
    public class DefaultData : SingletonBehaviour<DefaultData>
    {
        public GameSettings gameSettings;
        public HoleSkinScriptable holeSkinScriptable;
        public HoleCameraScriptable holeCameraScriptable;
        public HoleGrowthScriptable holeGrowthScriptable;
        public HoleMovementScriptable holeMovementScriptable;
        public MapManagerDataScriptable mapManagerDataScriptable;
        public WeaponsScriptable weaponsScriptable;
        public ObstacleControlledFallDataScriptable obstacleControlledFallDataScriptable;

        [SerializeField] internal StringCollection tipsStringCollection;

        public List<string> GetTipsStringCollection()
        {
            return tipsStringCollection.List as List<string>;
        }

        public string GenerateRandomGuestName()
        {
            System.Random random = new System.Random();
            int id = random.Next(gameSettings.guestNameIdRangeMin, gameSettings.guestNameIdRangeMax);
            return $"Guest_{id}";
        }

        public string GenerateRandomUserId()
        {
            string guid = Guid.NewGuid().ToString();
            return guid;
        }

        public string GenerateRandomRoomId()
        {
            string roomId = "";
            if (gameSettings.randomizeDefaultRoomName)
            {
                roomId = Guid.NewGuid().ToString().Left(gameSettings.randomDefaultRoomDigits);
            }
            else
            {
                roomId = gameSettings.defaultRoomName;
            }
            roomId = gameSettings.roomPrefix + roomId;
            return roomId;
        }
    }
}