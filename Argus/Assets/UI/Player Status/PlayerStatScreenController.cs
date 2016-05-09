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

    public void RestartText()
    {
        playerStatus = GameObject.Find("Player").GetComponent<Stats>();
        playerCurrency.text = "Soul Essences " + playerStatus.status.score;
        PlayerHealth.text = "Your Health " + playerStatus.status.currentHealth + " / " + playerStatus.status.maxHealth;
        playerGainedHealth.text = "Gained health This run " + playerStatus.status.gainedHealth;

        PlayerMana.text = "Your Mana " + playerStatus.status.currentMana + " / " + playerStatus.status.maxMana;
        PlayerGainedMana.text = "Gained mana This run " + playerStatus.status.gainedMana;

        playerDamage.text = "Your damage " + playerStatus.status.damage;
        playerGainedDamage.text = "Gained damage This run " + playerStatus.status.gainedDamage;

        playerSpellpower.text = "Your Spellpower " + playerStatus.status.spellpower;
        playerGainedSpellpower.text = "Gained spellpower This run " + playerStatus.status.gainedSpellpower;

        Node.text = "Spend currency at the nearest shop to purchase gained attributes in the following runs.";
        Deaths.text = "You have been resurrected " + playerStatus.status.deathCount + " Times";
    }
}