using UnityEngine;
using System.Collections;

public class ItemUpgrades : MonoBehaviour {

    public bool RandomSpawn = true;    //Tells us if the player wants an item to drop specifically.
    public Transform SpawnObject;       //Object that will be spawned if RandomSpawn is set to True.

    public Transform ArmorPrefab;
    public int Chances = 6;             //the higher this integer is. The less chance there is of upgrade spawn.
    public float heightOfSpawn = 1f;

    private int calculate = 0;

    //this variable is static readonly to ensure that the number is random between objects ( uses a unique seed).
    private static readonly System.Random random = new System.Random();     //Create a read only random variable.

    // Use this for initialization
    void Awake () {

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
            calculate = random.Next(1, Chances + 1);           //randomise a number between 1 add the chances variable.

            //Debug.Log(calculate);
            if(calculate == 1)
            {
                Instantiate(ArmorPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
	    
	}
}
