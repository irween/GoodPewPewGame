using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{

    // creating public variables that can be edited
    public float speed;
    public float distance;

    public float health;

    // creating GameObject
    public GameObject player;
    public Transform playerTransform;

    // getting the GameObject's rigidbody component
    private Rigidbody enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        // getting the rigidbody component and setting it to enemyRb
        enemyRb = GetComponent<Rigidbody>();

        // finding the player GameObject on the screen
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // setting the lookDirection variable to the player location
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        // giving the rigidbody a force to move in the direction of the player
        enemyRb.AddForce(lookDirection * speed);
    }

    // this function detects when the GameObjects collider is triggered by another GameObject
    // parameters - Collider : this is the game object, other : this is the game object that collides with this object
    // return value - none
    private void OnTriggerEnter(Collider other)
    {
        // when the object collides with a projectile, the boss loses one "health",
        // when the boss loses all the health, it gets destroyed
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);

            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
