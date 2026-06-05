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
        if (player != null)
            playerController = player.GetComponent<PlayerController>();
        else
            Debug.LogError("NPC (" + gameObject.name + "): 'Player' not found in scene.");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        var dmObj = GameObject.Find("DialogueManager");
        if (dmObj != null)
            dialogueManager = dmObj.GetComponent<DialogueManager>();
        else
            Debug.LogError("NPC (" + gameObject.name + "): 'DialogueManager' not found in scene.");
        dialogue = GetComponent<Dialogue>();
        var gcObj = GameObject.Find("GameController");
        if (gcObj != null)
            gameController = gcObj.GetComponent<GameController>();
        else
            Debug.LogError("NPC (" + gameObject.name + "): 'GameController' not found in scene.");


    }
    private void Update()
    {
        if (player == null) return;
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        bool nowInRange = distanceToPlayer <= activationDistance && isAlive;

        if (nowInRange && !isInRange)
        {
            isInRange = true;
            if (gameController != null) gameController.RegisterInteractSource(this);
        }
        else if (!nowInRange && isInRange)
        {
            isInRange = false;
            if (gameController != null) gameController.UnregisterInteractSource(this);
        }
    }

    private void OnDisable()
    {
        if (gameController != null) gameController.UnregisterInteractSource(this);
        isInRange = false;
    }

    public void TriggerDialogue()
    {
        if (gameController != null) gameController.UnregisterInteractSource(this);
        isInRange = false;
        if (playerController != null) playerController.isInConversation = true;
        if (dialogueManager != null) dialogueManager.StartDialogue(dialogue);

    }

    public void TakeDamage(int damage)
    {
        if (!isAlive) return;
        currentHp -= damage;
        if (animator != null) animator.SetTrigger("hit");
        if (hitSound != null && audioSource != null)
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
