using UnityEngine;
using System.Collections;

namespace  UnityStandardAssets._2D
{
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
		private bool KnightIsAttacking = false;
		private bool pickedActionBetweenAttacks = false;
		private bool isBlocking = false;
		private bool midAnimation = false;
		public Transform SwordTrigger;

		Vector2 enemyPos;
		Vector2 playerPos;


		private Animator KnightAnimator; 
		private Transform Knight;

		private bool normalAnimationPlayed = false, heavyAnimationPlayed = false, comboAnimationPlayed = false, shieldAnimationPlayed = false;

		//new shit
		enum KnightState {Calm, Combat, Attacking, Chasing, Searching, Patrolling, Idle, SingleAttack, HacknSlash, ChargedAttack};
		private int CurrentState;
		Transform withinRangeTrigger;
		private static readonly System.Random randomAttackIDGenerator = new System.Random();
		float attackCooldown = 1;
		float attackTimer = 0;

		private float alertTimer = 2;
		private float calmTimer = -1;
		private bool Patrolling = true;

		private bool HeavyAttackPicked = false, ComboAttackPicked = false, NormalAttackPicked = false, BlockingPicked = false, CombatModePicked = false, shieldAttackPicked = false; 	/* make sure we do nothing else when we commit to one of these actions by flagging their designated bool while performing them */


		/* Update Variables made accessible */
		private RaycastHit2D hitForwards, hitDown, rayToPlayer;

		// Use this for initialization
		void Start()
		{

			//Time.timeScale = 0.4F;


			player = GameObject.Find("Player");
			trackPoint = transform.FindChild("trackPoint");
			Physics2D.IgnoreLayerCollision(8, 9, true);
			Physics2D.IgnoreLayerCollision(8, 8, true);



			Knight = transform.FindChild("Dismounted_Knight");
			KnightAnimator = Knight.GetComponent<Animator> ();

			CurrentState = (int)KnightState.Calm; // Default state
			withinRangeTrigger = transform.FindChild("EnemyMeleeAttackTrigger");
			SwordTrigger.gameObject.SetActive (false);

		}

		// Update is called once per frame
		void Update ()
		{
			/* Check if the player is visible to the enemy using rays*/
			if (!game.gm.isPlayerDead) {
				player = GameObject.Find ("Player");
				playerPos = new Vector2 (player.transform.position.x, player.transform.position.y);
			} else {
				playerPos = new Vector2 (game.gm.DeadState.transform.position.x, game.gm.DeadState.transform.position.y);
			}
			enemyPos = new Vector2 (transform.position.x, transform.position.y);
			Vector2 calculateAngle = playerPos - enemyPos;

			rayToPlayer = Physics2D.Raycast (enemyPos, calculateAngle, aggroRange, aggroLayers);

			Vector2 trackPosition = new Vector2 (trackPoint.position.x, trackPoint.position.y);
			hitDown = Physics2D.Raycast (trackPosition, new Vector2 (0, -1), fallDistance, NotHit);
			hitForwards = Physics2D.Raycast (trackPosition, new Vector2 (1, 0), collideDistance, NotHit);

			Debug.Log ("Heavy: " + HeavyAttackPicked + "             Combo: " +ComboAttackPicked + "             Normal: " + NormalAttackPicked + "             block: " + BlockingPicked + "             combatmode: " + CombatModePicked);

			if(HeavyAttackPicked == true){ //Currently doing Heavy Attack, allow nothing else to happen meanwhile
				//set anim to true
				HeavyAttack ();
			}
			else if(shieldAttackPicked == true){
				ShieldAttack ();
			}
			else if(ComboAttackPicked == true){  //Currently doing Combo Attack, allow nothing else to happen meanwhile
				ComboAttack ();
			}
			else if(NormalAttackPicked == true){  //Currently doing Normal Attack, allow nothing else to happen meanwhile
				//set anim to true
				Debug.Log ("Normal Attack each frame yo ");
				NormalAttack ();

			}
			else if (BlockingPicked == true){ //Currently doing Blocking, allow nothing else to happen meanwhile
				//set anim to true
				Block();
			}
			else if (CombatModePicked == true){ //Currently doing Combat Mode, allow nothing else to happen meanwhile
				//set anim to true
				//KnightAnimator.SetBool ("CombatMode",CombatModePicked);
				CombatMode ();
			}
			else{ 								// No current animation or action being performed, seek next action



				/* check if the ray collides with the player */
				if (rayToPlayer.collider != null) {											// Ray has hit ANY object
					if (rayToPlayer.collider.gameObject.layer == 10) { 						// If we hit ground, don't react
						//Do NOTHING!
					} 
					else if (rayToPlayer.collider.gameObject.layer == 8) { 					// If we find player, switch to combat mode
						CurrentState = (int)KnightState.Combat;
					} 

				} 
				else{
					if(CurrentState == (int)KnightState.Attacking || CurrentState == (int)KnightState.Combat || CurrentState == (int)KnightState.Chasing){
						CurrentState = (int)KnightState.Searching;
					}
				}
				/*else{
				if (CurrentState != (int)KnightState.Searching){				// if not in combat and not searching, go to calm by default
					CurrentState = (int)KnightState.Calm;
				}
				
			}*/

				/* Depending in state, call the relevant state function */
				Debug.Log ("State of player: " + CurrentState);
				switch (CurrentState) {
				case (int)KnightState.Calm:
					Debug.Log ("Calm??");	
					Calm ();

					break;
				case (int)KnightState.Combat:
					Combat ();
					break;
				case (int)KnightState.Searching:
					Searching ();
					break;
				default:
					CurrentState = (int)KnightState.Searching;
					Searching ();
					break;
				}

			}
		} /* Update */

