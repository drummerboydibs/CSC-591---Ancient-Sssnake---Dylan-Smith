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
    SecretPassageSwitch secretPassageSwitch;
    ShadyManController2 smc2;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        passageWay = GameObject.Find("PassageWay1");
        animator = GetComponent<Animator>();
        secretPassageSwitch = GameObject.Find("Secret_Passage_Torch").GetComponent<SecretPassageSwitch>();
        smc2 = gameObject.GetComponent<ShadyManController2>();


        // Allow for continuous movement
        agent.autoBraking = false;

        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (secretPassageSwitch.isActivated && !isPart2)
        {
            isPart2 = true;

        }

        // Pick a new destination when approaching current one.
        if (!agent.pathPending && agent.remainingDistance < 0.2f && !isPart2)
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
        destPoint = (destPoint + 1) % pos.Length;

    }


}
