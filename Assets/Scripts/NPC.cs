using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    public float distanceToPlayer;
    public float activationDistance;
    
    private GameObject player;
    private PlayerController playerController;
    private GameObject dialogueManagerOb;
    private DialogueManager dialogueManager;


    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        dialogueManagerOb = GameObject.Find("DialogueManager");
        dialogueManager = dialogueManagerOb.GetComponent<DialogueManager>();

    }
    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < activationDistance ) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();                
            }
        }

    }

    public void TriggerDialogue()
    {
        playerController.isInConversation = true;
        dialogueManager.StartDialogue(dialogue);
    }

}
