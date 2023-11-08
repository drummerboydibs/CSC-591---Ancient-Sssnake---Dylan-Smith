using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody rb;
    private float speed = 8f;
    private float turnSpeed = 120f;

    private float attackPower = 50f;

    // Start is called before the first frame update
    void Start()
    {
       // rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(0, 0, verticalInput * Time.deltaTime * speed));
        transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * turnSpeed);
    }
}
