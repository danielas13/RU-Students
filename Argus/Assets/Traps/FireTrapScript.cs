using UnityEngine;
using System.Collections;

public class FireTrapScript : MonoBehaviour {

    public GameObject Fire;
    public int trapDamage = 20;             //damage of the trap.
    private Stats playerStats;              //player statistic
    private float DamageCooldown = 0f;      //cooldown on how many times the player will be damaged while staying in the fire.
    public float trapDuration = 6;          //The timer the trap will be active
    public float trapDownTime = 3;          //how long will the trap lay dormant between each iteration.
    private float currentTimer = 0;
    private bool isActive = false;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<Stats>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (DamageCooldown <= 0)        //Damage cooldown while the plauer stays in the trap.
            {
                if (isActive)
                {
                    playerStats.damagePlayer(trapDamage);
                    DamageCooldown = 0.5f;
                }
            }
        }
    }

    void Update()
    {
        if(DamageCooldown >= 0)
        {
            DamageCooldown -= Time.deltaTime;   //incrementing the damage cooldown timer.
        }

        if (isActive)
        {
            if(currentTimer >= trapDuration)
            {
                Fire.SetActive(false);
               // RightFire.SetActive(false);
                isActive = false;
                currentTimer = 0;
            }
        }
        else
        {
            if(currentTimer >= trapDownTime)
            {
                Fire.SetActive(true);
               // RightFire.SetActive(true);
                isActive = true;
                currentTimer = 0;
            }
        }


        currentTimer += Time.deltaTime;
    }
}
