using MonitorComponents;
using UnityEngine;

public class Tester : SingletonBehaviour<Tester>
{
    private float val;

    private void Update()
    {
        val += Time.deltaTime;
    }
}