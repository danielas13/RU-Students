using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OneToOneDoor : MonoBehaviour {

    public Transform TargetObject;
    public bool reciever = false;
    public float cooldown = 1;
    public bool IsLocked = false;
    public Canvas lockedBoard;

    private float islockedTextCounter = 0;

    //Will trigger if the player is withing the door boundaries and pressesthe up key.
    void OnTriggerStay2D(Collider2D other)
    {
        if(TargetObject != null)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) && other.gameObject.tag == "Player")
            {
                if (!IsLocked)
                {
                    if (cooldown <= 0)
                    {
                        GameObject player = GameObject.Find("Player");
                        player.transform.position = new Vector3(TargetObject.position.x, TargetObject.position.y, TargetObject.position.z - 6f);
                        GameObject camera = GameObject.Find("MainCamera");
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
                    if(islockedTextCounter <= 0)
                    {
                        lockedBoard.transform.Translate(new Vector3(0, 0, -5));
                        islockedTextCounter = 5;
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
        islockedTextCounter = 0;
        IsLocked = false;
        lockedBoard.transform.Translate(new Vector3(0, 0, 5));
    }
}


