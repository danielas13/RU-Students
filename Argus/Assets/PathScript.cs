using UnityEngine;
using System.Collections;

public class PathScript : MonoBehaviour {

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Interact") && other.gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("Player");
			Vector3 door = transform.FindChild ("ChildDoor").transform.position;
			player.transform.position = new Vector3(door.x, door.y, player.transform.position.z);
            GameObject camera = GameObject.Find("MainCamera");
            camera.transform.position = player.transform.position;
        }
    }
}
