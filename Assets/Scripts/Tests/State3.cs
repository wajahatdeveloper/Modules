using System.Collections;
using System.Collections.Generic;
using RobustFSM.Mono;
using UnityEngine;

public class State3 : MonoState
{
    public override void Execute()
    {
        base.Execute();
    
        Debug.Log("State3");

        if (Input.GetKeyDown(KeyCode.Return))
        {
            MyFSM.Instance.ChangeState_NextFrame<State1>(true);
        }
    }
}
