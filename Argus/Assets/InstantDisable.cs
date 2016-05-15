using UnityEngine;
using System.Collections;

public class InstantDisable : MonoBehaviour
{
    private Stats stats;
    private float spendMana = 1;
    private bool stoppedChannel = false;//Check to make sure that the player can not rechannel the already in effect spell
    public LayerMask notHit;
    private float freezeTime = 1;
    private float damageCooldown = 1;
    private bool outOfMana = false;

    private static readonly System.Random damageGen = new System.Random();

    void Update()
    {
        
        if(stats.status.currentMana < 1)
        {
            outOfMana = true;
        }

        if (!Input.GetKey(KeyCode.T) || outOfMana)
        {
            GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().isChanneling = false;//Re-enable movement for the player
            Destroy(this.gameObject);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), 20, notHit);
        if(hit.transform != null)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                int randomDmg = damageGen.Next(stats.status.minSpellPower, stats.status.maxSpellPower);
                hit.transform.GetComponent<EnemyStats>().damageEnemy(randomDmg / 2);
                damageCooldown = 1;
            }
            
            freezeTime -= Time.deltaTime;
            if (freezeTime < 0)
            {
                hit.transform.GetComponent<EnemyBehavior>().frozen = true;
                hit.transform.GetComponent<EnemyBehavior>().frozenTimer = 4f;
            }
            
        }
        else
        {
            freezeTime = 1;
        }

        if (spendMana < 0)
        {

            stats.spendMana(1);
            spendMana = 1;
        }
        spendMana -= Time.deltaTime;
        
        

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(0, 0, 10 * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(new Vector3(0, 0, -10 *Time.deltaTime));
        }
    }
    void Start()
    {
        GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().isChanneling = true;//Disable movement for the player
        stats = GameObject.Find("Player").GetComponent<Stats>();
    }
}


