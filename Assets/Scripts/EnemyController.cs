using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHp = 200;
    public int currentHp = 200;
    public int attackPower = 30;
    public float timeSinceLastAttack = 0f;
    public float attackDelay = 1.0f;
    public float activationRange = 2.0f;
    public bool isAlive = true;

    GameController gameController;
    GameObject playerObject;
    PlayerController playerController;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        var gcObj = GameObject.Find("GameController");
        if (gcObj != null) gameController = gcObj.GetComponent<GameController>();
        playerObject = GameObject.Find("Player");
        if (playerObject != null) playerController = playerObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (!isAlive || playerObject == null) return;

        float distance = (transform.position - playerObject.transform.position).magnitude;
        if (distance <= activationRange && timeSinceLastAttack >= attackDelay)
        {
            Attack();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive) return;
        currentHp -= damage;
        if (animator != null) animator.SetTrigger("hit");
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Attack()
    {
        if (animator != null) animator.SetTrigger("attack");
        timeSinceLastAttack = 0;

        // Apply damage to the player when in melee range.
        if (playerController != null)
        {
            playerController.currentHP -= attackPower;
            if (playerController.currentHP < 0) playerController.currentHP = 0;
        }
    }

    void Die()
    {
        isAlive = false;
        if (animator != null) animator.SetTrigger("die");
    }
}
