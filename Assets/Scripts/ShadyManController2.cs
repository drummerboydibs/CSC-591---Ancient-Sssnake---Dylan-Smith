using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class ShadyManController2 : MonoBehaviour
{
    private NavMeshAgent agent;
    ShadyMan smc;
    private int destPoint = 2;
    Animator animator;
    public bool isDead = false;
    GameObject boulder;

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        boulder = GameObject.Find("Boulder");
        smc = GetComponent<ShadyMan>();


    }

    // Update is called once per frame
    void Update()
    {
        // Pick a new destination when approaching current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !isDead)
        {
            GoToNextPoint();
        }

    }

    void GoToNextPoint()
    {
        if (smc.pos.Length == 0)
        {
            return;
        }
        // Go to currently selected destination
        agent.destination = smc.pos[destPoint].position;
        animator.SetTrigger("Walk");

        // Head to next destination. Cycle to beginning if end of array.
        if (destPoint < 4)
        {
            destPoint = (destPoint + 1);
        } else {
            destPoint = 1;
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Boulder")
        {
            animator.SetTrigger("Dead");
            isDead = true;
            StartCoroutine(CleanUp());
        }
    }

    IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}
