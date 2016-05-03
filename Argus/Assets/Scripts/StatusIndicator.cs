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
    [SerializeField]
    private Text armorText;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        float value = (float)currentHealth / maxHealth;         //calculate the current health status.
        healthBar.localScale = new Vector3(value, 1f, 1f);      //Change the scale of the healthbar.
        healthText.text = currentHealth + "/" + maxHealth;
    }
    public void SetMana(int currentMana, int maxMana)
    {
        float value = (float)currentMana / maxMana;             //calculate the current mana status.
        manaBar.localScale = new Vector3(value, 1f, 1f);        //Change the scale of the manabar.
        manaText.text = currentMana + "/" + maxMana;
    }

    public void SetArmor(int armor)
    {
        armorText.text = armor.ToString();
    }
}
