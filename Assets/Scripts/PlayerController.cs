using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce = 20f;
    public float baseSpeed = 5f;
    public float turnSpeed = 120f;
    public float attackPower = 10f;
    public Rigidbody rb;
    public int maxHP = 100;
    public int currentHP = 100;
    public bool isInConversation = false;
    public Vector3 sizeChange;

    private bool isOnGround = true;
    private Animator playerAnim;
    private GameObject playerSnake;

    private GameObject gameControllerObject;
    private GameController gameController;
    

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSnake = GameObject.Find("Snake");
        playerAnim = playerSnake.GetComponent<Animator>();
        gameControllerObject = GameObject.Find("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Control the snake's motion
        if (!gameController.isGameOver && !isInConversation)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(0, 0, verticalInput * Time.deltaTime * baseSpeed));
            transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * turnSpeed);
        }
        

        // Enable jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true && !gameController.isGameOver && !isInConversation)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("jump");
        }

        // Rescue if player falls through world
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.y);            
        }

        // Death animation
        if (currentHP == 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check what the player collided with.
        // If ground, can jump again.
        if (collision.gameObject.CompareTag("Environment"))
        {
            isOnGround = true;
            playerAnim.SetTrigger("walk");
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            // decrease health
            currentHP -= 20;
        } else if (collision.gameObject.CompareTag("Lava"))
        {
            Die();
            
        }

    }

    public void LevelUp()
    {
        maxHP += 10;
        currentHP += 10;
        baseSpeed += .5f;
        attackPower += 5f;
        transform.localScale += sizeChange;

    }

    public void Die()
    {
        playerAnim.SetTrigger("die");
        gameController.isGameOver = true;
    }

    public void Attack()
    {
        playerAnim.SetTrigger("attack");

    }

    

}
