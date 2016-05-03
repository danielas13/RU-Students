﻿using UnityEngine;
using System.Collections;

public class UpgradeStatus : MonoBehaviour {

    public bool RandomSpawn = false;    //Tells us if the player wants health or mana specifically.
    public bool ManaOrHealth = false;        //integer representation of what the user wants. 1 = health, 2 = mana.

    //Status prefabs.
    public Transform ManaPrefab; 
    public Transform HealthPrefab;

    public int Chances = 4;             //the higher this integer is. The less chance there is of upgrade spawn.
    public float heightOfSpawn = 1f;    //The height that the object will spawn above 

    // Use this for initialization
    void Awake () {
        if (!RandomSpawn)
        {
            System.Random rnd = new System.Random();        //Create a Random object.
            int calculate = rnd.Next(1, Chances);           //randomise a number between 1 add the chances variable.

            //check if the randomised number represents a mana upgrade or player upgrade.
            if (calculate < 3)
            {
                if (calculate == 1)                         //The upgrade is a health upgrade.
                {
                    //Create a new healthUpgrade using the spawner's position plus the height of heightofSpawn variable.
                    Instantiate(HealthPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
                }
                else if (calculate == 2)                    //the upgrade is a mana upgrade.
                {
                    //Create a new manaUpgrade using the spawner's position plus the height of heightofSpawn variable.
                    Instantiate(ManaPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
                }
            }
        }
        else    //The user wants the spawner to create a specific upgrade.
        {
            if(ManaOrHealth)
            {
                //Create a new healthUpgrade using the spawner's position plus the height of heightofSpawn variable.
                Instantiate(HealthPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
            }
            else if(!ManaOrHealth)
            {
                //Create a new manaUpgrade using the spawner's position plus the height of heightofSpawn variable.
                Instantiate(ManaPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
            }
        }
        Destroy(this.gameObject);       //Destroy this spawner.
    }
}