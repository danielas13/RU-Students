using UnityEngine;
using System.Collections;

public class DoorButtonKeyScript : MonoBehaviour {
    public GameObject Target;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Target.GetComponent<ActivateDoorButtonScript>().Unlocked();
            Destroy(this.gameObject);
        }
    }
}
