using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {
    private GameObject player;      //player object
    private bool isClimbing = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //checking if the player has exited the ladder.
        {
            isClimbing = false;         //The player is not climbing anymoreþ
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //check if the player is on the ladder
        {
            isClimbing = true;          //The player is climbing.
        }
    }

    void Update()
    {
        if (game.gm.isPlayerDead)
        {
            isClimbing = false;
        }
        if (isClimbing)     //is the player climbing?
        {
            if (Input.GetAxis("Vertical") >= 0.2f)  //Player is going up.
            {
                player.transform.GetComponent<Rigidbody2D>().velocity = Vector2.up*3; 

            }
            else if (Input.GetAxis("Vertical") < -0.2f)   //Player is going down.
            {
                player.transform.GetComponent<Rigidbody2D>().velocity = Vector2.down*2;

            }
            else                                        //Player is staying still on the ladder.
            {
                player.transform.GetComponent<Rigidbody2D>().velocity = Vector2.up * 0.6f;
            }
        }
    } 
}
