using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float bounds = 35;


    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > bounds)
        {
            Destroy(gameObject);
        }

        else if (transform.position.z < -bounds)
        {
            Destroy(gameObject);
        }

        else if (transform.position.x > bounds)
        {
            Destroy(gameObject);
        }

        else if (transform.position.x < -bounds)
        {
            Destroy(gameObject);
        }
    }
}
