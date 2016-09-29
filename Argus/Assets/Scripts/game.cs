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
    public List<GameObject> ShopDoors = new List<GameObject>();
    public ButtonIndicatorController ButtonIndicatorController;

    public List<EnemySpawn> enemySpawners = new List<EnemySpawn>();

    public GameObject playerStatsScreenObj;                 //The player status screens.
    private GameObject playerStatsScreen;
    private bool statScreenUp = false;
    private GameObject floatinBoss, MeleeBoss, MeleeBoss2;
    private List<GameObject> floatingBossPillars = new List<GameObject>();
    public GameObject SkeletonBody;
    public GameObject playerLights;

    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
        }
    }

    void Start()
    {
        MeleeBoss = GameObject.Find("EnemyBoss");
        MeleeBoss2 = GameObject.Find("EnemyBossTwo");
        floatinBoss = GameObject.Find("FloatingBoss");
        playerStatsScreen = GameObject.Find(Instantiate(playerStatsScreenObj, transform.position, transform.rotation).name);
        playerStatsScreen.SetActive(false);

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

        GameObject[] pillars = GameObject.FindGameObjectsWithTag("FloatBossPillar");
        if (pillars != null)
        {
            for (int i = 0; i < pillars.Length; i++)
            {
                floatingBossPillars.Add(pillars[i]);
            }
        }

        GameObject[] storeDoors = GameObject.FindGameObjectsWithTag("StoreDoor");
        if (storeDoors != null)
        {
            for (int i = 0; i < storeDoors.Length; i++)
            {
                ShopDoors.Add(storeDoors[i].gameObject);
            }
        }
    }

    public static void KillPlayer()
    {
        Vector2 pos = gm.player.transform.position;

        GameObject newBody = (GameObject)Instantiate(gm.SkeletonBody, gm.SkeletonBody.transform.position, gm.SkeletonBody.transform.rotation);
        newBody.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        GameObject pl = (GameObject)Instantiate(gm.playerLights, gm.playerLights.transform.position, gm.playerLights.transform.rotation);
        pl.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        Destroy(newBody, 2);
        Destroy(pl, 2);
        gm.player.SetActive(false);
        gm.isPlayerDead = true;
        gm.DeadState.SetActive(true);
        gm.DeadState.transform.position = new Vector2(pos.x,pos.y+2);
    }
    public static void KillEnemy(EnemyStats enemy)
    {
        //Stats playerStatus = gm.player.GetComponent<Stats>();
        //playerStatus.increaseScore(1);
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

        if (Input.GetButtonDown("Cancel"))
        {
            if (statScreenUp)
            {
                
                playerStatsScreen.SetActive(false);
                statScreenUp = false;
            }
            else
            {
                playerStatsScreen.SetActive(true);
                gm.playerStatsScreen.GetComponent<PlayerStatScreenController>().RestartText();
                statScreenUp = true;
            }
        }

    }
    public void respawn()
    {
        gm.player.SetActive(true);                                                              //Disabling the player objects.
        gm.DeadState.SetActive(false);                                                          //Seting DeadState as active.
        gm.isPlayerDead = false;                                                                //Taging the player as dead.
        gm.player.transform.position = GameObject.Find("OriginialSpawn").transform.position;    //locating the player to the same position as the spawnpoint.
        Stats playerStatus = gm.player.GetComponent<Stats>();
        playerStatus.status.score = 0;                                                                     //Reseting the player's score.              

        //Assigning the new stats as previous max stats minus half the gained points of damage,health,mana and spellpower.
        playerStatus.status.maxHealth = playerStatus.status.maxHealth - playerStatus.status.gainedHealth;
        playerStatus.status.maxMana = playerStatus.status.maxMana - playerStatus.status.gainedMana;
        playerStatus.status.minDamage = playerStatus.status.minDamage - playerStatus.status.gainedDamage;
        playerStatus.status.maxDamage = playerStatus.status.maxDamage - playerStatus.status.gainedDamage;
        playerStatus.status.minSpellPower = playerStatus.status.minSpellPower - playerStatus.status.gainedSpellpower;

        playerStatus.status.maxSpellPower = playerStatus.status.maxSpellPower - playerStatus.status.gainedSpellpower;

        //Setting the current health and mana values.
        playerStatus.status.currentHealth = playerStatus.status.maxHealth;
        playerStatus.status.currentMana = playerStatus.status.maxMana;
        playerStatus.status.armor = 0;
        //Resets the Gained stats inside the status object.
        playerStatus.resetGained();
        //reseting the status indicator.
        playerStatus.indicator.SetHealth(playerStatus.status.currentHealth, playerStatus.status.maxHealth);
        playerStatus.indicator.SetMana(playerStatus.status.currentMana, playerStatus.status.maxMana);
        playerStatus.indicator.SetArmor(playerStatus.status.armor);
        playerStatus.indicator.SetScore(0);

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

        if(enemySpawners.Count != 0)
        {
            foreach(EnemySpawn spawner in enemySpawners)
            {
                spawner.resetSpawner();
            }
        }


        for (int i = 0; i < ShopDoors.Count; i++)
        {
      /*      if(ShopDoors != null)
            {
                if (!ShopDoors[i].gameObject.transform.GetComponent<OneToStoreDoor>().Guaranteed)
                {
                    ShopDoors[i].gameObject.transform.GetComponent<OneToStoreDoor>().restart();
                }
            }*/
        }
        if(MeleeBoss != null)
        {
            if (!MeleeBoss.activeSelf)
            {
                MeleeBoss.SetActive(true);
            }

            MeleeBoss.GetComponent<EnemyMeleeBossBehavior>().CombatStarted = false;
            MeleeBoss.GetComponent<EnemyMeleeBossStats>().setHealth(MeleeBoss.GetComponent<EnemyMeleeBossStats>().status.maxHealth);
            MeleeBoss.GetComponent<EnemyMeleeBossBehavior>().ResetMinions();
        }

        //MeleeBoss.GetComponent<EnemyMeleeBossStats>().status.maxHealth = 300;
        //MeleeBoss.GetComponent<EnemyMeleeBossStats>().status.currentHealth = 300;

        if (MeleeBoss2 != null)
        {
            if (!MeleeBoss2.activeSelf)
            {
                MeleeBoss2.SetActive(true);
            }
            MeleeBoss2.GetComponent<EnemyMeleeBossBehavior>().CombatStarted = false;
            MeleeBoss2.GetComponent<EnemyMeleeBossStats>().setHealth(MeleeBoss.GetComponent<EnemyMeleeBossStats>().status.maxHealth);
            MeleeBoss2.GetComponent<EnemyMeleeBossBehavior>().ResetMinions();
        }
        //MeleeBoss2.GetComponent<EnemyMeleeBossStats>().status.maxHealth = 300;
        //MeleeBoss2.GetComponent<EnemyMeleeBossStats>().status.currentHealth = 300;


        floatinBoss.SetActive(true);
        floatinBoss.GetComponent<FloatingBossController>().ResetBoss();
        /*
        for (int i = 0; i < floatingBossPillars.Count; i++)
        {
            floatingBossPillars[i].GetComponent<FloatingBossPillars>().restart();
        }*/
    }
}
