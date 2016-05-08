using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    //Status class that contains all the health/mana values.
    [System.Serializable]
    public class PlayerStats
    {

        public int maxHealth = 10;          //The player's maximum health pool.
        public int maxMana = 10;            //The player's maximum mana pool.

        public int currentMana = 10;        //The player's current mana.
        public int currentHealth = 10;      //The player's current health.

        [HideInInspector]
        public int gainedHealth = 0;        //the health gained in the current run.
        [HideInInspector]
        public int gainedMana = 0;          //The mana gained in the current run.
        [HideInInspector]
        public int gainedDamage = 0;        //The damage gained in the current run.
        [HideInInspector]
        public int gainedSpellpower = 0;    //the spellpower gianed in the current run.

        public int damage = 3;              //melee damage of the character.
        public int spellpower = 4;          //Damage done with spells.
        public int score = 0;               //Player score.
        public int armor = 0;               //One hit protection count.
    }
    //The global status object.
    public GameObject combatText;
    public Color HealColor;
    public Color DamageColor;
    public Color ManaSpendColor;
    public Color ManaRegainColor;
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
        //restart();
	}

    //reseting the current health mana and setting the indicator.
    public void restart()
    {
        /*status.currentHealth = status.maxHealth;
        status.currentMana = status.maxMana;
        status.gainedDamage = 0;
        status.gainedHealth = 0;
        status.gainedDamage = 0;
        status.gainedSpellpower = 0;*/
        if (this.indicator != null)
        {
            indicator.SetHealth(status.currentHealth, status.maxHealth);
            indicator.SetMana(status.currentMana, status.maxMana);
            indicator.SetArmor(status.armor);
        }
    }
	
	// Update is called once per frame
	void Update () {
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
        skelAnim.SetBool("hit", true);
        combatText.GetComponent<Text>().text = "-" + damage.ToString();
        combatText.GetComponent<Text>().color = DamageColor;
        Instantiate(combatText, transform.position, transform.rotation);

        //Reducing the player´s health.
        //If there is no armor. Damage the players health directly.

        if (this.status.armor > 0)
        {
            this.status.armor -= 1;

        }
        else
        {
            if((this.status.currentHealth - damage) > 0)
            {
                this.status.currentHealth -= damage;
            }
            else
            {
                this.status.currentHealth = 0;
            }
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
            game.KillPlayer();
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
    public void restoreHealth(int heal)
    {
        combatText.GetComponent<Text>().text = "+" + heal.ToString();
        combatText.GetComponent<Text>().color = HealColor;
        Instantiate(combatText, transform.position, transform.rotation);
        //Increasing the player´s health. Checking for overheal.
        if ((this.status.currentHealth+heal) > this.status.maxHealth)
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

    //increases the player's health.
    public void restoreMana(int heal)
    {
        combatText.GetComponent<Text>().text = "+" + heal.ToString();
        combatText.GetComponent<Text>().color = ManaRegainColor;

        Instantiate(combatText, transform.position, transform.rotation);
        //Increasing the player´s health. Checking for overheal.
        if ((this.status.currentMana + heal) > this.status.maxMana)
        {
            this.status.currentMana = this.status.maxMana;
        }
        else
        {
            this.status.currentMana += heal;
        }

        //indicator.
        if (this.indicator != null)
        {
            indicator.SetMana(status.currentMana, status.maxMana);
        }
    }

    //increasing the player´s max health.
    public void increaseMaxHealth(int health)
    {
        this.status.maxHealth += health;
        this.status.gainedHealth += health;

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
        this.status.gainedMana += mana;

        //indicator.
        if (this.indicator != null)
		{
			indicator.SetMana(status.currentMana, status.maxMana);
		}
	}

    //increasing the player´s spellpower.
    public void increaseSpellpower(int power)
    {
        this.status.spellpower += power;
        this.status.gainedSpellpower += power;
    }

    //increasing the player´s damage.
    public void increaseDamage(int damage)
    {
        this.status.damage += damage;
        this.status.gainedDamage += damage;
    }

    //increasing the player's score.
    public void increaseScore(int amount)
    {
        this.status.score += amount;
        //indicator.
        if (this.indicator != null)
        {
            indicator.SetScore(status.score);
        }
    }



    //Spend someof the player's mana.
    public void spendMana(int cost)
    {
        combatText.GetComponent<Text>().text = "-" + cost.ToString();
        combatText.GetComponent<Text>().color = ManaSpendColor;

        Instantiate(combatText, transform.position, transform.rotation);
        this.status.currentMana -= cost;

        //Mana indicator.
        if (this.indicator != null)
        {
            indicator.SetMana(status.currentMana, status.maxMana);
        }
    }

    public void resetGained()
    {
        this.status.gainedHealth = 0;
        this.status.gainedMana = 0;
        this.status.gainedDamage = 0;
        this.status.gainedSpellpower = 0;
    }
}
