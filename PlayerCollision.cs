using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    //Loads and assigns the Players rigidbody to the variable rigidBody
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    //If the players hitbox collides with an object with the tag asteroid
    //then its velocity and angular velocity is set to 0
    //Then it turns off the gameObject and then runs the PlayerDied method in the Game Manager
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = 0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}