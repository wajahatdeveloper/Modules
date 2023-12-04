using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameFeatures
{
    public class CannedChat : MonoBehaviour
    {
        public PUNChatClient chatClient;
        public GameObject cannedImagePreview;
        public Image cannedImage;
        public float previewTime = 2.0f;
        
        public List<SpecialMessageDTO> specialMessages = new();

        private void OnEnable()
        {
            chatClient.OnReceivedMessage += Handle_OnReceivedMessage;
        }

        private void OnDisable()
        {
            chatClient.OnReceivedMessage -= Handle_OnReceivedMessage;
        }

        public void OnClick_SendMessage(string message)
        {
            chatClient.OnSendCustomMessage(message);
        }

        private void Handle_OnReceivedMessage(string sender, string message)
        {
            var item = specialMessages.SingleOrDefault(x => x.message == message);
            if (item == null) { return; }

            cannedImagePreview.SetActive(true);
            cannedImage.sprite = item.sprite;
            this.Invoke(()=>cannedImagePreview.SetActive(false), previewTime);
        }

        [Serializable]
        public class SpecialMessageDTO
        {
            public string message;
            public Sprite sprite;
        }
    }
}