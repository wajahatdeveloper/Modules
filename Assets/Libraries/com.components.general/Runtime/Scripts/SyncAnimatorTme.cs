using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncAnimatorTme : MonoBehaviour
{
    public Animator sourceAnimator;
    public int srcLayerIndex;
    public string srcStateName;

    public int layerIndex;
    public string stateName;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (sourceAnimator.GetCurrentAnimatorStateInfo(srcLayerIndex).IsName(srcStateName))
        {
            if (animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName))
            {
                animator.Play(stateName,layerIndex, sourceAnimator.GetCurrentAnimatorStateInfo(srcLayerIndex).normalizedTime);
            }
        }
    }
}