using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angus : MonoBehaviour
{
    GameObject shadyMan;
    ShadyManController2 shadyManController2;
    NPC npcController;

    // Start is called before the first frame update
    void Start()
    {
        shadyMan = GameObject.Find("ShadyMan");
        shadyManController2 = shadyMan.GetComponent<ShadyManController2>();
        npcController = GetComponent<NPC>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!npcController.isInRange)
        {
            return;
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            npcController.TriggerDialogue();
        }
        
        if (!shadyManController2.isDead)
        {
            return;
        } else
        {
            gameObject.AddComponent<FriendController>();
            Destroy(this);
        }
    }
}
