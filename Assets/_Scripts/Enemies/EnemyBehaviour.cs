using UnityEngine;
using System.Collections;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected Enemy_Stats stats;
    public enum EnemyState
    {
        initializing,
        idle,
        patrol,
        walk,
        dead,
        attack,
        stayDead
    }
    /*This is the currentState of the Enemy, this is what you'll change in the child-Class*/
    public EnemyState currentState;

    protected GameObject playerReference;
    protected Transform player;

    void Awake()
    {
        currentState = EnemyState.initializing;
        stats = GetComponent<Enemy_Stats>();
        Debug.Log("stats name: " + stats.name);
        playerReference = GameObject.Find("Player");
        stats.player = playerReference.transform;
        //playerPawn = stats.player.GetComponent<Pawn>();
    }

    /*In here there is a switch-statement which handles which method that is going
    * to be updating, this is chosen by the currentState of the enemy.
     It is in here that you will add your own EnemyState.yourState-case and call for your own method below*/
    public virtual void Update()
    {

        switch (currentState)
        {
            case EnemyState.initializing:
                /*filling in the player reference for easier access*/

                ChangeState(EnemyState.idle);
                break;
            case EnemyState.attack:
                Attack();
                break;
            case EnemyState.dead:
                Dead();
                break;
            case EnemyState.idle:
                Idle();
                break;
            case EnemyState.patrol:
                Patrol();
                break;
            case EnemyState.walk:
                Walk();
                break;
            default:
                Idle();
                break;
        }
    }

    protected void ChangeState(EnemyState newState)
    {
        StartState(newState);
        currentState = newState;
    }

    /*When you add your own methods here they need to be virtual, this is so you can in override them in your own
     class*/

    protected virtual void Idle()
    {
    }
    protected virtual void Attack()
    {
    }
    protected virtual void Patrol()
    {
    }
    protected virtual void Walk()
    {
    }
    protected virtual void Dead()
    {
    }

    protected virtual void StartState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.attack:
                StartAttack();
                break;
            case EnemyState.dead:
                StartDead();
                break;
            case EnemyState.idle:
                StartIdle();
                break;
            case EnemyState.patrol:
                StartPatrol();
                break;
            case EnemyState.walk:
                StartWalk();
                break;
            default:
                StartIdle();
                break;
        }
    }
    protected virtual void StartIdle()
    {
    }
    protected virtual void StartAttack()
    {
    }
    protected virtual void StartPatrol()
    {
    }
    protected virtual void StartWalk()
    {
    }
    protected virtual void StartDead()
    {
    }
}