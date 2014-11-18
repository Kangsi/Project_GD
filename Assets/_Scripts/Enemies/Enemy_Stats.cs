using UnityEngine;
using UnityEngine;
using System.Collections;

public class Enemy_Stats : MonoBehaviour
{
    public float runSpeed;
    public AnimationClip[] anim;

    public float aggroRange;
    public float attackRange;

    public int healthPoints;
    public int maxHealthPoints, armorPoints, damagePoints;
    public float maxHealthScale, armorScale, damageScale;

    public float minIdleTime, maxIdleTime;
    public float deadTime;
    public float minAttackDelayTime, maxAttackDelayTime;

    public Transform player;
    protected int level;

    // Use this for initialization
    void Start()
    {
        //level = player.GetComponent<Player>().level;
        level = 1;
        StatsInit();
        healthPoints = maxHealthPoints;
    }

    void StatsInit()
    {
        maxHealthPoints *= (int)(maxHealthScale * level);
        armorPoints *= (int)(armorScale * level); 
        damagePoints *= (int)(damageScale * level);
    }

    public void TakeDamage(int damage)
    {
        if (damage > armorPoints) healthPoints -= damage - armorPoints;
        if (healthPoints < 0 ) healthPoints = 0;
    }

    public int HealthPoints
    {
        get
        {
            return healthPoints;
        }
    }
}

