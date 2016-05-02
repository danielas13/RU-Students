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
        Destroy(player.gameObject);
        gm.playerRespawn();
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
