using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparrow : MonoBehaviour
{
    NPC npcController;
    public bool hasBeenTalkedTo = false;
    GameObject player;
    public bool isBeingCarried = false;
    GameObject charlie;
    public float distanceToCharlie;
    bool didTriggerSpecial = false;

    // Start is called before the first frame update
    void Start()
    {
        npcController = GetComponent<NPC>();
        if (npcController == null)
            Debug.LogError("Sparrow (" + gameObject.name + "): missing NPC component.");
        player = GameObject.Find("Player");
        charlie = GameObject.Find("Charlie");
    }

    // Update is called once per frame
    void Update()
    {
        // Range-gated: only the dialogue trigger needs the player nearby.
        if (npcController != null && npcController.isInRange && !hasBeenTalkedTo && Input.GetKeyDown(KeyCode.E))
        {
            if (npcController != null) npcController.TriggerDialogue();
            hasBeenTalkedTo = true;
            isBeingCarried = true;
        }

        // Follow must run regardless of range, otherwise the sparrow is
        // stranded the moment the player walks away.
        if (isBeingCarried && player != null)
        {
            transform.position = (player.transform.position + (Vector3.back / 2));
        }

        // Only meaningful once we're being carried toward Charlie.
        if (isBeingCarried && !didTriggerSpecial && charlie != null)
        {
            distanceToCharlie = (transform.position - charlie.transform.position).magnitude;
            var charlieNpc = charlie.GetComponent<NPC>();
            float activation = charlieNpc != null ? charlieNpc.activationDistance : 3f;
            if (distanceToCharlie < activation)
            {
                Special();
            }
        }
    }



    void Special()
    {
        didTriggerSpecial = true;
        isBeingCarried = false;
        if (charlie.GetComponent<FriendController>() == null)
        {
            charlie.AddComponent<FriendController>();
        }
        if (npcController != null) npcController.Die();
        enabled = false;
        StartCoroutine(CleanUp());
    }

    IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
