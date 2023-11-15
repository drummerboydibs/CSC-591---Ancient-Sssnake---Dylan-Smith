using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrapTrigger : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audio;

    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        audio.clip = audioClips[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audio.Play();
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
        audio.clip = audioClips[1];
        audio.Play();
        rb.useGravity = true;
    }

}
