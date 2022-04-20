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

    private float timeToFire;
    private GameManager gameManager;

    public float timeToFireInterval = 0.5f;
    public GameObject[] projectilePrefab;
    public Material[] powerupMaterial;
    public int projectileIndex;
    public Vector3 offset = new Vector3(0, 0, 0);

    public Quaternion shotgunAngle;
    public float angle;
    public float angleModifier;
    public int shotgunAmount;

    // Update is called once per frame
    void Update()
    {
        timeToFire -= Time.deltaTime;

        if (Input.GetMouseButton(0) && timeToFire <= 0)
        {
            if (piercing)
            {
                Instantiate(projectilePrefab[1], transform.position + offset, transform.rotation);
                impulse.GenerateImpulse(0.5f);
                timeToFireInterval = 0.5f;
                timeToFire = timeToFireInterval;
            }
            else if (rapidFire)
            {
                Instantiate(projectilePrefab[2], transform.position + offset, transform.rotation);
                impulse.GenerateImpulse(0.2f);
                timeToFireInterval = 0.1f;
            }
            else if (shotgun)
            {
                impulse.GenerateImpulse(0.8f);
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
            else
            {
                impulse.GenerateImpulse(0.3f);
                Instantiate(projectilePrefab[0], transform.position + offset, transform.rotation);
                timeToFireInterval = 0.5f;
                timeToFire = timeToFireInterval;
            }
        }
    }
    public bool piercing = false;
    public bool rapidFire = false;
    public bool shotgun = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Piercing"))
        {
            piercing = true;
            rapidFire = false;
            shotgun = false;

            Destroy(other.gameObject);

            StartCoroutine(PowerupCountdownRoutine(4));

            GetComponent<Renderer>().material = powerupMaterial[1];
            gameManager.UpdatePowerup("Piercing");
        }
        else if (other.gameObject.CompareTag("RapidFire"))
        {
            rapidFire = true;
            piercing = false;
            shotgun = false;

            Destroy(other.gameObject);

            StartCoroutine(PowerupCountdownRoutine(1));

            GetComponent<Renderer>().material = powerupMaterial[2];
            gameManager.UpdatePowerup("RapidFire");

        }

        else if (other.gameObject.CompareTag("Shotgun"))
        {
            shotgun = true;
            rapidFire = false;
            piercing = false;

            Destroy(other.gameObject);

            StartCoroutine(PowerupCountdownRoutine(4));

            GetComponent<Renderer>().material = powerupMaterial[3];
            gameManager.UpdatePowerup("Shotgun");

        }
    }
    IEnumerator PowerupCountdownRoutine(int powerupTime)
    {
        yield return new WaitForSeconds(powerupTime);
        piercing = false;
        rapidFire = false;
        shotgun = false;
        GetComponent<Renderer>().material = powerupMaterial[0];
        gameManager.UpdatePowerup("None");

    }
}
