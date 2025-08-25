using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    public Vector2 movementInput;

    public float verticalInput; // to store the Y value from the vertor 2 movementInput
    public float horizontalInput; // to store the X value from the vertor 2 movementInput

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void HandleMovementInput() //function to handle inputs from player movements
    {
        verticalInput = movementInput.y; //get just the Y value from the vector 2 movementInput 
        horizontalInput = movementInput.x; //get just the X value from the vector 2 movementInput
    }

    public void HandleAllInputs()//function to handle all movements
    {
        HandleMovementInput();//To handle movement
        //HandleJumpingInput();//To manage Jumping
        //Handle any other function I want!

    }
}

