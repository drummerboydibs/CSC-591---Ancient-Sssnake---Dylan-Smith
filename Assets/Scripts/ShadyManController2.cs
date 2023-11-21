using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
using static UnityEditor.PlayerSettings;
#endif

public class ShadyManController2 : MonoBehaviour
{
    private NavMeshAgent agent;
    ShadyMan smc;
    private int destPoint = 0;
    public Transform[] pos;
    Animator animator;
    public bool isDead = false;
    GameObject boulder;
    

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        boulder = GameObject.Find("Boulder");
        smc = gameObject.GetComponent<ShadyMan>();
        agent.autoRepath = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (smc.isPart2 && !isDead)
        {
            // Pick a new destination when approaching current one.
            if (!agent.pathPending && agent.remainingDistance < 0.2f)
            {
                GoToNextPoint();
            }
            
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

        
        if (destPoint < pos.Length - 1)
        {
            destPoint ++;
        } else {
            return;
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
