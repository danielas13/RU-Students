using UnityEngine;
using System.Collections;

public class ResoreHealth : MonoBehaviour {


    public int minHeal = 20; //The amount of mana that will be gained by this object.
    public int maxHeal = 40;
    private int AmountIncrease = 0;
    private static readonly System.Random random = new System.Random();     //Create a read only random variable.

    //checking on collition with objects.
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.tag == "Player")
        {
            AmountIncrease = random.Next(minHeal, maxHeal);                         //randomise a number between 1 add the HealPerObject variable.
            GameObject character = GameObject.FindGameObjectWithTag("Player");          //find the player object.
            Stats st = character.gameObject.GetComponent<Stats>();                      //Access the player stats.
            st.restoreHealth(AmountIncrease);                                           //increase the player current health.
            Destroy(this.gameObject);                                                   //Destroy this object.
        }
    }
}
