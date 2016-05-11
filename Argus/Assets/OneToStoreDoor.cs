using UnityEngine;
using System.Collections;

public class OneToStoreDoor : MonoBehaviour {

    public Transform TargetObject;

    private static readonly System.Random random = new System.Random();     //Create a read only random variable.
    public int chance = 1;
    private int TotalChance = 0;
    GameObject player, camera;
    void Start()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("MainCamera");
        TotalChance = random.Next(1, chance + 1);
    }
    //Will trigger if the player is withing the door boundaries and pressesthe up key.
    void OnTriggerStay2D(Collider2D other)
    {
        if (TargetObject != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && other.gameObject.tag == "Player")
            {
                Debug.Log(TotalChance);
                if(TotalChance == 2)
                {    
                    player.transform.position = new Vector3(TargetObject.position.x, TargetObject.position.y, TargetObject.position.z - 6f);
                    camera.transform.position = player.transform.position;
                    TargetObject.GetComponent<OneToOneDoor>().TargetObject = transform;
                }
            }
        }
    }

    public void restart()
    {
        TotalChance = random.Next(1, chance + 1);
    }
}
