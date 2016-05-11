using UnityEngine;
using System.Collections;
public class EnemyMeleeAttackTriggerScript : MonoBehaviour {
	public bool withinAttackRange = false;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true && col.CompareTag("Player"))
		{
			withinAttackRange = true;
			//transform.SendMessageUpwards("PlayerWithinRange", col);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.isTrigger != true && col.CompareTag("Player"))
		{
			//Debug.Log("Player Exited");
			withinAttackRange = false;
			//transform.SendMessageUpwards("PlayerExitedRange", col);
		}

	}
}

