using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {

    void OnTriggerExit2D(Collider2D other)
    {
        //GameObject player = GameObject.Find("Player");
        // player.transform.GetComponent<Rigidbody2D>().gravityScale = 3;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        GameObject player = GameObject.Find("Player");
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
}
