using UnityEngine;
using System.Collections;

public class AI_StandGuard : Mob
{
    protected override void Idle()
    {
        base.Idle();

        if (Time.time > (stateTime + idleTime))
        {
            ChangeState(EnemyState.patrol);
        }
    }

    protected override void Patrol()
    {
        animation.CrossFade(stats.anim[1].name);
        if (patrolPoints[patrolWalkTo] != null)
        {
            if (Distance2D(transform.position, patrolPoints[patrolWalkTo].position) < 1)
            {
                if (patrolWalkTo >= patrolPoints.Length) patrolWalkTo = 0;
                ChangeState(EnemyState.idle);
            }
        }
        else ChangeState(EnemyState.idle);
    }
}
