using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkyMonkeyController : MonoBehaviour
{
    NPC npcController;
    GameController gameController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        npcController = GetComponent<NPC>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!npcController.isInRange)
        {
            return;
        } else if (Input.GetKeyDown(KeyCode.E) && gameController.playerLevel < 4)
        {
            npcController.TriggerDialogue();
        } else if (Input.GetKeyDown(KeyCode.E) && gameController.playerLevel >= 4)
        {
            gameController.LevelUp();
            npcController.Die();
        }

    }
}
