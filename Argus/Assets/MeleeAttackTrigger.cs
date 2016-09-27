using UnityEngine;
using System.Collections;

public class MeleeAttackTrigger : MonoBehaviour
{
    private static readonly System.Random randomAttackGenerator = new System.Random();
    private Stats player;
    private PlayerMeleeAttack playerAttack;
    void Start()
    {
        GameObject pl = GameObject.Find("Player");
        player = pl.GetComponent<Stats>();
        playerAttack = pl.GetComponent<PlayerMeleeAttack>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("enemy"))
        {
            transform.GetComponent<Collider2D>().enabled = false;
            if (player.transform.GetComponent<Stats>().status.FireBlade)
            {
                if (col.transform.GetComponent<EnemyStats>())
                {
                    col.transform.GetComponent<EnemyStats>().Ignite(5);
                }

            }
            else if (player.status.ManaBlade)
            {
                player.restoreMana(5);
                player.indicator.SetMana(player.status.currentMana, player.status.maxMana);
            }
            if (player.status.ShadowBlade)
            {
                int randomDmg = randomAttackGenerator.Next(player.status.minDamage, (player.status.maxDamage + (int)(0.3*player.status.maxDamage)));
                if (playerAttack.PowerAttack)
                {
                    col.SendMessageUpwards("damageEnemy", randomDmg + (randomDmg*playerAttack.PowerAttackDamageIncrease));
                    print("powerAttack");
                }
                else
                {
                    col.SendMessageUpwards("damageEnemy", randomDmg);
                }
            }
            else
            {
                int randomDmg = randomAttackGenerator.Next(player.status.minDamage, player.status.maxDamage);
                if (playerAttack.PowerAttack)
                {
                    col.SendMessageUpwards("damageEnemy", randomDmg + (randomDmg * playerAttack.PowerAttackDamageIncrease));
                    print("powerAttack");
                }
                else
                {
                    col.SendMessageUpwards("damageEnemy", randomDmg);
                }

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