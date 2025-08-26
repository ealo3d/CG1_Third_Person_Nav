using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;

    AnimatorManager animatorManager;//to reference the script


    public Vector2 movementInput;
    private float moveAmount;//to store the movement amount
    public float verticalInput;
    public float horizontalInput;

    private void Awake()//create
    {
        animatorManager = GetComponent<AnimatorManager>();//Get the component
    }

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

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        //Clampts the ABSolute value of horizontal and vertical input to 0 - 1 range
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        //pass just the vertical movement (W,S keys)
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }
}

