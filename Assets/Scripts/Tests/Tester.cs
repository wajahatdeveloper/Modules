using System.Collections;
using System.Numerics;
using Baracuda.Monitoring;
using Quartzified.EditorAttributes;
using UnityEngine;

public class Tester : SingletonBehaviour<Tester>
{
    private IEnumerator Start()
    {
        var cor = CoroutineX.Run(gameObject, Routine_Counter());
        yield return cor.WaitForComplete();
        cor.Rerun();
        yield return cor.WaitForComplete();
    }

    private IEnumerable Routine_Counter()
    {
        int i = 0;

        while (i < 2)
        {
            i++;
            print(i);
            yield return new WaitForSeconds(0.5f);
        }
    }
}