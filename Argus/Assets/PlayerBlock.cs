using UnityEngine;
using System.Collections;

/// <summary>
/// This class takes care of the blocking funcitonality
/// It actiaves a collider on the player's indicator that acts as the player himself
/// </summary>
public class PlayerBlock : MonoBehaviour {

    [SerializeField]
    private GameObject shield;
    private PlatformerCharacter2D player;
    private PlayerMeleeAttack playerAttack;
    private bool isBlocking = false;


    public float StartCooldown = 0.5f;
    public float StopCooldown = 0.5f;
    private float startCounter = 0f, stopCounter = 0f;

    // Use this for initialization
    void Start () {
        player = GetComponent<PlatformerCharacter2D>();
        playerAttack = GetComponent<PlayerMeleeAttack>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetAxis("Block") > 0f)
        {
            if (!isBlocking && !player.isChanneling && !playerAttack.attacking)
            {
                startCounter = StartCooldown;
                isBlocking = true;
                player.isChanneling = true;
            }
            else if (startCounter <= 0 && !shield.activeSelf && isBlocking == true)
            {
                shield.SetActive(true);
            }
        }
        else
        {
            if (isBlocking)
            {
                stopCounter = StopCooldown;
                isBlocking = false;
            }
            if (stopCounter <= 0 && shield.activeSelf)
            {
                shield.SetActive(false);
                player.isChanneling = false;
            }
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
