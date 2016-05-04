using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveFireBall : MonoBehaviour {

    public int speed = 20;
    public int duration = 2;
    public int range = 2;
    public List<GameObject> targets = new List<GameObject>();

    private int damage = 2;


    // Use this for initialization
    void Start () {
        GameObject character = GameObject.FindGameObjectWithTag("Player");
        Stats player = character.GetComponent<Stats>();
        damage = player.status.spellpower;

        GameObject[] totalTargets = GameObject.FindGameObjectsWithTag("enemy");
        if (totalTargets != null)
        {
            for (int i = 0; i < totalTargets.Length; i++)
            {
                targets.Add(totalTargets[i]);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (targets != null)
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
        }

        transform.Translate(Vector3.right * Time.deltaTime * speed);
        Destroy(this.gameObject,duration);
    }
}
