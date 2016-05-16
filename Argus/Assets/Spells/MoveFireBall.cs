using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveFireBall : MonoBehaviour {
    private static readonly System.Random randomSpellPowerGenerator = new System.Random();
    public int speed = 20;
    public int duration = 2;
    public int range = 2;

    private int damage = 2;

    private Stats player;

    // Use this for initialization
    void Start () {
        GameObject character = GameObject.FindGameObjectWithTag("Player");
        player = character.GetComponent<Stats>();
        Physics2D.IgnoreLayerCollision(10, 12, false);
        //transform.FindChild("MovedTrail").FindChild("Trail").gameObject.SetActive(true);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        

        if (col.isTrigger != true && col.gameObject.CompareTag("enemy"))
        {
            damage = randomSpellPowerGenerator.Next(player.status.minSpellPower, player.status.maxSpellPower);
            col.gameObject.SendMessageUpwards("damageEnemy", damage);
            //transform.FindChild("MovedTrail").FindChild("Explosion").gameObject.SetActive(true);
            Destroy(this.gameObject);

        }
        if (col.isTrigger != true && col.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Wall Collision");
            // Destroy(this.gameObject);
        }

    }
    // Update is called once per frame
    void Update () {




        transform.Translate(Vector3.right * Time.deltaTime * speed);
        Destroy(this.gameObject, duration);


        /*if (targets != null)
          {
              for (int i = 0; i < targets.Count; i++)
              {
                  if (targets[i] != null)
                  {
                      if (Vector2.Distance(targets[i].transform.position, transform.position) < range)
                      {
                          EnemyStats enemy = targets[i].GetComponent<EnemyStats>();
                          if (enemy != null)
                          {
                              Debug.Log("Spell did : " + damage + " damage.");
                              enemy.damageEnemy(damage);
                              Destroy(this.gameObject);
                          }
                      }
                  }
              }
          }*/


    }
}
