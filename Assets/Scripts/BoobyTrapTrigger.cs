using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrapTrigger : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            StartCoroutine(TriggerBoobyTrapCountDown(audioClips[0].length));
        }
    }

    IEnumerator TriggerBoobyTrapCountDown(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        TriggerBoobyTrap();
    }

    void TriggerBoobyTrap()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
        rb.useGravity = true;
        StartCoroutine(CleanUp());
    }

    IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
