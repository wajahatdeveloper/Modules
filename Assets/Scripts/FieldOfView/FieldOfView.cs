using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 10f;
    [Range(0,360)] public float viewAngle = 60f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        var targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius,targetMask);
        foreach (Collider target in targetsInViewRadius)
        {
            if (visibleTargets.Contains(target.transform))
            {
                continue;
            }

            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2.0f)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                if (!Physics.Raycast(transform.position,dirToTarget,distanceToTarget,obstacleMask))
                {
                    visibleTargets.Add(target.transform);
                }
            }
        }
    }
}