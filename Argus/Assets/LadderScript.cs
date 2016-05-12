using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {
    private GameObject player;      //player object
    public float upSpeed = 3;
    public float downSpeed = 2;
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
        if (isClimbing)     //is the player climbing?
        {
            if (Input.GetKey(KeyCode.UpArrow))  //Player is going up.
            {
                player.transform.GetComponent<Rigidbody2D>().velocity = Vector2.up*upSpeed; 

            }
            else if (Input.GetKey(KeyCode.DownArrow))   //Player is going down.
            {
                player.transform.GetComponent<Rigidbody2D>().velocity = Vector2.down*downSpeed;

            }
            else                                        //Player is staying still on the ladder.
            {
                player.transform.GetComponent<Rigidbody2D>().velocity = Vector2.up * 0.6f;
            }
        }
    } 
}
