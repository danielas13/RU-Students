using UnityEngine;
using System.Collections;


public class DoorKey : MonoBehaviour {
    public GameObject TargetDoor;
    void OnTriggerStay2D(Collider2D other)
    {
         if (other.gameObject.tag == "Player")
         {
            TargetDoor.GetComponent<OneToOneDoor>().unlock();
            Destroy(this.gameObject);
         }
    }
}
