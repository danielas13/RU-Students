using UnityEngine;
using System.Collections;

public class TimedSpikeTrapScript : MonoBehaviour {
    public bool isMoving = false;
    public float distance = 1.5f;
    public float speed = 2f;
    private float moved = 0f;
    private bool reachedTop = false;            //The spikes will start flowing down.
    public bool endlessTrap = false;            //The trap has endless iterations. Will NOT work by itself.
    public bool SelfRepeat = false;             //The Trap will start as soon as it finished by itself.
    public float StartingDelay = 0;             //The delay at the begining of the trap.
    public int trapHealth = 1;                  //Iterations for the trap if endless is false.
    public float delay = 2;                     //Delay between each iteration.
    private float DelayVar = 0;

    void Start()
    {
        if (endlessTrap || SelfRepeat)      //Make the spikes endless if that is the case. plus set their health to the trap's
        {
            transform.FindChild("TimedSpikeTrapTrigger").GetComponent<TimedSpikeTrapTrigger>().Endless = true;
        }
        if (SelfRepeat)
        {
            isMoving = true;
        }
        transform.FindChild("TimedSpikeTrapTrigger").GetComponent<TimedSpikeTrapTrigger>().TimeAlive = trapHealth;
    }

    void Update()
    {
        if (StartingDelay <= 0)
        {
            if (isMoving == true)            //The trap is active
            {
                if (DelayVar >= delay)      //Delay counter
                {
                    if (reachedTop == false)    //Direction False = going up.
                    {
                        if (moved < distance)        //checking for the distance mvoed.
                        {
                            transform.FindChild("Spikes").transform.Translate(Vector2.up * Time.deltaTime * speed);
                            moved += Time.deltaTime * speed;
                        }
                        if (moved >= distance)
                        {
                            reachedTop = true;
                        }
                    }
                    if (reachedTop == true)          //The trap is going down.
                    {
                        transform.FindChild("Spikes").transform.Translate(Vector2.down * Time.deltaTime * speed);
                        moved -= Time.deltaTime * speed;

                        if (moved <= 0)
                        {
                            moved = 0;
                            reachedTop = false;
                            if (!SelfRepeat)
                            {
                                isMoving = false;
                            }
                            else
                            {
                                DelayVar = 0;
                            }
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
        else
        {
            StartingDelay -= Time.deltaTime;
        }
       
    }

    public void restart()
    {
        transform.FindChild("TimedSpikeTrapTrigger").GetComponent<TimedSpikeTrapTrigger>().triggers = 0;
        if (!SelfRepeat)
        {
            if (isMoving == true)
            {
                reachedTop = true;
            }
        }
    }
}
