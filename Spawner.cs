using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float rate = 1.0f;
    public float AIRate = 0.5f;
    public float amount = 1;
    public float distance = 15.0f;
    public float trajectoryVariance = 15.0f;

    public Asteroid asteroidPrefab;
    public IntelligentAsteroid AIAsteroidPrefab;

    //Invokes the Spawn method at a set rate that can be changed in the editor
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.rate, this.rate);
        InvokeRepeating(nameof(AISpawn), this.AIRate, this.AIRate);
    }

    //Loops depending on amount which is changed in editor
    //Spawns an asteroid at a set distance at a random point in a circle
    //Then sets its rotation on spawn by using the variance value which is a random value between two set values
    //Its mass is then set to a random variable between a min and max value
    //Its trajectory is then set using the values calculated earlier
    private void Spawn()
    {

        for (int i = 0; i<this.amount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * distance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion spawnRotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, spawnRotation);
            asteroid.mass = Random.Range(asteroid.min, asteroid.max);

            asteroid.Trajectory(spawnRotation * -spawnDirection);
        }
    }

    private void AISpawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * distance;
        Vector3 spawnPoint = this.transform.position + spawnDirection;

        float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
        Quaternion spawnRotation = Quaternion.AngleAxis(variance, Vector3.forward);

        IntelligentAsteroid asteroid = Instantiate(this.AIAsteroidPrefab, spawnPoint, spawnRotation);
        asteroid.mass = Random.Range(asteroid.min, asteroid.max);

        asteroid.Trajectory(spawnRotation * -spawnDirection);
    }
}
