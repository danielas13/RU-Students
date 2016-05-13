﻿using UnityEngine;
using System.Collections;

public class SpecialStoreSpawner : MonoBehaviour {
    public Transform store;
    private bool EnteredStore = false;
    void OnTriggerStay2D(Collider2D collision)
    {
        //Check if the collition is with a player.
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.U))
            {
                if (EnteredStore == false)
                {
                    Instantiate(store, transform.position, transform.rotation);
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
