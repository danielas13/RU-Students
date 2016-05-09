using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeraldController : MonoBehaviour {

    public GameObject[] Categories;
    public GameObject[] EnemyButtons;
    public GameObject[] UpgradeButtons;
    public GameObject[] LoreButtons;
    public GameObject[] RestorableButtons;

    public GameObject[] Pannels; //0 = Upgrades, 1 = enemies, 2 = Lore.

    private int CategorySelection = 0;
    private int CurrentSelection = 0; 
    // Use this for initialization
    void Start () {
        Categories[CategorySelection].GetComponent<Button>().image.color = Color.red;
        Pannels[1].SetActive(false);
        Pannels[2].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.RightArrow))         //Selecting Categories to the right.
        {
            Pannels[CategorySelection].SetActive(false);        //Disabling old selection
            Categories[CategorySelection].GetComponent<Button>().image.color = Color.cyan;
            if (CategorySelection == Categories.Length - 1)     //The counter is at the rightmost option.
            {
                CategorySelection = 0;
                Categories[CategorySelection].GetComponent<Button>().image.color = Color.red;
            }
            else                                                //The counter is at the first option.
            {
                CategorySelection = CategorySelection + 1;
                Categories[CategorySelection].GetComponent<Button>().image.color = Color.red;
            }
            CurrentSelection = 0;
            Pannels[CategorySelection].SetActive(true);        //Enabling current selection
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))         //Selecting Categories to the left.
        {
            Pannels[CategorySelection].SetActive(false);        //Disabling old selection
            Categories[CategorySelection].GetComponent<Button>().image.color = Color.cyan;
            if (CategorySelection == 0)     //The counter is at the LeftMost option.
            {
                CategorySelection = Categories.Length - 1;
                Categories[CategorySelection].GetComponent<Button>().image.color = Color.red;
            }
            else                                                //The counter is at the last option.
            {
                CategorySelection = CategorySelection - 1;
                Categories[CategorySelection].GetComponent<Button>().image.color = Color.red;
            }
            CurrentSelection = 0;
            Pannels[CategorySelection].SetActive(true);        //Enabling current selection
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
        else if (CategorySelection == 2)                        //The Restorables category is selected
        {
            useButtons(LoreButtons);
        }
    }

    void useButtons(GameObject[] lis)
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))         //Selecting Categories to the right.
        {
            if (CategorySelection == Categories.Length - 1)     //The counter is at the rightmost option.
            {
                CategorySelection = 0;
                Categories[CategorySelection].GetComponent<Button>().image.color = Color.red;
            }
            else                                                //The counter is at the first option.
            {
                CategorySelection = CategorySelection + 1;
                Categories[CategorySelection].GetComponent<Button>().image.color = Color.red;
            }
            CurrentSelection = 0;
            Pannels[CategorySelection].SetActive(true);        //Enabling current selection
        }
    }
}
