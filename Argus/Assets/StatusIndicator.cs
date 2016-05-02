using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusIndicator : MonoBehaviour {
    [SerializeField]
    private RectTransform healthBar;
    [SerializeField]
    private RectTransform manaBar;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text manaText;
    //[SerializeField]
    //private GameObject pl;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
       /* if (pl == null)
        {

            GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
            if (tempPlayer == null)
            {
                return;
            }
            else
            {
                pl = tempPlayer;
                Stats playerStats = pl.GetComponent<Stats>();
                playerStats.indicator = this;
                SetHealth(playerStats.status.currentHealth, playerStats.status.maxHealth);
                SetMana(playerStats.status.currentMana, playerStats.status.maxMana);      
            }
        }
        transform.position = new Vector3(pl.transform.position.x, pl.transform.position.y + 1.2f, 0f);//pl.transform.position.z);
        */
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        float value = (float)currentHealth / maxHealth; //calculate the current health status.
        healthBar.localScale = new Vector3(value, 1f, 1f);//healthBar.localScale.x, healthBar.localScale.z);  //Change the scale of the healthbar.
        healthText.text = currentHealth + "/" + maxHealth;
    }
    public void SetMana(int currentMana, int maxMana)
    {
        float value = (float)currentMana / maxMana; //calculate the current mana status.
        manaBar.localScale = new Vector3(value, 1f, 1f);// manaBar.localScale.x, manaBar.localScale.z);    //Change the scale of the manabar.
        manaText.text = currentMana + "/" + maxMana;
    }
}