		/* Either chose to attack or chase */
		private void Combat(){
			KnightAnimator.SetBool ("Idle", false);
			KnightAnimator.SetBool ("Patrolling", false);
			KnightAnimator.SetBool ("Combat", true);
			KnightAnimator.SetBool ("Calm", false);
			KnightAnimator.SetBool ("Searching", false);

			if (withinRangeTrigger.GetComponent <EnemyMeleeAttackTriggerScript>().withinAttackRange){ 	/* If the player is within attack range and not blocking or in combat*/ 
				CurrentState = (int)KnightState.Attacking;
				Debug.Log ("Attacking");
				Attacking ();

			}
			else if (!midAnimation){																			/* If the player is out of attack range */
				CurrentState = (int)KnightState.Chasing;
				Debug.Log ("Chasing");
				Chasing ();
			}
		}

		private void Attacking(){
			KnightAnimator.SetBool ("Attacking", true);
			KnightAnimator.SetBool ("Chasing", false);
			KnightAnimator.SetBool ("Searching", false);
			KnightAnimator.SetBool ("CombatMode", false);


			int KnightAttackID = randomAttackIDGenerator.Next (0, 10); 

			if((Mathf.Abs(playerPos.x - enemyPos.x)) < 1.5f){ 				//if the player is super close, prioritize knocking him back 
				shieldAttackPicked = true;
				KnightAnimator.SetBool ("ShieldAttack", true);
				player.GetComponent <Stats>().damagePlayer (2);
				player.GetComponent <PlatformerCharacter2D> ().knockBackPlayer(transform.position);
			}
			else{															//else, pick an attack

				Debug.Log ("PICKING NEW ATTACK");
				if (KnightAttackID == 0 || KnightAttackID == 1) { 		// 20% chance for a heavy attack
					HeavyAttackPicked = true; 
					KnightAnimator.SetBool ("HeavyAttack", true);
				} 
				else if (KnightAttackID == 2 || KnightAttackID == 3) { 	// 20% chance for a combo attack
					ComboAttackPicked = true; 
					KnightAnimator.SetBool ("ComboAttack", true);
				} 
				else {													// 60% chance for a normal attack
					NormalAttackPicked = true; 
					KnightAnimator.SetBool ("DefaultAttack", true);
				}


			}

			int block = randomAttackIDGenerator.Next (0, 10);
			if (block <= 2) {				// randomly select action
				//BlockingPicked = true;
				CombatModePicked = true;
			} 
			else {
				CombatModePicked = true;
			}
		}


