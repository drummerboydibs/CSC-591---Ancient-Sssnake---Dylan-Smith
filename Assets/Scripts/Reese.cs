using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reese : MonoBehaviour
{
    NPC npcController;
    PlayerController playerController;
    public bool hasBeenTalkedTo = false;

    // Start is called before the first frame update
    void Start()
    {
        npcController = gameObject.GetComponent<NPC>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!npcController.isInRange)
        {
            return;
        }
        else if (hasBeenTalkedTo)
        {
            Special();
        } else {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (!hasBeenTalkedTo)
                {
                    npcController.TriggerDialogue();
                    hasBeenTalkedTo=true;
                }
            }
        }
    }

    void Special()
    {
        gameObject.AddComponent<FriendController>();
        Destroy(this);
    }
}
