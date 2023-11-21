using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charlie : MonoBehaviour
{
    NPC npcController;
    public bool hasBeenTalkedTo;
    
    // Start is called before the first frame update
    void Start()
    {
        npcController = GetComponent<NPC>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!npcController.isInRange)
        {
            return;
        }
        else if (!npcController.isAlive)
        {
            return;
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (!hasBeenTalkedTo)
                {
                    npcController.TriggerDialogue();
                    hasBeenTalkedTo = true;
                }
            }
        }
    }
}
