using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public ParticleSystem explosionParticle;
    public int pointValue = 50;
    private GameManager gameManager;
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            explosionParticle.Play();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            explosionParticle.Play();
            Destroy(other.gameObject);
            gameManager.UpdateScore(pointValue);
        }
    }
}
