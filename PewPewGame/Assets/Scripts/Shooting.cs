using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float timeToFireInterval = 1.0f;
    private float timeToFire;
    public GameObject[] projectilePrefab;
    public int projectileIndex;
    public Vector3 offset = new Vector3(0, 0, 0);
    // Update is called once per frame
    void Update()
    {
        timeToFire -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && timeToFire <= 0)
        {
            if (piercing)
            {
                Instantiate(projectilePrefab[1], transform.position + offset, transform.rotation);
                timeToFire = timeToFireInterval;
            }
            if (rapidFire)
            {
                Instantiate(projectilePrefab[2], transform.position + offset, transform.rotation);
                timeToFireInterval = 0.1f;
            }
            else
            {
                Instantiate(projectilePrefab[0], transform.position + offset, transform.rotation);
                timeToFire = timeToFireInterval;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Piercing"))
        {
            piercing = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
        else if (other.gameObject.CompareTag("RapidFire"))
        {
            rapidFire = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
    public bool piercing = false;
    public bool rapidFire = false;
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        piercing = false;
        rapidFire = false;
    }
}
