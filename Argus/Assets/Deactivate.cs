using UnityEngine;
using System.Collections;

public class Deactivate : MonoBehaviour {
    public float deactivateTime = 5;
	// Use this for initialization
	void Start () {
	
	}
	void OnEnable()
    {
        deactivateTime = 3;
    }
	// Update is called once per frame
	void Update () {
        deactivateTime -= Time.deltaTime;
        if(deactivateTime < 0)
        {
            this.gameObject.SetActive(false);
        }
	}
}
