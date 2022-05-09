using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // public player variables
    public float speed = 10f;
    public float playerBoundaries = 30;
    public float dodgeTimeout;
    public float dodgeCooldown;
    public float dodgeSpeed = 30f;

    // private player variables
    private float horizontalInput;
    private float verticalInput;
    private GameManager gameManager;
    private bool canDodge = true;

    // Start is called before the first frame update
    void Start()
    {
        // finding the game manager game object
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // setting the dodge boolean to true
        canDodge = true;
    }
 
    // Update is called once per frame
    void Update()
    {

        // detecting when the player has pressed the spacebar and if the canDodge boolean is true
        if (Input.GetKeyDown(KeyCode.Space) && canDodge)
        {
            speed += dodgeSpeed; // adding the dodge speed variable to the player speed
            canDodge = false; // setting canDodge boolean to false so the player can't spam the dodge button
            StartCoroutine(DodgeTimeout(dodgeCooldown)); // starting the dodge cooldown coroutine 
            StartCoroutine(DodgeSpeedTimeout(dodgeTimeout)); // starting the dodge speed cooldown coroutine 
        }

        // detecting when the player presses/holds the shift key and increases the player speed to give a sprint effect
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 20f; 
        }

        // detecting when the player lifts the shift key which resets the player speed
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 10f;
        }

        // movement //

        horizontalInput = Input.GetAxis("Horizontal"); // gets the horizontal input (A, D) with a value (-1, 1)
        verticalInput = Input.GetAxis("Vertical"); // gets the horizontal input (W, S) with a value (-1, 1)
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed); // translates the player position by a vector3 value (multiplies by horizontalInput to change the direction)
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed); // translates the player position by a vector3 value (multiplies by verticalInput to change the direction)

        // player boundaries //

        // if the player is out of the specified range, it changes the position of the player to keep them inside the boundaries
        if (transform.position.x < -playerBoundaries)
        {
            transform.position = new Vector3(-playerBoundaries, transform.position.y, transform.position.z);
        }

        if (transform.position.x > playerBoundaries)
        {
            transform.position = new Vector3(playerBoundaries, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -playerBoundaries)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -playerBoundaries);
        }

        if (transform.position.z > playerBoundaries)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, playerBoundaries);
        }
    }

    // this function destroys the player when called from another script
    // parameters - game over boolean that sets the gameover screen to true when the boolean is true
    // return value - none
    public void DestroyPlayer(bool gameOver)
    {
        // finding each powerup to be destroyed
        GameObject[] rapidFire = GameObject.FindGameObjectsWithTag("RapidFire");
        GameObject[] piercing = GameObject.FindGameObjectsWithTag("Piercing");
        GameObject[] invincibility = GameObject.FindGameObjectsWithTag("Invincibility");
        GameObject[] shotgun = GameObject.FindGameObjectsWithTag("Shotgun");

        // destroying each powerupp //
        foreach (var rapidFireObject in rapidFire)
        {
            Destroy(rapidFireObject);
        }

        foreach (var piercingObject in piercing)
        {
            Destroy(piercingObject);
        }

        foreach (var invincibilityObject in invincibility)
        {
            Destroy(invincibilityObject);
        }

        foreach (var shotgunObject in shotgun)
        {
            Destroy(shotgunObject);
        }

        gameManager.UpdateGameOver(gameOver);
        Destroy(gameObject);
        Debug.Log("HIT");
    }

    // this coroutine is a countdown to reset the dodge speed float (speed)
    // parameters - time oun float sets the length of time the player can't dodge
    // return value - none
    IEnumerator DodgeSpeedTimeout(float timeOut)
    {
        yield return new WaitForSeconds(timeOut);
        speed = 10f;
    }

    // this coroutine is a countdown to reset the dodge boolean
    // parameters - time oun float sets the length of time the player can't dodge
    // return value - none
    IEnumerator DodgeTimeout(float timeOut)
    {
        yield return new WaitForSeconds(timeOut);
        canDodge = true;
    }
}
