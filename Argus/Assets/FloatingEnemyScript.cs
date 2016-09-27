using UnityEngine;
using System.Collections;

public class FloatingEnemyScript : MonoBehaviour {

    public bool movementDirection = true;
    public float movementVelocity = 1f;
    //enemy patrol/pathfinding variables.
    public LayerMask NotHit;
    public float fallDistance = 1f;
    public float collideDistance = 0.5f;
    public float direction = -1f;
    public float vertDirection = -1f;

    //Enemy chase variables
    public float aggroRange = 5f;
    public LayerMask aggroLayers;
    GameObject player;
    private bool chase = false;
    public float chasingVelocity = 6f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        //player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
            if(player == null)
            {
                return;
            }
        }
        Vector2 playerPos;
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

        //Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 calculateAngle = playerPos - enemyPos;

        RaycastHit2D rayToPlayer = Physics2D.Raycast(enemyPos, calculateAngle, aggroRange, aggroLayers);

        //RaycastHit2D hitDown = Physics2D.Raycast(trackPosition, new Vector2(0, -1), fallDistance, NotHit);
        //RaycastHit2D hitForwards = Physics2D.Raycast(trackPosition, new Vector2(1, 0), collideDistance, NotHit);

        //Check if the unit should enter chase mode.
        if (rayToPlayer.collider != null)
        {
            if (rayToPlayer.collider.gameObject.layer == 10)
            {
                //Debug.DrawLine(transform.position, player.transform.position, Color.red);
            }
            else if (rayToPlayer.collider.gameObject.layer == 8)
            {
                this.chase = true;
                //Debug.DrawLine(transform.position, player.transform.position, Color.blue);
            }
            else
            {
                this.chase = false;
            }
        }
        else
        {
            this.chase = false;
        }

        if(this.chase == true)
        {
            if(Vector2.Distance(playerPos, enemyPos) >= 4.0f)
            {
                Vector2 dest = new Vector2(playerPos.x - enemyPos.x, playerPos.y - enemyPos.y);
                transform.Translate(dest.normalized * Time.deltaTime * chasingVelocity);
            }
            else if(Vector2.Distance(playerPos, enemyPos) < 3.7f)
            {
                Vector2 dest = new Vector2(enemyPos.x - playerPos.x, enemyPos.y - playerPos.y);
                transform.Translate(dest.normalized * Time.deltaTime * chasingVelocity);

            }

        }
    }
}
