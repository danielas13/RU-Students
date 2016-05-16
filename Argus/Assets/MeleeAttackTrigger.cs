using UnityEngine;
using System.Collections;

public class MeleeAttackTrigger : MonoBehaviour
{
    private static readonly System.Random randomAttackGenerator = new System.Random();
    private Stats player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Stats>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("enemy"))
        {
            int randomDmg = randomAttackGenerator.Next(player.status.minDamage, player.status.maxDamage);
            col.SendMessageUpwards("damageEnemy", randomDmg);
            transform.GetComponent<Collider2D>().enabled = false;
            if (player.transform.GetComponent<Stats>().status.FireBlade)
            {
                col.transform.GetComponent<EnemyStats>().Ignite(5);
            }
        }
        else if(col.CompareTag("Chest"))
        {
            col.SendMessageUpwards("destroyChest");
        }
        else if (col.CompareTag("FloatBossPillar"))
        {
            col.gameObject.GetComponent<FloatingBossPillars>().DisablePillar();// 'col.SendMessageUpwards("DisablePillar");
            transform.GetComponent<Collider2D>().enabled = false;
        }
    }


}