using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce = 15f;

    private float speed = 8f;
    private float turnSpeed = 120f;
    private float attackPower = 50f;
    private Rigidbody rb;
    private bool isOnGround = true;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Control the snake's motion
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(0, 0, verticalInput * Time.deltaTime * speed));
        transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * turnSpeed);

        // Enable jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.y);            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check what the player collided with.
        // If ground, can jump again.
    }

}
