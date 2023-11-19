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

    private GameObject interactUI;
    

    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        dialogueManagerOb = GameObject.Find("DialogueManager");
        dialogueManager = dialogueManagerOb.GetComponent<DialogueManager>();
        interactUI = GameObject.Find("InteractButton");
        

    }
    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < activationDistance ) 
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();                
            }
        }

    }

    public void TriggerDialogue()
    {
        interactUI.SetActive(false);
        playerController.isInConversation = true;
        dialogueManager.StartDialogue(dialogue);
    }

}
