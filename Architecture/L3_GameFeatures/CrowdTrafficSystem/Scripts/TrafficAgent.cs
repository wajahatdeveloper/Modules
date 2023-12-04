using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

public class TrafficAgent : CommonAgentImpl
{
    public float moveSpeed = 2.0f;

    public List<GameObject> models = new();

    [ReadOnly] public TrafficPath path;
    [ReadOnly] public Transform point1;
    [ReadOnly] public Transform point2;

    private void Start()
    {
        var visualModel = Instantiate(models[Random.Range(0, models.Count)], transform);
        var meshRenderer = visualModel.GetComponent<MeshRenderer>();
        meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
        meshRenderer.receiveShadows = false;
    }
}