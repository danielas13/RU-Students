using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
    [System.Serializable]
    public class enemyStats
    {
        public int currentHealth = 4;       //Current health of enemies.
        public int damage =5;               //The damage of the enemy.
        public int spellpower = 5;          //TODO: Make this work with spells.
    }

    private static readonly System.Random random = new System.Random();     //Create a read only random variable.
    public enemyStats status = new enemyStats();
    public int restoreSpawnChance = 10;
    public Transform manaPrefab, healthPrefab;//mana and health prefabs.
    //function that damages the current enemy.
    public void damageEnemy(int damage)
    {
        
        this.status.currentHealth -= damage;                    //add the damage.
        if (this.status.currentHealth <= 0)                     //Check if the enemy died.
        {
            int chance = random.Next(1, restoreSpawnChance + 1);//Generate a random number in order to calculate if a restore item drops.
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
            game.KillEnemy(this);
        }
    }
}
