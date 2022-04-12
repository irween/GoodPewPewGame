using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private GameManager gameManager;
    public float timeToFireInterval = 0.5f;
    private float timeToFire;
    public GameObject[] projectilePrefab;
    public int projectileIndex;
    public Vector3 offset = new Vector3(0, 0, 0);
    // Update is called once per frame
    void Update()
    {
        // taking a number away from time to fire every frame
        timeToFire -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && timeToFire <= 0) // when the left mouse button
        {
            if (piercing) // if the player has the piercing powerup
            {
                Instantiate(projectilePrefab[1], transform.position + offset, transform.rotation); // this spawns the piercing projectile which shoots
                timeToFireInterval = 0.5f; // this sets the interval between shooting to 
                timeToFire = timeToFireInterval;  // 
            }
            if (rapidFire)
            {
                Instantiate(projectilePrefab[2], transform.position + offset, transform.rotation);
                timeToFireInterval = 0.1f;
            }
            else
            {
                Instantiate(projectilePrefab[0], transform.position + offset, transform.rotation);
                timeToFireInterval = 0.5f;
                timeToFire = timeToFireInterval;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Piercing"))
        {
            piercing = true;
            rapidFire = false;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            gameManager.UpdatePowerup("Piercing");
        }
        else if (other.gameObject.CompareTag("RapidFire"))
        {
            rapidFire = true;
            Destroy(other.gameObject);
            piercing = false;
            StartCoroutine(PowerupCountdownRoutine());
            gameManager.UpdatePowerup("RapidFire");

        }
    }
    public bool piercing = false;
    public bool rapidFire = false;
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        piercing = false;
        rapidFire = false;
        gameManager.UpdatePowerup("None");

    }
}
