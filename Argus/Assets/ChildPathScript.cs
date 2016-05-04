using UnityEngine;
using System.Collections;

public class ChildPathScript : MonoBehaviour {

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Eitthvað");
        if (Input.GetKeyDown(KeyCode.UpArrow) && other.gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("Player");
            player.transform.position = transform.parent.transform.position;
            GameObject camera = GameObject.Find("MainCamera");
            camera.transform.position = player.transform.position;
        }
    }
}
