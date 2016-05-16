using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PillarStatus : MonoBehaviour {

    [System.Serializable]
    public class enemyStats
    {

        public int maxHealth = 40;          //Maximum health of enemies
        public int currentHealth = 40;          //Maximum health of enemies
    }
    public EnemyStatusIndicator indicator;
    public GameObject combatText; //Add the canvas prefab to create a floating combat text
    public Color DamageColor;     //The color of the floating combat text that will be created when damage is taken.
    private static readonly System.Random random = new System.Random();     //Create a read only random variable.
    public enemyStats status = new enemyStats();
    public int restoreSpawnChance = 8;
    public int scoreSpawnChance = 5;


    //function that damages the current enemy.
    void Start()
    {
        indicator.SetHealth(this.status.currentHealth, this.status.maxHealth);
    }
    public void damageEnemy(int damage)
    {

        combatText.GetComponent<Text>().text = "-" + damage.ToString();
        combatText.GetComponent<Text>().color = DamageColor;
        Instantiate(combatText, transform.position, Quaternion.Euler(new Vector3(0, 0, 1)));

        if (this.status.currentHealth > 0)                     //Check if the enemy died.
        {
            this.status.currentHealth -= damage;                    //add the damage.
        }

        indicator.SetHealth(this.status.currentHealth, this.status.maxHealth);

    }

    public void healEnemy(int heal)
    {
        if (this.status.currentHealth < this.status.maxHealth)
        {
            this.status.currentHealth += heal;                    //add the damage.
            indicator.SetHealth(this.status.currentHealth, this.status.maxHealth);
        }
    }
}
