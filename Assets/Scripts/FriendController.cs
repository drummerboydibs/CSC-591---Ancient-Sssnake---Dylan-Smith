using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    public float jumpForce = 20f;
    public float baseSpeed;
    public float turnSpeed = 120f;
    public float attackPower = 10f;
    public Rigidbody rb;
    public int maxHP = 100;
    public int currentHP = 100;

    private Animator anim;
    private AnimalSoundController asc;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
        rb = GetComponent<Rigidbody>();
        
        // Reduce sound / chatter when an animal becomes your friend
        asc = GetComponent<AnimalSoundController>();
        asc.minOffset += 4;
        asc.maxOffset += 4;
    }

    // Update is called once per frame
    void Update()
    {
        // Follow player
        // 

        // if enemy is in range, attack!

        if (currentHP == 0)
        {
            Die();
        }

        // Rescue if player falls through world
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.y);
        }

    }

    void Attack()
    {
        anim.SetTrigger("attack");
    }

    public void Die()
    {
        anim.SetTrigger("die");
    }

}
