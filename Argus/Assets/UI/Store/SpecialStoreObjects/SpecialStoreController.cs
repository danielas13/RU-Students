using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpecialStoreController : MonoBehaviour {

    public GameObject[] Categories;
    public GameObject[] SpellButtons;
    public GameObject[] UpgradeButtons;

    private int[] SpellPriceAmount = new int[3];
    private int[] UpgradeAmount = new int[4];           //SET NEW PRICES HERE FOR EACH CATEGORY. Reset them in the InitiateArray func.

    public Text[] Prices;
    public Text CurrentEssence;
    public Text ErrorMessage;


	private Color basicColor = new Color(0F, 0F, 0F, 1f);
	private Color selectedColor = new Color(244/255f, 244/255f, 244/255f, 244/255f); 

    public Text textArea;
    private string[,] TextArray = new string[4, 4];          //Array containing all the dialogs
    public GameObject[] Pannels; //0 = Upgrades, 1 = enemies
    public GameObject canvas;           //The canvas object.

    private int CategorySelection = 0;
    private int CurrentSelection = 0;
    private float counter = 0.3f;

    private Stats playerStats;
    private Spell playerSpell;

    private bool VertDpadPressed = false;
    private bool HorizDpadPressed = false;

    public GameObject storeSpawner;
    // Use this for initialization
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<Stats>();
        playerSpell = GameObject.Find("Player").GetComponent<Spell>();
        Categories[CategorySelection].GetComponent<Button>().image.color = selectedColor;
        for (int i = 0; i < Categories.Length; i++)
        {
            ResetColor(i);
            Categories[i].GetComponent<Button>().image.color = basicColor;
        }
        Pannels[1].SetActive(false);
		Pannels[0].SetActive(true);
        initiateArray();
        textArea.text = TextArray[CategorySelection, CurrentSelection];
        canvas.SetActive(true);
		SetPrices(SpellPriceAmount);
		ErrorMessage.text = " ";
        Time.timeScale = 0;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Interact"))
            {/*
                if (!canvas.activeSelf)
                {
                    canvas.SetActive(true);
                }
                else
                {
                    canvas.SetActive(false);
                }*/
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Destroy(this.gameObject);
        //canvas.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel")){
            Destroy(this.gameObject);
        }
        if (Input.GetButtonDown("Interact"))
        {
            if(counter <= 0)
            {
                Time.timeScale = 1;
                if (storeSpawner != null)
                {
                    storeSpawner.GetComponent<SpecialStoreSpawner>().EnteredStore = false;
                }
                Destroy(this.gameObject);
            }
        }
        if(counter >= 0)
        {
            counter -= 0.02f;
        }
        if (Input.GetButtonDown("Right") || (Input.GetAxisRaw("DHoriz") >= 1 && !HorizDpadPressed))         //Selecting Categories to the right.
        {
            HorizDpadPressed = true;
            ResetColor(CategorySelection);
            Pannels[CategorySelection].SetActive(false);        //Disabling old selection
            Categories[CategorySelection].GetComponent<Button>().image.color = basicColor;
            if (CategorySelection == Categories.Length - 1)     //The counter is at the rightmost option.
            {
                CategorySelection = 0;
                Categories[CategorySelection].GetComponent<Button>().image.color = selectedColor;
            }
            else                                                //The counter is at the first option.
            {
                CategorySelection = CategorySelection + 1;
                Categories[CategorySelection].GetComponent<Button>().image.color = selectedColor;
            }
            CurrentSelection = 0;
            Pannels[CategorySelection].SetActive(true);        //Enabling current selection
            textArea.text = TextArray[CategorySelection, CurrentSelection];
            ErrorMessage.text = " ";
            if (CategorySelection == 0)                              //The Spell category is selected           //RESETING THE PRICES
            {
                SetPrices(SpellPriceAmount);
            }
            else if (CategorySelection == 1)                        //The upgrade category is selected
            {
                SetPrices(UpgradeAmount);
            }
        }
        if (Input.GetButtonDown("Left") || (Input.GetAxisRaw("DHoriz") <= -1 && !HorizDpadPressed))         //Selecting Categories to the left.
        {
            HorizDpadPressed = true;
            ResetColor(CategorySelection);
            Pannels[CategorySelection].SetActive(false);        //Disabling old selection
            Categories[CategorySelection].GetComponent<Button>().image.color = basicColor;
            if (CategorySelection == 0)     //The counter is at the LeftMost option.
            {
                CategorySelection = Categories.Length - 1;
                Categories[CategorySelection].GetComponent<Button>().image.color = selectedColor;
            }
            else                                                //The counter is at the last option.
            {
                CategorySelection = CategorySelection - 1;
                Categories[CategorySelection].GetComponent<Button>().image.color = selectedColor;
            }
            CurrentSelection = 0;
            Categories[CategorySelection].GetComponent<Button>().image.color = selectedColor;
            Pannels[CategorySelection].SetActive(true);        //Enabling current selection
            textArea.text = TextArray[CategorySelection, CurrentSelection];
            ErrorMessage.text = " ";

            if (CategorySelection == 0)                              //The Spell category is selected           //RESETING THE PRICES
            {
                SetPrices(SpellPriceAmount);
            }
            else if (CategorySelection == 1)                        //The upgrade category is selected
            {
                SetPrices(UpgradeAmount);
            }
        }

        if (CategorySelection == 0)                              //The Spell category is selected //CHECKING INPUT FOR UP AND DOWN IN useButton.
        {
            useButtons(SpellButtons);
        }
        else if (CategorySelection == 1)                        //The upgrade category is selected
        {
            useButtons(UpgradeButtons);
        }

        if (Input.GetButtonDown("Jump"))
        {
            callFunction();
        }


        if (Input.GetAxisRaw("DHoriz") == 0)
        {
            HorizDpadPressed = false;
        }
        if (Input.GetAxisRaw("DVert") == 0)
        {
            VertDpadPressed = false;
        }
    }

    private void SetPrices(int[] lis)           //Sets the appropriate prices.
    {
        for(int i = 0; i < lis.Length; i++)
        {
            Prices[i].text = lis[i].ToString() + "x Soul Essences Required";
        }

        for (int i = lis.Length; i < Prices.Length; i++)        //reseting the rest of the price tags.
        {
            Prices[i].text = " ";
        }
        if(CategorySelection == 1)
        {
            if (playerStats.status.ShadowBlade)
            {
                Prices[0].text = "Equipped";
            }
            else if (playerStats.status.ManaBlade)
            {
                Prices[1].text = "Equipped";
            }
            
        }

    }


    private void callFunction()
    {
        if (CategorySelection == 1)                              //The Upgrade category is selected
        {
            UpgradeButtons[CurrentSelection].GetComponent<Button>().onClick.Invoke();
        }
        else if (CategorySelection == 0)                        //The Enemy category is selected
        {
            SpellButtons[CurrentSelection].GetComponent<Button>().onClick.Invoke();
        }
    }
    void useButtons(GameObject[] lis)                           //Use the down/up keys to navigate through catagory content.
    {
        if (Input.GetButtonDown("Down") || (Input.GetAxisRaw("DVert") <= -1 && !VertDpadPressed))
        {
            VertDpadPressed = true;
            lis[CurrentSelection].GetComponent<Button>().image.color = basicColor;
            if (CurrentSelection == lis.Length - 1)             //The counter is the last option.
            {
                CurrentSelection = 0;
                lis[CurrentSelection].GetComponent<Button>().image.color = selectedColor;
            }
            else                                                //The counter is at the first option.
            {
                CurrentSelection = CurrentSelection + 1;
                lis[CurrentSelection].GetComponent<Button>().image.color = selectedColor;
            }
            textArea.text = TextArray[CategorySelection, CurrentSelection];
            ErrorMessage.text = " ";
        }
        if (Input.GetButtonDown("Up") || (Input.GetAxisRaw("DVert") >= 1 && !VertDpadPressed))
        {
            VertDpadPressed = true;
            lis[CurrentSelection].GetComponent<Button>().image.color = basicColor;
            if (CurrentSelection == 0)     //The counter is at the bottom option.
            {
                CurrentSelection = lis.Length - 1;
                lis[CurrentSelection].GetComponent<Button>().image.color = selectedColor;
            }
            else                           //go down a selection
            {
                CurrentSelection = CurrentSelection - 1;
                lis[CurrentSelection].GetComponent<Button>().image.color = selectedColor;
            }
            textArea.text = TextArray[CategorySelection, CurrentSelection];
            ErrorMessage.text = " ";
        }
    }


    void ResetColor(int x)          //Resets the colors of buttons based on the given category.
    {
        if (x == 0)
        {
            for (int i = 0; i < UpgradeButtons.Length; i++)
            {
                if (i == 0)
                {
                    UpgradeButtons[i].GetComponent<Button>().image.color = selectedColor;
                }
                else
                {
                    UpgradeButtons[i].GetComponent<Button>().image.color = basicColor;
                }

            }
        }
        else if (x == 1)
        {
            for (int i = 0; i < SpellButtons.Length; i++)
            {
                if (i == 0)
                {
                    SpellButtons[i].GetComponent<Button>().image.color = selectedColor;
                }
                else
                {
                    SpellButtons[i].GetComponent<Button>().image.color = basicColor;
                }

            }
        }
    }
    public void ButtonClick()
    {

        textArea.text = TextArray[CategorySelection, CurrentSelection];
    }

    public void PurchaseShadowBlade()
    {
        if (!playerStats.status.ShadowBlade)
        {
            if (playerStats.status.score >= UpgradeAmount[0])
            {
                if (playerStats.status.ManaBlade)
                {
                    Prices[1].text = UpgradeAmount[3].ToString() + "x " + "Essences Required";
                }
                else if (playerStats.status.FireBlade)
                {
                    Prices[2].text = UpgradeAmount[0].ToString() + "x " + "Essences Required";
                }
                playerStats.AddShadowBlade();
                playerStats.status.score -= UpgradeAmount[0];
                Prices[0].text = "Equipped";
                CurrentEssence.text = "Current Essences " + playerStats.status.score;
                playerStats.restart();
            }
            else
            {
                ErrorMessage.text = "Not enough essences!";
            }
        }
        else
        {
            ErrorMessage.text = "Already in use";
        }
    }

    public void PurchaseManaBlade()
    {
        if (!playerStats.status.ManaBlade)
        {
            if (playerStats.status.score >= UpgradeAmount[1])
            {
                if (playerStats.status.ShadowBlade)
                {
                    Prices[0].text = UpgradeAmount[1].ToString() + "x " + "Essences Required";
                }
                if (playerStats.status.FireBlade)
                {
                    Prices[2].text = UpgradeAmount[1].ToString() + "x " + "Essences Required";
                }
                playerStats.AddManaBlade();
                playerStats.status.score -= UpgradeAmount[1];
                Prices[1].text = "Equipped";
                CurrentEssence.text = "Current Essences " + playerStats.status.score;
                playerStats.restart();


            }
            else
            {
                ErrorMessage.text = "Not enough essences!";
            }
        }
        else
        {
            ErrorMessage.text = "Already in use";
        }


    }

    public void PurchaseArmorUpgrade()
    {
        if(playerStats.status.score >= UpgradeAmount[3])
        {
            playerStats.addDamageReduction(2);
            playerStats.status.score -= UpgradeAmount[3];
            UpgradeAmount[3] = UpgradeAmount[3] * 2;
            Prices[3].text = UpgradeAmount[3].ToString() + "x " + "Essences Required";
            CurrentEssence.text = "Current Essence " + playerStats.status.score;
            playerStats.restart();
        }
        else
        {
			ErrorMessage.text = "Not enough essences!";
        }
    }

	public void PurchaseFireBlade()
	{
        if (!playerStats.status.FireBlade)
        {
            if (playerStats.status.score >= UpgradeAmount[2])
            {
                if (playerStats.status.ShadowBlade)
                {
                    Prices[0].text = UpgradeAmount[2].ToString() + "x " + "Essences Required";
                }
                if (playerStats.status.ManaBlade)
                {
                    Prices[1].text = UpgradeAmount[2].ToString() + "x " + "Essences Required";
                }
                playerStats.AddFireBlade();
                playerStats.status.score -= UpgradeAmount[2];
                Prices[2].text = "Equipped";
                CurrentEssence.text = "Current Essences " + playerStats.status.score;
                playerStats.restart();
            }
            else
            {
                ErrorMessage.text = "Not enough essences!";
            }
        }
        else
        {
            ErrorMessage.text = "Already in use";
        }
    }


    public void PurchaseHeal()
    {
        if (!playerSpell.LearnedHeal)
        {
            if (playerStats.status.score >= SpellPriceAmount[0])
            {
                playerSpell.LearnedHeal = true;
                playerStats.status.score -= SpellPriceAmount[0];
                Prices[2].text = "Purchased";
                CurrentEssence.text = "Current Essences " + playerStats.status.score;
                playerStats.restart();
            }
            else
            {
                ErrorMessage.text = "Not enough essences!";
            }
        }
        else
        {
            ErrorMessage.text = "Spell Already known.";
        }
    }
    public void PurchaseFrostRay()
    {
        if (!playerSpell.LearnedFrost)
        {
            if (playerStats.status.score >= SpellPriceAmount[1])
            {
                playerSpell.LearnedFrost = true;
                playerStats.status.score -= SpellPriceAmount[1];
                Prices[1].text = "Purchased";
                CurrentEssence.text = "Current Essences " + playerStats.status.score;
                playerStats.restart();
            }
            else
            {
                ErrorMessage.text = "Not enough essences!";
            }
        }
        else
        {
            ErrorMessage.text = "Spell Already known.";
        }
    }
    public void PurchaseFireRay()
    {
        if (!playerSpell.LearnedFire)
        {
            if (playerStats.status.score >= SpellPriceAmount[2])
            {
                playerSpell.LearnedFire = true;
                playerStats.status.score -= SpellPriceAmount[2];
                Prices[0].text = "Purchased";
                CurrentEssence.text = "Current Essences " + playerStats.status.score;
                playerStats.restart();
            }
            else
            {
                ErrorMessage.text = "Not enough essences!";
            }
        }
        else
        {
            ErrorMessage.text = "Spell Already known.";
        }
    }


    private void initiateArray()
    {
        //private int[] SpellPriceAmount;
        //private int[] UpgradeAmount;

        if(playerStats.status.DamageReduction != 0)
        {
            int amount = playerStats.status.DamageReduction / 2;
            UpgradeAmount[2] = 400 * amount;
        }else
        {
            UpgradeAmount[2] = 400;
        }
        CurrentEssence.text = "Current Essences " + playerStats.status.score;

        UpgradeAmount[0] = 500;
        UpgradeAmount[1] = 500;
		UpgradeAmount[3] = 500;


        SpellPriceAmount[0] = 500;
        SpellPriceAmount[1] = 500;
        SpellPriceAmount[2] = 250;



        //Upgrade texts.
		TextArray[1, 0] = ""; // Blade Upgrade: The Shadowblade upgrade will increase the damage of the player. Only one blade upgrade can be active at any given time.
		TextArray[1, 1] = ""; // Blade Upgrade: The mana upgrade will increase the damage of the player's spells. Only one blade upgrade can be active at any given time.
		TextArray[1, 2] = ""; // The armor upgrade will reduce damage taken by a small amount. Additive upgrade.
		TextArray[1, 3] = ""; 

        //Enemy Texts -knights-wizards-floaters(Revenants)-Spawns
        TextArray[0, 0] = "";
        TextArray[0, 1] = "";
        TextArray[0, 2] = "";


    }
}
