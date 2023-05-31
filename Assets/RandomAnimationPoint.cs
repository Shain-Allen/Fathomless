using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationPoint : MonoBehaviour
{
    public Animator animator;
    public AnimationClip animationClip;

    private void Start()
    {
        if (animator != null && animationClip != null)
        {
            float randomTime = Random.Range(0f, animationClip.length);
            animator.Play(animationClip.name, -1, randomTime);
        }
    }
}
