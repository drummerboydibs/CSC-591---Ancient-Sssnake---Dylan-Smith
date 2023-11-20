using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ShadyMan : MonoBehaviour
{
    public GameObject passageWay;
    NavMeshAgent agent;
    public Transform[] pos;
    Animator animator;
    private int destPoint = 0;
    public bool isPart2 = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        passageWay = GameObject.Find("PassageWay1");
        animator = GetComponent<Animator>();

        // Allow for continuous movement
        agent.autoBraking = false;

        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (passageWay.transform.position.y < -14 && !isPart2)
        {
            gameObject.AddComponent<ShadyManController2>();
            isPart2 = true;
        }

        // Pick a new destination when approaching current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !isPart2)
        {
            GoToNextPoint();
        }

    }

    void GoToNextPoint()
    {
        if (pos.Length == 0)
        {
            return;
        }
        // Go to currently selected destination
        agent.destination = pos[destPoint].position;
        animator.SetTrigger("Walk");

        // Head to next destination. Cycle to beginning if end of array.
        if (destPoint == 0)
        {
            destPoint = 1;
        } else if (destPoint == 1)
        {
            destPoint = 0;
        }
        
    }
}
