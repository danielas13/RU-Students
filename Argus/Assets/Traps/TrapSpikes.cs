using UnityEngine;
using System.Collections;

public class TrapSpikes : MonoBehaviour {

    //Will trigger if the player is withing the door boundaries and pressesthe up key.
    void OnCollisionEnter2D(Collision2D Victim)
    {
        if (Victim.gameObject.tag == "Player")
        {
            game.KillPlayer();
        }
    }
}