using UnityEngine;
using System.Collections;
public class EnemyAttack : MonoBehaviour
{

	Transform attackTrigger;
	float attackDelay = 0.5f;
	float attackTimer = 0;
	float attackCooldown = 1;

	public Animator knightAnim; // Animator for Enemy 
	private Transform knight;

	void Start()
	{
		knight = transform.FindChild("Knight");
		knightAnim = knight.GetComponent<Animator> ();
	}
	void Update()
	{
		if (attackTrigger == null)
		{
			attackTrigger = transform.FindChild("EnemyMeleeAttackTrigger");
		}

		EnemyMeleeAttackTriggerScript AttackTriggerComponent = attackTrigger.gameObject.GetComponent<EnemyMeleeAttackTriggerScript>(); // Fetch AttackTriggerScript and store as EnemyMeleeAttackTriggerScript variable


		if (AttackTriggerComponent.withinAttackRange == true && AttackTriggerComponent.attacking == false) // no attack active and player is within attack range, commence new attack
		{
			AttackTriggerComponent.attacking = true;
			attackTimer = attackCooldown;
			knightAnim.SetBool("attack", AttackTriggerComponent.attacking);
		}

		if (AttackTriggerComponent.attacking) 	// If we are already attacking
		{
			if (attackTimer > 0)				// we are mid attack, decrement timer
			{
				attackTimer -= Time.deltaTime;
			}
			else 								// attack has finished
			{
				AttackTriggerComponent.attacking = false;
		
			}

			if(AttackTriggerComponent.withinAttackRange == true && attackTimer < attackDelay)  // after a delay during the attack, we decide to dish out the D
			{
				AttackTriggerComponent.playerTakesDamage = true;
				AttackTriggerComponent.attacking = false;
			}


		}
	}


//	void PlayerWithinRange(Collider2D col){
//		Debug.Log ("zup homie from " +  col.name);
//	}
//
//	void PlayerExitedRange(Collider2D col){ 
//		Debug.Log ("bye homie from "  +  col.name);
//	}

}

