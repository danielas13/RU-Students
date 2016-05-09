using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMeleeAttack : MonoBehaviour
{

    bool attacking = false;
    float attackTimer = 0;
    float attackCooldown = 0.6f;
    public Collider2D attackCollider;

	private Animator skelAnim2; 
	private Transform skeleton2;

	private static readonly System.Random randomAttackIDGenerator = new System.Random();   

    void Awake()
    {
        attackCollider.enabled = false;


		skeleton2 = transform.FindChild("Skeleton_warlord");
		skelAnim2 = skeleton2.GetComponent<Animator> ();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !attacking)
        {
            attacking = true;
            attackTimer = attackCooldown;
            attackCollider.enabled = true;

			skelAnim2.SetBool ("MidSwing", true);
			skelAnim2.SetTrigger("Swing1");
			int attackID = randomAttackIDGenerator.Next (0, 4);      
			//Debug.Log (attackID);
			skelAnim2.SetInteger ("AttackID", attackID);
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
				skelAnim2.SetBool ("MidSwing", false);
            }

        }
    }
}