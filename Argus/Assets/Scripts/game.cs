using UnityEngine;
using System.Collections;

public class game : MonoBehaviour {
    public static game gm;
    public GameObject DeadState = null;
    public bool isPlayerDead = false;
    private GameObject player = null;

    void Start()
    {
        DeadState = GameObject.Find("DeadState");
        player = GameObject.Find("Player");
        DeadState.SetActive(false);

        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
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
        Destroy(enemy.gameObject);
    }

    public Transform playerPrefab, spawn;
    public void playerRespawn()
    {
        //Instantiate(playerPrefab, spawn.position, spawn.rotation);
    }

    void Update()
    {
        //manual respawn for the player.
        if (Input.GetKeyDown(KeyCode.R))
        {
            respawn();
        }
    }
    void respawn()
    {
        gm.player.SetActive(true);                                                              //Disabling the player objects.
        gm.DeadState.SetActive(false);                                                          //Seting DeadState as active.
        gm.isPlayerDead = false;                                                                //Taging the player as dead.
        gm.player.transform.position = GameObject.Find("OriginialSpawn").transform.position;    //locating the player to the same position as the spawnpoint.
        Stats x = gm.player.GetComponent<Stats>();                                  

        //Assigning the new stats as previous max stats minus half the gained points of damage,health,mana and spellpower.
        x.status.maxHealth = x.status.maxHealth - (int)Mathf.Floor(x.status.gainedHealth / 2);          
        x.status.maxMana = x.status.maxMana - (int)Mathf.Floor(x.status.gainedMana / 2);
        x.status.damage = x.status.damage - (int)Mathf.Floor(x.status.gainedDamage / 2);
        x.status.spellpower = x.status.spellpower - (int)Mathf.Floor(x.status.gainedSpellpower / 2);

        //Setting the current health and mana values.
        x.status.currentHealth = x.status.maxHealth;
        x.status.currentMana = x.status.maxMana;
        //Resets the Gained stats inside the status object.
        x.resetGained();
        //reseting the status indicator.
        x.indicator.SetHealth(x.status.currentHealth, x.status.maxHealth);
        x.indicator.SetMana(x.status.currentMana, x.status.maxMana);
        x.indicator.SetArmor(x.status.armor);
    }
}
