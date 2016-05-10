﻿using UnityEngine;
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

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        trackPoint = transform.FindChild("trackPoint");

    }

    // Update is called once per frame
    void Update()
    {
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

		if(castingSpell){
			Quaternion preCastingQuaternion = transform.rotation;
			Vector3 preCastingPosition = transform.position;
			//Tell to be idle
			if (playerPos.x > enemyPos.x)   //The player is to the right
			{
				if (direction != 1)
				{
					direction = -direction;
					transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
				}
			}
			else if (playerPos.x < enemyPos.x)
			{
				if (direction == 1)
				{
					direction = -direction;
					transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
				}
			}
		} 
		else {
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
					direction = -direction;
					transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
				}
				transform.Translate(Vector3.right * Time.deltaTime * movementVelocity * direction);
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
							direction = -direction;
							transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
						}
					}
					else if (playerPos.x < enemyPos.x)
					{
						if (direction == 1)
						{
							direction = -direction;
							transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
						}
					}

				}
				else if (playerPos.x < enemyPos.x)
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
				else if (playerPos.x > enemyPos.x)
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
}
