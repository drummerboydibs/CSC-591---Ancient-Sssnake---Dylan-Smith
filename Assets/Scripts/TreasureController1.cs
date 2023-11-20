using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController1 : MonoBehaviour
{
    GameObject boulder;
    GameObject bougie;
    public float activationRange = 1.0f;
    public bool isBeingCarried = false;
    public float distanceToBougie;
    GameObject player;
    public float distanceToPlayer;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        boulder = GameObject.Find("Boulder");
        bougie = GameObject.Find("Bougie");
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = (transform.position - player.transform.position).magnitude;
        distanceToBougie = (transform.position - bougie.transform.position).magnitude;

        if (isBeingCarried && distanceToBougie > activationRange)
        {
            transform.position = (player.transform.position + Vector3.back);
        } 
        else if (isBeingCarried && distanceToBougie <= activationRange)
        {
            bougie.AddComponent<FriendController>();
            CleanUp();
        }
        else if (!isBeingCarried && distanceToPlayer <= activationRange && Input.GetKeyDown(KeyCode.E))
        {
            isBeingCarried = true;
            rb.useGravity = false;
        }
        
    }

    void CleanUp()
    {
        Destroy(boulder);
        Destroy(gameObject);
    }
}
