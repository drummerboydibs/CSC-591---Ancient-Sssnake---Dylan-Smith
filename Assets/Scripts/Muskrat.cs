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
    public bool hasBeenTalkedTo = false;
    GameController gameController;
    NPC npc;

    
        
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        npc = GetComponent<NPC>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

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

        if (!npc.isInRange)
        {
            return;
        } else if (Input.GetKeyUp(KeyCode.E) && !hasBeenTalkedTo)
        {
            npc.TriggerDialogue();
            hasBeenTalkedTo = true;
        } else if (Input.GetKeyUp(KeyCode.E) && hasBeenTalkedTo) 
            {
                TriggerInteraction();
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
        StopAllCoroutines();
        gameController.LevelUp();
        npc.Die();
    }

}
