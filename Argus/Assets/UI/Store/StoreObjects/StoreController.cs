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
	private Color highlightedColor; 
	private Color defaultColor;
	private Color darkColor; 
    public Transform canvasPrefab;
	Text textcolor;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenShop();
        }
    }
    void Start()
    {
		darkColor = new Color (0f, 0f, 0f, 1f);
		highlightedColor = new Color(244/255f, 244/255f, 244/255f, 244/255f); 
		defaultColor = new Color(0F, 0F, 0F, 0F);

        /*
        if (sc == null)
        {
            sc = GameObject.FindGameObjectWithTag("Store").GetComponent<StoreController>();
        }*/
        playerStatus = GameObject.Find("Player").GetComponent<Stats>();
        RestartText();
        buttons[SelectedButton].GetComponent<Button>().image.color = highlightedColor;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            buttons[SelectedButton].GetComponent<Button>().image.color = defaultColor;
			textcolor = buttons [SelectedButton].GetComponent<Button> ().GetComponentInChildren<Text> ();
			textcolor.color = highlightedColor;
            if (SelectedButton == buttons.Length-1)
            {
                SelectedButton = 0;
                buttons[SelectedButton].GetComponent<Button>().image.color = highlightedColor;


            }
            else
            {
               SelectedButton = SelectedButton + 1;
               buttons[SelectedButton].GetComponent<Button>().image.color = highlightedColor;
            }
        }

        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            buttons[SelectedButton].GetComponent<Button>().image.color = defaultColor;
            if (SelectedButton == 0)
            {
                SelectedButton = buttons.Length - 1;
                buttons[SelectedButton].GetComponent<Button>().image.color = highlightedColor;
            }
            else
            {
                SelectedButton = SelectedButton-1;
                buttons[SelectedButton].GetComponent<Button>().image.color = highlightedColor;
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
        if(playerStatus.status.gainedHealth>10)
        {
            int healthcost = 100 + (playerStatus.status.maxHealth - 100) * 2;
            if (playerStatus.status.score > healthcost)
            {
                playerStatus.status.score -= healthcost;
               playerStatus.status.gainedHealth -= 10;
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
        if (playerStatus.status.gainedMana > 10)
        {
            int manaCost = 150 + (playerStatus.status.maxMana - 100) * 2;
            if (playerStatus.status.score > manaCost)
            {
                playerStatus.status.score -= manaCost;
                playerStatus.status.gainedMana -= 10;
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
            int damageCost = 400 + (playerStatus.status.minDamage - 15) * 4;
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
            int spellpowerCost = 300 + (playerStatus.status.minSpellPower - 25) * 4;
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
        AvailableHealth.text = "Available Upgrade \n+" + playerStatus.status.gainedHealth;
		AvailableMana.text = "Available Upgrade \n+" + playerStatus.status.gainedMana;
		AvailablePower.text = "Available Upgrade \n+" + playerStatus.status.gainedSpellpower;
		AvailableDamage.text = "Available Upgrade \n+" + playerStatus.status.gainedDamage;

		HealthCost.text = (100 + (playerStatus.status.maxHealth-100 - playerStatus.status.gainedHealth) *2).ToString ();
		ManaCost.text = (150 + (playerStatus.status.maxMana-100 - playerStatus.status.gainedMana) * 2).ToString ();
		DamageCost.text = (400 + (playerStatus.status.minDamage-15 - playerStatus.status.gainedDamage) * 4).ToString ();
		SpellpowerCost.text = (300 + (playerStatus.status.minSpellPower-25 - playerStatus.status.gainedSpellpower) * 4).ToString ();
    }
}
