using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingBossStats : MonoBehaviour {
    [System.Serializable]
    public class BossStats
    {

        public int maxHealth = 40;          //Maximum health of enemies
        public int currentHealth = 40;      //Current health of enemies.
        public int minDamage = 35;          //The min damage of the enemy.
        public int maxDamage = 45;          //The max damage of the enemy.
    }
    public EnemyStatusIndicator indicator;
    public Transform Key;         //the loot that the boss will drop.
    public GameObject combatText; //Add the canvas prefab to create a floating combat text
    public Color DamageColor;     //The color of the floating combat text that will be created when damage is taken.
    private static readonly System.Random random = new System.Random();     //Create a read only random variable.
    public BossStats status = new BossStats();
    public GameObject ExitDoor;

    public int MaxScore = 20;
    public int MinScore = 10;

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
        this.status.currentHealth -= damage;                    //add the damage.
        indicator.SetHealth(this.status.currentHealth, this.status.maxHealth);

        if (this.status.currentHealth <= 0)                     //Check if the enemy died.
        {

            Key.gameObject.SetActive(true);
            ExitDoor.GetComponent<OneToOneDoor>().unlock();
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
            /*
            else if (chance == 3)
            {
                GameObject newObj = (GameObject)Instantiate(scorePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                newObj.
           }*/
            // game.KillEnemy(this);
        }
    }

    public void resetFloatingBossStatus()
    {
        this.status.currentHealth = this.status.maxHealth;                    //add the damage.
        indicator.SetHealth(this.status.currentHealth, this.status.maxHealth);
    }
}
