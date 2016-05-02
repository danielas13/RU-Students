using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack : MonoBehaviour {

    public int damage = 1;    //damage of an attack.
    public int range = 2;
    public List<GameObject> targets = new List<GameObject>();
    private float attackTimer = 0;
    public int attackCd = 2;

    // Use this for initialization
    void Start () {
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
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(attackTimer <= 0)
            {
                Swing();
                attackTimer = attackCd;
            }
        }
	}

    void Swing()
    {
        Debug.Log("ATTACKED number of enemies = : " + targets.Count);
        if(targets != null)
        {
            for(int i = 0; i < targets.Count; i++)
            {
                if(targets[i] != null)
                {
                    if(Vector2.Distance(targets[i].transform.position, transform.position) < range)
                    {
                        EnemyStats enemy = targets[i].GetComponent<EnemyStats>();
                        if(enemy != null)
                        {
                            Debug.Log("ATTACKED AN ENEMY WITH : " + enemy.status.currentHealth +  " Health");
                            enemy.damageEnemy(damage);
                        }
                    }
                }
            }
        }

        return;
    }
}
