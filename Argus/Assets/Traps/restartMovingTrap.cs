using UnityEngine;
using System.Collections;

public class restartMovingTrap : MonoBehaviour {

    //private Vector2 originalPos;
    // Use this for initialization
    void Start()
    {
        //originalPos = transform.position;
    }

    public void restart()
    {
        transform.FindChild("TrapTrigger").GetComponent<TrapTrigger>().restart();
        transform.FindChild("MovingTrap").GetComponent<MoveTrap>().restart();
        //transform.position = originalPos;
    }
}
