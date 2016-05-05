using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour
{
    public bool active = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(active == true)
        {
            if (other.gameObject.tag == "Player")
            {
                transform.parent.FindChild("MovingTrap").GetComponent<MoveTrap>().isMoving = true;
                active = false;
                //Destroy(this.gameObject);
            }
        }
    }

    public void restart()
    {
        active = true;
    }
}