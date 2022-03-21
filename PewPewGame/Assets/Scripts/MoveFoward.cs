using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFoward : MonoBehaviour
{
    public Vector3 offset  = new Vector3(90, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(offset);
    }
    public float speed = 40f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
