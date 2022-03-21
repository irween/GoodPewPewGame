using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public ParticleSystem explosionParticle;

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
        }
        else if (other.gameObject.CompareTag("Spawner"))
        {
            explosionParticle.Play();
        }
    }
}
