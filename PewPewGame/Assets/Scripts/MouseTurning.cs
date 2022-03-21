using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTurning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool piercing = false;
    public float timeToFireInterval = 1.0f;
    private float timeToFire;
    public GameObject[] projectilePrefab;
    public int projectileIndex;
    // Update is called once per frame
    void Update()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.down);

        timeToFire -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && timeToFire <= 0)
        {
            if (piercing)
            {
                Instantiate(projectilePrefab[1], transform.position, transform.rotation);
                timeToFire = timeToFireInterval;
            }
            else
            {
                Instantiate(projectilePrefab[0], transform.position, transform.rotation);
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
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        piercing = false;
    }
}
