using UnityEngine;
using System.Collections;

public class AddDamage : MonoBehaviour {

    public int Amount = 2;
     
    //checking on collition with objects.
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.tag == "Player")
        {
            GameObject character = GameObject.FindGameObjectWithTag("Player");          //find the player object.
            Stats st = character.gameObject.GetComponent<Stats>();                      //Access the player stats.
            st.increaseDamage(Amount);                                                       //increase the player Health.
            Destroy(this.gameObject);                                                   //Destroy this object.
        }
    }
}
