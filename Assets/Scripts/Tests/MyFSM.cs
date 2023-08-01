using System;
using System.Collections;
using System.Collections.Generic;
using RobustFSM.Mono;
using UnityEngine;

public class MyFSM : MonoSingletonFSM<MyFSM>
{
    private void OnEnable()
    {
        AddMonoState<State1>();
        AddMonoState<State2>();
        AddMonoState<State3>();
        
        SetInitialMonoState<State1>();
    }
}
