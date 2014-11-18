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
        enemyInSight,
        attackDelay,
        attack,
        forcedAttack
    }
    /*This is the currentState of the Enemy, this is what you'll change in the child-Class*/
    public EnemyState currentState;

    protected GameObject playerReference;
    protected Transform player;

    void Awake()
    {
        currentState = EnemyState.initializing;
        stats = GetComponent<Enemy_Stats>();
        playerReference = GameObject.Find("Player");
        stats.player = playerReference.transform;
    }

    /*In here there is a switch-statement which handles which method that is going
    * to be updating, this is chosen by the currentState of the enemy. */
    public virtual void Update()
    {

        switch (currentState)
        {
            #region
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
            case EnemyState.forcedAttack:
                ForcedAttack();
                break;
            case EnemyState.enemyInSight:
                EnemyInSight();
                break;
            case EnemyState.attackDelay:
                AttackDelay();
                break;
            default:
                Idle();
                break;
            #endregion
        }
    }

    protected void ChangeState(EnemyState newState)
    {
        StartState(newState);
        currentState = newState;
        Debug.Log("newstate = " + currentState);
    }

    /*When you add your own methods here they need to be virtual, this is so you can in override them in your own
     class*/

    protected virtual void Idle() { }
    protected virtual void Attack() { }
    protected virtual void Patrol() { }
    protected virtual void Walk() { }
    protected virtual void Dead() { }
    protected virtual void ForcedAttack() { }
    protected virtual void EnemyInSight() { }
    protected virtual void AttackDelay() { }

    protected virtual void StartState(EnemyState newState)
    {
        switch (newState)
        {
            #region
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
            case EnemyState.forcedAttack:
                StartForcedAttack();
                break;
            case EnemyState.enemyInSight:
                StartEnemyInSight();
                break;
            case EnemyState.attackDelay:
                StartAttackDelay();
                break;
            default:
                StartIdle();
                break;
            #endregion
        }
    }
    protected virtual void StartIdle() { }
    protected virtual void StartAttack() { }
    protected virtual void StartPatrol() { }
    protected virtual void StartWalk() { }
    protected virtual void StartDead() { }
    protected virtual void StartForcedAttack() { }
    protected virtual void StartEnemyInSight() { }
    protected virtual void StartAttackDelay() { }
}