using UnityEngine;
using System.Collections;

public class game : MonoBehaviour {
    public static game gm;

    void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
        }
    }

    public static void KillPlayer(Stats player)
    {
        int h, m, s, d;
        h = player.status.gainedHealth;

        m = player.status.gainedMana;

        s = player.status.gainedSpellpower;

        d = player.status.gainedDamage;

        Destroy(player.gameObject);
        gm.playerRespawn();

        Stats newStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        newStatus.status.maxHealth = h;
        newStatus.status.maxMana = m;
        newStatus.increaseSpellpower(s);
        newStatus.increaseDamage(d);
        Debug.Log("health " + h);
        Debug.Log("mana " + m);
        Debug.Log("damage " + d);
        Debug.Log("power " + s);
        //newStatus.restart();
    }
    public static void KillEnemy(EnemyStats enemy)
    {
        Destroy(enemy.gameObject);
    }

    public Transform playerPrefab, spawn;
    public void playerRespawn()
    {
        Instantiate(playerPrefab, spawn.position, spawn.rotation);
    }
}
