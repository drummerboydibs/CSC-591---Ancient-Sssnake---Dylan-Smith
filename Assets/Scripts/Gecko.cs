using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gecko : MonoBehaviour
{
    NPC npcController;
    public bool hasBeenTalkedTo = false;
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
        } else if (Input.GetKeyDown(KeyCode.E) && !hasBeenTalkedTo)
        {
            npcController.TriggerDialogue();
            hasBeenTalkedTo = true;
        } else if (Input.GetKeyDown(KeyCode.E) && hasBeenTalkedTo)
        {
            gameController.LevelUp();
            npcController.Die();
        }
    }
}
