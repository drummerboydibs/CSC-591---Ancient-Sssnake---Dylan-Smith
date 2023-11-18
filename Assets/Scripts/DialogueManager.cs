using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private GameObject player;
    private PlayerController playerController;
    
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the queue with the dialogue for the GameObject.
        sentences = new Queue<string>();

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        animator = GameObject.Find("DialogueBackground").GetComponent<Animator>();
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        // Clear the queue of any previous dialogue.
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        // End dialogue if dialogue runs out
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Queue is FIFO. Dequeue grabs the first object in the queue.
        string sentence = sentences.Dequeue();
        // Stop already appearing sentence when a new one starts (if player starts one before previous finishes)
        StopAllCoroutines();
        // Animate text appearing
        StartCoroutine(TypeSentence(sentence));
        
        dialogueText.text = sentence;
        
        //// Allow player to skip
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    DisplayNextSentence();
        //} else if (Input.GetKeyDown(KeyCode.Escape)) 
        //{
        //    EndDialogue();
        //}
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        playerController.isInConversation = false;
        animator.SetBool("isOpen", false);
    }

    IEnumerator TypeSentence (string sentence)
    {
        // Initialize to empty string
        dialogueText.text = "";

        // Break provided sentence into an array of characters so they can be added one at a time
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

}
