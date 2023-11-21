using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimmyController : MonoBehaviour
{
    NPC npcController;
    bool hasBeenTalkedTo = false;
    GameObject tommy;

    // Start is called before the first frame update
    void Start()
    {
        npcController = GetComponent<NPC>();
        tommy = GameObject.Find("Tommy");
    }

    // Update is called once per frame
    void Update()
    {
        if (!npcController.isInRange)
        {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !hasBeenTalkedTo)
        {
            npcController.TriggerDialogue();
            hasBeenTalkedTo = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && hasBeenTalkedTo)
        {
            TriggerSpecial();
        }


    }

    void TriggerSpecial()
    {
        gameObject.AddComponent<FriendController>();
        tommy.AddComponent<FriendController>();
        tommy.GetComponent<TommyController>().TriggerSpecial();
        Destroy(this);
    }
}
