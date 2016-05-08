using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game : MonoBehaviour {
    public static game gm;
    public GameObject DeadState = null;
    public bool isPlayerDead = false;
    private GameObject player = null;
    private float deadTimer = 2;
    public List<GameObject> ItemSpawners = new List<GameObject>();


    void Start()
    {
        DeadState = GameObject.Find("DeadState");
        player = GameObject.Find("Player");
        DeadState.SetActive(false);

        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
        }


        GameObject[] ItemSpawns = GameObject.FindGameObjectsWithTag("Chest");
        if (ItemSpawns != null)
        {
            for (int i = 0; i < ItemSpawns.Length; i++)
            {
                ItemSpawners.Add(ItemSpawns[i]);
            }
        }
    }

    public static void KillPlayer()
    {
        Vector2 pos = gm.player.transform.position;
        gm.player.SetActive(false);
        gm.isPlayerDead = true;
        gm.DeadState.SetActive(true);
        gm.DeadState.transform.position = new Vector2(pos.x,pos.y+2);
    }
    public static void KillEnemy(EnemyStats enemy)
    {
        Stats playerStatus = gm.player.GetComponent<Stats>();
        playerStatus.increaseScore(1);
        Destroy(enemy.gameObject);
    }

    void Update()
    {
        //manual respawn for the player.
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            respawn();
        }*/
        if (isPlayerDead)
        {
            deadTimer -= Time.deltaTime;
            if (deadTimer <= 0)
            {
                respawn();
                deadTimer = 2;
            }
        }

    }
    void respawn()
    {
        gm.player.SetActive(true);                                                              //Disabling the player objects.
        gm.DeadState.SetActive(false);                                                          //Seting DeadState as active.
        gm.isPlayerDead = false;                                                                //Taging the player as dead.
        gm.player.transform.position = GameObject.Find("OriginialSpawn").transform.position;    //locating the player to the same position as the spawnpoint.
        Stats playerStatus = gm.player.GetComponent<Stats>();
        playerStatus.status.score = 0;                                                                     //Reseting the player's score.              

        //Assigning the new stats as previous max stats minus half the gained points of damage,health,mana and spellpower.
        playerStatus.status.maxHealth = playerStatus.status.maxHealth - (int)Mathf.Floor(playerStatus.status.gainedHealth / 2);
        playerStatus.status.maxMana = playerStatus.status.maxMana - (int)Mathf.Floor(playerStatus.status.gainedMana / 2);
        playerStatus.status.damage = playerStatus.status.damage - (int)Mathf.Floor(playerStatus.status.gainedDamage / 2);
        playerStatus.status.spellpower = playerStatus.status.spellpower - (int)Mathf.Floor(playerStatus.status.gainedSpellpower / 2);

        //Setting the current health and mana values.
        playerStatus.status.currentHealth = playerStatus.status.maxHealth;
        playerStatus.status.currentMana = playerStatus.status.maxMana;
        //Resets the Gained stats inside the status object.
        playerStatus.resetGained();
        //reseting the status indicator.
        playerStatus.indicator.SetHealth(playerStatus.status.currentHealth, playerStatus.status.maxHealth);
        playerStatus.indicator.SetMana(playerStatus.status.currentMana, playerStatus.status.maxMana);
        playerStatus.indicator.SetArmor(playerStatus.status.armor);

        //Restarting the moving traps.
        GameObject[] totalMoving = GameObject.FindGameObjectsWithTag("MovingTrap");
        if (totalMoving != null)
        {
            for (int i = 0; i < totalMoving.Length; i++)
            {
				totalMoving[i].GetComponent<restartMovingTrap>().restart();
            }
        }
        

       //Restarting the Timed traps
       GameObject[] totalTimed = GameObject.FindGameObjectsWithTag("TimedTrap");
        if (totalTimed != null)
        {
            for (int i = 0; i < totalTimed.Length; i++)
            {
                totalTimed[i].GetComponent<TimedSpikeTrapScript>().restart();
            }
        }
        //Destroying all upgrades/items on the map.
        GameObject[] totalItems = GameObject.FindGameObjectsWithTag("Loot");
        if (totalItems != null)
        {
            for (int i = 0; i < totalItems.Length; i++)
            {
                Destroy(totalItems[i].gameObject);
            }
        }
        //REseting all Status upgrade spawns.
        GameObject[] totalUpgrades = GameObject.FindGameObjectsWithTag("StatusSpawn");
        if (totalUpgrades != null)
        {
            for (int i = 0; i < totalUpgrades.Length; i++)
            {
                totalUpgrades[i].GetComponent<UpgradeStatus>().Spawn();
            }
        }

        for (int i = 0; i < ItemSpawners.Count; i++)
        {
            ItemSpawners[i].SetActive(true);
        }
    }
}
