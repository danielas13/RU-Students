using UnityEngine;
using System.Collections;

public class DartSpikeScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	
	}
    /*
    void OnTriggerEnter2D(Collider2D col)
    {

        Debug.Log("Collison " + col.name);
        if (col.isTrigger != true && col.gameObject.CompareTag("enemy"))
        {
            col.gameObject.SendMessageUpwards("damageEnemy", damage);
            Destroy(this.gameObject);
        }
        if (col.isTrigger != true && col.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Wall Collision");
            Destroy(this.gameObject);
        }

    }
    */
}
