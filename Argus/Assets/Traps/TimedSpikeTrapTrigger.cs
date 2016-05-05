using UnityEngine;

public class TimedSpikeTrapTrigger : MonoBehaviour {
           
    public int TimeAlive = 1;           //How many times will the trap trigger.
    public int triggers = 0;
    public bool Endless = false;        //Will this trap live on forever.
        
    void OnTriggerEnter2D(Collider2D other)
    {
        if(TimeAlive > triggers || Endless)      //Can this trap trigger again?
            if(transform.parent.GetComponent<TimedSpikeTrapScript>().isMoving == false)             //is the parent already active?
            {
                if (other.gameObject.tag == "Player")                                               //Collition with the player.
                {   
                    transform.parent.GetComponent<TimedSpikeTrapScript>().isMoving = true;          //Activate parent.
                    triggers++;                                                                     //increment trigger counter.
                    //Destroy(this.gameObject);
                }
            }
    }
}
