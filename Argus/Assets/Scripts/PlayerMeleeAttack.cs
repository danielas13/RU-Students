using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMeleeAttack : MonoBehaviour
{

    bool attacking = false;
    float attackTimer = 0;
    float attackCooldown = 0.3f;
    public Collider2D attackCollider;

    void Awake()
    {
        attackCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !attacking)
        {
            attacking = true;
            attackTimer = attackCooldown;
            attackCollider.enabled = true;
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackCollider.enabled = false;
            }
        }
    }
}