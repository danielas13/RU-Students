using UnityEngine;
using System.Collections;

public class RestoreMana : MonoBehaviour {

    public int MinHealPerObject = 15; //The amount of mana that will be gained by this object.
    public int MaxHealPerObject = 25; //The amount of mana that will be gained by this object.
    private int AmountIncrease = 0;
    private static readonly System.Random random = new System.Random();     //Create a read only random variable.

    //checking on collition with objects.
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.tag == "Player")
        {
            AmountIncrease = random.Next(MinHealPerObject, MaxHealPerObject);                         //randomise a number between 1 add the HealPerObject variable.
            GameObject character = GameObject.FindGameObjectWithTag("Player");          //find the player object.
            Stats st = character.gameObject.GetComponent<Stats>();                      //Access the player stats.
            st.restoreMana(AmountIncrease);                                             //increase the player current health.
            Destroy(this.gameObject);                                                   //Destroy this object.
        }
    }
}
