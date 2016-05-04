using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{

    public bool movementDirection = true;
    public float movementVelocity = 1f;
    Transform trackPoint;
    //enemy patrol/pathfinding variables.
    public LayerMask NotHit;
    public float fallDistance = 1f;
    public float collideDistance = 0.5f;
    public float direction = -1f;

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
        trackPoint = transform.FindChild("trackPoint");

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 calculateAngle = playerPos - enemyPos;

        RaycastHit2D rayToPlayer = Physics2D.Raycast(enemyPos, calculateAngle, aggroRange, aggroLayers);

        Vector2 trackPosition = new Vector2(trackPoint.position.x, trackPoint.position.y);
        RaycastHit2D hitDown = Physics2D.Raycast(trackPosition, new Vector2(0, -1), fallDistance, NotHit);
        RaycastHit2D hitForwards = Physics2D.Raycast(trackPosition, new Vector2(1, 0), collideDistance, NotHit);

        //Check if the unit should enter chase mode.
        if (rayToPlayer.collider != null)
        {
            if(rayToPlayer.collider.gameObject.layer == 10)
            {
                //Debug.DrawLine(transform.position, player.transform.position, Color.red);
            }
            else if(rayToPlayer.collider.gameObject.layer == 8)
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

        if (game.gm.isPlayerDead)
        {
            chase = false;
            transform.FindChild("EnemyMeleeAttackTrigger").GetComponent<EnemyMeleeAttackTriggerScript>().withinAttackRange = false;
        }
        if (!this.chase)
        {
            if (hitDown.collider != null && hitForwards.collider == null)
            {

            }
            else
            {
               direction = -direction;
               transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
             }
            transform.Translate(Vector3.right * Time.deltaTime * movementVelocity * direction);
        }
        else
        {
            //The player is to the right.
            if((Mathf.Abs(playerPos.x - enemyPos.x)) < 1.5f){
                if(playerPos.x > enemyPos.x)
                {
                    if (direction != 1)
                    {
                        direction = -direction;
                        transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                    }
                }
                else if(playerPos.x < enemyPos.x)
                {
                    if (direction == 1)
                    {
                        direction = -direction;
                        transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                    }
                }
            }
           else  if(playerPos.x > enemyPos.x)
            {
                if (direction != 1)
                {
                    direction = -direction;
                    transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                }
                if (hitDown.collider != null && hitForwards.collider == null) 
                {
                    transform.Translate(Vector3.right * Time.deltaTime * chasingVelocity * direction);
                }
            }
            else if(playerPos.x < enemyPos.x)
            {
                if (direction == 1)
                {
                    direction = -direction;
                    transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                }
                if (hitDown.collider != null && hitForwards.collider == null)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * chasingVelocity * direction);
                }      
            }
        }
    }
}
