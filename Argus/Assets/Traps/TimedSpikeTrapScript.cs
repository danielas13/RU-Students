using UnityEngine;
using System.Collections;

public class TimedSpikeTrapScript : MonoBehaviour {
    public bool isMoving = false;           //Is the trap triggered.
    public float distance = 1.5f;           //The total distance that the spikes will go.
    public float speed = 2f;                //Speed of the trap.
    public bool endlessTrap = false;        //True if the trap can always be triggered again.
    public int trapHealth = 1;              //How many times the trap can be triggered.
    public float delay = 2;                 //The delay of the trap after trigger.
    public float globalDelay = 1;            //The global delay of the trap. Only done once at the start of the spike.

    public bool AutomaticTrap = false;     //True if the trap should be permanently triggering.

    private float moved = 0f;               //The distance that the trap has moved.
    private bool reachedTop = false;        //indicates if a trap is moving down.
    private float DelayVar = 0;             //The Delay counter.  
    private float CooldownVar = 0;          //The global cooldown timer.

    void Start()
    {
        if (endlessTrap || AutomaticTrap)                    //If the trapis endless. Make the spike child move endlessly.
        {
            transform.FindChild("TimedSpikeTrapTrigger").GetComponent<TimedSpikeTrapTrigger>().Endless = true;

        }
        //set the spike's health to´the trap's health.
        transform.FindChild("TimedSpikeTrapTrigger").GetComponent<TimedSpikeTrapTrigger>().TimeAlive = trapHealth;
    }

    void Update()
    {
        if(CooldownVar >= globalDelay)
        {
            if (AutomaticTrap)
            {
                if (DelayVar >= delay)          //Delay of the spikes.
                {
                    if (reachedTop == false)    //The spikes are moving upwards.
                    {
                        if (moved < distance)    //The spikes have not reached their max distance.
                        {
                            transform.FindChild("Spikes").transform.Translate(Vector2.up * Time.deltaTime * speed);
                            moved += Time.deltaTime * speed;
                        }
                        if (moved >= distance)   //The spikes have reached theyr max distance.
                        {
                            reachedTop = true;
                        }
                    }
                    if (reachedTop == true)      //The spikes are moving downwards.
                    {
                        transform.FindChild("Spikes").transform.Translate(Vector2.down * Time.deltaTime * speed);
                        moved -= Time.deltaTime * speed;

                        if (moved <= 0)          //The spikes have reset.
                        {
                            moved = 0;
                            reachedTop = false;
                            isMoving = false;
                            DelayVar = 0;
                        }
                    }
                }
                else                            //There is still delay on the spikes. increment it's delay counter.
                {
                    DelayVar += Time.deltaTime;
                }
            }
            else
            {
                if (isMoving == true)                //The trap is triggered.
                {
                    if (DelayVar >= delay)          //Delay of the spikes.
                    {
                        if (reachedTop == false)    //The spikes are moving upwards.
                        {
                            if (moved < distance)    //The spikes have not reached their max distance.
                            {
                                transform.FindChild("Spikes").transform.Translate(Vector2.up * Time.deltaTime * speed);
                                moved += Time.deltaTime * speed;
                            }
                            if (moved >= distance)   //The spikes have reached theyr max distance.
                            {
                                reachedTop = true;
                            }
                        }
                        if (reachedTop == true)      //The spikes are moving downwards.
                        {
                            transform.FindChild("Spikes").transform.Translate(Vector2.down * Time.deltaTime * speed);
                            moved -= Time.deltaTime * speed;

                            if (moved <= 0)          //The spikes have reset.
                            {
                                moved = 0;
                                reachedTop = false;
                                isMoving = false;
                            }
                        }
                    }
                    else                            //There is still delay on the spikes. increment it's delay counter.
                    {
                        DelayVar += Time.deltaTime;
                    }
                }
                else                               //The trap has finished it's iteration and the delay is reset.                             
                {
                    DelayVar = 0;
                }
            }
        }
        else
        {
            CooldownVar += Time.deltaTime;
        }
        
    }

    public void restart()
    {
        transform.FindChild("TimedSpikeTrapTrigger").GetComponent<TimedSpikeTrapTrigger>().triggers = 0;
        if(!AutomaticTrap && isMoving == true)
        {
            reachedTop = true;
        }
    }
}
