using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public bool movementDirection = true;
    public float movementVelocity = 2.5f;
    Transform trackPoint;
    public LayerMask NotHit;
    public float fallDistance = 1f;
    public float collideDistance = 0.5f;
    public float direction = -1f;

    // Use this for initialization
    void Start () {
        trackPoint = transform.FindChild("trackPoint");

    }
	
	// Update is called once per frame
	void Update() {
        
        Vector2 trackPosition = new Vector2(trackPoint.position.x, trackPoint.position.y);
        RaycastHit2D hitDown = Physics2D.Raycast(trackPosition, new Vector2(0,-1), fallDistance, NotHit);
        RaycastHit2D hitForwards = Physics2D.Raycast(trackPosition, new Vector2(1, 0), collideDistance, NotHit);
        //Debug.Log(hit.distance);
        if (hitDown.collider != null && hitForwards.collider == null)
        {
            //The ai has hit a wall/floor
            //Debug.Log(hitDown.distance);
        }
        else
        {
            direction = -direction;
            //trackPoint.transform.position = new Vector3(-trackPoint.position.x, trackPoint.position.y, trackPoint.position.z);
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
            Debug.Log("posX : " + trackPoint.position.x + "Pos y : " + trackPoint.position.y + "Pos z  : " + trackPoint.position.z);
        }
        transform.Translate(Vector3.right * Time.deltaTime * 1f * direction);

    }
}
