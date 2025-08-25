using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager; //to reference the InputManager script
    PlayerMovement playerMovement; //to reference the PlayerMovement3rd script

    private void Awake()
    {
        inputManager = GetComponent<InputManager>(); //   
        playerMovement = GetComponent<PlayerMovement>(); //
    }

    private void Update()
    {
        inputManager.HandleAllInputs();//call HandleAllInputs() on inputManager3rd 
    }

    //Fixed Update update at a fixed steps defined in the editor
    //Recommended for all physics and collisions that rely on time intervals  
    private void FixedUpdate()
    {
        //call the function that handle all movements
        playerMovement.HandleAllMovement();
    }
}