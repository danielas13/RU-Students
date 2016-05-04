using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour {
    //Place the spawner about 1f above the ground on y axis in order to have the knights on level with the ground.

    public int TotalSpawns = 2;     //Max Spawns of this spawner.
    public Transform EnemyPrefab;   //The enemy prefab.
    public float coolDown = 10;       //cooldown between spawns

    public float MaxRangeFromPlayer = 10f;  //Distance between the player in order to spawn new objects.
    public LayerMask spawnLayers;           //the layer filtering out the player.

    private float currentCount = 0;   //The current counter for spawning enemies.
    private List<Object> lis = new List<Object>();  //The list containing our objects.
    GameObject player;                //The Player object.

    // Use this for initialization
    void Start () {
        //Fetching the player object.
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        //fidning the player object in case the player has respawned.
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        //Checking current amount of spawns.
        if (this.TotalSpawns > this.lis.Count)
        {
            //Updating the timer if it's on cooldown.
            if(this.currentCount > 0)
            {
                currentCount -= Time.deltaTime;
            }
            else if(this.currentCount <= 0)
            {
                //Creating the ray towards player to check if he is in sight.
                Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
                Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
                RaycastHit2D rayToPlayer = Physics2D.Raycast(enemyPos, playerPos - enemyPos, MaxRangeFromPlayer, spawnLayers);

                //Check if the player is in range.
                if(rayToPlayer.collider == null)
                {
                    //Reset the timer.
                    this.currentCount = this.coolDown;
                    //instantiate the new object ( create a new clone of the enemy prefab).
                    Object newObj = Instantiate(EnemyPrefab, transform.position, transform.rotation);
                    //add the new enemy object to the list.
                    this.lis.Add(newObj);
                }
            }
        }
        //There are already maximum amounts of spawns.
        else
        {
            //iterate through the list of objects and remove objects that have been destroyed from the list.
            for(int i = 0; i < this.lis.Count;i++)
            {
                if(this.lis[i] == null)
                {
                    this.lis.Remove(this.lis[i]);
                }
            }
        }
	}
}
