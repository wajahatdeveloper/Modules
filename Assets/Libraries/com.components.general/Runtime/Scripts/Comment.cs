﻿// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

[AddComponentMenu("Toolkit/Comment")]
public class Comment : MonoBehaviour
{
    public string Message;
    public CommentType Type = CommentType.Info;

    private void Awake()
    {
        enabled = false;
    }

    public enum CommentType
    {
        Info,
        Warning
    }
}