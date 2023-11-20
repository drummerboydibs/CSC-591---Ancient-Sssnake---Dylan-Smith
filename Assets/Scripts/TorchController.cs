using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TorchController : MonoBehaviour
{
    public float activationRange = 2.0f;
    public float distanceToPlayer;
    public bool isBeingCarried = false;
    
    GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        if (!isBeingCarried && distanceToPlayer <= activationRange && Input.GetKeyDown(KeyCode.E))
        {
            isBeingCarried = true;
        }

        if (isBeingCarried )
        {
            transform.position = (player.transform.position + Vector3.back);
        }
    }

}
