using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    //Status class that contains all the health/mana values.
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 10;
        public int maxMana = 10;

        public int currentMana = 10;
        public int currentHealth = 10;
    }
    //The global status object.
    public PlayerStats status = new PlayerStats();
    //indicator for the health/mana bar.
    public StatusIndicator indicator;

    // Use this for initialization
    void Start () {
        //restarting the current health/mana.
        restart();
	}

    //reseting the current health mana and setting the indicator.
    void restart()
    {
        status.currentHealth = status.maxHealth;
        status.currentMana = status.maxMana;
        if (this.indicator != null)
        {
            indicator.SetHealth(status.currentHealth, status.maxHealth);
            indicator.SetMana(status.currentMana, status.maxMana);
        }

    }
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y <= -20)
        {
            damagePlayer(100);
        }
        //Finding a indicator if none is found.
        if (indicator == null)
        {
            GameObject tempIndicator = GameObject.FindGameObjectWithTag("indicator");
            if (tempIndicator == null)
            {
                return;
            }
            else
            {
                indicator = tempIndicator.GetComponent<StatusIndicator>(); ;
                indicator.SetHealth(status.currentHealth, status.maxHealth);
                indicator.SetMana(status.currentMana, status.maxMana);
            }
        }
    }

    public void damagePlayer(int damage)
    {
        //Reducing the player´s health.
        this.status.currentHealth -= damage;

        //updating the indicator.
        if (this.indicator != null)
        {
            indicator.SetHealth(status.currentHealth, status.maxHealth);
        }

        //Checking if the player is dead.
        if (this.status.currentHealth <= 0)
        {
            game.KillPlayer(this);
        }
    }

    public void healPlayer(int heal)
    {
        //Increasing the player´s health. Checking for overheal.
        if((this.status.currentHealth+heal) > this.status.maxHealth)
        {
            this.status.currentHealth = this.status.maxHealth;
        }
        else
        {
            this.status.currentHealth += heal;
        }

        //indicator.
        if (this.indicator != null)
        {
            indicator.SetHealth(status.currentHealth, status.maxHealth);
        }
    }

    //increasing the player´s max health.
    public void increaseMaxHealth(int health)
    {
        this.status.maxHealth += health;

        //indicator.
        if (this.indicator != null)
        {
            indicator.SetHealth(status.currentHealth, status.maxHealth);
        }
    }

    //Spend someof the player's mana.
    public void spendMana(int cost)
    {
        this.status.currentMana -= cost;
        Debug.Log("LeftOver Mana :" + this.status.currentMana);

        //Mana indicator.
        if (this.indicator != null)
        {
            indicator.SetMana(status.currentMana, status.maxMana);
        }
    }
}
