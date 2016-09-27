using UnityEngine;
using System.Collections;

public class ScoreUpgrade : MonoBehaviour {
    public int MaxValue = 100;
    public int MinValue = 60;
    private int calculatedAmount = 0;
    private static readonly System.Random random = new System.Random();     //Create a read only random variable.

    void Start()
    {
        calculatedAmount = random.Next(MinValue, MaxValue);                         //randomise a number between 1 add the HealPerObject variable.
    }

    //checking on collition with objects.
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.tag == "Player")
        {
            GameObject character = GameObject.FindGameObjectWithTag("Player");          //find the player object.
            Stats st = character.gameObject.GetComponent<Stats>();                      //Access the player stats.
            st.increaseScore(calculatedAmount);                                         //increase the player Health.
            Destroy(this.gameObject);                                                   //Destroy this object.
        }
    }
}
