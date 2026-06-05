using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce = 20f;
    public float baseSpeed = 5f;
    public float turnSpeed = 120f;
    public int attackPower = 10;
    public Rigidbody rb;
    public int maxHP = 100;
    public int currentHP = 100;
    public bool isInConversation = false;
    public Vector3 sizeChange;
    public float timeSinceLastAttack = 0f;
    public float attackDelay = 1f;
    public float attackRange = 1.5f;
    public float attackOffset = 1f;
    public AudioClip attackSound;
    public AudioClip deathSound;
    public AudioSource audioSource;
    

    private bool isOnGround = true;
    private bool wasMoving = false;
    private Animator playerAnim;
    private GameObject playerSnake;

    private GameObject gameControllerObject;
    private GameController gameController;
    

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSnake = GameObject.Find("Snake");
        if (playerSnake != null)
            playerAnim = playerSnake.GetComponent<Animator>();
        else
            Debug.LogError("PlayerController: 'Snake' child object not found.");
        gameControllerObject = GameObject.Find("GameController");
        if (gameControllerObject != null)
            gameController = gameControllerObject.GetComponent<GameController>();
        else
            Debug.LogError("PlayerController: 'GameController' object not found.");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController == null) return;
        if (!gameController.isTitleScreenActive)
        {
            timeSinceLastAttack += Time.deltaTime;

            // Control the snake's motion
            if (!gameController.isGameOver && !isInConversation)
            {
                float verticalInput = Input.GetAxis("Vertical");
                float horizontalInput = Input.GetAxis("Horizontal");
                transform.Translate(new Vector3(0, 0, verticalInput * Time.deltaTime * baseSpeed));
                transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * turnSpeed);

                // Only fire the animation trigger on the frame the state changes,
                // not every frame — SetTrigger queues events and will spam the Animator.
                bool isMoving = verticalInput != 0 && isOnGround;
                if (isMoving != wasMoving)
                {
                    wasMoving = isMoving;
                    if (playerAnim != null)
                        playerAnim.SetTrigger(isMoving ? "walk" : "stop");
                }
            }


            // Enable jumping
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true && !gameController.isGameOver && !isInConversation && !gameController.isTitleScreenActive)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("jump");
            }

            // Rescue if player falls through world
            if (transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            }

            // Death animation
            if (currentHP == 0)
            {
                Die();
            }

            // Enable attack
            if (Input.GetMouseButtonDown(0) && timeSinceLastAttack > attackDelay)
            {
                Attack();
            }
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
        attackPower += 5;
        jumpForce += 1;
        playerSnake.transform.localScale += sizeChange;

    }

    public void Die()
    {
        playerAnim.SetTrigger("die");
        audioSource.clip = deathSound;
        audioSource.Play();
        gameController.GameOver();
    }

    public void Attack()
    {
        playerAnim.SetTrigger("attack");
        timeSinceLastAttack = 0;

        if (attackSound != null && audioSource != null)
        {
            audioSource.clip = attackSound;
            audioSource.Play();
        }

        Vector3 attackOrigin = transform.position + transform.forward * attackOffset;
        Collider[] hits = Physics.OverlapSphere(attackOrigin, attackRange);
        var hitNpcs = new HashSet<NPC>();
        var hitEnemies = new HashSet<EnemyController>();
        foreach (var hit in hits)
        {
            if (hit.transform.IsChildOf(transform)) continue;

            var npc = hit.GetComponentInParent<NPC>();
            if (npc != null && hitNpcs.Add(npc) && npc.isAlive)
            {
                npc.TakeDamage(attackPower);
                continue;
            }

            var enemy = hit.GetComponentInParent<EnemyController>();
            if (enemy != null && hitEnemies.Add(enemy))
            {
                enemy.TakeDamage(attackPower);
            }
        }
    }


}
