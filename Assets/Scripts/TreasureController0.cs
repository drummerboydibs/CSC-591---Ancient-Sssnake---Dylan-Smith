using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureController0 : MonoBehaviour
{
    GameObject boulder;
    GameObject shadyMan;
    public float distanceToShadyMan;
    public float activationRange = 1.0f;
    public bool isBeingCarried = false;
    ShadyManController2 smc;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        boulder = GameObject.Find("Boulder");
        shadyMan = GameObject.Find("ShadyMan");
        rb = GetComponent<Rigidbody>();
        smc = GetComponent<ShadyManController2>();

    }

    // Update is called once per frame
    void Update()
    {
        distanceToShadyMan = (transform.position - shadyMan.transform.position).magnitude;

        if (!isBeingCarried && distanceToShadyMan <= activationRange && Input.GetKeyDown(KeyCode.E))
        {
            isBeingCarried = true;
            rb.useGravity = false;
        }
        if (isBeingCarried)
        {
            transform.position = (shadyMan.transform.position + Vector3.back);
            StartCoroutine(ReleaseBoulder());
        }
        else if (smc.isDead)
        {
            StopAllCoroutines();
            CleanUp();
        }
    }

    void CleanUp()
    {
        rb.useGravity = true;
    }

    IEnumerator ReleaseBoulder()
    {
        boulder.SetActive(true);
        yield return new WaitForSeconds(12);
    }
}
