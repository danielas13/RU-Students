using UnityEngine;
using System.Collections;

public class UpgradeStatus : MonoBehaviour {

    public bool RandomSpawn = false;    //Tells us if the player wants health or mana specifically.
    public float MHDS = 0;        //integer representation of what the user wants. 1 = health, 2 = mana.

    //Status prefabs.
    public Transform ManaPrefab; 
    public Transform HealthPrefab;
    public Transform DamagePrefab;
    public Transform SpellpowerPrefab;

    public int Chances = 4;             //the higher this integer is. The less chance there is of upgrade spawn.
    public float heightOfSpawn = 1f;    //The height that the object will spawn above 

    //this variable is static readonly to ensure that the number is random between objects ( uses a unique seed).
    private static readonly System.Random random = new System.Random();     //Create a read only random variable.
    public int  calculate = 0;
    
    // Use this for initialization
    void Awake () {
        Spawn();
    }

    public void Spawn()
    {
        if (!RandomSpawn)
        {
            // System.Random rnd = new System.Random();        //Create a Random object.
            calculate = random.Next(1, Chances + 4);           //randomise a number between 1 add the chances variable.

            //check if the randomised number represents a mana upgrade or player upgrade.
            if (calculate < 7)
            {
                if (calculate == 1 || calculate == 3)                         //The upgrade is a health upgrade.
                {
                    //Create a new healthUpgrade using the spawner's position plus the height of heightofSpawn variable.
                    Instantiate(HealthPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
                }
                else if (calculate == 2 || calculate == 4)                    //the upgrade is a mana upgrade.
                {
                    //Create a new manaUpgrade using the spawner's position plus the height of heightofSpawn variable.
                    Instantiate(ManaPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
                }
                else if (calculate == 5)                    //the upgrade is a damage upgrade.
                {
                    //Create a new damageUpgrade using the spawner's position plus the height of heightofSpawn variable.
                    Instantiate(DamagePrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
                }
                else if (calculate == 6)                    //the upgrade is a spellpower upgrade.
                {
                    //Create a new spellpowerUpgrade using the spawner's position plus the height of heightofSpawn variable.
                    Instantiate(SpellpowerPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
                }
            }
        }
        else    //The user wants the spawner to create a specific upgrade.
        {
            if (MHDS == 1)
            {
                //Create a new healthUpgrade using the spawner's position plus the height of heightofSpawn variable.
                Instantiate(HealthPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
            }
            else if (MHDS == 2)
            {
                //Create a new manaUpgrade using the spawner's position plus the height of heightofSpawn variable.
                Instantiate(ManaPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
            }
            else if (MHDS == 3)
            {
                //Create a new manaUpgrade using the spawner's position plus the height of heightofSpawn variable.
                Instantiate(SpellpowerPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
            }
            else if (MHDS == 4)
            {
                //Create a new manaUpgrade using the spawner's position plus the height of heightofSpawn variable.
                Instantiate(DamagePrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z), transform.rotation);
            }
        }
    }
}
