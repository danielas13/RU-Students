using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMeleeAttack : MonoBehaviour
{

    bool attacking = false;
    float attackTimer = 0;
    float attackCooldown = 0.3f;
    public Collider2D attackCollider;

	private Transform skeleton;
	private Animator skelAnim; 
	//private Transform skeletonBody;

    void Awake()
    {
        attackCollider.enabled = false;
		skeleton = transform.FindChild("Skeleton");
		//skeletonBody = skeleton.FindChild ("Body");
		skelAnim = skeleton.GetComponent<Animator> ();
		//GetAnimationClips(skeleton)
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !attacking)
        {
            attacking = true;
            attackTimer = attackCooldown;
            attackCollider.enabled = true;
			//skelAnim["attack"].AddMixingTransform(skeletonBody);
			skelAnim.SetBool("attack", true);
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