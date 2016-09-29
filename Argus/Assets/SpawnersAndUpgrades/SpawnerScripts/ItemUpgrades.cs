using UnityEngine;
using System.Collections;

public class ItemUpgrades : MonoBehaviour {

    public bool RandomSpawn = true;    //Tells us if the player wants an item to drop specifically.
    public Transform SpawnObject;       //Object that will be spawned if RandomSpawn is set to True.

    public Transform ArmorPrefab;
    public Transform manaRestorePrefab;
    public Transform healthRestorePrefab;
    public Transform scorePrefab;
    public int Chances = 6;             //the higher this integer is. The less chance there is of upgrade spawn.
    public int scoreChances = 3;
    public float heightOfSpawn = 1f;

    //public int MaxScore = 10;
    //public int MinScore = 5;

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
			Instantiate(SpawnObject, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z-3f), transform.rotation);
            this.gameObject.SetActive(false);//Destroy(this.gameObject);
        }
        else
        {
            calculate = random.Next(1, Chances + 4);           //randomise a number between 1 add the chances variable.

            //Debug.Log(calculate);
            if(calculate == 1 && calculate == 4)
            {
				Instantiate(ArmorPrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z-3f), transform.rotation);
            }
            else if (calculate == 2)
            {
				Instantiate(manaRestorePrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn,transform.position.z-3f), transform.rotation);
            }
            else if (calculate == 3)
            {
				Instantiate(healthRestorePrefab, new Vector3(transform.position.x, transform.position.y + heightOfSpawn, transform.position.z-3f), transform.rotation);
            }
            calculate = random.Next(1, scoreChances + 1);
            if(calculate == 2 || calculate == 1)
            {
                Object newObj = Instantiate(scorePrefab, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                //GameObject.Find(newObj.name).GetComponent<EnemyScoreUpgrade>().scoreAmount = random.Next(MinScore, MaxScore);
            }

            this.gameObject.SetActive(false);//Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
	    
	}
}
