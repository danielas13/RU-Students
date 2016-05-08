using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FloatText : MonoBehaviour {

    public int speed = 20;
    public int duration = 2;
    public int range = 2;
    //private int damage = 2;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * 0.1f * speed);
        //transform.GetComponent<Text>().a -= (duration * Time.deltaTime * 0.5f);
        Destroy(this.gameObject, duration);
    }
}
