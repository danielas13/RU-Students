using UnityEngine;
using System.Collections;

public class TimedSpikeTrapScript : MonoBehaviour {
    public bool isMoving = false;
    public float distance = 1.5f;
    public float speed = 2f;
    private float moved = 0f;
    private bool reachedTop = false;
    public bool endlessTrap = false;
    public int trapHealth = 1;
    public float delay = 2;
    private float DelayVar = 0;

    void Start()
    {
        if (endlessTrap)
        {
            transform.FindChild("TimedSpikeTrapTrigger").GetComponent<TimedSpikeTrapTrigger>().Endless = true;
        }
        transform.FindChild("TimedSpikeTrapTrigger").GetComponent<TimedSpikeTrapTrigger>().TimeAlive = trapHealth;
    }

    void Update()
    {

        if(isMoving == true)
        {
            if (DelayVar >= delay)
            {
                if (reachedTop == false)
                {
                    if(moved < distance)
                    {
                        transform.FindChild("Spikes").transform.Translate(Vector2.up * Time.deltaTime * speed);
                        moved += Time.deltaTime * speed;
                    }
                    if(moved >= distance)
                    {
                        reachedTop = true;
                    }
                }
                if(reachedTop == true)
                {
                    transform.FindChild("Spikes").transform.Translate(Vector2.down * Time.deltaTime * speed);
                    moved -= Time.deltaTime * speed;

                    if(moved <= 0)
                    {
                        moved = 0;
                        reachedTop = false;
                        isMoving = false;
                    }
                }
            }
            else
            {
                DelayVar += Time.deltaTime;
            }  
        }
        else
        {
            DelayVar = 0;
        }
    }

    public void restart()
    {
        transform.FindChild("TimedSpikeTrapTrigger").GetComponent<TimedSpikeTrapTrigger>().triggers = 0;
        if(isMoving == true)
        {
            reachedTop = true;
        }
    }
}
