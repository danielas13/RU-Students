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
        //Debug.Log("Exited Collision");
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
        /*
        if (player.transform.GetComponent<Rigidbody2D>().velocity.y < (Vector2.up.y * 4))
        {
            player.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 0.8f, ForceMode2D.Impulse);
        }
        */
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
        /*
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.UpArrow) && other.gameObject.tag == "Player")
        {
            //player.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            //player.transform.Translate(Vector3.up * Time.deltaTime*5);
            if(player.transform.GetComponent<Rigidbody2D>().velocity.y < (Vector2.up.y * 4))
            {
                player.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 0.8f, ForceMode2D.Impulse);
            }
           
        }
    }
    */
}
