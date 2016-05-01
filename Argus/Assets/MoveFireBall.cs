using UnityEngine;
using System.Collections;

public class MoveFireBall : MonoBehaviour {

    public int speed = 20;
    public int duration = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        Destroy(this.gameObject,duration);
    }
}
