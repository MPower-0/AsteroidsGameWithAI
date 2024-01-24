using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Initialisation of private variables
    private Rigidbody2D rigidBody;
    private float turn;
    private bool forward;

    //Initialisation of public variables so they can be edited in the engine directly
    public float forwardSpeed;
    public float turnSpeed;
    
    //Before the game starts it gets the rigid body attatched to the Player game object
    //and assigns it to the variable rigidBody
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    //Every frame it checks if forward is true otherwise it is false
    //It also checks if the ship is turning and in which case adds a float value to the variable turn
    void Update()
    {
        forward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turn = 1.0f;
        }

        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turn = -1.0f;
        }
        
        else
        {
            turn = 0.0f;
        }
    }

    //Method is run every time the physics system is updated and adds the forces
    //to the rigidBody variable declared earlier to move the ship
    private void FixedUpdate()
    {
        if (forward)
        {
            rigidBody.AddForce(this.transform.up * this.forwardSpeed);
        }

        if(turn != 0.0f)
        {
            rigidBody.AddTorque(turn * this.turnSpeed);
        }
    }
}