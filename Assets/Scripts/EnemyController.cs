using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHp = 200;
    public int currentHp = 200;
    public int attackPower = 30;
    public float timeSinceLastAttack = 0f;
    public float attackDelay = 1.0f;
    public float activationRange = 2.0f;

    GameController gameController;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    void Attack()
    {
        animator.SetTrigger("attack");
        timeSinceLastAttack = 0;
    }

    void Die()
    {
        animator.SetTrigger("die");

    }
}
