using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemySwordTriggerScript : MonoBehaviour
{
    private bool attackedShield = false;
    private float shieldCounter = 0;

    private List<Collider2D> list = new List<Collider2D>();

    [SerializeField]
    private GameObject particleForBlocking;

	public int damage;
	void OnTriggerEnter2D(Collider2D col)
	{
        list.Add(col);

     /*   if (col.CompareTag("Shield"))
        {
            print("Blocked");
            //  shieldCounter = 0.70f;
            this.gameObject.SetActive(false);
            Instantiate(particleForBlocking,transform.position,transform.rotation);
        }
        
      //  else if (shieldCounter<= 0)
      //  {
            if (col.isTrigger != true && col.CompareTag("Player"))
            {
                col.SendMessageUpwards("damagePlayer", damage);
                gameObject.SetActive(false);
            }
    //    }*/

	}

    void LateUpdate()
    {
        /*
        if(shieldCounter >= 0)
        {
            shieldCounter -= Time.deltaTime;
        }
        */
        bool isBlocked = false;
        bool DamagedPlayer = false;
        Collider2D collision = new Collider2D();
        foreach(Collider2D col in list)
        {
            if (col.CompareTag("Shield")){
                isBlocked = true;
                list.Clear();
                Instantiate(particleForBlocking, transform.position, transform.rotation);
                this.gameObject.SetActive(false);
                return;
            }
            else if (col.CompareTag("Player"))
            {
                DamagedPlayer = true;
                collision = col;
            }
        }
        if (!isBlocked && DamagedPlayer)
        {
            collision.SendMessageUpwards("damagePlayer", damage);
            list.Clear();
            gameObject.SetActive(false);
        }
        list.Clear();
    }
}