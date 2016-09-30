﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour {
    [System.Serializable]
    public class enemyStats
    {

        public int maxHealth = 40;          //Maximum health of enemies
        public int currentHealth = 40;      //Current health of enemies.
        public int minDamage = 35;          //The min damage of the enemy.
        public int maxDamage = 45;          //The max damage of the enemy.
        public int spellpower = 40;          //TODO: Make this work with spells.
    }
    public EnemyStatusIndicator indicator;
    public GameObject combatText; //Add the canvas prefab to create a floating combat text
    public Color DamageColor;     //The color of the floating combat text that will be created when damage is taken.
    public Color shadowColor;     //The color of the floating combat text that will be created when damage is taken.
    private static readonly System.Random random = new System.Random();     //Create a read only random variable.
    public enemyStats status = new enemyStats();
    public int restoreSpawnChance = 8;
    public int scoreSpawnChance = 5;
    public Transform manaPrefab, healthPrefab, scorePrefab;//mana and health prefabs.

    public int MaxScore = 20;
    public int MinScore = 10;
    public int deathSource = 1;

    public Transform OnFire;

    //function that damages the current enemy.
    void Start()
    {
        indicator.SetHealth(this.status.currentHealth, this.status.maxHealth);
    }

    public void Ignite(float timer)
    {
        OnFire.gameObject.SetActive(true);
        OnFire.GetComponent<DamageOverTime>().Reset();
    }

    public void damageEnemy(int damage)
    {
        
        combatText.GetComponent<Text>().text = "-" + damage.ToString();
        combatText.GetComponent<Text>().color = DamageColor;
        Instantiate(combatText, transform.position, Quaternion.Euler(new Vector3(0, 0, 1)));
        this.status.currentHealth -= damage;                    //add the damage.
        indicator.SetHealth(this.status.currentHealth, this.status.maxHealth);

        if (this.status.currentHealth <= 0)                     //Check if the enemy died.
        {
            int chance = random.Next(1, restoreSpawnChance + 3);//Generate a random number in order to calculate if a restore item drops.
            if(chance < 3)                                      //checks if an item spawns.
            {
                if(chance == 1)                                 //the restore item spawned is a health increase item.
                {
                    Instantiate(healthPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                }
                else if(chance == 2)                            //the restore item spawned is a mana increase item.
                {
                    Instantiate(manaPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                }
            }
            chance = random.Next(1, scoreSpawnChance + 2);
            if(chance < 3)
            {
                if(chance == 1 || chance == 2)
                {
                    //Object newObj = Instantiate(scorePrefab, new Vector3(transform.position.x+0.5f, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                    Transform obj = (Transform)Instantiate(scorePrefab, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z),transform.rotation);
                }
            }
            /*
            else if (chance == 3)
            {
                GameObject newObj = (GameObject)Instantiate(scorePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                newObj.
           }*/
            game.KillEnemy(this, deathSource);
        }
    }

    public void damageShadowBlade(int damage)
    {

        combatText.GetComponent<Text>().text = "-" + damage.ToString();
        combatText.GetComponent<Text>().color = shadowColor;
        Instantiate(combatText, transform.position + (Vector3.down / 2) + (Vector3.right / 2), Quaternion.Euler(new Vector3(0, 0, 1)));
        this.status.currentHealth -= damage;                    //add the damage.
        indicator.SetHealth(this.status.currentHealth, this.status.maxHealth);

   /*     if (this.status.currentHealth <= 0)                     //Check if the enemy died.
        {
            int chance = random.Next(1, restoreSpawnChance + 3);//Generate a random number in order to calculate if a restore item drops.
            if (chance < 3)                                      //checks if an item spawns.
            {
                if (chance == 1)                                 //the restore item spawned is a health increase item.
                {
                    Instantiate(healthPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                }
                else if (chance == 2)                            //the restore item spawned is a mana increase item.
                {
                    Instantiate(manaPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                }
            }
            chance = random.Next(1, scoreSpawnChance + 2);
            if (chance < 3)
            {
                if (chance == 1 || chance == 2)
                {
                    //Object newObj = Instantiate(scorePrefab, new Vector3(transform.position.x+0.5f, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                    Transform obj = (Transform)Instantiate(scorePrefab, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                }
            }*/
            /*
            else if (chance == 3)
            {
                GameObject newObj = (GameObject)Instantiate(scorePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                newObj.
           }*/
    }

}
