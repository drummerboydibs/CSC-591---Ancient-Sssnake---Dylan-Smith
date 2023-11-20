using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    public float distanceToPlayer;
    public float activationDistance;
    public int maxHp = 100;
    public int currentHp = 100;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public AudioClip attackSound;
    public bool isAlive = true;
    public GameObject npcScript;
    
    AudioSource audio;
    public Animator animator;
    
    private GameObject player;
    private PlayerController playerController;

    private DialogueManager dialogueManager;

    public GameObject interactUI;
    

    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        audio = GetComponent<AudioSource>();
        Animator animator = GetComponent<Animator>();
        
        //dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        interactUI = GameObject.Find("InteractButton");
        

    }
    private void Update()
    {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        if (distanceToPlayer <= activationDistance && isAlive) 
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();                
            } else if (Input.GetMouseButton(1))
            {
                GetHit();
            }
        }

    }

    public void TriggerDialogue()
    {
        interactUI.SetActive(false);
        Debug.Log("Interact.UI set to false.");
        playerController.isInConversation = true;
        Debug.Log("IsInConversation bool set to true.");
        dialogueManager.StartDialogue(dialogue);

    }

    

    void GetHit()
    {
        currentHp -= playerController.attackPower;
        GetComponent<Animator>().SetTrigger("hit");
        audio.clip = deathSound;
        audio.Play();
        if (currentHp <= 0)
        {
            isAlive = false;
            Die();
        }
    }

    void Die()
    {
        if (npcScript != null)
        {
            npcScript.SetActive(false);
        }
        GetComponent<Animator>().SetTrigger("die");
        audio.clip = deathSound;
        audio.Play();
        StartCoroutine(CleanupCorpse());
    }

    IEnumerator CleanupCorpse()
    {
        Destroy(this);
        yield return new WaitForSeconds(deathSound.length);
    }

}
