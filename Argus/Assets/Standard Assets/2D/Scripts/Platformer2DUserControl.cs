using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

		private Animator skelAnim2; 
		private Transform skeleton2;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
			skeleton2 = transform.FindChild("Skeleton_warlord");
			skelAnim2 = skeleton2.GetComponent<Animator> ();
			skelAnim2.SetLayerWeight (0,1);
			skelAnim2.SetLayerWeight (1,1);
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
			// Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;

        }
    }
}
