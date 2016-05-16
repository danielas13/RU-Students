using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeraldController : MonoBehaviour {

    public GameObject[] Categories;
    public GameObject[] EnemyButtons;
    public GameObject[] UpgradeButtons;
    public GameObject[] LoreButtons;
    public GameObject[] RestorableButtons;
    public GameObject[] ControlsButtons;
    public GameObject[] MiscButtons;
	private Color basicColor = new Color(0F, 0F, 0F, 0F);
	private Color selectedColor = new Color(244/255f, 244/255f, 244/255f, 244/255f); 

    public Text textArea;
    private string[,] TextArray = new string[6,6];          //Array containing all the dialogs
	private GameObject[,] ImageArray = new GameObject[6,6];
    public GameObject[] Pannels; //0 = Upgrades, 1 = enemies, 2 = Lore.
    public GameObject canvas;           //The canvas object.

    private int CategorySelection = 0;
    private int CurrentSelection = 0;
    // Use this for initialization
    void Start () {
        Categories[CategorySelection].GetComponent<Button>().image.color = selectedColor;
        for (int i = 0; i < Categories.Length; i++)
        {
            ResetColor(i);
            Categories[i].GetComponent<Button>().image.color = basicColor;
        }
		initiateArray();

        Pannels[1].SetActive(false);
        Pannels[2].SetActive(false);
        Pannels[3].SetActive(false);
        Pannels[4].SetActive(false);
        Pannels[5].SetActive(false);
		Pannels[CategorySelection].SetActive(true);
		if(CategorySelection!=4){
			ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (true);	
		}
   
        textArea.text = TextArray[CategorySelection, CurrentSelection];
        canvas.SetActive(true);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (!canvas.activeSelf)
                {
                    canvas.SetActive(true);
                }
                else
                {
                    canvas.SetActive(false);
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        canvas.SetActive(false);
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))         //Selecting Categories to the right.
        {
			if(CategorySelection!=4){
				ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (false);	
			}
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
			if(CategorySelection!=4){
				ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (true);	
			}

            textArea.text = TextArray[CategorySelection, CurrentSelection];
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))         //Selecting Categories to the left.
        {
			if(CategorySelection!=4){
				ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (false);	
			}
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
			if(CategorySelection!=4){
				ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (true);	
			}
            textArea.text = TextArray[CategorySelection, CurrentSelection];
        }

        if(CategorySelection == 0)                              //The Upgrade category is selected
        {
            useButtons(UpgradeButtons);
        }
        else if (CategorySelection == 1)                        //The Enemy category is selected
        {
            useButtons(EnemyButtons);
        }
        else if (CategorySelection == 2)                        //The Lore category is selected
        {
            useButtons(LoreButtons);
        }
        else if (CategorySelection == 3)                        //The Restorables category is selected
        {
            useButtons(RestorableButtons);
        }
        else if (CategorySelection == 4)                        //The Restorables category is selected
        {
            useButtons(ControlsButtons);
        }
        else if (CategorySelection == 5)                        //The Restorables category is selected
        {
            useButtons(MiscButtons);
        }

        
    }

    private void callFunction()
    {
        if (CategorySelection == 0)                              //The Upgrade category is selected
        {
            UpgradeButtons[CurrentSelection].GetComponent<Button>().onClick.Invoke();
        }
        else if (CategorySelection == 1)                        //The Enemy category is selected
        {
            EnemyButtons[CurrentSelection].GetComponent<Button>().onClick.Invoke();
        }
        else if (CategorySelection == 2)                        //The Lore category is selected
        {
            LoreButtons[CurrentSelection].GetComponent<Button>().onClick.Invoke();
        }
        else if (CategorySelection == 3)                        //The Restorables category is selected
        {
            RestorableButtons[CurrentSelection].GetComponent<Button>().onClick.Invoke();
        }
        else if (CategorySelection == 4)                        //The Restorables category is selected
        {
            ControlsButtons[CurrentSelection].GetComponent<Button>().onClick.Invoke();
        }
        else if (CategorySelection == 5)                        //The Restorables category is selected
        {
            MiscButtons[CurrentSelection].GetComponent<Button>().onClick.Invoke();
        }
    }

    void useButtons(GameObject[] lis)                           //Use the down/up keys to navigate through catagory content.
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
			if(lis.Length > 0){
				ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (false);
				lis[CurrentSelection].GetComponent<Button>().image.color = basicColor;
				if (CurrentSelection == lis.Length - 1)             //The counter is the last option.
				{
					CurrentSelection = 0;
					lis[CurrentSelection].GetComponent<Button>().image.color = selectedColor;
					ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (true);
				}
				else                                                //The counter is at the first option.
				{
					CurrentSelection = CurrentSelection + 1;
					lis[CurrentSelection].GetComponent<Button>().image.color = selectedColor;
					ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (true);
				}
				textArea.text = TextArray[CategorySelection, CurrentSelection];
			}

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))            
        {
			if (lis.Length > 0) {
				ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (false);
				lis [CurrentSelection].GetComponent<Button> ().image.color = basicColor;
				if (CurrentSelection == 0) {     //The counter is at the bottom option.
					CurrentSelection = lis.Length - 1;
					lis [CurrentSelection].GetComponent<Button> ().image.color = selectedColor;
					ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (true);

				} else {                           //go down a selection
					CurrentSelection = CurrentSelection - 1;
					lis [CurrentSelection].GetComponent<Button> ().image.color = selectedColor;
					ImageArray [CategorySelection, CurrentSelection].gameObject.SetActive (true);
				}
				textArea.text = TextArray [CategorySelection, CurrentSelection];
			}
        }
    }


    void ResetColor(int x)          //Resets the color of a given category ID to cyan(PLACEHOLDER)
    {
        if (x == 0)
        {
            for(int i = 0; i < UpgradeButtons.Length; i++)
            {
                if(i== 0)
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
            for (int i = 0; i < EnemyButtons.Length; i++)
            {
                if (i == 0)
                {
                    EnemyButtons[i].GetComponent<Button>().image.color = selectedColor;
                }
                else
                {
                    EnemyButtons[i].GetComponent<Button>().image.color = basicColor;
                }
                
            }
        }
        else if (x == 2)
        {
            for (int i = 0; i < LoreButtons.Length; i++)
            {
                if (i == 0)
                {
                    LoreButtons[i].GetComponent<Button>().image.color = selectedColor;
                }
                else
                {
                    LoreButtons[i].GetComponent<Button>().image.color = basicColor;
                }
                
            }
        }
        else if (x == 3)
        {
            for (int i = 0; i < RestorableButtons.Length; i++)
            {
                if (i == 0)
                {
                    RestorableButtons[i].GetComponent<Button>().image.color = selectedColor;
                }
                else
                {
                    RestorableButtons[i].GetComponent<Button>().image.color = basicColor;
                }
                
            }
        }
        else if (x == 4)
        {
            for (int i = 0; i < ControlsButtons.Length; i++)
            {
                if (i == 0)
                {
                    ControlsButtons[i].GetComponent<Button>().image.color = selectedColor;
                }
                else
                {
                    ControlsButtons[i].GetComponent<Button>().image.color = basicColor;
                }

            }
        }
        else if (x == 5)
        {
            for (int i = 0; i < MiscButtons.Length; i++)
            {
                if (i == 0)
                {
                    MiscButtons[i].GetComponent<Button>().image.color = selectedColor;
                }
                else
                {
                    MiscButtons[i].GetComponent<Button>().image.color = basicColor;
                }

            }
        }
    }
    public void ButtonClick()
    {

        textArea.text = TextArray[CategorySelection,CurrentSelection];
        Debug.Log(CategorySelection + " " + CurrentSelection);
    }

    private void initiateArray()
    {
		for(int i = 0; i < UpgradeButtons.Length; i++){
			ImageArray[0,i] = (UpgradeButtons [i].transform.GetChild (1).transform.gameObject);
			ImageArray [0, i].SetActive (false);
		}
		for(int i = 0; i < EnemyButtons.Length; i++){
			ImageArray[1,i] = (EnemyButtons [i].transform.GetChild (1).transform.gameObject);
			ImageArray [1, i].SetActive (false);
		}
		for(int i = 0; i < LoreButtons.Length; i++){
			ImageArray[2,i] =(LoreButtons [i].transform.GetChild (1).transform.gameObject);
			ImageArray [2, i].SetActive (false);
		}
		for(int i = 0; i < RestorableButtons.Length; i++){
			ImageArray[3,i] =(RestorableButtons [i].transform.GetChild (1).transform.gameObject);
			ImageArray [3, i].SetActive (false);
		}
		for(int i = 0; i < ControlsButtons.Length; i++){
			//#4
		}
		for(int i = 0; i < MiscButtons.Length; i++){
			ImageArray[5,i] =(MiscButtons [i].transform.GetChild (1).transform.gameObject);
			ImageArray [5, i].SetActive (false);
		}


        //Upgrade texts.
        TextArray[0,0] = "Health upgrades will increase your maximum health for the current run. These upgrades can be purchased permanently in the upgrade store.";
        TextArray[0,1] = "Mana upgrades will increase your maximum mana for the current run. These upgrades can be purchased permanently in the upgrade store.";
        TextArray[0,2] = "Damage upgrades will increase the damage you deal with melee attacks. These upgrades can be purchased permanently in the upgrade store.";
        TextArray[0,3] = "Spellpower upgrades will increase the damage you deal with spells. These upgrades can be purchased permanently in the upgrade store.";
        TextArray[0,4] = "Essences are your currency in the game. They drop from killing enemies and looting objects. These can be spend in the store to buy upgrades but will be lost at death.";



        //Enemy Texts -knights-wizards-floaters(Revenants)-Spawns
        TextArray[1, 0] = "Knights are firece warriors who will run towards you when you get too close and attack you with melee attacks.";
        TextArray[1, 1] = "Wizards will cast harmful spells towards you while in range, but will start to flee when the you approach them.";
        TextArray[1, 2] = "Revenants are flying beings that will fly towards you and cast harmful spells. He will try to avoid you if you get too close.";
        TextArray[1, 3] = "Enemies will recover the ground that you previously cleared out. So beware tracing your steps.";

        //Lore Texts -Something-something TODO
        TextArray[2, 0] = "Adventurers have come to your crypt in order to plunder it of all of it´s treasures and secrets. You are one of the few remaining guardians of the crypt  and it is your job to rid it of these pathetic invaders.";
        TextArray[2, 1] = "The Crypt is the home of many generations of kings, queens and other great beings. It is famed for it's guardians and adventurers and heroes often test their mantle by entering the crypt and try to escape with not only their lives, but some valuables as well. A huge wave of adventurers has entered the crypt and the threat to the crypt and it´s inhabitants is greater than ever. ";

        //Refillables Texts - health-Mana
        TextArray[3, 0] = "Health restore globes are red globes that will drop from enemies and from destroyable crates or barrels. These orbs will give the player some of his health back.";
        TextArray[3, 1] = "Mana restore globes are blue globes that drop form enemies and from destroyable objects. These orbs will restore the player's mana.";

        //Control Texts Move-Jump-Attack-castSpells-Interract
        TextArray[4, 0] = "The player can move using the w,a,s,d buttons as well as the arrow keys";
        TextArray[4, 1] = "The player can jump using spacebar.";
        TextArray[4, 2] = "The player can swing his weapon using the F key.";
        TextArray[4, 3] = "The player can cast harmful spells using the E key.";
        TextArray[4, 4] = "The player can Interract with the shop or Herald using the U key.";

        //Misc
        TextArray[4, 0] = "The player can swing his weapon using the F key.";
        TextArray[4, 1] = "The player can cast harmful spells using the E key.";
        TextArray[4, 2] = "The player can Interract with the shop or Herald using the U key.";
        //Misc, Destructables-Spaners
        TextArray[5, 0] = "The player can destroy some crates or barrels he passes by attacking them with his melee attack. These objects might drop some valuables or restorables.";
        TextArray[5, 1] = "After the player resurrects, some of the objects he encountered previously will return so he might be able to find them again.";
    }
}