using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 defaultAngle = new Vector3(19.096f, 0f, 0f);
    
    public float horizontalSpeed;
    public float verticalSpeed;

    public float h;
    public float v;
     
    // Update is called once per frame
    void Update()
    {
        h = horizontalSpeed * Input.GetAxis("Mouse X");
        v = verticalSpeed * Input.GetAxis("Mouse Y");
        
        if (h != 0 || v != 0)
        {
            transform.Rotate(v, h, 0);
        } else
        {
            transform.Rotate(defaultAngle);
        }
        
        
    }

}

