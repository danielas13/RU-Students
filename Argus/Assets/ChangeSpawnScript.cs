using UnityEngine;
using System.Collections;

public class ChangeSpawnScript : MonoBehaviour {

	public Transform TargetObject;

    void Start()
    {

    }

	//Will trigger if the player is withing the door boundaries and pressesthe up key.
	void OnTriggerStay2D(Collider2D other)
	{
        if(TargetObject!= null)
        {
            if (Input.GetButtonDown("Interact") && other.gameObject.tag == "Player")
            {

                GameObject startingDoor = GameObject.Find("StartingDoor");
                startingDoor.GetComponent<OneToOneDoor>().TargetObject = TargetObject;

                GameObject player = GameObject.Find("Player");
                player.transform.position = new Vector3(TargetObject.position.x, TargetObject.position.y, player.transform.position.z);
                GameObject camera = GameObject.Find("MainCamera");
                if (TargetObject.CompareTag("StoreDoor"))
                {
                    TargetObject.GetComponent<OneToStoreDoor>().cooldown = 1;
                }
                else
                {
                    OneToOneDoor Door = TargetObject.GetComponent<OneToOneDoor>();
                    if (Door.reciever)
                    {
                        Door.cooldown = 1;
                        Door.TargetObject = this.transform;

                    }
                    else
                    {
                        Door.cooldown = 1;
                    }
                }
                camera.transform.position = player.transform.position;
            }
        }
	}
}
