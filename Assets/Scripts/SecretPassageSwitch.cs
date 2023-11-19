using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPassageSwitch : MonoBehaviour
{
    public float activationRange;
    GameObject player;
    public float distanceToPlayer;
    Animator secretPassage;
    AudioSource secretPassageAudioSource;
    bool isActivated = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        secretPassage = GameObject.Find("PassageWay1").GetComponent<Animator>();
        secretPassageAudioSource = GameObject.Find("PassageWay1").GetComponent<AudioSource>();
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
                secretPassage.SetTrigger("Open_Passageway");
                secretPassageAudioSource.Play();
                isActivated = true;
            }
        }

    }
}
