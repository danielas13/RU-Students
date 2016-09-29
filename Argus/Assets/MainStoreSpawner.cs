using UnityEngine;
using System.Collections;

public class MainStoreSpawner : MonoBehaviour {
    public Transform store;
    public bool EnteredStore = false;
    private Transform storeObject;
    void OnTriggerStay2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (EnteredStore == false)
                {
                    Transform storeObject = (Transform)Instantiate(store, transform.position, transform.rotation);
                    storeObject.GetComponent<StoreController>().storeSpawner = this.gameObject;
                    EnteredStore = true;
                }
                else
                {
                    if(storeObject != null)
                    {
                        Destroy(storeObject);
                        Time.timeScale = 1;
                    }
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
