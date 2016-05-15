using UnityEngine;
using System.Collections;

public class disableLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!Input.GetKey(KeyCode.T))
        {
            transform.GetComponent<Light>().range = 0;
        }
        else
        {
            transform.GetComponent<Light>().range = 30;
        }
    }
}

