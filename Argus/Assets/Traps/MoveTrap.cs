using UnityEngine;
using System.Collections;

public class MoveTrap : MonoBehaviour {

    public bool isMoving = false;       //Is the spike trap activated?
    public int distance = 10;           //The total distance the trap will cover.

    public int direction = 0;
    public int speed = 5;               //Speed of the trap once activated.
    private float moved = 0;              //How much has the trap moved.
	// Update is called once per frame
    private Vector2 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }
	void Update () {
        if (isMoving)                   //is the trap activated?
        {
            if(moved < distance)        //Has the trap reached it's destination
            {
                if (direction == 0)           //The trap is suposed to go upwards.
                {
                    transform.Translate(Vector2.up * Time.deltaTime * speed);
                    moved += Time.deltaTime * speed;    //Update the moved counter by the traveling distance.
                }
                else if (direction == 1)     //The trap is suposed to go upwards.
                {
                    transform.Translate(Vector2.down * Time.deltaTime * speed);
                    moved += Time.deltaTime * speed;    //Update the moved counter by the traveling distance.
                }
                else if (direction == 2)   //The trap is suposed to go to the left.
                {
                    transform.Translate(Vector2.left * Time.deltaTime * speed);
                    moved += Time.deltaTime * speed;    //Update the moved counter by the traveling distance.
                }
                else if (direction == 3)  //The trap is suposed to go to the Right.
                {
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                    moved += Time.deltaTime * speed;    //Update the moved counter by the traveling distance.
                }
            }
            //Else the trap has reached it's destination and does nothing. 
        }
	}

    public void restart()
    {
        isMoving = false;
        moved = 0;
        transform.position = originalPos;
    }
}
