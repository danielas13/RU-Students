using UnityEngine;
using System.Collections;

public class ItemUpgrades : MonoBehaviour {

    public bool RandomSpawn = false;    //Tells us if the player wants an item to drop specifically.
    public Transform SpawnObject;       //Object that will be spawned if RandomSpawn is set to True.

    public Transform ArmorPrefab;
    public int Chances = 6;             //the higher this integer is. The less chance there is of upgrade spawn.

    // Use this for initialization
    void Awake () {
        if (!RandomSpawn)
        {
            System.Random rnd = new System.Random();        //Create a Random object.
            int calculate = rnd.Next(1, Chances);           //randomise a number between 1 add the chances variable.
        }
    }

    //checking on collition with objects.
    void OnCollisionStay2D(Collision2D collision)
    {

        //Check if the collition is with a player.
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Hello");
            }
        }
    }

    // Update is called once per frame
    void Update () {
	    
	}
}
