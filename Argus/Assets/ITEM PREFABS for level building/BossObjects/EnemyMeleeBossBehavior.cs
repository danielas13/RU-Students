using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMeleeBossBehavior : MonoBehaviour {
    private static readonly System.Random randomDamageGenerator = new System.Random();     //Create a read only random variable.
    public bool movementDirection = true;
    public float movementVelocity = 1f;
    Transform trackPoint;
    //enemy patrol/pathfinding variables.
    public LayerMask NotHit;
    public float fallDistance = 1f;
    public float collideDistance = 0.5f;
    public float direction = -1f;

    public GameObject EnemyPrefab;
    private float Cooldown = 12;

    private List<GameObject> MinionList = new List<GameObject>();
 

    //Enemy chase variables
    public float aggroRange = 5f;
    public LayerMask aggroLayers;
    GameObject player;
    private bool chase = false;
    public float chasingVelocity = 6f;
    private bool KnightIsAttacking = false;
    private bool pickedActionBetweenAttacks = false;
    private bool isBlocking = false;
    private bool midAnimation = false;
    public Transform SwordTrigger;

    Vector2 enemyPos;
    Vector2 playerPos;

    public GameObject PowerSwing;       //For the boss fireEffectPrefab
    private float PowerTimer = 0;
    private bool isPowerAttacking = true;


    private Animator KnightAnimator;
    private Transform Knight;

    private bool normalAnimationPlayed = false, heavyAnimationPlayed = false, comboAnimationPlayed = false, shieldAnimationPlayed = false;

    //new shit
    public enum KnightState { Calm, Combat, Attacking, Chasing, Searching, Patrolling, Idle, SingleAttack, HacknSlash, ChargedAttack };
    [HideInInspector]
    public int CurrentState;
    Transform withinRangeTrigger;
    private static readonly System.Random randomAttackIDGenerator = new System.Random();
    float attackCooldown = 1;
    float attackTimer = 0;

    private float alertTimer = 2;
    private float calmTimer = -1;
    private bool Patrolling = true;
    private EnemyMeleeBossStats enemyStat;
    private int currentDmg = 30;

    private bool HeavyAttackPicked = false, ComboAttackPicked = false, NormalAttackPicked = false, BlockingPicked = false, CombatModePicked = false, shieldAttackPicked = false;    /* make sure we do nothing else when we commit to one of these actions by flagging their designated bool while performing them */
    public bool frozen = false;
    public float frozenTimer = 2;

    private float comboTimer = 0;
    public bool CombatStarted = false;

    /* Update Variables made accessible */
    private RaycastHit2D hitForwards, hitDown, rayToPlayer;

    // Use this for initialization
    void Start()
    {
        enemyStat = transform.GetComponent<EnemyMeleeBossStats>();
        //Time.timeScale = 0.4F;


        player = GameObject.Find("Player");
        trackPoint = transform.FindChild("trackPoint");
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);



        Knight = transform.FindChild("Dismounted_Knight");
        KnightAnimator = Knight.GetComponent<Animator>();

        CurrentState = (int)KnightState.Calm; // Default state
        withinRangeTrigger = transform.FindChild("EnemyMeleeAttackTrigger");
        SwordTrigger.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("frozen: " + frozen + " Timer: " + frozenTimer);
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            frozen = !frozen;
        }*/
        if (frozen)
        {
            KnightAnimator.speed = 0f;
            frozenTimer -= Time.deltaTime;
            if (frozenTimer < 0)
            {
                frozen = false;
            }
        }
        else
        {
            KnightAnimator.speed = 1;
            /* Check if the player is visible to the enemy using rays*/
            playerPos = new Vector2(player.transform.position.x, player.transform.position.y);    
            enemyPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 calculateAngle = playerPos - enemyPos;

            rayToPlayer = Physics2D.Raycast(enemyPos, calculateAngle, aggroRange, aggroLayers);

            Vector2 trackPosition = new Vector2(trackPoint.position.x, trackPoint.position.y);
            hitDown = Physics2D.Raycast(trackPosition, new Vector2(0, -1), fallDistance, NotHit);
            hitForwards = Physics2D.Raycast(trackPosition, new Vector2(1, 0), collideDistance, NotHit);

            //Debug.Log ("Heavy: " + HeavyAttackPicked + "             Combo: " +ComboAttackPicked + "             Normal: " + NormalAttackPicked + "             block: " + BlockingPicked + "             combatmode: " + CombatModePicked);

            if (HeavyAttackPicked == true)
            { //Currently doing Heavy Attack, allow nothing else to happen meanwhile
              //set anim to true
                HeavyAttack();
            }
            else if (shieldAttackPicked == true)
            {
                ShieldAttack();
            }
            else if (ComboAttackPicked == true)
            {  //Currently doing Combo Attack, allow nothing else to happen meanwhile
                ComboAttack();
            }
            else if (NormalAttackPicked == true)
            {  //Currently doing Normal Attack, allow nothing else to happen meanwhile
               //set anim to true
                NormalAttack();

            }
            else if (BlockingPicked == true)
            { //Currently doing Blocking, allow nothing else to happen meanwhile
              //set anim to true
                Block();
            }
            else if (CombatModePicked == true)
            { //Currently doing Combat Mode, allow nothing else to happen meanwhile
              //set anim to true
              //KnightAnimator.SetBool ("CombatMode",CombatModePicked);
                CombatMode();
            }
            else
            {                               // No current animation or action being performed, seek next action



                /* check if the ray collides with the player */
                if (rayToPlayer.collider != null)
                {                                           // Ray has hit ANY object
                    if (rayToPlayer.collider.gameObject.layer == 10)
                    {                       // If we hit ground, don't react
                                            //Do NOTHING!
                    }
                    else if (rayToPlayer.collider.gameObject.layer == 8)
                    {                   // If we find player, switch to combat mode
                        CurrentState = (int)KnightState.Combat;
                    }

                }
                else
                {
                    if (CurrentState == (int)KnightState.Attacking || CurrentState == (int)KnightState.Combat || CurrentState == (int)KnightState.Chasing)
                    {
                        CurrentState = (int)KnightState.Searching;
                    }
                }
                /*else{
                if (CurrentState != (int)KnightState.Searching){				// if not in combat and not searching, go to calm by default
                    CurrentState = (int)KnightState.Calm;
                }

            }*/

                /* Depending in state, call the relevant state function */
                switch (CurrentState)
                {
                    case (int)KnightState.Calm:
                        Calm();

                        break;
                    case (int)KnightState.Combat:
                        Combat();
                        break;
                    case (int)KnightState.Searching:
                        Searching();
                        break;
                    default:
                        CurrentState = (int)KnightState.Searching;
                        Searching();
                        break;
                }

            }
        }
        if(PowerTimer >= 0)
        {
            PowerTimer -= Time.deltaTime;
        }

        if(Cooldown >= 0 && CombatStarted)
        {
            Cooldown -= Time.deltaTime;
        }


    } /* Update */

    /* Either chose to attack or chase */
    private void Combat()
    {
        CombatStarted = true;
        KnightAnimator.SetBool("Idle", false);
        KnightAnimator.SetBool("Patrolling", false);
        KnightAnimator.SetBool("Combat", true);
        KnightAnimator.SetBool("Calm", false);
        KnightAnimator.SetBool("Searching", false);

        if (withinRangeTrigger.GetComponent<EnemyMeleeAttackTriggerScript>().withinAttackRange)
        {   /* If the player is within attack range and not blocking or in combat*/
            CurrentState = (int)KnightState.Attacking;
            Attacking();

        }
        else if (!midAnimation)
        {                                                                           /* If the player is out of attack range */
            CurrentState = (int)KnightState.Chasing;
            Chasing();
        }
        if(Cooldown <= 0)
        {
            MinionList.Add ((GameObject)Instantiate(EnemyPrefab, new Vector3(transform.position.x + 1, transform.position.y+2, transform.position.z-2), EnemyPrefab.transform.rotation));
            Cooldown = 12;
        }
    }

    public void ResetMinions()
    {
        foreach(GameObject minion in MinionList)
        {
            Destroy(minion);
        }
        MinionList = new List<GameObject>();
        Cooldown = 12;
    }

    private void Attacking()
    {
        KnightAnimator.SetBool("Attacking", true);
        KnightAnimator.SetBool("Chasing", false);
        KnightAnimator.SetBool("Searching", false);
        KnightAnimator.SetBool("CombatMode", false);

        int KnightAttackID = randomAttackIDGenerator.Next(0, 8);

        if ((Mathf.Abs(playerPos.x - enemyPos.x)) < 1.5f)
        {               //if the player is super close, prioritize knocking him back 
            shieldAttackPicked = true;
            KnightAnimator.SetBool("ShieldAttack", true);
            currentDmg = randomDamageGenerator.Next(enemyStat.status.minDamage - 5, enemyStat.status.maxDamage - 5);

            player.GetComponent<Stats>().damagePlayer(currentDmg);
            player.GetComponent<PlatformerCharacter2D>().knockBackPlayer(transform.position);
        }
        else
        {                                                           //else, pick an attack

            if (KnightAttackID == 0 || KnightAttackID == 1 || KnightAttackID == 4 || KnightAttackID == 5)
            {       // 20% chance for a heavy attack
                HeavyAttackPicked = true;
                KnightAnimator.SetBool("HeavyAttack", true);
            }
            else if (KnightAttackID == 2 || KnightAttackID == 3)
            {   // 20% chance for a combo attack
                ComboAttackPicked = true;
                KnightAnimator.SetBool("ComboAttack", true);
            }
            else
            {                                                   // 60% chance for a normal attack
                NormalAttackPicked = true;
                KnightAnimator.SetBool("DefaultAttack", true);
            }


        }

        int block = randomAttackIDGenerator.Next(0, 10);
        if (block <= 2)
        {               // randomly select action
                        //BlockingPicked = true;
            CombatModePicked = true;
        }
        else
        {
            CombatModePicked = true;
        }
    }


    void NormalAttack()
    {


        if (KnightAnimator.GetCurrentAnimatorStateInfo(0).IsName("MKnght_1H_sword_swing_high_right"))
        {
            if (!normalAnimationPlayed)
            {
                SwordTrigger.gameObject.SetActive(true);
            }
            normalAnimationPlayed = true;

            currentDmg = randomDamageGenerator.Next(enemyStat.status.minDamage, enemyStat.status.maxDamage);

            SwordTrigger.GetComponent<EnemySwordTriggerScript>().damage = currentDmg;
        }
        else if (normalAnimationPlayed == true)
        {
            NormalAttackPicked = false;
            KnightAnimator.SetBool("DefaultAttack", false);
            KnightAnimator.SetBool("Attacking", false);
            KnightAnimator.SetBool("CombatMode", true);
            SwordTrigger.gameObject.SetActive(false);
            normalAnimationPlayed = false;
        }
        else
        {
            // Do nothing, wait for animation to play 
        }


    }


    void ShieldAttack()
    {
        if (KnightAnimator.GetCurrentAnimatorStateInfo(0).IsName("MKnght_1H_shield_blow"))
        {
            //SwordTrigger.gameObject.SetActive (true);
            //SwordTrigger.GetComponent <EnemySwordTriggerScript>().damage = transform.GetComponent <EnemyStats>().status.damage *2;

            shieldAnimationPlayed = true;
        }
        else if (shieldAnimationPlayed == true)
        {
            shieldAttackPicked = false;
            KnightAnimator.SetBool("ShieldAttack", false);
            KnightAnimator.SetBool("Attacking", false);
            KnightAnimator.SetBool("CombatMode", true);
            SwordTrigger.gameObject.SetActive(false);
            shieldAnimationPlayed = false;


        }
        else
        {
            // Do nothing, wait for animation to play 
        }
    }
    void HeavyAttack()
    {

        if (KnightAnimator.GetCurrentAnimatorStateInfo(0).IsName("MKnght_1H_Heavy Smash"))
        {
            if (!isPowerAttacking)
            {
                PowerTimer = 2f;
            }
            if (!heavyAnimationPlayed)
            {
                SwordTrigger.gameObject.SetActive(true);
                isPowerAttacking = true;


            }

            currentDmg = randomDamageGenerator.Next(enemyStat.status.minDamage, enemyStat.status.maxDamage);
            SwordTrigger.GetComponent<EnemySwordTriggerScript>().damage = currentDmg * 2;
            heavyAnimationPlayed = true;

            if (isPowerAttacking)
            {
                if (PowerTimer <= 0)
                {
                    Instantiate(PowerSwing, new Vector3(this.transform.position.x, this.transform.position.y - 0.6f, this.transform.position.z), this.transform.rotation);
                    isPowerAttacking = false;
                }
            }


        }
        else if (heavyAnimationPlayed == true)
        {
            HeavyAttackPicked = false;
            KnightAnimator.SetBool("HeavyAttack", false);
            KnightAnimator.SetBool("Attacking", false);
            KnightAnimator.SetBool("CombatMode", true);
            SwordTrigger.gameObject.SetActive(false);
            heavyAnimationPlayed = false;
           // Instantiate(PowerSwing, new Vector3(this.transform.position.x, this.transform.position.y - 0.6f, this.transform.position.z), this.transform.rotation);
            //PowerSwing.SetActive(true);
            //PowerSwing.GetComponent<BossSwingScript>().restart();

        }
        else
        {

            // Do nothing, wait for animation to play 
        }

    }
    void ComboAttack()
    {

        if (KnightAnimator.GetCurrentAnimatorStateInfo(0).IsName("MKnght_1H_5X_Combo_move_forward"))
        {
            if (SwordTrigger.gameObject.activeSelf == false)
            {
                comboTimer -= Time.deltaTime;
            }

            if (comboTimer < 0)
            {
                SwordTrigger.gameObject.SetActive(true);
                comboTimer = 0.5f;
            }

            comboAnimationPlayed = true;
            currentDmg = randomDamageGenerator.Next(enemyStat.status.minDamage, enemyStat.status.maxDamage);

            SwordTrigger.GetComponent<EnemySwordTriggerScript>().damage = currentDmg;

        }
        else if (comboAnimationPlayed == true)
        {
            ComboAttackPicked = false;
            KnightAnimator.SetBool("ComboAttack", false);
            KnightAnimator.SetBool("Attacking", false);
            KnightAnimator.SetBool("CombatMode", true);
            SwordTrigger.gameObject.SetActive(false);
            comboAnimationPlayed = false;
        }
        else
        {
            // Do nothing, wait for animation to play 
        }
    }

    private void Block()
    {
        //TODO blocking
        BlockingPicked = false;
    }
    private void CombatMode()
    {

        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0)
        {
            attackCooldown = 1;
            KnightAnimator.SetBool("CombatMode", false);
            CombatModePicked = false;
        }

        if (hitDown.collider != null && hitForwards.collider == null)                               // There is ground to move on, but no wall to collide on we translate the enemy
        {
            transform.Translate(Vector3.right * Time.deltaTime * (float)movementVelocity / 1.5f);   //Translate our character
                                                                                                    //transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;
        }
        else                                                                                        //There is no ground in this direction or we hit a wall, we turn the enemy around and walk in that direction
        {
            Flip();
            transform.Translate(Vector3.right * Time.deltaTime * movementVelocity);     //Translate our character after turning
                                                                                        /*direction = -direction;
                                                                                        transform.rotation = transform.rotation * Quaternion.Euler(Vector3.up * 180);
                                                                                        //transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                                                                                        transform.Translate(Vector3.right * Time.deltaTime * (float)movementVelocity / 1.5f);  	//Translate our character after turning
                                                                                        //transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;*/

        }


    }

    private void Chasing()
    {
        KnightAnimator.SetBool("Chasing", true);
        KnightAnimator.SetBool("Attacking", false);
        KnightAnimator.SetBool("Searching", false);


        float xDiff = player.transform.position.x - transform.position.x; //relative X position between player and enemy

        if (Mathf.Abs(xDiff) < aggroRange + 2)
        { // if we are within aggro range, keep chasing

            if ((Mathf.Abs(playerPos.x - enemyPos.x)) < 1.5f)
            {  //if the player is within 1.5 units of the enemy, the enemy  will stand still to avoid glitchy behaviour
                if (playerPos.x > enemyPos.x)
                {
                    if (direction != 1)
                    {
                        Flip();
                        /*direction = -direction;
                        //transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                        transform.rotation = transform.rotation * Quaternion.Euler(Vector3.up * 180);*/
                    }
                }
                else if (playerPos.x < enemyPos.x)
                {
                    if (direction == 1)
                    {
                        Flip();
                        /*direction = -direction;
                        //transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                        transform.rotation = transform.rotation * Quaternion.Euler(Vector3.up * 180);*/
                    }
                }
                //KnightAnimator.SetFloat ("CombatMovementSpeed", 0);
            }
            else if (playerPos.x > enemyPos.x)              //if the player is outside of 1.5 units of the enemy, the enemy will chase the player 
            {
                //KnightAnimator.SetFloat ("CombatMovementSpeed", 1);
                if (direction != 1)
                {
                    Flip();
                    /*
						direction = -direction;
						//transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
						transform.rotation = transform.rotation * Quaternion.Euler(Vector3.up * 180);*/
                }
                if (hitDown.collider != null && hitForwards.collider == null)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * chasingVelocity);
                }
            }
            else if (playerPos.x < enemyPos.x)
            {
                if (direction == 1)
                {
                    Flip();
                    /*direction = -direction;
                    //transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                    transform.rotation = transform.rotation * Quaternion.Euler(Vector3.up * 180);*/
                }
                if (hitDown.collider != null && hitForwards.collider == null)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * chasingVelocity);
                }
            }
        }
        else
        {
            CurrentState = (int)KnightState.Searching;
            Searching();

        }

    }

    private void Searching()
    {
        KnightAnimator.SetBool("Combat", false);
        KnightAnimator.SetBool("Chasing", false);
        KnightAnimator.SetBool("Attacking", false);

        KnightAnimator.SetBool("Searching", true);

        alertTimer -= Time.deltaTime;
        if (alertTimer < 0)
        {
            CurrentState = (int)KnightState.Calm;
            alertTimer = 2; // CHANGE VALUE!!!
            KnightAnimator.SetBool("Searching", false);

            Calm();

        }
        else
        {
            if (hitDown.collider != null && hitForwards.collider == null)                               // There is ground to move on, but no wall to collide on we translate the enemy
            {
                transform.Translate(Vector3.right * Time.deltaTime * movementVelocity / 2 * direction);     //Translate our character
                                                                                                            //transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;
            }
            else                                                                                        //There is no ground in this direction or we hit a wall, we turn the enemy around and walk in that direction
            {
                Flip();

                //direction = -direction;
                //transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                transform.Translate(Vector3.right * Time.deltaTime * movementVelocity / 2 * direction);     //Translate our character after turning
                                                                                                            //transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;

            }
        }

    }

    private void Calm()
    {
        KnightAnimator.SetBool("Calm", true);
        calmTimer -= Time.deltaTime;


        /* Randomly select patrolling or idle as current calm state */
        if (calmTimer < 0)
        {
            int calmID = randomAttackIDGenerator.Next(0, 10);
            if (calmID <= 4)
            {
                Patrolling = true;
            }
            else
            {
                Patrolling = false;
            }
            calmTimer = 12; //Resetting the timer in which defines how long the enemy stays within the current Calm state
        }

        /* Depending on randomly selected state, either patrol or stay idle */
        if (Patrolling)
        {
            Patrol();
        }
        else
        {
            Idle();
        }
    } /* Calm() */

    /* TODO change to velocity over translate -> inerta problems */
    private void Patrol()
    {
        KnightAnimator.SetBool("Idle", false);
        KnightAnimator.SetBool("Patrolling", true);
        if (hitDown.collider != null && hitForwards.collider == null)                               // There is ground to move on, but no wall to collide on we translate the enemy
        {
            transform.Translate(Vector3.right * Time.deltaTime * movementVelocity);     //Translate our character
                                                                                        //transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;
        }
        else                                                                                        //There is no ground in this direction or we hit a wall, we turn the enemy around and walk in that direction
        {
            Flip();
            transform.Translate(Vector3.right * Time.deltaTime * movementVelocity);     //Translate our character after turning
                                                                                        /*direction = -direction;
                                                                                        //transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
                                                                                        transform.rotation = transform.rotation * Quaternion.Euler(Vector3.up * 180);
                                                                                        transform.Translate(Vector3.right * Time.deltaTime * movementVelocity);  	//Translate our character after turning
                                                                                        //transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;*/

        }
    }

    private void Idle()
    {
        KnightAnimator.SetBool("Patrolling", false);
        KnightAnimator.SetBool("Idle", true);



    }
    void Flip()
    {
        direction = -direction;
        //transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
        transform.rotation = transform.rotation * Quaternion.Euler(Vector3.up * 180);
        transform.FindChild("NotToFlip").transform.rotation = transform.FindChild("NotToFlip").transform.rotation * Quaternion.Euler(Vector3.up * 180);
    }

}
