using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatScreenController : MonoBehaviour {


    private Stats playerStatus;

    [SerializeField]
    private Text PlayerHealth;
    [SerializeField]
    private Text playerGainedHealth;
    [SerializeField]
    private Text PlayerMana;
    [SerializeField]
    private Text PlayerGainedMana;
    [SerializeField]
    private Text playerDamage;
    [SerializeField]
    private Text playerGainedDamage;
    [SerializeField]
    private Text playerSpellpower;
    [SerializeField]
    private Text playerGainedSpellpower;
    [SerializeField]
    private Text playerCurrency;
    [SerializeField]
    private Text Node;
    [SerializeField]
    private Text Deaths;

    void Start()
    {
		
        playerStatus = GameObject.Find("Player").GetComponent<Stats>();
        RestartText();

        //RestartText();
    }
	void OnEnable(){
		RestartText();
		Time.timeScale = 0;
	}
	void OnDisable() {
		Time.timeScale = 1;
	}

    public void RestartText()
    {
        playerStatus = GameObject.Find("Player").GetComponent<Stats>();
        playerCurrency.text = "Soul Essences:  " + playerStatus.status.score;
		PlayerHealth.text = "Health: \n " + playerStatus.status.currentHealth + " / " + playerStatus.status.maxHealth + " (+" + playerStatus.status.gainedHealth + " this run)";
		//playerGainedHealth.text = " +" + playerStatus.status.gainedHealth + " this run.";

		PlayerMana.text = "Mana: \n" + playerStatus.status.currentMana + " / " + playerStatus.status.maxMana + " (+" + playerStatus.status.gainedMana + " this run)";
		//PlayerGainedMana.text = " +" + playerStatus.status.gainedMana + " this run.";

		playerDamage.text = "Damage:  \n" + playerStatus.status.minDamage + " - " + playerStatus.status.maxDamage + " (+" + playerStatus.status.gainedDamage + " this run)";
       	//playerGainedDamage.text = " +" + playerStatus.status.gainedDamage + " this run.";
		 
		playerSpellpower.text = "Spellpower: \n " + playerStatus.status.minSpellPower + " - " +playerStatus.status.minSpellPower + " (+" + playerStatus.status.gainedSpellpower + " this run)";
		//playerGainedSpellpower.text = " +" + playerStatus.status.gainedSpellpower + " this run.";

        Node.text = "Use your Soul Essances at an enchanter or an enhancer to improve your attributes or gain special spells and enchants. ";
        Deaths.text = "You have been resurrected " + playerStatus.status.deathCount + " times.";
    }
}