﻿using UnityEngine;
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
        if (Input.GetKeyDown(KeyCode.F) && !attacking) 				//light attack
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

		if(Input.GetKeyDown (KeyCode.Q) && !attacking){		 		//	heavy attack, change binding +later

			// 	!!!!  
			//		TODO Check if grounded 
			//	!!!!
			attacking = true;
			attackCollider.enabled = true;
			attackTimer = attackCooldown + 0.5f; 					//	longer attacks required, if this is removed you can fast attack too soon


			skeletonAnimator.SetInteger("AttackID", 3);				// 	3 = heavy attack animation
			skeletonAnimator.SetBool ("MidSwing", true); 			//	can do no other attack animation while this is true
			skeletonAnimator.SetTrigger ("Swing"); 			//	specific trigger for heavy attack

			//	TODO, disable ALL input for player while this attack is used. Essentially just busy loop the controls or functions handling 
			// 	player movement

			// NOTE: Should only be able to perform when GROUNDED +
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