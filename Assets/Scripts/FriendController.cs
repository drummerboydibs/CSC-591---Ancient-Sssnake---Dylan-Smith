using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class FriendController : MonoBehaviour
{
    public float jumpForce = 20f;
    public float baseSpeed;
    public float turnSpeed = 120f;
    public int attackPower = 10;
    public Rigidbody rb;
    public GameObject player;
    public PlayerController playerController;
    public GameObject enemy;
    public EnemyController enemyController;
    public GameController gameController;
    public float attackRange = 1.5f;
    public float distanceToPlayer;
    public float distanceToEnemy;
    public bool isAggroed = false;
    public float timeSinceLastAttack = 0f;
    public float attackDelay = 1f;

    NavMeshAgent agent;
    private Animator anim;
    private AnimalSoundController asc;
    NPC npc;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
        rb = GetComponent<Rigidbody>();
        if (gameObject.GetComponent<NavMeshAgent>() != null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        else Debug.Log("No NavMeshAgent loaded on " + gameObject.name);
        
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        enemy = GameObject.Find("Archaeologist");
        enemyController = enemy.GetComponent<EnemyController>();
        npc = GetComponent<NPC>();

        // Player levels up every time they make a friend!
        playerController.LevelUp();
        Debug.Log("Levelled up.");
        
        
        // Reduce sound / chatter when an animal becomes your friend
        asc = GetComponent<AnimalSoundController>();
        asc.minOffset += 4;
        asc.maxOffset += 4;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        distanceToEnemy = (transform.position - enemy.transform.position).magnitude;
        timeSinceLastAttack += Time.deltaTime;

        // Follow player
        if (distanceToPlayer > 1.5 && !isAggroed)
        {
            agent.destination = player.transform.position;
        }
        
        if (distanceToPlayer > 10  && !isAggroed)
        {
            CatchUp();
        }

        // if enemy is in range, attack!
        if (distanceToEnemy < 10)
        {
            isAggroed = true;
            agent.destination = enemy.transform.position;
        }

        if (isAggroed && distanceToEnemy < attackRange && timeSinceLastAttack > attackDelay)
        {
            Attack();
        }

        if (npc.currentHp == 0)
        {
            Die();
        }

        // Rescue if player falls through world
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.y);
        }

    }

    void Attack()
    {
        Debug.Log("Entered FriendController.Attack().");
        anim.SetTrigger("attack");
        enemyController.currentHp -= attackPower;
        timeSinceLastAttack = 0;
    }

    public void Die()
    {
        Debug.Log("Entered FriendController.Die()");
        anim.SetTrigger("die");
    }

    void CatchUp()
    {
        transform.position = player.transform.position;
    }

}
