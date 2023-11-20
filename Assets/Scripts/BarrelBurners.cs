using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBurners : MonoBehaviour
{
    public GameObject torch;
    public GameObject fire;
    public float distanceToTorch;
    public bool isBurning = false;
    public float activationRange = 3f;
    AudioSource audioSource;

    

    // Start is called before the first frame update
    void Start()
    {
        torch = GameObject.Find("OldHandTorchGroup");
        fire = GameObject.Find("FireGroup");
        fire.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTorch = (transform.position - torch.transform.position).magnitude;
        if (!isBurning && distanceToTorch <= activationRange)
        {
            isBurning = true;
            BurnBarrels();
        }
    }

    void BurnBarrels()
    {
        fire.SetActive(true);
        audioSource.Play();
        StartCoroutine(Extinguish());
    }

    IEnumerator Extinguish()
    {
        yield return new WaitForSeconds(5);
        Destroy(torch);
        Destroy(gameObject);
    }
}
