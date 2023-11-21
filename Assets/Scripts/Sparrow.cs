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

    // Start is called before the first frame update
    void Start()
    {
        npcController = GetComponent<NPC>();
        player = GameObject.Find("Player");
        charlie = GameObject.Find("Charlie");
    }

    // Update is called once per frame
    void Update()
    {
        if (!npcController.isInRange)
        {
            return;
        }
        
        else if (!hasBeenTalkedTo && Input.GetKeyDown(KeyCode.E))
        {
            npcController.TriggerDialogue();
            hasBeenTalkedTo = true;
            isBeingCarried = true;
        }
        
        if (isBeingCarried)
        {
            transform.position = (player.transform.position + (Vector3.back / 2));
        }

        distanceToCharlie = (transform.position - charlie.transform.position).magnitude;
        if (distanceToCharlie < charlie.GetComponent<NPC>().activationDistance)
        {
            Special();
        }

    }

    

    void Special()
    {
        charlie.AddComponent<FriendController>();
        npcController.Die();
        StartCoroutine(CleanUp());
    }

    IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
