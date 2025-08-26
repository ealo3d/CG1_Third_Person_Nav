using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isRunning)
    {
        if (isRunning)
        {
            verticalMovement = 2;
        }

        animator.SetFloat(horizontal, horizontalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, verticalMovement, 0.1f, Time.deltaTime);
    }

    //New function to play any animation we want
    //Arguments: "targetAnimation" to receive the name of the animation
    //"isInteracting" to lock the animation and avoid to do something else like walking or running 
    public void PlayerTargetAnimation(string targetAnimation, bool isInteracting)
    {
        //set this parameter (create) in the animation depending on the value of isInteracting variable
        animator.SetBool("isInteracting", isInteracting);

        //Smooth transition, targetAnimation = the name of the animation we want to transition to
        //0.2f = the duration of the transition/blend
        animator.CrossFade(targetAnimation, 0.2f);
    }

}