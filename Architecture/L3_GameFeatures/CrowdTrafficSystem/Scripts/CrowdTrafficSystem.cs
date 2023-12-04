using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class CrowdTrafficSystem : SingletonBehaviour<CrowdTrafficSystem>
{
    [AssetsOnly] public GameObject crowdAgentPrefab;
    [AssetsOnly] public GameObject trafficAgentPrefab;

    [Space]

    [SceneObjectsOnly] public Transform crowdContainerTransform;
    [SceneObjectsOnly] public Transform trafficContainerTransform;

    [Space]

    [ReadOnly] public List<CrowdAgent> crowdAgents = new();
    [ReadOnly] public List<TrafficAgent> trafficAgents = new();
    [ReadOnly] public List<CrowdPath> crowdPaths = new();
    [ReadOnly] public List<TrafficPath> trafficPaths = new();

    [Space]

    public int crowdAgentCount;
    public int trafficAgentCount;
    public bool initializeOnEnable = false;
    public float nodeRadius = 1.0f;

    private void OnEnable()
    {
        if (initializeOnEnable)
        {
            StartCoroutine(Initialize());
        }
    }

    private void OnDrawGizmos()
    {
        if (crowdAgents.Count <= 0) { return; }
        Gizmos.color = Color.magenta;
        foreach (CrowdAgent crowdAgent in crowdAgents)
        {
            Gizmos.DrawCube(crowdAgent.transform.position, Vector3.one * 0.12f);
        }

        if (trafficAgents.Count <= 0) { return; }
        Gizmos.color = Color.green;
        foreach (TrafficAgent trafficAgent in trafficAgents)
        {
            Gizmos.DrawCube(trafficAgent.transform.position, Vector3.one * 0.12f);
        }
    }

    public IEnumerator Initialize()
    {
        // find and populate all paths from the loaded map
        crowdPaths = GameObject.FindObjectsOfType<CrowdPath>().ToList();
        trafficPaths = GameObject.FindObjectsOfType<TrafficPath>().ToList();

        // spawn agents and add them to their respective lists
        for (int i = 0; i < crowdAgentCount; i++)
        {
            var crowdAgent = Instantiate(crowdAgentPrefab, crowdContainerTransform).GetComponent<CrowdAgent>();
            crowdAgents.Add(crowdAgent);
        }
        for (int i = 0; i < trafficAgentCount; i++)
        {
            var trafficAgent = Instantiate(trafficAgentPrefab, trafficContainerTransform).GetComponent<TrafficAgent>();
            trafficAgents.Add(trafficAgent);
        }

        yield return null;

        Handle_OnGameStart();
    }

    private void Handle_OnGameStart()
    {
        int crowdAgentsAssigned = 0;
        int totalCrowdAgentsToAssign = crowdAgents.Count;

        // assign agents to paths sequentially
        foreach (var crowdPath in crowdPaths)
        {
            while (crowdPath.currAgentCount < crowdPath.maxAgentCount &&
                   crowdAgentsAssigned < totalCrowdAgentsToAssign)
            {
                crowdAgents[crowdAgentsAssigned].path = crowdPath;
                crowdAgentsAssigned++;
            }
        }

        int trafficAgentsAssigned = 0;
        int totalTrafficAgentsToAssign = trafficAgents.Count;

        // assign agents to paths sequentially
        foreach (var trafficPath in trafficPaths)
        {
            while (trafficPath.currAgentCount < trafficPath.maxAgentCount &&
                   trafficAgentsAssigned < totalTrafficAgentsToAssign)
            {
                trafficAgents[trafficAgentsAssigned].path = trafficPath;
                trafficAgentsAssigned++;
            }
        }

        // put all agents at random locations along their paths incrementally
        foreach (CrowdAgent crowdAgent in crowdAgents)
        {
            var path = crowdAgent.path;
            if (path == null) { continue; }

            var position = path.GetRandomPathPositionWithDirection(
                out Vector3 lookAlongDirection,
                out Transform point1,
                out Transform point2);

            crowdAgent.point1 = point1;
            crowdAgent.point2 = point2;
            crowdAgent.transform.position = position;
            crowdAgent.transform.LookAt(lookAlongDirection);
        }

        // put all agents at random locations along their paths incrementally
        foreach (TrafficAgent trafficAgent in trafficAgents)
        {
            var path = trafficAgent.path;
            if (path == null) { continue; }

            var position = path.GetRandomPathPositionWithDirection(
                out Vector3 lookAlongDirection,
                out Transform point1,
                out Transform point2);

            trafficAgent.point1 = point1;
            trafficAgent.point2 = point2;
            trafficAgent.transform.position = position;
            trafficAgent.transform.LookAt(lookAlongDirection);
        }
    }

    private void Update()
    {
        // put all agents at random locations along their paths incrementally
        foreach (CrowdAgent crowdAgent in crowdAgents)
        {
            var targetPoint = crowdAgent.point2;
            if (targetPoint == null) { continue; }

            crowdAgent.transform.LookAt(targetPoint);
            crowdAgent.transform.position += crowdAgent.transform.forward * crowdAgent.moveSpeed * Time.deltaTime;

            if (Vector3.Distance(crowdAgent.transform.position, targetPoint.position) < nodeRadius)
            {
                var nextPoint = crowdAgent.path.GetNextPoint(targetPoint);
                crowdAgent.point1 = crowdAgent.point2;
                crowdAgent.point2 = nextPoint;
            }
        }

        // put all agents at random locations along their paths incrementally
        foreach (TrafficAgent trafficAgent in trafficAgents)
        {
            var targetPoint = trafficAgent.point2;
            if (targetPoint == null) { continue; }

            trafficAgent.transform.LookAt(targetPoint);
            trafficAgent.transform.position += trafficAgent.transform.forward * trafficAgent.moveSpeed * Time.deltaTime;

            if (Vector3.Distance(trafficAgent.transform.position, targetPoint.position) < nodeRadius)
            {
                var nextPoint = trafficAgent.path.GetNextPoint(targetPoint);
                trafficAgent.point1 = trafficAgent.point2;
                trafficAgent.point2 = nextPoint;
            }
        }
    }
}