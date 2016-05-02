using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
    [System.Serializable]
    public class enemyStats
    {
        public int currentHealth = 2;
    }

    public enemyStats status = new enemyStats();

    // Use this for initialization
    void Start()
    {

    }

    public void damageEnemy(int damage)
    {
        this.status.currentHealth -= damage;
        if (this.status.currentHealth <= 0)
        {
            game.KillEnemy(this);
        }
    }
}
