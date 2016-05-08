using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
		private Animator skelAnim; 
		private Animator skelAnim2; 
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
		private Transform skeleton;
		private Transform skeleton2;
        private bool knockback = false;
        private float xDiff;
        private float timer = 1;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
			skeleton = transform.FindChild("Skeleton");
			skeleton2 = transform.FindChild("Skeleton_warlord");

			skelAnim = skeleton.GetComponent<Animator> ();
			skelAnim2 = skeleton2.GetComponent<Animator> ();

        }

        void knockBackPlayer(Vector3 pos)
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
                else
                {
                    // transform.GetComponent<Rigidbody2D>().AddForce(, ForceMode2D.Impulse);
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
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

		void Update(){ // per frame updates
			if (m_FacingRight){
				skeleton2.transform.rotation = Quaternion.Euler(0,90,0);

			}
			else{
				skeleton2.transform.rotation = Quaternion.Euler(0,-90,0);

			}
		}

        public void Move(float move, bool crouch, bool jump)
        {



            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

				if(m_Grounded == true)
				{
					skelAnim2.SetFloat ("Movementspeed", Mathf.Abs(move));
				}

				/*
				if(Math.Abs(move)  > 0.1f && m_Grounded == true) {
					//skelAnim.SetFloat("Speed", Mathf.Abs(move));
					skelAnim.SetBool ("walk", true);
					skelAnim2.SetBool ("Running", true);
					//skelAnim2.SetFloat ("XMovement", move);
					//skelAnim2.Play("Skeleton_Run", -1);
					//skelAnim2.SetBool ("Skeleton_Run", true);

				}
				else{
					//skelAnim2.SetFloat ("XMovement", move);
					skelAnim2.SetBool ("Running", false);

				}
			*/

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

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
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {

//			else
//				transform.rotation = Vector3.left;
            // Switch the way the player is labelled as facing.




            m_FacingRight = !m_FacingRight;

			if (m_FacingRight){
				skeleton2.transform.rotation = Quaternion.Euler(0,90,0);

			}
			else{
				skeleton2.transform.rotation = Quaternion.Euler(0,-90,0);

			}

            // Multiply the player's x local scale by -1.
            //Vector3 theScale = transform.localScale;
            //theScale.x *= -1;
            //transform.localScale = theScale;
        }
    }
}
