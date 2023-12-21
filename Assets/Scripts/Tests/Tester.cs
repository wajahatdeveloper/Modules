using MonitorComponents;
using UnityEngine;

public class Tester : SingletonBehaviour<Tester>
{
    [AutoRef(AutoRefTargetType.Self)]
    public AudioSource source;

    private float val;

    private void Update()
    {
        val += Time.deltaTime;
    }
}