using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class EnemyStatusIndicator : MonoBehaviour {
    //public RectTransform healthBar;
	public Image enemyHealthBar;
    // Use this for initialization
    
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetHealth(int currentHealth, int maxHealth)
    {
        float value = (float)currentHealth / maxHealth;         //calculate the current health status.
		enemyHealthBar.fillAmount = value;      //Change the scale of the healthbar.
        //healthText.text = currentHealth + "/" + maxHealth;
    }
}
