using UnityEngine;
using System.Collections;

public class CastMultipleSpells : MonoBehaviour {

    public LayerMask NotHit;
    public Transform FirePrefab;
    public float spellDistance = 1000;
    GameObject player;
    private float cooldown = 3;
    private float angle;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

        }
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        


        Vector2 calculateAngle = new Vector2(playerPos.x - enemyPos.x, playerPos.y - enemyPos.y);
        calculateAngle.Normalize();
        angle = Mathf.Atan2(calculateAngle.y, calculateAngle.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //Debug.Log("angle:  " + angle);

        

        RaycastHit2D hit = Physics2D.Raycast(transform.position, calculateAngle, spellDistance, NotHit);
        //Debug.DrawLine(transform.position, new Vector2(transform.position.x - spellDistance, character.transform.position.y), Color.red);
        if (hit.collider != null && cooldown < 0)
        {
            //Debug.Log(hit.transform.tag + " detected");
            if (hit.transform.tag == "Player")
            {
                CastFireBall();
                cooldown = 3;
            }
        }
        cooldown -= Time.deltaTime;
    }
    void CastFireBall()
    {
        //Vector2 SpellPosition = new Vector2(transform.position.x, transform.position.y);
        //Vector2 playerPos = player.transform.position;
        //RaycastHit2D hit = Physics2D.Raycast(SpellPosition, new Vector2(SpellPosition.x - playerPos.x, SpellPosition.y - playerPos.y), spellDistance, NotHit
        
        Instantiate(FirePrefab, transform.position, transform.rotation );
        
    }
}
