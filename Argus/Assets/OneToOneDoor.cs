using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OneToOneDoor : MonoBehaviour {

    public Transform TargetObject;
    public bool reciever = false;
    public float cooldown = 1;
    public bool IsLocked = false;
    public bool SecondLock = false;
    public Canvas lockedBoard;
    private GameObject player;
    private GameObject camera;

    private float islockedTextCounter = 0;

    void Start()
    {
        player = player = GameObject.Find("Player");
        camera = GameObject.Find("MainCamera");
        if (SecondLock)
        {
            IsLocked = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (!game.gm.ButtonIndicatorController.DoorDefault)
        {
            game.gm.ButtonIndicatorController.ResetInterract();
        }
    }

    //Will trigger if the player is withing the door boundaries and pressesthe up key.
    void OnTriggerStay2D(Collider2D other)
    {
        if(TargetObject != null)
        {
            if(other.gameObject.tag == "Player")
            {
                if(game.gm.ButtonIndicatorController.DoorDefault)
                {
                    game.gm.ButtonIndicatorController.ChangeInterract("Enter [Press]");
                }

                if (Input.GetButtonDown("Interact"))
                {
                    if (!IsLocked)
                    {
                        if (cooldown <= 0)
                        {
                            player.transform.position = new Vector3(TargetObject.position.x, TargetObject.position.y, TargetObject.position.z - 6f);

                            if (TargetObject.CompareTag("StoreDoor"))
                            {
                                TargetObject.GetComponent<OneToStoreDoor>().cooldown = 1;
                            }
                            else
                            {
                                OneToOneDoor Door = TargetObject.GetComponent<OneToOneDoor>();
                                if (Door.reciever)
                                {
                                    Door.cooldown = 1;
                                    Door.TargetObject = this.transform;

                                }
                                else
                                {
                                    Door.cooldown = 1;
                                }
                            }
                            camera.transform.position = player.transform.position;
                        }
                    }
                    else
                    {
                        if (islockedTextCounter <= 0)
                        {
                            lockedBoard.transform.Translate(new Vector3(0, 0, -5));
                            islockedTextCounter = 5;
                        }
                    }
                }
            }

        }
    }
    void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        if (IsLocked)
        {
            if (islockedTextCounter > 0)
            {
                islockedTextCounter -= Time.deltaTime;
                if (islockedTextCounter <= 0)
                {
                    lockedBoard.transform.Translate(new Vector3(0, 0, 5));
                }
            }
        }
    }

    public void unlock()
    {
        if (SecondLock)
        {
            SecondLock = false;
        }
        else
        {
            islockedTextCounter = 0;
            IsLocked = false;
            lockedBoard.transform.Translate(new Vector3(0, 0, 5));
        }
    }
}


