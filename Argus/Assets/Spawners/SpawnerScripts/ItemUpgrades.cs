using UnityEngine;
using System.Collections;

public class ItemUpgrades : MonoBehaviour {

    public bool RandomSpawn = true;    //Tells us if the player wants an item to drop specifically.
    public Transform SpawnObject;       //Object that will be spawned if RandomSpawn is set to True.

    public Transform ArmorPrefab;
    public int Chances = 6;             //the higher this integer is. The less chance there is of upgrade spawn.
    public float heightOfSpawn = 1f;

    // Use this for initialization
    void Awake () {
        if (!RandomSpawn)
        {
            System.Random rnd = new System.Random();        //Create a Random object.
            int calculate = rnd.Next(1, Chances);           //randomise a number between 1 add the chances variable.
        }
    }

    public void destroyChest()
    {
        if (RandomSpawn)
        {
            Instantiate(SpawnObject, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(ArmorPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
	    
	}
}
