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
    bool isInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null)
            Debug.LogError("ChestController (" + gameObject.name + "): 'Player' not found in scene.");
        var dmObj = GameObject.Find("DialogueManager");
        if (dmObj != null)
            dialogueManager = dmObj.GetComponent<DialogueManager>();
        else
            Debug.LogError("ChestController (" + gameObject.name + "): 'DialogueManager' not found in scene.");
        dialogue = GetComponent<Dialogue>();
        var gcObj = GameObject.Find("GameController");
        if (gcObj != null)
            gameController = gcObj.GetComponent<GameController>();
        else
            Debug.LogError("ChestController (" + gameObject.name + "): 'GameController' not found in scene.");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        bool nowInRange = distanceToPlayer <= activationRange;

        if (nowInRange && !isInRange)
        {
            isInRange = true;
            if (gameController != null) gameController.RegisterInteractSource(this);
        }
        else if (!nowInRange && isInRange)
        {
            isInRange = false;
            if (gameController != null) gameController.UnregisterInteractSource(this);
        }

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (gameController != null) gameController.UnregisterInteractSource(this);
            isInRange = false;
            dialogueManager.StartDialogue(dialogue);
        }
    }

    private void OnDisable()
    {
        if (gameController != null) gameController.UnregisterInteractSource(this);
        isInRange = false;
    }
}
