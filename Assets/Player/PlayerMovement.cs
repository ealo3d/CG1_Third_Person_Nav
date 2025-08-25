using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;
    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    private void Awake()
    {
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
        moveDirection = moveDirection * movementSpeed;
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

        if (targetDirection == Vector3.zero) //if the rotation = starting rotation
            targetDirection = transform.forward; //set rotation point forward

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();

    }
}

