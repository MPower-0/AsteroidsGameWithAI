using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float speed = 100.0f;
    public float lifeTime = 10.0f;

    //Before load gets the rigid body component and assigns it to the rigid body variable
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    //When called with a Vector2 it adds the force to the rigid body of the bullet
    //After a certain set time it then is destroyed
    public void Direction(Vector2 direction)
    {
        rigidBody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.lifeTime);
    }

    //If the bullet collides with the walls then it is destroyed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}