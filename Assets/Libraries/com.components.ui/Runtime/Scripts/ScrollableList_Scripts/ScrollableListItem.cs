﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollableListItem : MonoBehaviour
{
    public Text title;
    public Text subtitle;
    public Image image;
    public Button button;

    public virtual void DoUpdate()
    {
    }
}