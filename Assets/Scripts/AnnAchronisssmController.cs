using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnAchronisssmController : MonoBehaviour
{
    NPC npcController;
    AudioSource audioSource;
    public AudioClip clip;
    PlayerController playerController;
    bool hasBeenTalkedTo = false;

    // Start is called before the first frame update
    void Start()
    {
        npcController = gameObject.GetComponent<NPC>();
        audioSource = gameObject.GetComponent<AudioSource>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        if (clip == null)
        {
            Debug.Log(gameObject.name + "is missing required clip.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!npcController.isInRange)
        {
            return;
        } else
        {
            if (Input.GetKeyUp(KeyCode.E)) 
            {
                if (!hasBeenTalkedTo)
                {
                    npcController.TriggerDialogue();
                    hasBeenTalkedTo=true;
                } else
                {
                    Special();
                }
            }
        }

    }

    void Special()
    {
        playerController.isInConversation = true;
        gameObject.GetComponent<AnimalSoundController>().StopAllCoroutines();
        Debug.Log("Stopped AnimalSoundController coroutines.");
        audioSource.clip = clip;
        Debug.Log("Clip" + clip.name + "loaded.");
        StartCoroutine(ForceToListen());
    }

    // Make player listen to clip, then make them your friend.
    IEnumerator ForceToListen()
    {
        yield return new WaitForSeconds(clip.length);
        Debug.Log("Forcing to listen for " + clip.length + "seconds.");
        gameObject.AddComponent<FriendController>();
    }

}
