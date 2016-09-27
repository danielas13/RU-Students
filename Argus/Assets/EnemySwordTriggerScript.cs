using UnityEngine;
using System.Collections;

public class EnemySwordTriggerScript : MonoBehaviour
{
    private bool attackedShield = false;
    private float shieldCounter = 0;

	public int damage;
	void OnTriggerEnter2D(Collider2D col)
	{

        if(col.CompareTag("Shield"))
        {
            shieldCounter = 0.45f;
        } else if (shieldCounter<= 0)
        {
            if (col.isTrigger != true && col.CompareTag("Player"))
            {
                col.SendMessageUpwards("damagePlayer", damage);
                print("player");
                gameObject.SetActive(false);
            }
        }

	}

    void Update()
    {
        if(shieldCounter >= 0)
        {
            shieldCounter -= Time.deltaTime;
        }
    }
}