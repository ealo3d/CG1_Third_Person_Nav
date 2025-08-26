using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerManager playerManager;

    AnimatorManager animatorManager;//reference to the script

    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    public float inAirTimer;//variable to build up speed in the falling
    public float leapingVelocity;//to specify the amount of leap before the falling
    public float fallingVelocity;//to control the falling velocity
    public float rayCastHeightOffset = 0.5f;//to set the offset of the raycast from the ground
    public LayerMask groundLayer;//create a Layer selector for the ground check
    public float maxDistance = 1;//to control the max Distance to check the ground and cast the sphere

    public float walkingSpeed = 2.5f;
    public float runningSpeed = 7;
    public float rotationSpeed = 15;

    public bool isRunning;
    public bool isGrounded;//To check if the player is in the ground or not


    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();

        animatorManager = GetComponent<AnimatorManager>();//get the component
        isGrounded = true;

        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.y = 0;
        moveDirection.Normalize();

        if (isRunning)
        {
            moveDirection = moveDirection * runningSpeed;
        }
        else
        {
            moveDirection = moveDirection * walkingSpeed;
        }


        Vector3 movementVelocity = moveDirection;
        playerRigidbody.linearVelocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.y = 0;
        targetDirection.Normalize();

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();//Must be called first because this actions have priority over movements

        if (playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();

    }

    private void HandleFallingAndLanding()//create
    {
        RaycastHit hit; //new variable of type RaycastHit 
        Vector3 rayCastOrigin = transform.position;//new variable of type Vector3 to store the initial position
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;//to add a little offset in Y, ´cause the value at the floor level may not detect the floor 


        if (!isGrounded) //if is NOT grounded
        {
            if (!playerManager.isInteracting) //if is NOT interacting
            {
                //if all the above meet it means that IS FALLING
                animatorManager.PlayerTargetAnimation("Falling", true);//pass the name of the animation and the value for isInteracting
            }

            inAirTimer += Time.deltaTime;//increase as soon as falling with frame independancy 

            //impulse the rigidbody forward, simulating a leap (not falling immediately)
            playerRigidbody.AddForce(transform.forward * leapingVelocity);

            //ad a force down * customizable speed * increasing every frame. 
            playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTimer);

        }

        //if we detect that the invisible sphere created at the feet of the player of 0.2 radius touch the groundLayer
        if (Physics.SphereCast(rayCastOrigin, 0.1f, Vector3.down, out hit, maxDistance, groundLayer))
        {
            //if is NOT grounded and NOT interacting
            if (!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayerTargetAnimation("Landing", true);//pass the name of the animation and the value for isInteracting
            }

            inAirTimer = 0;//reset the value 'cause player reach the ground
            isGrounded = true;//now is in the ground
            playerManager.isInteracting = false;//is not interacting
        }
        else
        {
            isGrounded = false;//player is in the AIR
        }
    }
}

