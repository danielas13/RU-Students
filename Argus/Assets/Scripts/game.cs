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
		//BoxCollider2D collisionbBox = player.GetComponent<BoxCollider2D>();
		//collisionbBox.transform.Translate (new Vector2 (100.0f,100.0f));
		//Debug.Log (collisionbBox.transform.position);

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
