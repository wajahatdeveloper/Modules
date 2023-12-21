using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicRangeSections : MonoBehaviour
{
    public List<RangeSection> rangeSections = new();
}

[System.Serializable]
public class RangeSection
{
    public string rangeName;
    public float rangeValue;
    public Color rangeColor;
}