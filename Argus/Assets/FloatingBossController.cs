using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloatingBossController : MonoBehaviour {

    public bool movementDirection = true;
    public float movementVelocity = 1f;
    //enemy patrol/pathfinding variables.
    public LayerMask NotHit;
    public float fallDistance = 1f;
    public float collideDistance = 0.5f;
    public float direction = -1f;
    public float vertDirection = -1f;

    //Enemy chase variables
    public float aggroRange = 5f;
    public LayerMask aggroLayers;
    private GameObject player;
    private bool chase = false;
    public float chasingVelocity = 6f;

    private List<GameObject> MinionList = new List<GameObject>();


    public int PillarsActiveCount = 4;      //how many pillars are activated.
    private float damageCounter = 0;

    public GameObject FireEffect;

    public GameObject EnemyPrefab;
    private float Cooldown = 8;
    public bool CombatStarted = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        FireEffect.SetActive(true);
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(PillarsActiveCount == 0)
        {
            CombatStarted = true;
            if (damageCounter <= 0)
            {
                if (damageCounter <= 0)
                {
                    this.GetComponent<FloatingBossStats>().damageEnemy(10);
                    FireEffect.SetActive(false);
                    damageCounter = 8;
                }
            }
        }

        if(PillarsActiveCount == 4)
        {
            CombatStarted = false;
        }

        if(PillarsActiveCount > 0 && !FireEffect.activeSelf)
        {
            FireEffect.SetActive(true);
        }

        if(damageCounter >= 0) {
            damageCounter -= Time.deltaTime;
        }

        if (Cooldown >= 0)
        {
            Cooldown -= Time.deltaTime;
        }
        if (Cooldown <= 0 && CombatStarted)
        {
            MinionList.Add((GameObject)Instantiate(EnemyPrefab, new Vector3(transform.position.x, transform.position.y-1, transform.position.z), transform.rotation));
            Cooldown = 8;
        }
    }
    public void ResetMinions()
    {
        foreach (GameObject minion in MinionList)
        {
            Destroy(minion);

        }
        MinionList = new List<GameObject>();
        Cooldown = 8;
    }
}
