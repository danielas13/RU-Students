using UnityEngine;
using System.Collections;

public class OneToStoreDoor : MonoBehaviour {

    public Transform TargetObject;

    private static readonly System.Random random = new System.Random();     //Create a read only random variable.
    public int chance = 1;
    private int TotalChance = 0;                    //decreaeses the chances of geting to the store.
    public bool Guaranteed = false;                 //True if the door will lead to the shop every time.
    public float cooldown = 1;                      //Cooldown of wich the door can activate.(reset by other doors)
    GameObject player, camera;
    private float MessageCounter = 0;               //Counter for the "Locked" text lifetime.
    public GameObject Text;
    void Start()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("MainCamera");
        TotalChance = random.Next(1, chance + 1);
        Text.SetActive(false);
    }
    //Will trigger if the player is withing the door boundaries and pressesthe up key.
    void OnTriggerStay2D(Collider2D other)
    {
        if (TargetObject != null)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) && other.gameObject.tag == "Player")
            {
                //Debug.Log(TotalChance);
                if(TotalChance == 2 || Guaranteed)  
                {
                    if (cooldown <= 0)                      //No cooldown on the door activation.
                    {
                        player.transform.position = new Vector3(TargetObject.position.x, TargetObject.position.y, TargetObject.position.z - 6f);
                        camera.transform.position = player.transform.position;              //translate the camera.
                        TargetObject.GetComponent<OneToOneDoor>().cooldown = 1;             //Sending cooldown to target door.
                        TargetObject.GetComponent<OneToOneDoor>().TargetObject = transform;
                    }
                }
                else    //The door is locked. Activating text message.
                {
                    MessageCounter = 3;
                    Text.SetActive(true);
                }
            }
        }
    }
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        if(MessageCounter > 0)
        {
            MessageCounter -= Time.deltaTime;
            if(MessageCounter <= 0)
            {
                Text.SetActive(false);
            }
        }
    }

    public void restart()
    {
        TotalChance = random.Next(1, chance + 1);
    }
}
