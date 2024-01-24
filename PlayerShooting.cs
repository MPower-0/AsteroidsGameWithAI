using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //bulletPrefab is instantiated publicly so it can be easily accessed in the editor 
    public Bullet bulletPrefab;

    private GameObject shootingPoint;

    //Before the game loads a shootingPoint is set to a game object connected to the front of the ship
    private void Awake()
    {
        shootingPoint = GameObject.Find("Shooting Point");
    }

    //Checks every frame if space bar has been pressed down fully
    //if so calls the Shoot method
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    //Instantiates a new bullet and its direction of travel
    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, shootingPoint.transform.position, this.transform.rotation);
        bullet.Direction(this.transform.up);
    }
}