using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingProjectile : MonoBehaviour
{
    // public variables
    public int pointValue = 50;

    // private variables
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // finds the game manager game object
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    // this function detects whenever the projectile collides with another object
    // parameters - Collider : the game object, other : the object that has collided with this object
    private void OnTriggerEnter(Collider other)
    {

        // detects if the projectile has collided with the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            // destroys the other object
            Destroy(other.gameObject);

            // updates the score with the point value
            gameManager.UpdateScore(pointValue);
        }

        // detects if the projectile has collided with the boss
        if (other.gameObject.CompareTag("Boss"))
        {
            // updates the score with the point value
            gameManager.UpdateScore(pointValue);
        }
    }
}
