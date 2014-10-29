using UnityEngine;
using UnityEngine;
using System.Collections;

public class Enemy_Stats : MonoBehaviour
{

    //public CharacterController controller;
    public float runSpeed;
    public AnimationClip[] anim;

    public float aggroRange;
    public float attackRange;

    public int maxHealthPoints = 100;
    public int healthPoints;
    public int minIdleTime;
    public int maxIdleTime;
    public int deadTime;
    public int movingRadius;

    public Transform player;

    public Vector3[] patrolPoints;
    // Use this for initialization
    void Start()
    {
        healthPoints = maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

