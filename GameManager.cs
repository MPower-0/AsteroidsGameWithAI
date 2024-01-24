using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnTime = 2.0f;
    public int score = 0;
    public Text scoreUI;
    public Image lifeImage1;
    public Image lifeImage2;
    public Image lifeImage3;
    public Text informationUI;

    //When an asteroid is destroyed it plays a particle effect at the location
    //It then increases the score and updates the UI
    //If the score is over 20 then it transitions the user to level 2
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        this.score++;
        scoreUI.text = "Points: " + this.score;

        if(this.score >= 20)
        {
            informationUI.text = "Level 2";
            Invoke(nameof(LoadLevel2), 2.0f);
        }

    }

    public void AIAsteroidDestroyed(IntelligentAsteroid AIAsteroid)
    {
        this.explosion.transform.position = AIAsteroid.transform.position;
        this.explosion.Play();

        this.score++;
        scoreUI.text = "Points: " + this.score;

        if(this.score >= 20)
        {
            informationUI.text = "Level 2";
            Invoke(nameof(LoadLevel2), 2.0f);
        }
    }

    //If the player dies it plays an explosion particle effect where they died
    //It then subtracts their lifes and updates the UI
    //If they have 0 lives left then it runs the GameOver method otherwise it calls to the Respawn method after a set time
    public void PlayerDied()
    {
        this.explosion.transform.position = this.playerMovement.transform.position;
        this.explosion.Play();

        this.lives--;

        if (this.lives == 2)
        {
            lifeImage3.enabled = false;
        }
        else if(this.lives == 1)
        {
            lifeImage2.enabled = false;
        }
        else if(this.lives == 0)
        {
            lifeImage1.enabled = false;
        }

        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    //Sets the users position to the default starting position
    //Then activates the users game object and changes the users layer so that the user
    //can't collide with the asteroid so can't be destroyed until after 3 seconds of immunity
    private void Respawn()
    {
        this.playerMovement.transform.position = Vector3.zero;
        this.playerMovement.gameObject.layer = LayerMask.NameToLayer("No Collisions");
        this.playerMovement.gameObject.SetActive(true);
        
        Invoke(nameof(CollisionsOn), 3.0f);
    }

    //Re-enables the users collisions with asteroids
    private void CollisionsOn()
    {
        this.playerMovement.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    //Shows the game over text and calls to the LoadLevel1 method after 2 seconds
    private void GameOver()
    {
        informationUI.text = "Game Over";

        Invoke(nameof(LoadLevel1), 2.0f);

    }

    //Uses the scenemanager to load Level 1
    private void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    //Uses the scenemanager to load Level 2
    private void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
}