using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTurning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        // points the player object towards the mouse pointer - adapted from unity forum :
        // changed some parts of the code to be compatible in 3D, and point in the correct direction
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.down);
    }
}
