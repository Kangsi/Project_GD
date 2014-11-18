using UnityEngine;
using System.Collections;

public class AI_Patrol : Mob 
{
    protected override void Idle()
    {
        base.Idle();

        if (Time.time > (stateTime + idleTime))
        {
            ChangeState(EnemyState.patrol);
        }
    }
}
