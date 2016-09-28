using UnityEngine;
using System.Collections;

public class FollowCasterCamera : MonoBehaviour {

    public Transform lookat;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    this.transform.position = new Vector3(lookat.position.x, lookat.position.y + 1.795f, lookat.position.z - 2f);
        this.transform.LookAt(lookat);
    }
}
