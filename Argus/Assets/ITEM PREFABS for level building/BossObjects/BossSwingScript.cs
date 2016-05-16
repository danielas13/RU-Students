using UnityEngine;
using System.Collections;

public class BossSwingScript : MonoBehaviour {

    public float duration = 4;                   //LifeTime of the object
    private float DamageCooldown = 0.5f;      //cooldown on how many times the player will be damaged while staying in the fire.
    private int fireDamage = 10;
    private Stats playerStats;
    private GameObject fireChild;
    public float dist = 0.04f;
 //   private Stats playerStats;

    void Start()
    {
        fireChild = this.transform.GetChild(0).gameObject;
        playerStats = GameObject.Find("Player").GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update () {
	    if(duration <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {

            transform.Translate(Vector2.right * dist);
            fireChild.transform.Translate(Vector2.right * dist/2);
            Vector2 size = this.gameObject.GetComponent<BoxCollider2D>().size;
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(size.x + dist, size.y);
            duration -= Time.deltaTime;
        }
	}
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (DamageCooldown <= 0)        //Damage cooldown while the plauer stays in the trap.
            {
                playerStats.damagePlayer(fireDamage);
                DamageCooldown = 0.5f;
            }
            else
            {
                DamageCooldown -= Time.deltaTime;
            }
        }
    }
}
