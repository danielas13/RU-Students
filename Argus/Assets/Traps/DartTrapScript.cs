using UnityEngine;
using System.Collections;

public class DartTrapScript : MonoBehaviour {
    public float TrapRange = 5f;                //The range that the trap will activate.
    public LayerMask TargetMask;                //The mask for the player. (Player collision detection)                
    private RaycastHit2D RayForwards;                   //The ray pointing forward in local coords.
    public Transform spikePrefab;                      //Prefab of the spike object.
    public float cooldown = 2;

    private float cooldownCounter = 0;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        RayForwards = Physics2D.Raycast(transform.position, transform.right, TrapRange, TargetMask);
        if(cooldownCounter <= 0)
        {
            if (RayForwards.collider != null)
            {
                if (RayForwards.collider.gameObject.layer == 8)
                {
                    Instantiate(spikePrefab, transform.position, transform.rotation);
                    cooldownCounter = cooldown;

                    //tempObj.GetComponent<MoveFireBall>().duration = 1;
                    //Debug.DrawLine(transform.position, transform.right, Color.blue);
                }
                else
                {
                    //Debug.DrawLine(transform.position, transform.right, Color.red);
                }
            }
        }
        else
        {
            cooldownCounter -= Time.deltaTime; ;
        }
    }
}