		void NormalAttack(){


			if(KnightAnimator.GetCurrentAnimatorStateInfo(0).IsName("MKnght_1H_sword_swing_high_right")){
				Debug.Log ("NORMAL");
				SwordTrigger.gameObject.SetActive (true);
				normalAnimationPlayed = true;
				SwordTrigger.GetComponent <EnemySwordTriggerScript>().damage = transform.GetComponent <EnemyStats>().status.damage;
			}
			else if (normalAnimationPlayed == true){
				NormalAttackPicked = false;
				KnightAnimator.SetBool ("DefaultAttack", false);
				KnightAnimator.SetBool ("Attacking", false);
				KnightAnimator.SetBool ("CombatMode", true);
				SwordTrigger.gameObject.SetActive (false);
				normalAnimationPlayed = false; 
			}
			else{
				// Do nothing, wait for animation to play 
			}


		}


		void ShieldAttack(){
			if (KnightAnimator.GetCurrentAnimatorStateInfo (0).IsName ("MKnght_1H_shield_blow")) {
				Debug.Log ("ShieldAttack");
				//SwordTrigger.gameObject.SetActive (true);
				//SwordTrigger.GetComponent <EnemySwordTriggerScript>().damage = transform.GetComponent <EnemyStats>().status.damage *2;

				shieldAnimationPlayed = true;
			} 
			else if (shieldAnimationPlayed == true){
				shieldAttackPicked = false;
				KnightAnimator.SetBool ("ShieldAttack", false);
				KnightAnimator.SetBool ("Attacking", false);
				KnightAnimator.SetBool ("CombatMode", true);
				SwordTrigger.gameObject.SetActive (false);
				shieldAnimationPlayed = false;


			}
			else{
				// Do nothing, wait for animation to play 
			}
		}
		void HeavyAttack(){

			if (KnightAnimator.GetCurrentAnimatorStateInfo (0).IsName ("MKnght_1H_sword_swing_high_straight_down")) {
				Debug.Log ("HEAVY");
				SwordTrigger.gameObject.SetActive (true);
				SwordTrigger.GetComponent <EnemySwordTriggerScript>().damage = transform.GetComponent <EnemyStats>().status.damage *2;
				heavyAnimationPlayed = true;
			} 
			else if (heavyAnimationPlayed == true){
				HeavyAttackPicked = false;
				KnightAnimator.SetBool ("HeavyAttack", false);
				KnightAnimator.SetBool ("Attacking", false);
				KnightAnimator.SetBool ("CombatMode", true);
				SwordTrigger.gameObject.SetActive (false);
				heavyAnimationPlayed = false;
			}
			else{
				// Do nothing, wait for animation to play 
			}

		}
		void ComboAttack(){

			if (KnightAnimator.GetCurrentAnimatorStateInfo (0).IsName ("MKnght_1H_5X_Combo_move_forward")) {
				Debug.Log ("COMBO");
				SwordTrigger.gameObject.SetActive (true);
				comboAnimationPlayed = true;
				SwordTrigger.GetComponent <EnemySwordTriggerScript>().damage = transform.GetComponent <EnemyStats>().status.damage;

			} 
			else if (comboAnimationPlayed == true){
				ComboAttackPicked = false;
				KnightAnimator.SetBool ("ComboAttack", false);
				KnightAnimator.SetBool ("Attacking", false);
				KnightAnimator.SetBool ("CombatMode", true);
				SwordTrigger.gameObject.SetActive (false);
				comboAnimationPlayed = false;
			}
			else{
				// Do nothing, wait for animation to play 
			}
		}

		private void Block(){
			//TODO blocking
			Debug.Log ("Blocking");
			BlockingPicked = false;
		}
		private void CombatMode(){
			//TODO blocking

			Debug.Log ("CombatMode");
			attackCooldown -= Time.deltaTime;
			if(attackCooldown < 0){
				attackCooldown = 1;
				KnightAnimator.SetBool ("CombatMode",false);
				CombatModePicked = false;
			}

			if (hitDown.collider != null && hitForwards.collider == null) 								// There is ground to move on, but no wall to collide on we translate the enemy
			{
				transform.Translate(Vector3.right * Time.deltaTime * movementVelocity/2 * direction); 	//Translate our character
				//transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;
			}
			else 																						//There is no ground in this direction or we hit a wall, we turn the enemy around and walk in that direction
			{
				direction = -direction;
				transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
				transform.Translate(Vector3.right * Time.deltaTime * movementVelocity/2 * direction);  	//Translate our character after turning
				//transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;

			}


		}

