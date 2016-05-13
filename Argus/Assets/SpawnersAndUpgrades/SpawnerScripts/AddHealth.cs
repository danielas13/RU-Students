﻿using UnityEngine;
using System.Collections;

public class AddHealth : MonoBehaviour {

    public int maxHealthPerObject = 10;    //The amount of health that will be gained by this object.
    private int AmountIncrease = 0;
    private static readonly System.Random random = new System.Random();     //Create a read only random variable.

    //checking on collition with objects.
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.tag == "Player")
        {
            //AmountIncrease = random.Next(1, maxHealthPerObject+1);                      //randomise a number between 1 add the maxManaPerObject variable.
            GameObject character = GameObject.FindGameObjectWithTag("Player");          //find the player object.
            Stats st = character.gameObject.GetComponent<Stats>();                      //Access the player stats.
            st.increaseMaxHealth(maxHealthPerObject);                                       //increase the player Health.
            Destroy(this.gameObject);                                                   //Destroy this object.
        }
    }
}
