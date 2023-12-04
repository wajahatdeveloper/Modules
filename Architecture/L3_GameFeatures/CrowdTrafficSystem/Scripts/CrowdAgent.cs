using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CrowdAgent : CommonAgentImpl
{
    public float moveSpeed = 2.0f;

    [ReadOnly] public CrowdPath path;
    [ReadOnly] public Transform point1;
    [ReadOnly] public Transform point2;
}