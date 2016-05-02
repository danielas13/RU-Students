using UnityEngine;
using System.Collections;

public class MeleeAttackTrigger : MonoBehaviour
{

    public int damage = 5;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("enemy"))
        {
            col.SendMessageUpwards("damageEnemy", damage);
            Debug.Log("Attack msg sent to enemy");
        }
    }
}