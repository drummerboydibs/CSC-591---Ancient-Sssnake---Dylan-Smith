using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager dialogueManager;

    public float distanceToPlayer;
    public float activationDistance = 3;
    public bool isInRange = false;

    public int maxHp = 100;
    public int currentHp = 100;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public AudioClip attackSound;
    AudioSource audioSource;
    public GameController gameController;

    public bool isAlive = true;
    public GameObject npcScript;
    
    public Animator animator;
    
    private GameObject player;
    private PlayerController playerController;

    
    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        Animator animator = GetComponent<Animator>();
        
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogue = GetComponent<Dialogue>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        

    }
    private void Update()
    {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        if (distanceToPlayer <= activationDistance && isAlive) 
        {
            gameController.interactUI.SetActive(true);
            isInRange = true;
            if (Input.GetMouseButton(1))
            {
                Debug.Log("Registered mouse button within range.");
                GetHit();

            }
        } else
        {
            gameController.interactUI.SetActive(false);
            isInRange = false;
        }

    }

    public void TriggerDialogue()
    {
        gameController.interactUI.SetActive(false);
        Debug.Log("Interact.UI set to false.");
        playerController.isInConversation = true;
        Debug.Log("IsInConversation bool set to true.");
        dialogueManager.StartDialogue(dialogue);

    }

    

    void GetHit()
    {
        Debug.Log("In GetHit()");
        currentHp -= playerController.attackPower;
        animator.SetTrigger("hit");
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
        Debug.Log("In Die()");
        if (npcScript != null)
        {
            npcScript.SetActive(false);
        }
        animator.SetTrigger("die");
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
