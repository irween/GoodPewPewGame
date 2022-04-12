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

    private float timeToFire;
    private GameManager gameManager;
    public float timeToFireInterval = 0.5f;
    public GameObject[] projectilePrefab;
    public int projectileIndex;
    public Vector3 offset = new Vector3(0, 0, 0);
    public int powerupTime;
    public Material RapidFire;
    public Material Piercing;
    public Material Gun;

    // Update is called once per frame
    void Update()
    {
        // taking a number away from time to fire every frame
        timeToFire -= Time.deltaTime;

        if (Input.GetMouseButton(0) && timeToFire <= 0) // when the left mouse button
        {
            if (piercing) // if the player has the piercing powerup
            {
                Instantiate(projectilePrefab[1], transform.position + offset, transform.rotation); // this spawns the piercing projectile which shoots
                timeToFireInterval = 0.5f; // this sets the interval between shooting to 
                timeToFire = timeToFireInterval;  // 
            }
            else if (rapidFire)
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
            powerupTime = 7;
            rapidFire = false;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            GetComponent<Renderer>().material = Piercing;
            gameManager.UpdatePowerup("Piercing");
        }
        else if (other.gameObject.CompareTag("RapidFire"))
        {
            rapidFire = true;
            powerupTime = 1;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            GetComponent<Renderer>().material = RapidFire;
            piercing = false;
            gameManager.UpdatePowerup("RapidFire");

        }
    }
    public bool piercing = false;
    public bool rapidFire = false;
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupTime);
        piercing = false;
        rapidFire = false;
        gameManager.UpdatePowerup("None");
        GetComponent<Renderer>().material = Gun;

    }
}
