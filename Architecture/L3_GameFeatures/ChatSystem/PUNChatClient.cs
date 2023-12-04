using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AuthenticationValues = Photon.Chat.AuthenticationValues;

namespace GameFeatures
{
    public class PUNChatClient : MonoBehaviour , IChatClientListener
    {
        private const string LogClassName = "PUNChatClient";

        [Header("References")]
        public TMP_InputField InputFieldChat;   // set in inspector
        public TMP_Text CurrentChannelText;     // set in inspector

        public event Action OnConnectionSuccess;
        public event Action<string, string> OnReceivedMessage;

        public string UserName { get; set; }
        public string ChannelId { get; set; }

        public ChatClient chatClient;
        protected internal ChatAppSettings chatAppSettings;

        private void OnEnable()
        {
            this.chatAppSettings = GetChatSettings(PhotonNetwork.PhotonServerSettings.AppSettings);
        }

        public void Connect(string userId, string channelId)
        {
            UserName = userId;
            ChannelId = channelId;

            chatClient = new ChatClient(this)
            {
                UseBackgroundWorkerForSending = true,
                AuthValues = new AuthenticationValues(UserName)
            };

            chatClient.ConnectUsingSettings(chatAppSettings);

            DebugX.Log($"{LogClassName} : Connecting as {UserName}.", LogFilters.Network, gameObject);
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            switch (level)
            {
                case ExitGames.Client.Photon.DebugLevel.ERROR:
                    Debug.LogError(message);
                    break;
                case ExitGames.Client.Photon.DebugLevel.WARNING:
                    Debug.LogWarning(message);
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }

        public void OnDisconnected()
        {
            DebugX.Log($"{LogClassName} : Disconnected.", LogFilters.Network, gameObject);
        }

        public void OnConnected()
        {
            chatClient.Subscribe(ChannelId);
            chatClient.SetOnlineStatus(ChatUserStatus.Online);
            OnConnectionSuccess.InvokeSafe();
        }

        public void OnChatStateChange(ChatState state)
        {
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            ChatChannel channel = null;
            bool found = this.chatClient.TryGetChannel(channelName, out channel);
            if (!found)
            {
                DebugX.LogError($"{LogClassName} : Failed to find channel {channelName}.", LogFilters.Network, gameObject);
                return;
            }

            for (var i = 0; i < senders.Length; i++)
            {
                var sender = senders[i];
                var message = messages[i].ToString();
                OnReceivedMessage.InvokeSafe(sender,message);
            }

            CurrentChannelText.text = channel.ToStringMessages();
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
        }

        public void OnUnsubscribed(string[] channels)
        {
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
        }

        public void OnUserSubscribed(string channel, string user)
        {
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
        }

        public void OnEnterSend()
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            {
                this.SendChatMessage(this.InputFieldChat.text);
                this.InputFieldChat.text = "";
            }
        }

        public void OnClickSend()
        {
            if (this.InputFieldChat != null)
            {
                this.SendChatMessage(this.InputFieldChat.text);
                this.InputFieldChat.text = "";
            }
        }

        public void OnSendCustomMessage(string message)
        {
            if (message.IsNullOrEmpty()) { return; }
            this.SendChatMessage(message);
        }

        public void Update()
        {
            if (this.chatClient != null)
            {
                this.chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
            }
        }

        public void OnDestroy()
        {
            if (this.chatClient != null)
            {
                this.chatClient.Disconnect();
            }
        }

        public void OnApplicationQuit()
        {
            if (this.chatClient != null)
            {
                this.chatClient.Disconnect();
            }
        }

        private void SendChatMessage(string inputLine)
        {
            if (string.IsNullOrEmpty(inputLine)) { return; }
            chatClient.PublishMessage(ChannelId, inputLine);
        }

        private ChatAppSettings GetChatSettings(AppSettings appSettings)
        {
            return new ChatAppSettings
            {
                AppIdChat = appSettings.AppIdChat,
                AppVersion = appSettings.AppVersion,
                FixedRegion = appSettings.IsBestRegion ? null : appSettings.FixedRegion,
                NetworkLogging = appSettings.NetworkLogging,
                Protocol = appSettings.Protocol,
                EnableProtocolFallback = appSettings.EnableProtocolFallback,
                Server = appSettings.IsDefaultNameServer ? null : appSettings.Server,
                Port = (ushort)appSettings.Port,
                ProxyServer = appSettings.ProxyServer
                // values not copied from AppSettings class: AuthMode
                // values not needed from AppSettings class: EnableLobbyStatistics
            };
        }
    }
}