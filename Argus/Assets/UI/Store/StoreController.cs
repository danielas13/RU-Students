using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreController : MonoBehaviour {

    public GameObject ShopCanvas;
    //private static StoreController sc;
    [SerializeField]
    public GameObject[] buttons;
    public int SelectedButton = 0;
    private Stats playerStatus;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text AvailableHealth;
    [SerializeField]
    private Text AvailableMana;
    [SerializeField]
    private Text AvailablePower;
    [SerializeField]
    private Text AvailableDamage;
    [SerializeField]
    private Text HealthCost, ManaCost, DamageCost, SpellpowerCost;

    public Transform canvasPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenShop();
        }
    }
    void Start()
    {
        /*
        if (sc == null)
        {
            sc = GameObject.FindGameObjectWithTag("Store").GetComponent<StoreController>();
        }*/
        playerStatus = GameObject.Find("Player").GetComponent<Stats>();
        RestartText();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            buttons[SelectedButton].GetComponent<Button>().image.color = Color.cyan;
            if (SelectedButton == buttons.Length-1)
            {
                SelectedButton = 0;
                buttons[SelectedButton].GetComponent<Button>().image.color = Color.red;
            }
            else
            {
               SelectedButton = SelectedButton + 1;
               buttons[SelectedButton].GetComponent<Button>().image.color = Color.red;
            }
        }

        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            buttons[SelectedButton].GetComponent<Button>().image.color = Color.cyan;
            if (SelectedButton == 0)
            {
                SelectedButton = buttons.Length - 1;
                buttons[SelectedButton].GetComponent<Button>().image.color = Color.red;
            }
            else
            {
                SelectedButton = SelectedButton-1;
                buttons[SelectedButton].GetComponent<Button>().image.color = Color.red;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            callFunction();
        }
    }

    private void callFunction()
    {
        buttons[SelectedButton].GetComponent<Button>().onClick.Invoke();
    }

    public void OpenShop()
    {
        RestartText();
        SelectedButton = 0;
        //sc.ShopCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
        SelectedButton = 0;
        //sc.ShopCanvas.SetActive(false);
    }


    public void IncreaseHealth()            //Health button pressed.
    {
        if(playerStatus.status.gainedHealth>0)
        {
            int healthcost = 100 + (playerStatus.status.maxHealth - 10) * 15;
            if (playerStatus.status.score > healthcost)
            {
                playerStatus.status.score -= healthcost;
               playerStatus.status.gainedHealth -= 1;
                RestartText();
               playerStatus.restart();
            }
        }
        else
        {
            Debug.Log("No health gained this Run");
        }
        
    }
    public void IncreaseMana()
    {
        if (playerStatus.status.gainedMana > 0)
        {
            int manaCost = 150 + (playerStatus.status.maxMana - 10) * 20;
            if (playerStatus.status.score > manaCost)
            {
                playerStatus.status.score -= manaCost;
                playerStatus.status.gainedMana -= 1;
                RestartText();
                playerStatus.restart();
            }
        }
        else
        {
            Debug.Log("No Mana gained this Run");
        }
    }
    public void IncreaseDamage()
    {
        if (playerStatus.status.gainedDamage > 0)
        {
            int damageCost = 400 + (playerStatus.status.damage - 2) * 40;
            if (playerStatus.status.score > damageCost)
            {
                playerStatus.status.score -= damageCost;
                playerStatus.status.gainedDamage -= 1;
                RestartText();
                playerStatus.restart();
            }
        }
        else
        {
            Debug.Log("No damage gained this Run");
        }
    }
    public void IncreaseSpellpower()
    {
        if (playerStatus.status.gainedSpellpower > 0)
        {
            int spellpowerCost = 300 + (playerStatus.status.spellpower - 3) * 40;
            if (playerStatus.status.score > spellpowerCost)
            {
                playerStatus.status.score -= spellpowerCost;
                playerStatus.status.gainedSpellpower -= 1;
                RestartText();
                playerStatus.restart();
            }
        }
    }

    public void UnlockSpell()
    {
        Debug.Log("Spell");
        RestartText();
    }
    void RestartText()
    {
        scoreText.text = "Soul Essences " + playerStatus.status.score;
        AvailableHealth.text = "Available Health " + playerStatus.status.gainedHealth;
        AvailableMana.text = "Available Mana " + playerStatus.status.gainedMana;
        AvailablePower.text = "Available Spellpower " + playerStatus.status.gainedSpellpower;
        AvailableDamage.text = "Available Damage " + playerStatus.status.gainedDamage;

        HealthCost.text = "Cost  " + (100 + (playerStatus.status.maxHealth-10 - playerStatus.status.gainedHealth) *15);
        ManaCost.text = "Cost  " + (150 + (playerStatus.status.maxMana-10 - playerStatus.status.gainedMana) * 20);
        DamageCost.text = "Cost  " + (400 + (playerStatus.status.damage-2 - playerStatus.status.gainedDamage) * 40);
        SpellpowerCost.text = "Cost  " + (300 + (playerStatus.status.spellpower-3 - playerStatus.status.gainedSpellpower) * 40);
    }
}
