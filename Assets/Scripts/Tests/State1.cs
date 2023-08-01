using System.Collections;
using System.Collections.Generic;
using RobustFSM.Mono;
using UnityEngine;

public class State1 : MonoState
{
    public override void Execute()
    {
        base.Execute();
        
        Debug.Log("State1");

        if (Input.GetKeyDown(KeyCode.Return))
        {
            MyFSM.Instance.ChangeState_NextFrame<State2>(true);
        }
    }
}
