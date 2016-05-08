using UnityEngine;
using System.Collections;

public class EnemyStatusIndicator : MonoBehaviour {
    public RectTransform healthBar;
    // Use this for initialization
    
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetHealth(int currentHealth, int maxHealth)
    {
        float value = (float)currentHealth / maxHealth;         //calculate the current health status.
        healthBar.localScale = new Vector3(value, 1f, 1f);      //Change the scale of the healthbar.
        //healthText.text = currentHealth + "/" + maxHealth;
    }
}
