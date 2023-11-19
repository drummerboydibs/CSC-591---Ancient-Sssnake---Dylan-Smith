using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour
{
    public AudioClip[] clip;
    public AudioSource source;
    public float minOffset = .5f;
    public float maxOffset = 3f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(PlayAudio(1));

    }

   

    IEnumerator PlayAudio(float offset)
    {
        yield return new WaitForSeconds(offset);
        int index = Random.Range(0, clip.Length);

        source.clip = clip[index];
        source.Play();

        offset = Random.Range(minOffset + (clip[index].length / 2), maxOffset);

        StartCoroutine(PlayAudio(offset));

    }


}
