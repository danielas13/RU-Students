﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveFireBall : MonoBehaviour {

    public int speed = 20;
    public int duration = 2;
    public int damage = 1;    //damage of an attack.
    public int range = 2;
    public List<GameObject> targets = new List<GameObject>();

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
                            Debug.Log("ATTACKED AN ENEMY WITH SPELL");
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