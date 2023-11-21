using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eliza : MonoBehaviour
{
    public GameObject barrelTrap;
    public bool hasBeenTalkedTo = false;
    NPC npcController;

    // Start is called before the first frame update
    void Start()
    {
        barrelTrap = GameObject.Find("BarrelTrap");
        npcController = GetComponent<NPC>();
    }

    // Update is called once per frame
    void Update()
    {
        if (npcController.isInRange && !hasBeenTalkedTo)
        {
            npcController.TriggerDialogue();
            hasBeenTalkedTo = true;
        } else if (barrelTrap != null)
        {
            return;
        } else
        {
            gameObject.AddComponent<FriendController>();
            Destroy(this);
        }
            
        
    }
}
