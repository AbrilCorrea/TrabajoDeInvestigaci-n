using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationsController : MonoBehaviour
{
    private Animator animator;

    public string animationToPlay;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator != null && !string.IsNullOrEmpty(animationToPlay))
        {
            animator.Play(animationToPlay);
        }
    }
}