		private void Chasing(){
			Debug.Log ("Chasing");
			KnightAnimator.SetBool ("Chasing", true);
			KnightAnimator.SetBool ("Attacking", false);
			KnightAnimator.SetBool ("Searching", false);


			float xDiff =  player.transform.position.x - transform.position.x; //relative X position between player and enemy

			if(Mathf.Abs(xDiff) < aggroRange +2  ){ // if we are within aggro range, keep chasing

				if((Mathf.Abs(playerPos.x - enemyPos.x)) < 1.5f){  //if the player is within 1.5 units of the enemy, the enemy  will stand still to avoid glitchy behaviour
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
					//KnightAnimator.SetFloat ("CombatMovementSpeed", 0);
				}
				else  if(playerPos.x > enemyPos.x)				//if the player is outside of 1.5 units of the enemy, the enemy will chase the player 
				{
					//KnightAnimator.SetFloat ("CombatMovementSpeed", 1);
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
			else{
				CurrentState = (int)KnightState.Searching;
				Searching ();

			}

		}

		private void Searching(){
			KnightAnimator.SetBool ("Combat", false);
			KnightAnimator.SetBool ("Chasing", false);
			KnightAnimator.SetBool ("Attacking", false);

			Debug.Log ("Searching");
			KnightAnimator.SetBool ("Searching", true);

			alertTimer -= Time.deltaTime;
			if(alertTimer < 0){
				CurrentState = (int)KnightState.Calm;
				alertTimer = 2; // CHANGE VALUE!!!
				KnightAnimator.SetBool ("Searching", false);

				Calm ();

			}
			else{
				if (hitDown.collider != null && hitForwards.collider == null) 								// There is ground to move on, but no wall to collide on we translate the enemy
				{
					transform.Translate(Vector3.right * Time.deltaTime * movementVelocity/2 * direction); 	//Translate our character
					//transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;
				}
				else 																						//There is no ground in this direction or we hit a wall, we turn the enemy around and walk in that direction
				{
					direction = -direction;
					transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
					transform.Translate(Vector3.right * Time.deltaTime * movementVelocity/2 * direction);  	//Translate our character after turning
					//transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;

				}
			} 

		}

		private void Calm(){
			//Debug.Log ("Calm" + calmTimer);
			KnightAnimator.SetBool ("Calm",true);
			calmTimer -= Time.deltaTime;


			/* Randomly select patrolling or idle as current calm state */
			if(calmTimer < 0){ 
				int calmID = randomAttackIDGenerator.Next (0, 10);
				if(calmID <= 4){
					Patrolling = true;
				}
				else{ 
					Patrolling = false; 
				}
				calmTimer = 12; //Resetting the timer in which defines how long the enemy stays within the current Calm state
			}

			/* Depending on randomly selected state, either patrol or stay idle */
			if(Patrolling){ 
				Patrol ();
			}
			else{
				Idle ();
			}
		} /* Calm() */

		/* TODO change to velocity over translate -> inerta problems */
		private void Patrol(){
			KnightAnimator.SetBool ("Idle", false);
			KnightAnimator.SetBool ("Patrolling", true);
			Debug.Log ("Patrolling");
			if (hitDown.collider != null && hitForwards.collider == null) 								// There is ground to move on, but no wall to collide on we translate the enemy
			{
				transform.Translate(Vector3.right * Time.deltaTime * movementVelocity * direction); 	//Translate our character
				//transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;
			}
			else 																						//There is no ground in this direction or we hit a wall, we turn the enemy around and walk in that direction
			{
				direction = -direction;
				transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
				transform.Translate(Vector3.right * Time.deltaTime * movementVelocity * direction);  	//Translate our character after turning
				//transform.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * movementVelocity * direction;

			}
		}

		private void Idle(){
			KnightAnimator.SetBool ("Patrolling", false);
			KnightAnimator.SetBool ("Idle", true);


			Debug.Log ("Idle");

		}

	} /* EnemyBehaviour */

	/*Vector2 playerPos;
	if (!game.gm.isPlayerDead) {
		player = GameObject.Find("Player");
		playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
	}
	else
	{
		playerPos = new Vector2(game.gm.DeadState.transform.position.x, game.gm.DeadState.transform.position.y);
	}
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
	if (!this.chase) // NOT CHASING
	{
		KnightAnimator.SetBool("Chasing", false);
		KnightAnimator.SetBool("Patrolling", true);


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
		KnightAnimator.SetBool("Chasing", true);
		KnightAnimator.SetBool("Patrolling", false);

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
*/


}
