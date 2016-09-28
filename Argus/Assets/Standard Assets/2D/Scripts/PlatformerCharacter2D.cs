using System;
using UnityEngine;


    public class PlatformerCharacter2D : MonoBehaviour
    {
        [HideInInspector]
        public bool blockingActive = false;
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        [NonSerialized]
        public bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
		private Animator skelAnim2; 
		private Animator skeletonAnimator; 

        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
		private Transform skeleton2;
		private Transform skeletonFootman;
		private Transform lights;
        private bool knockback = false;
        private float xDiff;
        private float timer = 1;
        [NonSerialized]
        public bool isChanneling = false;

		Transform toFlip;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

			//new skelebro
			skeleton2 = transform.FindChild("Skeleton_warlord");
			skelAnim2 = skeleton2.GetComponent<Animator> ();

			lights = transform.FindChild ("FrontLight");
			toFlip = transform.FindChild ("ToFlip");

			//Skeleton Footman
			skeletonFootman = transform.FindChild ("ToFlip").FindChild ("Skeleton_footman");
			skeletonAnimator = skeletonFootman.GetComponent <Animator> ();

        }

        public void knockBackPlayer(Vector3 pos)//We need to change this....it is fucked up
        {
            knockback = true;
            xDiff = transform.position.x - pos.x;
            if (xDiff < 0)
            {
               	xDiff = -10;
            }
            else
            {
                xDiff = 10;
            }

           transform.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.GetComponent<Rigidbody2D>().velocity.x, 10);
        }
        private void FixedUpdate()
        {
            if (knockback == true && timer > 0)
            {
                Vector2 force = new Vector2(xDiff, transform.GetComponent<Rigidbody2D>().velocity.y);
                if (timer > 0.5f)
                {
                    transform.GetComponent<Rigidbody2D>().velocity = force;
                }

                timer -= Time.deltaTime;
                m_AirControl = false;

            }
            if (timer < 0.1f)
            {
                knockback = false;
                timer = 1;
                m_AirControl = true;
            }
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            //m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

		void Update(){ 
			if (!m_FacingRight)
			{
				toFlip.rotation = Quaternion.Euler(Vector3.up * 180);
			}
			else
			{
				toFlip.rotation = Quaternion.Euler(Vector3.up * 0);
			}
		}

        public void Move(float move, bool crouch, bool jump)
        {
            if (!isChanneling) // if the player is channeling then he can not move
            {
                if (blockingActive)
                {
                    move = move / 3.5f;
                }
                // If crouching, check to see if the character can stand up
				if (!crouch) // if (!crouch && m_Anim.GetBool("Crouch"))
                {
                    // If the character has a ceiling preventing them from standing up, keep them crouching
                    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                    {
                        crouch = true;
                    }
                }

                // Set whether or not the character is crouching in the animator
                //m_Anim.SetBool("Crouch", crouch);

                //only control the player if grounded or airControl is turned on
                if (m_Grounded || m_AirControl)
                {

                    // Reduce the speed if crouching by the crouchSpeed multiplier
                    move = (crouch ? move * m_CrouchSpeed : move);

                    // The Speed animator parameter is set to the absolute value of the horizontal input.
                   // m_Anim.SetFloat("Speed", Mathf.Abs(move));

                    if (m_Grounded == true)
                    {
                       // Debug.Log("Grounded");
                       // skelAnim2.SetFloat("Movementspeed", Mathf.Abs(move));
						skeletonAnimator.SetFloat ("MovementSpeed", Math.Abs (move));
                    }
					skeletonAnimator.SetFloat ("MovementSpeed", Math.Abs (move));

                    //skelAnim2.SetFloat("Movementspeed", Mathf.Abs(move));
                    // Move the character
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                    // If the input is moving the player right and the player is facing left...
                    if (move > 0 && !m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                    // Otherwise if the input is moving the player left and the player is facing right...
                    else if (move < 0 && m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }


                }
			//	Debug.Log ("Grounded" + m_Grounded + " , jump " + jump);
                // If the player should jump...
                if (m_Grounded && jump)
                {

                    // Add a vertical force to the player.
                    m_Grounded = false;
                   // m_Anim.SetBool("Ground", false);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                    //skelAnim2.SetTrigger("Jump");
					skeletonAnimator.SetTrigger ("Jump");
                }

                if (m_Grounded)
                {
                    //skelAnim2.SetBool("IsJumping", false);
					skeletonAnimator.SetBool ("IsJumping", false);
                }
                else
                {
                    //skelAnim2.SetBool("IsJumping", true);
					skeletonAnimator.SetBool ("IsJumping", true);

                }
            }
            else
            {
                m_Rigidbody2D.velocity = new Vector2(0, 0);
             //   Debug.Log("PLAYER IS CHANNELING ADD A ANIMATION HERE!!!!!!!!!!!");
            }
        }

        private void Flip()
        {
			if(!skeletonAnimator.GetBool ("MidSwing")){
				m_FacingRight = !m_FacingRight;
			}

		
			lights.transform.rotation = Quaternion.Euler(0,180,0);
			if (m_FacingRight)	lights.transform.localEulerAngles = new Vector3(15,270,0);
			else lights.transform.localEulerAngles = new Vector3(15,-270,0);
        }
    }

