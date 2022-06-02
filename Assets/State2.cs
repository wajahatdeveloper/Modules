using System.Collections;
using System.Collections.Generic;
using RobustFSM.Mono;
using UnityEngine;

public class State2 : MonoState
{
    public override void Execute()
    {
        base.Execute();
        
        (MyFSM.Instance.CurrentState as State2).StateBefore();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            MyFSM.Instance.ChangeState_NextFrame<State3>(true);
        }
        
        (MyFSM.Instance.CurrentState as State2).StateAfter();

    }

    public void StateBefore()
    {
        Debug.Log("State2 Before Update");
    }

    public void StateAfter()
    {
        Debug.Log("State2 After Update");

    }
}
