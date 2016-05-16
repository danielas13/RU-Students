using UnityEngine;
using System.Collections;

public class EnemySwordTriggerScript : MonoBehaviour {

	public int damage;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true && col.CompareTag("Player"))
		{
			col.SendMessageUpwards("damagePlayer", damage);
<<<<<<< HEAD
			gameObject.SetActive(false);
		}

=======
            gameObject.SetActive(false);
        }
        
>>>>>>> origin/master
	}
}
