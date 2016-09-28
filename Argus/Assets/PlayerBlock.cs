using UnityEngine;
using System.Collections;

/// <summary>
/// This class takes care of the blocking funcitonality
/// It actiaves a collider on the player's indicator that acts as the player himself
/// </summary>
public class PlayerBlock : MonoBehaviour {

    [SerializeField]
    private GameObject shield;
    private PlatformerCharacter2D playerControlls;
    private PlayerMeleeAttack playerAttack;
    public bool BlockActive = false;

    private Animator skeletonAnimator;
    private Transform skeletonFootman;

    public float StartCooldown = 0.5f;
    public float StopCooldown = 0.5f;
    private float startCounter = 0f, stopCounter = 0f;

    // Use this for initialization
    void Start () {
        playerControlls = GetComponent<PlatformerCharacter2D>();
        playerAttack = GetComponent<PlayerMeleeAttack>();
        skeletonFootman = transform.FindChild("ToFlip").FindChild("Skeleton_footman");
        skeletonAnimator = skeletonFootman.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetAxis("Block") > 0f)
        {
            if (!BlockActive && !playerControlls.isChanneling && !playerAttack.attacking && playerControlls.m_Grounded)
            {
                if(stopCounter > 0)
                {
                    stopCounter = 0;
                }
                skeletonAnimator.SetBool("blocking", true);
                startCounter = StartCooldown;
                BlockActive = true;
                game.gm.ButtonIndicatorController.Blocking(true);

             //   player.isChanneling = true;
            }
            else if (startCounter <= 0 && !shield.activeSelf && BlockActive == true)
            {
                shield.SetActive(true);
                playerControlls.blockingActive = true;
            }
        }
        else
        {
            if (BlockActive)         //Let go of button
            {
                skeletonAnimator.SetBool("blocking", false);
                shield.SetActive(false);
                game.gm.ButtonIndicatorController.Blocking(false);
                if (startCounter > 0)
                {
                    startCounter = 0;
                }
                stopCounter = StopCooldown;
                BlockActive = false;
                playerControlls.blockingActive = false;
            }
      /*      if (stopCounter <= 0 && stopblocking)   //StopCooldown time after leting go of button
            {
                skeletonAnimator.SetBool("blocking", false);
                shield.SetActive(false);
                //    player.isChanneling = false;
                isBlocking = false;
                stopblocking = false;
            }*/
        }


        if(startCounter > 0)
        {
            startCounter -= Time.deltaTime;
        }
        if (stopCounter > 0)
        {
            stopCounter -= Time.deltaTime;
        }
    }
}
