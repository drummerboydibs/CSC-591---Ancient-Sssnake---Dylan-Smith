using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSoundController : MonoBehaviour
{
    public AudioClip[] animalSound;
    public AudioSource animalSoundPlayer;
    public float minOffset = .5f;
    public float maxOffset = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        animalSoundPlayer = GetComponent<AudioSource>();
        StartCoroutine(PlayAudio(1));
    }

    
    IEnumerator PlayAudio(float offset)
    {
        yield return new WaitForSeconds(offset);
        int index = Random.Range(0, animalSound.Length);

        animalSoundPlayer.clip = animalSound[index];
        animalSoundPlayer.Play();

        offset = Random.Range(minOffset + animalSound[index].length, maxOffset);

        StartCoroutine(PlayAudio(offset));

    }

}
