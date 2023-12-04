using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFeatures
{
    public class ChatSystem : SingletonBehaviour<ChatSystem>
    {
        [Header("References")]
        public GameObject chatPanel;
        public GameObject chatSystemCanvas;

        [Header("Attributes")]
        public bool autoShowChatBoxOnInit;

        public void Init()
        {
            string userId = "testUser";
            string channelId = "testChannel";
            PUNChatClient chatClient = GetComponent<PUNChatClient>();
            chatClient.OnConnectionSuccess += Handle_OnConnectionSuccess;
            chatClient.Connect(userId, channelId);
        }

        private void Handle_OnConnectionSuccess()
        {
            chatSystemCanvas.SetActive(true);

            if (autoShowChatBoxOnInit)
            {
                OnClick_ToggleChat();
            }
        }

        public void OnClick_ToggleChat()
        {
            chatPanel.ReverseActiveState();
        }
    }
}