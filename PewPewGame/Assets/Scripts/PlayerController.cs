using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    public float speed = 10f;
    public float xRange = 30;
    public float rollTime = 0.002f;
    public float rollSpeed = 30f;

    private bool invincibilityTime = false;
    private float horizontalInput;
    private float verticalInput;
    private GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed += rollSpeed;
            invincibilityTime = true;
            StartCoroutine(RollCountdownRoutine(rollTime));
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -xRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -xRange);
        }
        if (transform.position.z > xRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, xRange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && invincibilityTime == false)
        {
            Destroy(gameObject);
            gameManager.UpdateGameOver(true);
            Destroy(other.gameObject);
        }
    }

    IEnumerator RollCountdownRoutine(float timeOut)
    {
        yield return new WaitForSeconds(timeOut);
        invincibilityTime = false;
        speed = 10f;

    }
}
