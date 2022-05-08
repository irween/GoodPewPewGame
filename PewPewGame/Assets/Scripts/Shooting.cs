using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shooting : MonoBehaviour
{
    CinemachineImpulseSource impulse;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        impulse = transform.GetComponent<CinemachineImpulseSource>();
    }

    // creating variables
    private float timeToFire;
    private GameManager gameManager;
    
    // creating GameObject variables
    public GameObject Player;
    public GameObject Spawner;

    // creating GameObject/Material
    public GameObject[] projectilePrefab;
    public Material[] powerupMaterial;

    // creating public variables
    public float timeToFireInterval = 0.5f;
    public int projectileIndex;
    public Vector3 offset = new Vector3(0, 0, 0);

    // creating shotgun variables (controls the spread of the shotgun shot)
    public Quaternion shotgunAngle;
    public float angle;
    public float angleModifier;
    public int shotgunAmount;

    // creating the powerup booleans (controls if the powerup is active or not)
    public bool piercing = false;
    public bool rapidFire = false;
    public bool shotgun = false;
    public bool invincibility = false;

    // Update is called once per frame
    void Update()
    {
        // changes the fire interval by a constant amount every frame
        timeToFire -= Time.deltaTime;

        // detects when the player presses or holds the mouse button
        if (Input.GetMouseButton(0) && timeToFire <= 0)
        {

            // when the player has the piercing powerup, the gun object uses the piercing projectile prefab
            if (piercing)
            {
                Instantiate(projectilePrefab[1], transform.position + offset, transform.rotation);
                impulse.GenerateImpulse(0.3f);
                timeToFireInterval = 0.5f;
                timeToFire = timeToFireInterval;
            }

            // when the player has the rapid fire powerup, the gun object uses the rapid fire projectile prefab and lowers the firing interval
            else if (rapidFire)
            {
                Instantiate(projectilePrefab[2], transform.position + offset, transform.rotation);
                impulse.GenerateImpulse(0.2f);
                timeToFireInterval = 0.1f;
            }

            // when the player has the shotgun powerup, the gun object uses the shotgun projectile prefab and loops the instantiate code X times, and changes the fire angle
            else if (shotgun)
            {
                impulse.GenerateImpulse(2f);
                angle = -60;
                for (int i = 0; i < shotgunAmount; i++)
                {
                    angle += angleModifier;
                    shotgunAngle = transform.rotation * Quaternion.AngleAxis(angle, Vector3.up);
                    Instantiate(projectilePrefab[3], transform.position, shotgunAngle);
                    timeToFireInterval = 0.5f;
                    timeToFire = timeToFireInterval;
                }
            }

            // when the player has no powerup, the gun object uses the default projectile prefab.
            else
            {
                impulse.GenerateImpulse(0.1f);
                Instantiate(projectilePrefab[0], transform.position + offset, transform.rotation);
                timeToFireInterval = 0.5f;
                timeToFire = timeToFireInterval;
            }
        }
    }

    // this function detects when the object collides with another object.
    // parameters - Collider (the game object), other (the object that is colliding with this object)
    // return values - none
    private void OnTriggerEnter(Collider other)
    {
        
        // when the player collides with the piercing powerup it changes the corresponding boolean to true and the others to false, and starts a countdown coroutine, and changes the gun material
        if (other.gameObject.CompareTag("Piercing"))
        {
            piercing = true;
            rapidFire = false;
            shotgun = false;
            invincibility = false;

            Destroy(other.gameObject);

            StartCoroutine(PowerupCountdownRoutine(4));

            GetComponent<Renderer>().material = powerupMaterial[1];
            gameManager.UpdatePowerup("Piercing");
        }

        // when the player collides with the rapid fire powerup it changes the corresponding boolean to true and the others to false, and starts a countdown coroutine, and changes the gun material
        if (other.gameObject.CompareTag("RapidFire"))
        {
            gameManager.UpdatePowerup("RapidFire");

            rapidFire = true;
            piercing = false;
            shotgun = false;
            invincibility = false;

            Destroy(other.gameObject);

            StartCoroutine(PowerupCountdownRoutine(1));

            GetComponent<Renderer>().material = powerupMaterial[2];
        }

        // when the player collides with the shotgun powerup it changes the corresponding boolean to true and the others to false, and starts a countdown coroutine, and changes the gun material
        if (other.gameObject.CompareTag("Shotgun"))
        {
            gameManager.UpdatePowerup("Shotgun");

            shotgun = true;
            rapidFire = false;
            piercing = false;
            invincibility = false;

            Destroy(other.gameObject);

            StartCoroutine(PowerupCountdownRoutine(4));

            GetComponent<Renderer>().material = powerupMaterial[3];
        }

        // when the player collides with the invincibility powerup it changes the corresponding boolean to true and the others to false, and starts a countdown coroutine, and changes the gun material
        if (other.gameObject.CompareTag("Invincibility"))
        {
            gameManager.UpdatePowerup("Invincibility");

            invincibility = true;
            shotgun = false;
            rapidFire = false;
            piercing = false;

            Destroy(other.gameObject);

            StartCoroutine(PowerupCountdownRoutine(4));

            GetComponent<Renderer>().material = powerupMaterial[4];
        }

        // when the player collides with the enemy and they don't have the invincibility powerup, it starts the player death function in the player controller to destroy the player
        if (other.gameObject.CompareTag("Enemy") && !invincibility)
        {
            Player.GetComponent<PlayerController>().DestroyPlayer(true);

            Debug.Log("HIT");
        }

        // when the player collides with the boss and they don't have the invincibility powerup, it starts the player death function in the player controller to destroy the player
        if (other.gameObject.CompareTag("Boss") && !invincibility)
        {
            Player.GetComponent<PlayerController>().DestroyPlayer(true);

            Debug.Log("HIT");
        }
    }

    // the countdown coroutine that gives a time limit for the powerups
    // parameter - powerupTime : sets the length of time the power up stays on, once the time runs out, the coroutine sets all powerups to false and resets the guns material
    // return value - none
    IEnumerator PowerupCountdownRoutine(int powerupTime)
    {
        yield return new WaitForSeconds(powerupTime);
        piercing = false;
        rapidFire = false;
        shotgun = false;
        invincibility = false;
        GetComponent<Renderer>().material = powerupMaterial[0];
        gameManager.UpdatePowerup("None");
    }
}
