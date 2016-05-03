using UnityEngine;
using System.Collections;

public class AddMana : MonoBehaviour
{
    public int maxManaPerObject = 2; //The amount of mana that will be gained by this object.
    private int AmountIncrease = 0;

    //checking on collition with objects.
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.name == "Player")
        {
            System.Random rnd = new System.Random();                                    //Create a Random object.
            AmountIncrease = rnd.Next(1, maxManaPerObject+1);                           //randomise a number between 1 add the maxManaPerObject variable.
            GameObject character = GameObject.FindGameObjectWithTag("Player");          //find the player object.
            Stats st = character.gameObject.GetComponent<Stats>();                      //Access the player stats.
            st.increaseMaxMana(AmountIncrease);                                         //increase the player mana.
            Destroy(this.gameObject);                                                   //Destroy this object.
        }
    }
}
