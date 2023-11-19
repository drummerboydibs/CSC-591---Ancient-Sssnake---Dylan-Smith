using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSwitch : MonoBehaviour
{
    public float activationRange;
    GameObject player;
    public float distanceToPlayer;
    Animator exitPassage;
    AudioSource exitPassageAudioSource;
    bool isActivated = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        exitPassage = GameObject.Find("Exit_Wall").GetComponent<Animator>();
        exitPassageAudioSource = GameObject.Find("Exit_Wall").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer < activationRange && !isActivated)
        {
            Debug.Log("Player within activation range of torch. Display interact button.");
            if (Input.GetKeyDown(KeyCode.E))
            {
                exitPassage.SetTrigger("Open_Exit");
                exitPassageAudioSource.Play();
                isActivated = true;
            }
        }

    }
}
