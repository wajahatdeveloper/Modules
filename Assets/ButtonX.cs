using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonX : Button
{
    [ShowDrawerChain]
    [SceneObjectsOnly]
    public GameObject target;
}
