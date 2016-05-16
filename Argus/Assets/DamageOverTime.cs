using UnityEngine;
using System.Collections;

public class DamageOverTime : MonoBehaviour
{
    public float BurnTimer = 5;
    public float BurnDamage = 5;
    private float damageCooldown = 1;
    private Transform parent;
    void Start()
    {
        parent = transform.parent.parent;
    }
    void OnEnable()
    {
        BurnTimer = 5;
        damageCooldown = 1;
    }

    public void Reset()
    {
        BurnTimer = 5;
    }

    void Update()
    {
        BurnTimer -= Time.deltaTime;
        damageCooldown -= Time.deltaTime;
        if (damageCooldown <= 0)
        {
            parent.SendMessage("damageEnemy", BurnDamage);
            damageCooldown = 1;
        }
        if (BurnTimer <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
