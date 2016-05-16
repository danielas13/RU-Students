using UnityEngine;
using System.Collections;

public class ActivateDoorButtonScript : MonoBehaviour {

    /*public GameObject RedButton;*/
    public GameObject GreenButton;

    public void Unlocked()
    {
        Destroy(this.transform.GetChild(0).gameObject);
        Instantiate(GreenButton, transform.position, transform.rotation);
    }
}
