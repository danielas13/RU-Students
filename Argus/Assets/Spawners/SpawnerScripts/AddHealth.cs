using UnityEngine;
using System.Collections;

public class AddHealth : MonoBehaviour {

    public int maxHealthPerObject = 2;    //The amount of health that will be gained by this object.
    private int AmountIncrease = 0;

    //checking on collition with objects.
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.name == "Player")
        {
            System.Random rnd = new System.Random();                                    //Create a Random object.
            AmountIncrease = rnd.Next(1, maxHealthPerObject+1);                         //randomise a number between 1 add the maxManaPerObject variable.
            GameObject character = GameObject.FindGameObjectWithTag("Player");          //find the player object.
            Stats st = character.gameObject.GetComponent<Stats>();                      //Access the player stats.
            st.increaseMaxHealth(AmountIncrease);                                       //increase the player Health.
            Destroy(this.gameObject);                                                   //Destroy this object.
        }
    }
}
