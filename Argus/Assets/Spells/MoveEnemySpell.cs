﻿using UnityEngine;
using System.Collections;
public class MoveEnemySpell : MonoBehaviour {
	public int speed = 20;
	public int duration = 2;
    private static readonly System.Random randomSpellPowerGenerator = new System.Random();
    public int minDamage = 35;      //damage of an attack
    public int maxDamage = 45;      
	public float initDelay = 0f;
	//private GameObject player;
    // Update is called once per frame


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Shield"))
        {
            Destroy(this.gameObject);
        }
        //Debug.Log("Collison " + col.name);
        if (col.isTrigger != true && col.gameObject.CompareTag("Player"))
        {
            // Debug.Log("git deddd");
            
            col.gameObject.SendMessageUpwards("damagePlayer", randomSpellPowerGenerator.Next(minDamage, maxDamage));
            Destroy(this.gameObject);
        }
        /*if (col.isTrigger != true && col.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Wall Collision");
            Destroy(this.gameObject);
        }*/

    }
    void Start()
	{
		//player = GameObject.FindGameObjectWithTag("Player");
	}
	void Update () {
		/*if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		if(Vector2.Distance(transform.position, player.transform.position) < range)
		{
			Stats playerStat = player.GetComponent<Stats>();
			playerStat.damagePlayer(damage);
			Destroy(this.gameObject);
		}*/

		initDelay = initDelay - Time.deltaTime;
		if(initDelay < 0){
		transform.Translate(Vector3.right * Time.deltaTime * speed);
		Destroy(this.gameObject, duration);
		}
	}
}