using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[ExecuteInEditMode]
public class CommonPathImpl : MonoBehaviour
{
    public bool closeLoop = false;
    [Range(1, 32)] public int maxAgentCount;
    public int currAgentCount;
    public List<Transform> pathPoints = new();

    public Transform GetNextPoint(Transform point)
    {
        int index = pathPoints.FindIndex(x => x == point);

        index++;
        if (index >= pathPoints.Count)
        {
            index -= pathPoints.Count;
        }

        return pathPoints[index];
    }

    [Button]
    public void FetchPoints()
    {
        pathPoints.Clear();
        foreach (Transform child in transform)
        {
            pathPoints.Add(child);
        }
    }

    private void OnTransformChildrenChanged()
    {
        FetchPoints();
    }

    private void OnDrawGizmos()
    {
        var prevPoint = pathPoints.FirstOrDefault();
        if (prevPoint is null) { return; }

        Gizmos.color = Color.blue;
        foreach (Transform currPoint in pathPoints)
        {
            Gizmos.DrawSphere(currPoint.position, 0.12f);
        }

        Gizmos.color = Color.yellow;
        foreach (Transform currPoint in pathPoints)
        {
            Gizmos.DrawLine(prevPoint.position, currPoint.position);
            prevPoint = currPoint;
        }

        if (closeLoop)
        {
            Gizmos.DrawLine(prevPoint.position, pathPoints.First().position);
        }
    }

    // Get a random position along the path
    public Vector3 GetRandomPathPosition()
    {
        if (pathPoints.Count < 2)
        {
            Debug.LogError("Path should have at least two points.");
            return Vector3.zero;
        }

        // Get a random index between 0 and the number of points - 2
        int index = Random.Range(0, pathPoints.Count - 1);

        // Get the two adjacent points
        Transform point1 = pathPoints[index];
        Transform point2 = pathPoints[index + 1];

        // Get a random interpolation value between 0 and 1
        float t = Random.Range(0f, 1f);

        // Interpolate between the two points
        Vector3 randomPosition = Vector3.Lerp(point1.position, point2.position, t);

        return randomPosition;
    }

    // Get a random position along the path with direction
    public Vector3 GetRandomPathPositionWithDirection(out Vector3 lookAlongDirection,out Transform point1, out Transform point2)
    {
        lookAlongDirection = Vector3.zero;
        point1 = null;
        point2 = null;

        if (pathPoints.Count < 2)
        {
            Debug.LogError("Path should have at least two points.");
            return Vector3.zero;
        }

        // Get a random index between 0 and the number of points - 2
        int index = Random.Range(0, pathPoints.Count - 1);

        // Get the two adjacent points
        point1 = pathPoints[index];
        point2 = pathPoints[index + 1];

        // Get a random interpolation value between 0 and 1
        float t = Random.Range(0f, 1f);

        // Interpolate between the two points
        Vector3 randomPosition = Vector3.Lerp(point1.position, point2.position, t);

        lookAlongDirection = randomPosition + (point2.position - point1.position);

        return randomPosition;
    }
}