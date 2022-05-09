using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : MonoBehaviour
{
    // public variables
    public int pointValue = 50;

    // private variables
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // detects if the projectile has collided with the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);

            // updates the score
            gameManager.UpdateScore(pointValue);
        }

        // detects if the projectile has collided with the boss
        if (other.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);

            // updates the score
            gameManager.UpdateScore(pointValue);
        }
    }
}
