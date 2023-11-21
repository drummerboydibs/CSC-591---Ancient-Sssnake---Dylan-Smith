using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    GameObject player;
    DialogueManager dialogueManager;
    float distanceToPlayer;
    float activationRange = 3;
    Dialogue dialogue;
    GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogue = GetComponent<Dialogue>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;

        if (!(distanceToPlayer <= activationRange))
        {
            return;
        } else if (distanceToPlayer <= activationRange && !Input.GetKeyDown(KeyCode.E))
        {
            gameController.interactUI.SetActive(true);
        }
        else if (distanceToPlayer <= activationRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogueManager.StartDialogue(dialogue);
        }

    }
}
