using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator; //variable to store the animator component
    int horizontal; //variable to store the id versión of the parameters on the animator
    int vertical; //variable to store the id versión of the parameters on the animator

    private void Awake()
    {
        animator = GetComponent<Animator>(); //get the animator component

        //StringToHash converts the String into an unique int called "hash" or "id" 
        //The "id" number is unique and is more efficient since is 
        //faster to compare integers that strings
        horizontal = Animator.StringToHash("Horizontal"); //store the resulted id into the variable
        vertical = Animator.StringToHash("Vertical"); //store the resulted id into the variable
    }

    //function to be called from the InputManager, receiving horizontalMovement and verticalMovement
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        //set the animator using the ("id", the value of the horizontal movement, the blend time, frame independant for blend time)
        animator.SetFloat(horizontal, horizontalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, verticalMovement, 0.1f, Time.deltaTime);
    }
}

