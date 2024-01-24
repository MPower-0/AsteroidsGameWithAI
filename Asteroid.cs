using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float mass = 1.0f;
    public float min = 0.1f;
    public float max = 0.2f;
    public float separationMultiplier = 1.0f;
    public float lifeTime = 50.0f;
    public float speed = 10.0f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    //Loads and assigns the Sprite Renderer and Rigidbody component to their corresponding variables
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    //Before the first frame is loaded it selects a random sprite that is in the array
    //It then gives the asteroid a random rotation and scales the game object to be equal to the mass
    private void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0, 0, Random.value * 360.0f);
        this.transform.localScale = new Vector3(this.mass, this.mass, this.mass);

        rigidBody.mass = this.mass;
    }

    //Sets the trajectory of the asteroid by adding a force to the asteroids rigidbody
    //Then destroys the asteroid after a set amount of time that can be changed in the editor
    public void Trajectory(Vector2 direction)
    {
        rigidBody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.lifeTime);
    }

    //If the asteroid collides with a Bullet then it checks to see if it can be split
    //This is determined by checking if the asteroid / 2 is larger than the minimum asteroid size
    //If so it calls the Split method twice
    //It then destroys the original asteroid and calls to the Game Manager AsteroidDestroyed method
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            

            if (this.mass/2 >= this.min)
            {
                Split();
                Split();
            }

            Destroy(this.gameObject);
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            
        }
    }

    //Sets the new asteroids position to a random point in a radius around the current asteroid
    //Then instantiates a new asteroid at that position and its mass is set to half of the previous asteroid
    //Its trajectory is then set to a random point in a circle multiplied by a value that can be set in the editor
    private void Split()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.mass = this.mass / 2;

        half.Trajectory(Random.insideUnitCircle.normalized * this.separationMultiplier);
    }
}