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
    public bool isInRange = false;
    public GameObject npcScript;
    
    AudioSource audioSource;
    public Animator animator;
    
    private GameObject player;
    private PlayerController playerController;

    private DialogueManager dialogueManager;

    public GameObject interactUI;
    

    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        Animator animator = GetComponent<Animator>();
        
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        interactUI = GameObject.Find("InteractButton");
        

    }
    private void Update()
    {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        if (distanceToPlayer <= activationDistance && isAlive) 
        {
            
            interactUI.SetActive(true);
            isInRange = true;
            if (Input.GetMouseButton(1))
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
        if (hitSound != null)
        {
            audioSource.clip = hitSound;
            audioSource.Play();
        }
        
        if (currentHp <= 0)
        {
            isAlive = false;
            Die();
        }
    }

    public void Die()
    {
        if (npcScript != null)
        {
            npcScript.SetActive(false);
        }
        GetComponent<Animator>().SetTrigger("die");
        if (deathSound != null)
        {
            audioSource.clip = deathSound;
            audioSource.Play();
        }
        
        StartCoroutine(CleanupCorpse());
    }

    IEnumerator CleanupCorpse()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
