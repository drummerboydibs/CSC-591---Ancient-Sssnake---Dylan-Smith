using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TommyController : MonoBehaviour
{
    NPC npcController;
    
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
        } else if (npcController.isInRange && Input.GetKeyDown(KeyCode.E))
        {
            npcController.TriggerDialogue();
        }

    }

    // Triggered by Timmy.
    public void TriggerSpecial()
    {

        Destroy(this);
    }
}
