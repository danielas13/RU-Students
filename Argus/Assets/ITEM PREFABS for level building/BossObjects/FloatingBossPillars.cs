using UnityEngine;
using System.Collections;

public class FloatingBossPillars : MonoBehaviour {
    public GameObject Boss;
    private FloatingBossController bossStats;
    private float Delay = 0;
    private bool isActive = true;
    private PillarStatus stat;
    private GameObject beam;
    public bool alive = true;

    void Start()
    {
        beam = transform.GetChild(0).gameObject;
        stat = this.GetComponent<PillarStatus>();
        bossStats = Boss.GetComponent<FloatingBossController>();
    }
    public void DisablePillar()
    {
        if (alive)
        {
            if (stat.status.currentHealth == 1)
            {
                bossStats.PillarsActiveCount -= 1;
                isActive = false;
                Delay = 10;
                beam.SetActive(false);

            }
            if (stat.status.currentHealth > 0)
            {
                stat.damageEnemy(1);
            }
        }

    }

    void Update()
    {
        if(!Boss.activeSelf)
        {
            alive = false;
            beam.SetActive(false);
        }
        if (alive)
        {
            if (!isActive)
            {
                if (Delay >= 0)
                {
                    Delay -= Time.deltaTime;
                    if (Delay <= 0)
                    {
                        isActive = true;
                        beam.SetActive(true);
                        bossStats.PillarsActiveCount += 1;
                        stat.healEnemy(2);
                        stat.status.currentHealth = 2;
                    }
                }
            }
        }
    }

    public void restart()
    {
        beam.SetActive(true);
        isActive = true;
        alive = true;
    }
}
