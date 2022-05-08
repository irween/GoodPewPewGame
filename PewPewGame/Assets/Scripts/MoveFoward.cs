using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFoward : MonoBehaviour
{
    // public variables
    public float speed = 40f;
    public Vector3 offset  = new Vector3(90, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        // changes the rotation of the object
        transform.Rotate(offset);
    }

    // Update is called once per frame
    void Update()
    {
        // moves the gameobject by a designated amount every frame
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
