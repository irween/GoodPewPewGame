using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // public variable
    public float bounds = 35;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // object bounds //

        // these are the object bounds that destroys the gameobject whenever it reaches the coordinates
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
