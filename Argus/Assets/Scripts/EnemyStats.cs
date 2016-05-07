using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour {
    [System.Serializable]
    public class enemyStats
    {

        public int maxHealth = 4;           //Maximum health of enemies
        public int currentHealth = 4;       //Current health of enemies.
        public int damage =5;               //The damage of the enemy.
        public int spellpower = 5;          //TODO: Make this work with spells.
    }
    public EnemyStatusIndicator indicator;
    public GameObject combatText; //Add the canvas prefab to create a floating combat text
    public Color DamageColor;     //The color of the floating combat text that will be created when damage is taken.
    private static readonly System.Random random = new System.Random();     //Create a read only random variable.
    public enemyStats status = new enemyStats();
    public int restoreSpawnChance = 10;
    public Transform manaPrefab, healthPrefab;//mana and health prefabs.
    //function that damages the current enemy.
    void Start()
    {
        indicator.SetHealth(this.status.currentHealth, this.status.maxHealth);
    }
    public void damageEnemy(int damage)
    {
        
        combatText.GetComponent<Text>().text = "-" + damage.ToString();
        combatText.GetComponent<Text>().color = DamageColor;
        Debug.Log("The damage: " + damage + "The health: " + this.status.currentHealth);
        Instantiate(combatText, transform.position, transform.rotation);
        this.status.currentHealth -= damage;                    //add the damage.
        Debug.Log("The health: " + this.status.currentHealth);
        indicator.SetHealth(this.status.currentHealth, this.status.maxHealth);

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
