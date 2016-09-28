using UnityEngine;
using System.Collections;

public class PlayerShadowFollow : MonoBehaviour {

    public float fallDistance = 20f;
    public LayerMask NotHit;
    private bool ActiveShadow = true;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hitDown = Physics2D.Raycast(this.transform.parent.position, new Vector2(0, -1), fallDistance, NotHit);
        if(hitDown.collider == null)
        {
            if (ActiveShadow)
            {
                ActiveShadow = false;
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            if (!ActiveShadow)
            {
                ActiveShadow = true;
                this.GetComponent<SpriteRenderer>().enabled = true;
            }
            this.transform.position = new Vector3(transform.parent.position.x, hitDown.collider.transform.position.y, transform.parent.position.z);
        }
    }
}
