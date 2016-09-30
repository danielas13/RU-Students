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

        public int minDamage = 15;              //min melee damage of the character.
        public int maxDamage = 25;              //max melee damage of the character.
        public int minSpellPower = 4;          //Min damage done with spells.
        public int maxSpellPower = 4;          //Max damage done with spells.
        public int score = 0;               //Player score.
        public int armor = 0;               //One hit protection count.
        public int deathCount = 0;
        public int DamageReduction = 0;

        public bool ManaBlade = false;
        public bool ShadowBlade = false;
        public bool FireBlade = false;


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
	private int fontsizeForStuff = 13;

    public Transform NormalSword;

    public Transform ManaBladeTrans;
    public Transform ShadowBladeTrans;
    public Transform FireBladeTrans;
    public Transform Bubble;


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
        if (this.indicator != null)
        {
            indicator.SetHealth(status.currentHealth, status.maxHealth);
            indicator.SetMana(status.currentMana, status.maxMana);
            indicator.SetArmor(status.armor);
            indicator.SetScore(status.score);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Finding a indicator if none is found.

        if (status.armor > 0)// && !Bubble.gameObject.activeSelf)
        {
            Bubble.gameObject.SetActive(true);
        }
        else
        {
            Bubble.gameObject.SetActive(false);
        }
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

        //transform.GetComponent<PlatformerCharacter2D>().isChanneling = false;
        //  skelAnim.SetBool("hit", true);
        int realDamage =damage;
        if (status.ShadowBlade)
        {
            realDamage += (int)1.5 * damage;
        }
        if(realDamage - status.DamageReduction > 0)
        {
            combatText.GetComponent<Text>().text = "-" + (realDamage - status.DamageReduction).ToString();
        }
        else
        {
            combatText.GetComponent<Text>().text = "-" + 0.ToString();
        }

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
            if((this.status.currentHealth - (realDamage - status.DamageReduction)) > 0)
            {
                if((realDamage - status.DamageReduction) > 0)
                {
                    this.status.currentHealth -= (realDamage - status.DamageReduction);
                }
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
            status.deathCount += 1;
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
    //Increments the players damage reduction by 2
    public void AddShadowBlade()
    {
        NormalSword.gameObject.SetActive(false);
        status.ShadowBlade = true;
        ShadowBladeTrans.gameObject.SetActive(true);
        if (status.ManaBlade)
        {
            status.ManaBlade = false;
            ManaBladeTrans.gameObject.SetActive(false);
        }
        if (status.FireBlade)
        {
            status.FireBlade = false;
            FireBladeTrans.gameObject.SetActive(false);
        }

        //ShadowBladeTrans.gameObject.SetActive(true);
    }
    public void removeSpecialBlades()
    {
        NormalSword.gameObject.SetActive(true);
        if (status.ManaBlade)
        {
            status.ManaBlade = false;
            ManaBladeTrans.gameObject.SetActive(false);
        }
        if (status.FireBlade)
        {
            status.FireBlade = false;
            FireBladeTrans.gameObject.SetActive(false);
        }
        if (status.ShadowBlade)
        {
            ShadowBladeTrans.gameObject.SetActive(false);
            status.ShadowBlade = false;
        }
    }

    //Increments the players damage reduction by 2
    public void addDamageReduction(int amount)
    {
        this.status.DamageReduction += amount;
    }

    //Increments the players damage reduction by 2
    public void AddManaBlade()
    {
        NormalSword.gameObject.SetActive(false);
        status.ManaBlade = true;
        ManaBladeTrans.gameObject.SetActive(true);
        if (status.ShadowBlade)
        {
            ShadowBladeTrans.gameObject.SetActive(false);
            status.ShadowBlade = false;
        }
        if (status.FireBlade)
        {
            status.FireBlade = false;
            FireBladeTrans.gameObject.SetActive(false);
        }
    }
    public void AddFireBlade()
    {
        NormalSword.gameObject.SetActive(false);
        status.FireBlade = true;
        FireBladeTrans.gameObject.SetActive(true);
        if (status.ShadowBlade)
        {
            ShadowBladeTrans.gameObject.SetActive(false);
            status.ShadowBlade = false;
        }
        if (status.ManaBlade)
        {
            status.ManaBlade = false;
            ManaBladeTrans.gameObject.SetActive(false);
        }
    }


    //increases the player's health.
    public void restoreHealth(int heal)
    {
        combatText.GetComponent<Text>().text = "+ " + heal.ToString() + " health restored";
        combatText.GetComponent<Text>().color = HealColor;
		Instantiate(combatText, newPos(transform.position) + Vector3.up, transform.rotation);
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
        combatText.GetComponent<Text>().text = "+" + heal.ToString() + " mana restored";
        combatText.GetComponent<Text>().color = ManaRegainColor;

		Instantiate(combatText, newPos(transform.position), transform.rotation);
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

		combatText.GetComponent<Text>().text = "Maxmimum health increased by " + health.ToString();
		combatText.GetComponent<Text> ().fontSize = fontsizeForStuff;
		combatText.GetComponent<Text>().color = HealColor;
		Instantiate(combatText, newPos(transform.position), transform.rotation);

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
		combatText.GetComponent<Text>().text = "Maxmimum mana increased by " + mana.ToString();
		combatText.GetComponent<Text> ().fontSize = fontsizeForStuff;
		combatText.GetComponent<Text> ().color = HealColor;
		Instantiate(combatText, newPos(transform.position), transform.rotation);
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
		combatText.GetComponent<Text>().text = "Maxmimum spellpower increased by " + power.ToString();
		combatText.GetComponent<Text>().color = HealColor;
		combatText.GetComponent<Text> ().fontSize = fontsizeForStuff;

		Instantiate(combatText, newPos(transform.position), transform.rotation);
        this.status.minSpellPower += power;
        this.status.maxSpellPower += power;
        this.status.gainedSpellpower += power;
    }

    //increasing the player´s damage.
    public void increaseDamage(int damage)
    {
		combatText.GetComponent<Text>().text = "Maxmimum damage increased by " + damage.ToString();
		combatText.GetComponent<Text>().color = HealColor;
		combatText.GetComponent<Text> ().fontSize = fontsizeForStuff;

		Instantiate(combatText, newPos(transform.position), transform.rotation);
        this.status.minDamage += damage;
        this.status.maxDamage += damage;
        this.status.gainedDamage += damage;
    }

    //increasing the player's score.
    public void increaseScore(int amount)
    {
		combatText.GetComponent<Text>().text = amount.ToString()+ " Soul Essences picked up";
		combatText.GetComponent<Text>().color = HealColor;
		combatText.GetComponent<Text> ().fontSize = fontsizeForStuff;

		Instantiate(combatText, newPos(transform.position), transform.rotation);
        this.status.score += amount;
        //indicator.
        if (this.indicator != null)
        {
            indicator.SetScore(status.score);
        }
    }


    public void floatingText(string value)
    {
        combatText.GetComponent<Text>().text = value;
        combatText.GetComponent<Text>().color = ManaSpendColor;
        Instantiate(combatText, newPos(transform.position), transform.rotation);
    }
    //Spend someof the player's mana.
    public void spendMana(int cost)
    {
        combatText.GetComponent<Text>().text = "-" + cost.ToString();
        combatText.GetComponent<Text>().color = ManaSpendColor;
		Instantiate(combatText, newPos(transform.position), transform.rotation);
        this.status.currentMana -= cost;

        //Mana indicator.
        if (this.indicator != null)
        {
            indicator.SetMana(status.currentMana, status.maxMana);
        }
    }

	public Vector3 newPos(Vector3 oldpos){
		oldpos.z -= 5.0f;
		return oldpos;
		
	}
    public void resetGained()
    {
        this.status.gainedHealth = 0;
        this.status.gainedMana = 0;
        this.status.gainedDamage = 0;
        this.status.gainedSpellpower = 0;
    }
}
