using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(LookAtConstraint))]

public class LookAtAssigner : MonoBehaviour
{
    private void Awake()
    {
        Invoke(nameof(Assign), UnityEngine.Random.Range(0.1f, 0.5f));

    }

    private void Assign()
    {
        var sourceList = new List<ConstraintSource>();

        var camSource = new ConstraintSource
        {
            sourceTransform = GameObject.FindGameObjectWithTag("MainCamera").transform,
            weight = 1f
        };

        sourceList.Add(camSource);
        
        var newAssigner = GetComponent<LookAtConstraint>();
        
        newAssigner.SetSources(sourceList);
        newAssigner.constraintActive = true;

        Destroy(this);
    }

}