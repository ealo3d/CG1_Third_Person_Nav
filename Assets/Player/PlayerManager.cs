using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;//to store the animator component
    InputManager inputManager;
    PlayerMovement playerMovement;

    public bool isInteracting; //bool to get the status from the animator

    private void Awake()
    {
        animator = GetComponent<Animator>();//get the component
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }

    private void LateUpdate()//code here is executed after the update()
    {
        //check every frame in the animator the status of "isInteracting" and update isInteracting bool here
        isInteracting = animator.GetBool("isInteracting");
    }
}

