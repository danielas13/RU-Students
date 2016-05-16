using UnityEngine;
using System.Collections;

public class EnemySwordTriggerScript : MonoBehaviour {

	public int damage;
	void OnTriggerEnter2D(Collider2D col)
	{
		//Debug.Log ("COLLIDEEEE" + col.name);
		if (col.isTrigger != true && col.CompareTag("Player"))
		{
			col.SendMessageUpwards("damagePlayer", damage);
            gameObject.SetActive(false);
        }
        
	}
}
