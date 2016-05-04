using UnityEngine;
using System.Collections;

public class MeleeAttackTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("enemy"))
        {

            GameObject character = GameObject.FindGameObjectWithTag("Player");
            Stats player = character.GetComponent<Stats>();
            Debug.Log("Spell did : " + player.status.damage + " damage.");
            col.SendMessageUpwards("damageEnemy", player.status.damage);
            //Debug.Log("Attack msg sent to enemy");
        }
        else if(col.isTrigger != true && col.CompareTag("Chest"))
        {
            col.SendMessageUpwards("destroyChest");
        }
    }
}