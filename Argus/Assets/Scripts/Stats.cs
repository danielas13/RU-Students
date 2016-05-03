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

        public int armor = 0;
    }
    //The global status object.
    public PlayerStats status = new PlayerStats();
    //indicator for the health/mana bar.
    public StatusIndicator indicator;

	private Transform skeleton;
	private Animator skelAnim; 

    // Use this for initialization
    void Start () {
		skeleton = transform.FindChild("Skeleton");
		skelAnim = skeleton.GetComponent<Animator> ();
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
            indicator.SetArmor(status.armor);
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
                indicator.SetArmor(status.armor);
            }
        }
    }

    public void damagePlayer(int damage)
    {
		skelAnim.SetBool ("hit", true);

        //Reducing the player´s health.
        //If there is no armor. Damage the players health directly.
        if (this.status.armor == 0 )
        {
            this.status.currentHealth -= damage;
        }
        else
        {
            //damage the armor value instead.
            this.status.armor -= 1;
        }

        //updating the indicator.
        if (this.indicator != null)
        {
            indicator.SetHealth(status.currentHealth, status.maxHealth);
            indicator.SetArmor(status.armor);
        }

        //Checking if the player is dead.
        if (this.status.currentHealth <= 0)
        {
            game.KillPlayer(this);
        }
    }

    //Increments the players armor by 1
    public void addArmor()
    {
        this.status.armor += 1;
        if (this.indicator != null)
        {
            indicator.SetArmor(status.armor);
        }
    }

    //increases the player's health.
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

	//increasing the player´s max mana.
	public void increaseMaxMana(int mana)
	{
		this.status.maxMana += mana;

		//indicator.
		if (this.indicator != null)
		{
			indicator.SetMana(status.currentMana, status.maxMana);
		}
	}



    //Spend someof the player's mana.
    public void spendMana(int cost)
    {
        this.status.currentMana -= cost;

        //Mana indicator.
        if (this.indicator != null)
        {
            indicator.SetMana(status.currentMana, status.maxMana);
        }
    }
}
