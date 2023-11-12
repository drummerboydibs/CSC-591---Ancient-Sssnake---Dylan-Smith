using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce = 20f;
    public float speed = 7f;
    public float turnSpeed = 120f;
    public float attackPower = 50f;
    public Rigidbody rb;
    public bool isGameOver = false;
    public int health = 100;

    private bool isOnGround = true;
    private Animator playerAnim;
    private GameObject playerSnake;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSnake = GameObject.Find("Snake");
        playerAnim = playerSnake.GetComponent<Animator>();
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
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true && !isGameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("jump");
        }

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.y);            
        }

        if (health == 0)
        {
            playerAnim.SetTrigger("die");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check what the player collided with.
        // If ground, can jump again.
        if (collision.gameObject.CompareTag("Environment"))
        {
            isOnGround = true;
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            // decrease health
        }

    }

}
