using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewVisualizer : MonoBehaviour
{
    [Header("Refs")]
    public FieldOfView fieldOfView;

    [Header("Mesh")]
    public MeshFilter viewMeshFilter;
    public float meshResolution = 1.0f;
    public bool showDebugRays;

    [Header("Edge")]
    public float edgeResolveIterations = 4f;
    public float edgeDistanceThreshold = 0.5f;

    private Mesh viewMesh;

    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    private void DrawFieldOfView()
    {
        int rayCount = Mathf.RoundToInt(fieldOfView.viewAngle * meshResolution);
        float rayAngleSize = fieldOfView.viewAngle / rayCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i < rayCount; i++)
        {
            float angle = transform.eulerAngles.y - fieldOfView.viewAngle / 2.0f + rayAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDistanceThresholdExceeded =
                    Mathf.Abs(oldViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;

                if (oldViewCast.isHit != newViewCast.isHit || (oldViewCast.isHit && newViewCast.isHit && edgeDistanceThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero) { viewPoints.Add(edge.pointA); }
                    if (edge.pointB != Vector3.zero) { viewPoints.Add(edge.pointB); }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;

            if (showDebugRays)
            {
                Debug.DrawLine(transform.position,transform.position + fieldOfView.DirFromAngle(angle,true) * fieldOfView.viewRadius, Color.red);
            }
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = fieldOfView.DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, fieldOfView.viewRadius, fieldOfView.obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + direction * fieldOfView.viewRadius,
                fieldOfView.viewRadius, globalAngle);
        }
    }

    private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2.0f;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDistanceThresholdExceeded =
                Mathf.Abs(minViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;

            if (newViewCast.isHit == minViewCast.isHit && !edgeDistanceThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    public struct ViewCastInfo
    {
        public bool isHit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo(bool isHit, Vector3 point, float distance, float angle)
        {
            this.isHit = isHit;
            this.point = point;
            this.distance = distance;
            this.angle = angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 pointA, Vector3 pointB)
        {
            this.pointA = pointA;
            this.pointB = pointB;
        }
    }
}