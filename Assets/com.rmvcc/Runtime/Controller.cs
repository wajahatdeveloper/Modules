using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller<M,V> : MonoBehaviour
    where M : Model
    where V : View
{
    public V view;
    public M model;
}