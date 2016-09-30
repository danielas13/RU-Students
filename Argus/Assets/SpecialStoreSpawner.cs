using UnityEngine;
using System.Collections;

public class SpecialStoreSpawner : MonoBehaviour {
    public Transform store;
    public bool EnteredStore = false;
    void OnTriggerStay2D(Collider2D collision)
    {
        
        //Check if the collition is with a player.
        if (collision.transform.name == "Player")
        {

            if (Input.GetButtonDown("Interact"))
            {

                if (EnteredStore == false)
                {
                    Transform storeObject = (Transform)Instantiate(store, transform.position +new Vector3(0,0,0), transform.rotation);
                    storeObject.GetComponent<SpecialStoreController>().storeSpawner = this.gameObject;
                    EnteredStore = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.tag == "Player")
        {
            EnteredStore = false;
        }
    }
}
