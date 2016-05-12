using UnityEngine;
using System.Collections;

public class MovePlatFormScript : MonoBehaviour {


    public float distance = 2;
    private float moved = 0;
    public float speed = 5;
    public int dir = 0;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
        //Horizontal
        if (dir==0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            moved += Time.deltaTime * speed;
            if(moved > distance)
            {
                moved = 0;
                dir = 1;
            }
        }
        else if(dir == 1)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            moved += Time.deltaTime * speed;
            if (moved > distance)
            {
                moved = 0;
                dir = 0;
            }
        } //vertical 
        else if (dir == 2)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
            moved += Time.deltaTime * speed;
            if (moved > distance)
            {
                moved = 0;
                dir = 3;
            }
        }
        else if (dir == 3)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
            moved += Time.deltaTime * speed;
            if (moved > distance)
            {
                moved = 0;
                dir = 2;
            }
        }

    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (dir==0)
            {
                player.transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else if(dir==1)
            {
                player.transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
        }
    }
}
