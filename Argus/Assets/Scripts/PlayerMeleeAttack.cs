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

	private Animator skeletonAnimator;
	private Transform skeletonFootman;

	private static readonly System.Random randomAttackIDGenerator = new System.Random();   

    void Awake()
    {
        attackCollider.enabled = false;


		//skeleton2 = transform.FindChild("Skeleton_warlord");
		//skelAnim2 = skeleton2.GetComponent<Animator> ();

		//Skeleton Footman
		skeletonFootman = transform.FindChild ("ToFlip").FindChild ("Skeleton_footman");
		skeletonAnimator = skeletonFootman.GetComponent <Animator> ();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !attacking)
        {
            attacking = true;
            attackTimer = attackCooldown;
            attackCollider.enabled = true;

			int attackID = randomAttackIDGenerator.Next (0, 2);      
			//Debug.Log (attackID);
			skeletonAnimator.SetInteger("AttackID", attackID);

			skeletonAnimator.SetBool ("MidSwing", true);
			skeletonAnimator.SetTrigger ("Swing");
			//skelAnim2.SetBool ("MidSwing", true);
			//skelAnim2.SetTrigger("Swing1");

			//skelAnim2.SetInteger ("AttackID", attackID);
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
				skeletonAnimator.SetBool ("MidSwing", false);
				//skelAnim2.SetBool ("MidSwing", false);
            }

        }
    }
}