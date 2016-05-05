using UnityEngine;
using System.Collections;

public class OneToOneDoor : MonoBehaviour {

    public Transform TargetObject;

    //Will trigger if the player is withing the door boundaries and pressesthe up key.
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && other.gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("Player");
            player.transform.position = new Vector3(TargetObject.position.x, TargetObject.position.y, player.transform.position.z);
            GameObject camera = GameObject.Find("MainCamera");
            camera.transform.position = player.transform.position;
        }
    }
}
