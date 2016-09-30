using UnityEngine;
using System.Collections;

public class FireRayBehavior : MonoBehaviour {

    private Stats stats;
    private float spendMana = 0;
    private bool stoppedChannel = false;//Check to make sure that the player can not rechannel the already in effect spell
    public LayerMask notHit;
    private float damageCooldown = 0.3f;
    private bool outOfMana = false;
    private PlatformerCharacter2D player;

    private static readonly System.Random damageGen = new System.Random();

    void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>();
        }
        if (stats.status.currentMana < 1)
        {
            outOfMana = true;
        }

        if (!Input.GetButton("UseSpell") || outOfMana)
        {
            if(player != null)
            {
                player.isChanneling = false;//Re-enable movement for the player
            }
            Destroy(this.gameObject);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), 20, notHit);
        if (hit.transform != null)
        {
            //Debug.Log("Name: " + hit.transform.name);
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                if(hit.transform.GetComponent<EnemyMeleeBossStats>() == null)
                {
                    int randomDmg = damageGen.Next(stats.status.minSpellPower, stats.status.maxSpellPower);
                    float someNumber = randomDmg *0.4f; 
                    hit.transform.GetComponent<EnemyStats>().damageEnemy((int)someNumber);
                    hit.transform.GetComponent<EnemyStats>().Ignite(5f);
                    damageCooldown = 0.3f;
                }
                else
                {
                    int randomDmg = damageGen.Next(stats.status.minSpellPower, stats.status.maxSpellPower);
                    float someNumber = randomDmg * 0.4f;
                    hit.transform.GetComponent<EnemyMeleeBossStats>().damageEnemy((int)someNumber);
                    damageCooldown = 0.3f;
                }

            }
            

            /*freezeTime -= Time.deltaTime;
            if (freezeTime < 0)
            {
                if (hit.transform.name == "Enemy(Clone)")
                {
                    hit.transform.GetComponent<EnemyBehavior>().frozen = true;
                    hit.transform.GetComponent<EnemyBehavior>().frozenTimer = 4f;
                }
                else if (hit.transform.name == "EnemyCaster(Clone)")
                {
                    hit.transform.GetComponent<EnemyCasterBehavior>().frozen = true;
                    hit.transform.GetComponent<EnemyCasterBehavior>().frozenTimer = 4f;
                }
                else if (hit.transform.name == "FloatingEnemy(Clone)")
                {
                    hit.transform.GetComponent<EnemyBehavior>().frozen = true;
                    hit.transform.GetComponent<EnemyBehavior>().frozenTimer = 4f;
                }
            }*/

        }

        if (spendMana < 0)
        {

            stats.spendMana(10);
            spendMana = 0.5f;
        }
        spendMana -= Time.deltaTime;



        if (Input.GetAxis("Vertical") >= 0.2f)
        {
            transform.Rotate(new Vector3(0, 0, 30 * Time.deltaTime));
        }

        if (Input.GetAxis("Vertical") < -0.2f)
        {
            transform.Rotate(new Vector3(0, 0, -30 * Time.deltaTime));
        }
    }
    void Start()
    {
        GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().isChanneling = true;//Disable movement for the player
        stats = GameObject.Find("Player").GetComponent<Stats>();
        player = GameObject.Find("Player").GetComponent<PlatformerCharacter2D>();
    }
}
