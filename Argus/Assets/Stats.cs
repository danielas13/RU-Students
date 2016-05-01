using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 10;
        public int baseMana = 10;

        public int currentHealth = 10;
    }

    public PlayerStats stats = new PlayerStats();

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y <= -20)
        {
            damagePlayer(100);
        }
	}

    public void damagePlayer(int damage)
    {
        this.stats.currentHealth -= damage;
        if(this.stats.currentHealth <= 0)
        {
            game.KillPlayer(this);
        }
    }

    public void healPlayer(int heal)
    {
        if((this.stats.currentHealth+heal) > this.stats.maxHealth)
        {
            this.stats.currentHealth = this.stats.maxHealth;
        }
        else
        {
            this.stats.currentHealth += heal;
        }
    }

    public void increaseMaxHealth(int health)
    {
        this.stats.maxHealth += health;
    }

    public void spendMana(int cost)
    {
        Debug.Log("Spending " + cost + " Mana");
        this.stats.baseMana -= cost;
        Debug.Log("LeftOver Mana :" + this.stats.baseMana);
    }
}
