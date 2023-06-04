using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EelAnimationHandler : MonoBehaviour
{
    Animator animator;
    public LargeEnemyBehavior Eel;
    void Start()
    {
        animator = GetComponent<Animator>();
        Eel.CurrentDelayTime = Eel.InitialAttackdelay;
    }


    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("UpAndDown") && Eel.CurrentDelayTime < Eel.InitialAttackdelay / 2)
        {
            animator.SetTrigger("AnimShift1");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SideToSide") && Eel.CurrentDelayTime < Eel.InitialAttackdelay / 4)
        {
            animator.SetTrigger("AnimShift2");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SideToSideMouthOpen") && (Eel.currentState == LargeEnemyBehavior.State.Attack))
        {
            animator.SetTrigger("AnimShift3");
        }
        //Added the ability to slow time down to 10% for debugging.
        //if (Input.GetKey(KeyCode.X))
        //{
        //    Time.timeScale = 0.1f;
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //}
    }
}
