using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingProjectile : MonoBehaviour
{
    public int pointValue = 50;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            gameManager.UpdateScore(pointValue);
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            gameManager.UpdateScore(pointValue);
        }
    }
}
