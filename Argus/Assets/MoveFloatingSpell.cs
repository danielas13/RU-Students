using UnityEngine;
using System.Collections;

public class MoveFloatingSpell : MonoBehaviour {
    private static readonly System.Random randomSpellPowerGenerator = new System.Random();
    public int speed = 20;
    public int duration = 2;
    public int minDamage = 35;    //damage of an attack.
    public int maxDamage = 45;
    public int range = 2;
    private bool Stop = false;
    //private GameObject player;
    // Update is called once per frame
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        //Debug.Log("Collison " + col.name);
        if (col.isTrigger != true && col.gameObject.CompareTag("Player"))
        {
            
            //Destroy(this.gameObject);

            col.gameObject.SendMessageUpwards("damagePlayer", randomSpellPowerGenerator.Next(minDamage, maxDamage));
            transform.FindChild("Trail").FindChild("Impact").gameObject.SetActive(true);
            // transform.GetComponent<CircleCollider2D>().gameObject.SetActive(false);
            Stop = true;
        }
        /*if (col.isTrigger != true && col.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Wall Collision");
            Destroy(this.gameObject);
        }*/

    }
    void Update()
    {
        /* if (player == null)
         {
             player = GameObject.FindGameObjectWithTag("Player");
         }
         if (Vector2.Distance(transform.position, player.transform.position) < range)
         {
             Stats playerStat = player.GetComponent<Stats>();
             playerStat.damagePlayer(5);
             Destroy(this.gameObject);
         }*/
        if (!Stop)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            Destroy(this.gameObject, duration);
        }
    }
}
