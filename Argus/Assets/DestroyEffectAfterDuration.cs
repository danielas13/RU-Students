using UnityEngine;
using System.Collections;

public class DestroyEffectAfterDuration : MonoBehaviour {
    [SerializeField]
    private float cooldown = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
