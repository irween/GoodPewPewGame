using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Vector3 offset = new Vector3(0, 15, -3);
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;

    }
}
