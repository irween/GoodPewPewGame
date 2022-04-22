using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float xRange = 30;
    public float dodgeTimeout;
    public float dodgeCooldown;
    public float dodgeSpeed = 30f;

    private float horizontalInput;
    private float verticalInput;
    private GameManager gameManager;
    private bool canDodge = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canDodge = true;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDodge)
        {
            speed += dodgeSpeed;
            canDodge = false;
            StartCoroutine(RollTimeOut(dodgeCooldown));
            StartCoroutine(RollCountdownRoutine(dodgeTimeout));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 20f; 
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 10f;
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

    IEnumerator RollCountdownRoutine(float timeOut)
    {
        yield return new WaitForSeconds(timeOut);
        speed = 10f;

    }

    IEnumerator RollTimeOut(float timeOut)
    {
        yield return new WaitForSeconds(timeOut);
        canDodge = true;
    }

    public IEnumerator DestroyPlayer(bool gameOver)
    { 
        gameManager.UpdateGameOver(gameOver);
        Destroy(gameObject);
        Debug.Log("HIT");

        yield return null;
    }
}
