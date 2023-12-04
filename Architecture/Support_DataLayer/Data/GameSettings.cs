using Sirenix.OdinInspector;
using UnityEngine;

namespace DataLayer
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Custom/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [TitleGroup("RoomId")]
        public string defaultRoomName = "DefaultRoom";
        public string roomPrefix = "Room_";
        public bool randomizeDefaultRoomName = true;
        [Range(3,16)] public int randomDefaultRoomDigits = 6;

        [TitleGroup("PlayerId")]
        public int guestNameIdRangeMin = 11111;
        public int guestNameIdRangeMax = 99999;

        [TitleGroup("HighScore Mode")]
        public string mode1Name = "HighScore Mode";
        [Range(1,20)] public int minimumPlayersInHighScoreMode = 2;
        [Range(1,20)] public int maximumPlayersInHighScoreMode = 7;    // 20 CCU Max PUN2 Free Tier
        public int totalMatchTimeInHighScoreMode = 160;    // Two Minutes and Thirty Seconds

        [TitleGroup("BattleRoyale Mode")]
        public string mode2Name = "BattleRoyale";
        [InfoBox("Minimum Players in Mode2 Determined by Team Size * 2 at runtime")]
        [Range(1,20)] public int maximumPlayersInBattleRoyaleMode = 20;    // 20 CCU Max PUN2 Free Tier
        public int totalMatchTimeInBattleRoyaleMode = 1200;    // Twenty Minutes

        [TitleGroup("Rejoin Settings")]
        public RejoinSetting rejoinSetting = RejoinSetting.AutoRejoin;

        [TitleGroup("Leaderboard (InGame)")]
        [Range(1,25)]
        public int inGameLeaderboardEntities = 3;
    }

    public enum RejoinSetting
    {
        NoRejoin = 0,
        AutoRejoin = 1,
        ManualRejoin = 2,
    }
}