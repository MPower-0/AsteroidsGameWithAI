using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private GameObject player;
    public float speed = 1.0f;
    private bool playerSpotted = false;

    private void Awake()
    {
        rigidBody = this.gameObject.transform.parent.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //If the asteroid spots bullets by them entering the trigger attatched then the AI's state
    //will be changed to avoiding bullets
    //Else if the asteroid spots the player by them entering the trigger then
    //the state will be changed to moving towards the player
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "Bullet")
        {
            //Gets the direction of the bullet and finds the perpendicular vector to that
            Vector2 heading = this.gameObject.transform.position - trigger.gameObject.transform.position;
            Vector2 perp = Vector2.Perpendicular(heading);

            //Applies a force to the rigidbody of the asteroid in the direction of the previously calculated vector
            rigidBody.AddForce(perp * 10.0f);
            
        }

        //Sets the playerSpotted variable to true if player spotted
        if(trigger.gameObject.tag == "Player")
        {
            playerSpotted = true;

        }
    }

    //If the playerSpotted variable is true then the asteroid start to follow the user at a set speed that can be changed in the editor
    private void Update()
    {
        if (playerSpotted)
        {
            rigidBody.transform.position = Vector2.MoveTowards(rigidBody.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

}