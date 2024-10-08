﻿// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Comment))]
    public class CommentEditor : UnityEditor.Editor
    {
        private static GUIContent[] typeOptions;

        private bool isEditing;
        private string message;
        private int selectedTypeIndex;

        private void OnEnable()
        {
            Comment comment = (Comment) target;

            typeOptions = new[]
            {
                EditorGUIUtility.TrTextContentWithIcon("Info", "console.infoicon"),
                EditorGUIUtility.TrTextContentWithIcon("Warning", "console.warnicon")
            };

            message = comment.Message;
            selectedTypeIndex = typeOptions
                .Select(x => x.text)
                .ToList()
                .IndexOf(comment.Type.ToString());
        }

        public override void OnInspectorGUI()
        {
            Comment comment = (Comment) target;

            if (!isEditing)
            {
                if (GUILayout.Button("Edit"))
                    isEditing = !isEditing;

                MessageType messageType;
                switch (comment.Type)
                {
                    case Comment.CommentType.Info:
                        messageType = MessageType.Info;
                        break;
                    case Comment.CommentType.Warning:
                        messageType = MessageType.Warning;
                        break;
                    default:
                        messageType = MessageType.None;
                        break;
                }

                if (!string.IsNullOrEmpty(message))
                    EditorGUILayout.HelpBox(comment.Message, messageType);

                comment.Message = message;
                comment.Type = Enum<Comment.CommentType>.Parse(typeOptions[selectedTypeIndex].text);
            }
            else
            {
                if (GUILayout.Button("Save"))
                    isEditing = !isEditing;

                GUI.SetNextControlName("TextArea");
                message = EditorGUILayout.TextArea(message);
                GUI.FocusControl("TextArea");

                TextAnchor tempPopupAlignment = EditorStyles.popup.alignment;
                EditorStyles.popup.alignment = TextAnchor.MiddleCenter;

                selectedTypeIndex = EditorGUILayout.Popup(selectedTypeIndex,
                    typeOptions, GUILayout.Height(20));

                EditorStyles.popup.alignment = tempPopupAlignment;
            }
        }
    }