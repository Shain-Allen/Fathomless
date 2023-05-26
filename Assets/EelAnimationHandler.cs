using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelAnimationHandler : MonoBehaviour
{
    Animator animator;
    public LargeEnemyBehavior Eel;
    void Start()
    {
        animator = GetComponent<Animator>();   
    }


    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("UpAndDown") && Eel.CurrentDelayTime < Eel.InitialAttackdelay / 2)
        {
            animator.SetTrigger("AnimShift");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SideToSide") && Eel.CurrentDelayTime < Eel.InitialAttackdelay / 4)
        {
            animator.SetTrigger("AnimShift");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SideToSideMouthOpen") && (Eel.currentState == LargeEnemyBehavior.State.Attack))
        {
            animator.SetTrigger("AnimShift");
        }
    }
}
