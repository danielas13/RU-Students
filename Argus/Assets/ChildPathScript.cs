using UnityEngine;
using System.Collections;

public class ChildPathScript : MonoBehaviour {

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Eitthvað");
        if (Input.GetKeyDown(KeyCode.UpArrow) && other.gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("Player");
			Vector3 door = transform.parent.transform.position;
			player.transform.position = new Vector3(door.x, door.y, player.transform.position.z);
            GameObject camera = GameObject.Find("MainCamera");
            camera.transform.position = player.transform.position;
        }
    }
}
