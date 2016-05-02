using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{

    public int damage = 2;    //damage of an attack.
    //public GameObject target;
    public int range = 1;
    private float attackTimer = 0;
    public GameObject target;
    public int attackCd = 1;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (attackTimer <= 0)
        {
            Swing();
            attackTimer = attackCd;
        }
    }

    void Swing()
    {
        if (target != null)
        {
            if (Vector2.Distance(target.transform.position, transform.position) < range)
            {
                Stats player = target.GetComponent<Stats>();
                if (player != null)
                {
                    Debug.Log("ATTACKED Player WITH : " + player.status.currentHealth + " Health");
                    player.damagePlayer(damage);
                }
            }
        }
        else
        {
            GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
            if (tempPlayer == null)
            {
                return;
            }
            else
            {
                target = tempPlayer;
            }
            return;
        }
        return;
    }
}