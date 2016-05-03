using UnityEngine;
using System.Collections;
public class EnemyMeleeAttackTriggerScript : MonoBehaviour {
	public bool withinAttackRange = false;
	public bool attacking = false;
	public bool playerTakesDamage = false;
	public int damage = 5;
	private Collider2D collide;

	void OnTriggerEnter2D(Collider2D col)
	{
		collide = col;
		if (col.isTrigger != true && col.CompareTag("Player"))
		{
			withinAttackRange = true;
			//transform.SendMessageUpwards("PlayerWithinRange", col);
		}


	}

	void OnTriggerExit2D(Collider2D col)
	{
		collide = null;
		if (col.isTrigger != true && col.CompareTag("Player"))
		{
			Debug.Log("Player Exited");
			withinAttackRange = false;
			//transform.SendMessageUpwards("PlayerExitedRange", col);
		}

	}


		
	void Update()
	{

		if(collide == null){ //when player instance is deleted
			withinAttackRange = false;
		}

		if (playerTakesDamage && collide != null && collide.CompareTag("Player"))
		{
			collide.SendMessageUpwards("damagePlayer", damage);
			//Debug.Log("Attack msg sent to Player");
			playerTakesDamage = false;
		}
			
	}
}

