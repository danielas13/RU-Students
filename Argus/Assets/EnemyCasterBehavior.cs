using UnityEngine;
using System.Collections;

public class EnemyCasterBehavior : MonoBehaviour {

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
	public bool castingSpell = false;

    public bool frozen = false;
    public float frozenTimer = 2;
    private Animator CasterAnimator;
    // Use this for initialization
    Transform notFlip;
    void Start()
    {
        player = GameObject.Find("Player");
        trackPoint = transform.FindChild("trackPoint");
        CasterAnimator = transform.FindChild("Eva_Full_Animated").GetComponent<Animator>();
        notFlip = transform.FindChild("EnemyHealthBarCanvas");

    }

    void Flip()
    {
        direction = -direction;

        Debug.Log("FLIPPP" + direction);
        //transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
        //transform.rotation = transform.rotation * Quaternion.Euler(Vector3.up * 180);

        //notFlip.rotation = notFlip.rotation * Quaternion.Euler(Vector3.up * 180);
        
        // transform.FindChild("NotToFlip").transform.rotation = transform.FindChild("NotToFlip").transform.rotation * Quaternion.Euler(Vector3.up * 180);
    }

    // Update is called once per frame
    void Update()
    {

        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
        }
        if (frozen)
        {
            CasterAnimator.speed = 0f;
            frozenTimer -= Time.deltaTime;
            if (frozenTimer < 0)
            {
                frozen = false;
            }
        }
        else
        {
            CasterAnimator.speed = 1f;
            Vector2 playerPos;
            if (!game.gm.isPlayerDead)
            {
                player = GameObject.Find("Player");
                playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            }
            else
            {
                playerPos = new Vector2(game.gm.DeadState.transform.position.x, game.gm.DeadState.transform.position.y);
            }
            //Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 calculateAngle = playerPos - enemyPos;

            RaycastHit2D rayToPlayer = Physics2D.Raycast(enemyPos, calculateAngle, aggroRange, aggroLayers);

            Vector2 trackPosition = new Vector2(trackPoint.position.x, trackPoint.position.y);
            RaycastHit2D hitDown = Physics2D.Raycast(trackPosition, new Vector2(0, -1), fallDistance, NotHit);
            RaycastHit2D hitForwards = Physics2D.Raycast(trackPosition, new Vector2(1, 0), collideDistance, NotHit);
            Debug.DrawLine(trackPoint.position, hitForwards.point, Color.red);

            if (castingSpell)
            {
                Quaternion preCastingQuaternion = transform.rotation;
                Vector3 preCastingPosition = transform.position;
                //Tell to be idle
                if (playerPos.x > enemyPos.x)   //The player is to the right
                {
                    if (direction != 1)
                    {
                        Flip();
                    }
                }
                else if (playerPos.x < enemyPos.x)
                {
                    if (direction == 1)
                    {
                        Flip();
                    }
                }
            }
            else
            {
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

                if (!this.chase)
                {
                    if (hitDown.collider != null && hitForwards.collider == null)
                    {

                    }
                    else
                    {
                        Flip();
                    }
                    transform.Translate(Vector3.left * Time.deltaTime * movementVelocity);
                }
                else
                {
                    //The player is to the right.
                    if ((Mathf.Abs(playerPos.x - enemyPos.x)) < 0.1f)
                    {
                        if (playerPos.x > enemyPos.x)
                        {
                            if (direction != 1)
                            {
                                Flip();
                            }
                        }
                        else if (playerPos.x < enemyPos.x)
                        {
                            if (direction == 1)
                            {
                                Flip();
                            }
                        }

                    }
                    else if (playerPos.x < enemyPos.x)
                    {
                        if (direction != 1)
                        {
                            Flip();
                        }
                        if (hitDown.collider != null && hitForwards.collider == null)
                        {
                            transform.Translate(Vector3.left * Time.deltaTime * chasingVelocity);
                        }
                    }
                    else if (playerPos.x > enemyPos.x)
                    {
                        if (direction == 1)
                        {
                            Flip();
                        }
                        if (hitDown.collider != null && hitForwards.collider == null)
                        {
                            transform.Translate(Vector3.left * Time.deltaTime * chasingVelocity);
                        }
                    }
                }
            }
        }
        

    }
}
