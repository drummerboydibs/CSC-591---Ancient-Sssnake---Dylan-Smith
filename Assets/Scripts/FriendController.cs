using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
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
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            // Friends added at runtime (e.g. Reese) may not have a NavMeshAgent
            // pre-configured in the scene. Add one with sensible defaults and
            // snap it to the nearest NavMesh point so it can pathfind.
            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.speed = 3.5f;
            agent.angularSpeed = 120f;
            agent.acceleration = 8f;
            agent.stoppingDistance = 1f;
            agent.radius = 0.4f;
            agent.height = 1.2f;

            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }
            else
            {
                Debug.Log("Could not snap " + gameObject.name + " to NavMesh; follow will be disabled.");
            }
        }

        player = GameObject.Find("Player");
        if (player != null)
            playerController = player.GetComponent<PlayerController>();
        else
            Debug.LogError("FriendController (" + gameObject.name + "): 'Player' not found in scene.");
        enemy = GameObject.Find("Archaeologist");
        if (enemy != null) enemyController = enemy.GetComponent<EnemyController>();
        npc = GetComponent<NPC>();

        // Player levels up every time they make a friend!
        if (playerController != null)
        {
            playerController.LevelUp();
            Debug.Log("Levelled up.");
        }

        // Reduce sound / chatter when an animal becomes your friend
        asc = GetComponent<AnimalSoundController>();
        if (asc != null)
        {
            asc.minOffset += 4;
            asc.maxOffset += 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        bool enemyAvailable = enemy != null && enemyController != null && enemyController.isAlive;
        distanceToEnemy = enemyAvailable
            ? (transform.position - enemy.transform.position).magnitude
            : float.MaxValue;

        // De-aggro and resume following the player once the enemy is dead.
        if (isAggroed && !enemyAvailable)
            isAggroed = false;

        timeSinceLastAttack += Time.deltaTime;

        bool canPath = agent != null && agent.isOnNavMesh;

        // Follow player
        if (canPath && distanceToPlayer > 1.5 && !isAggroed)
        {
            agent.destination = player.transform.position;
        }

        if (distanceToPlayer > 10  && !isAggroed)
        {
            CatchUp();
        }

        // if enemy is in range, attack!
        if (enemyAvailable && distanceToEnemy < 10)
        {
            isAggroed = true;
            if (canPath) agent.destination = enemy.transform.position;
        }

        if (isAggroed && enemyAvailable && distanceToEnemy < attackRange && timeSinceLastAttack > attackDelay)
        {
            Attack();
        }

        if (npc != null && !npc.isAlive)
        {
            Die();
        }

        // Rescue if friend falls through world
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }

    }

    void Attack()
    {
        anim.SetTrigger("attack");
        if (enemyController != null && enemyController.isAlive)
        {
            enemyController.TakeDamage(attackPower);
        }
        timeSinceLastAttack = 0;
    }

    public void Die()
    {
        Debug.Log("Entered FriendController.Die()");
        anim.SetTrigger("die");
    }

    void CatchUp()
    {
        Vector3 target = player.transform.position;
        if (agent != null && NavMesh.SamplePosition(target, out NavMeshHit hit, 3f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
        else
        {
            transform.position = target;
        }
    }

}
