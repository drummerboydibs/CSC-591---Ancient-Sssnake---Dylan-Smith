using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Muskrat : MonoBehaviour
{
    public Transform[] patrolPaths;
    public float speed;
    private NavMeshAgent agent;
    private int destPoint = 0;
    NPC npc;

    float distanceToGoal;

    Animator anim;

    
        
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        npc = GetComponent<NPC>();

        // Allow for continuous movement
        agent.autoBraking = false;

        GoToNextPoint();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // Pick a new destination when approaching current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f && npc.isAlive)
        {
            GoToNextPoint();
        }

    }

    void GoToNextPoint()
    {
        if (patrolPaths.Length == 0)
        {
            return;
        }
        // Go to currently selected destination
        agent.destination = patrolPaths[destPoint].position;

        // Choose next destination
        destPoint = Random.Range(0, patrolPaths.Length);
                
    }

    void TriggerInteraction()
    {
        npc.Die();
    }

}
