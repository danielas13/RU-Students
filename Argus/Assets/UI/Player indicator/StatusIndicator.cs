using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusIndicator : MonoBehaviour {
	[SerializeField]
	private Image healthStatus;
	[SerializeField]
	private Image manaStatus;
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
    [SerializeField]
    private Text scoreText;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        float value = (float)currentHealth / maxHealth;         //calculate the current health status.
		healthStatus.fillAmount = 1 - value;
       // healthBar.localScale = new Vector3(value, 1f, 1f);      //Change the scale of the healthbar.
      //  healthText.text = currentHealth + "/" + maxHealth;
    }
    public void SetMana(int currentMana, int maxMana)
    {
        float value = (float)currentMana / maxMana;             //calculate the current mana status.
		manaStatus.fillAmount = 1 - value;
    }
    public void SetScore(int score)
    {
        scoreText.text = "x " + score;
    }

    public void SetArmor(int armor)
    {
        armorText.text = armor.ToString();
    }
}
