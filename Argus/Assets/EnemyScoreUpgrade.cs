﻿using UnityEngine;
using System.Collections;

public class EnemyScoreUpgrade : MonoBehaviour {
    public int scoreAmount = 10;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.tag == "Player")
        {
            GameObject character = GameObject.FindGameObjectWithTag("Player");          //find the player object.
            Stats st = character.gameObject.GetComponent<Stats>();                      //Access the player stats.
            st.increaseScore(scoreAmount);                                              //increase the player Health.
            Destroy(this.gameObject);                                                   //Destroy this object.
        }
    }
}
