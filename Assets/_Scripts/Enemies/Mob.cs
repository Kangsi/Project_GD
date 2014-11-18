using UnityEngine;
using System.Collections;
/*=====================================================================================================
 * 
=====================================================================================================*/


public class Mob : EnemyBehaviour
{
    # region
    protected float idleTime;
    protected float attackDelayTime;

    protected Vector3 newPosition;      // position used for random walk position
    protected float stateTime;          // time when a certain state starts
    protected int patrolWalkTo = 0;     // index of the patrol points to walk to
    public static int skeletonKills;    // amount of skeleton kills
    protected NavMeshAgent agent;       // used for pathfinding
    protected bool hitObjectFront;      // boolean when it hits something infront of him
    protected Transform plane;          // walkable plane
    protected Transform[] patrolPoints; // array of patrol points
    # endregion
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        plane = transform.parent.Find("WalkablePlane");       
        patrolPoints = transform.parent.Find("PatrolPoints").GetComponent<PatrolPoints>().PatrolPoint;
    }

	public override void Update () {    
		base.Update ();
        //RayCastFront();

        //if (inAgrroRange () && currentState != EnemyState.dead) 
        //{
        //    ChangeState (EnemyState.attack); 
        //} 
	}

	// Set random idleTime
	protected override void StartIdle()	
	{
		SetTime ();
		idleTime = Random.Range (stats.minIdleTime, stats.maxIdleTime);
        agent.ResetPath();
	}

	// Idle state: the object will stay at his location for a certain amount of time
	protected override void Idle()
	{
		animation.CrossFade (stats.anim[0].name);
        if (inAgrroRange()) ChangeState(EnemyState.enemyInSight);
	}
	
    // Play death animation and delete player target
	protected override void StartDead()
	{
        agent.ResetPath();
		// Play our die animation
		animation.CrossFade(stats.anim[5].name);
		
		// Disable the Character Controller to avoid collision
		transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);
        gameObject.rigidbody.isKinematic = true;
        gameObject.collider.enabled = false;

		if (Player.Instance.target = gameObject)                                                                          
            Player.Instance.target = null;
		SetTime ();
	}
   
	// Dead state: the object will be destroyed when the time is up
	protected override void Dead ()
	{
		if (Time.time > stateTime + stats.deadTime) 
        {
			Destroy (gameObject);
		}
	}

	// Generate a location to walk to on the plane
	protected override void StartWalk()
	{
        newPosition = new Vector3(Random.Range(-5 * plane.localScale.x, 5 * plane.localScale.x) + plane.position.x, 
		                           plane.position.y,
                                   Random.Range(-5 * plane.localScale.z, 5 * plane.localScale.z) + plane.position.z);
        MoveTo(newPosition);
	}

	// Walk state: walk to the location with the correct animation
	protected override void Walk()
	{
        animation.CrossFade(stats.anim[1].name);
		// when reaching his destination
		if (Distance2D(transform.position, newPosition) < 1) {
			ChangeState (EnemyState.idle);
		}
        if (inAgrroRange()) ChangeState(EnemyState.enemyInSight);
	}
	
	// Attack state: runs toward the player and attacks
	protected override void Attack()
	{
        animation.CrossFade(stats.anim[3].name);	
	}

    protected override void StartForcedAttack()
    {
        MoveTo(stats.player.position);
    }

    protected override void ForcedAttack()
    {
        animation.CrossFade(stats.anim[1].name);
        if (inAgrroRange() && currentState != EnemyState.dead)
        {
            ChangeState(EnemyState.attack);
        } 
    }

    protected override void StartPatrol()
    {
        if (patrolPoints[patrolWalkTo] != null)
        {
            MoveTo(patrolPoints[patrolWalkTo].position);
        }
    }

    protected override void Patrol()
    {
        animation.CrossFade(stats.anim[1].name);
        if (patrolPoints[patrolWalkTo] != null)
        {
            if (Distance2D(transform.position, patrolPoints[patrolWalkTo].position) < 1)
            {
                patrolWalkTo++;
                if (patrolWalkTo >= patrolPoints.Length) patrolWalkTo = 0;
                ChangeState(EnemyState.idle);
            }
            if (inAgrroRange()) ChangeState(EnemyState.enemyInSight);
        }
        else ChangeState(EnemyState.idle);
    }

    protected override void StartEnemyInSight()
    {
        
    }

    protected override void EnemyInSight()
    {
        MoveTo(stats.player.position);
        animation.CrossFade(stats.anim[1].name);
        if (!inAgrroRange()) ChangeState(EnemyState.idle);
        if (inAttackRange()) ChangeState(EnemyState.attackDelay);

    }

    protected override void StartAttackDelay()
    {
        agent.ResetPath();
        attackDelayTime = Random.Range(stats.minAttackDelayTime, stats.maxAttackDelayTime);
        SetTime();
    }

    protected override void AttackDelay()
    {
        animation.CrossFade(stats.anim[2].name);
        if (Time.time > stateTime + attackDelayTime) ChangeState(EnemyState.attack);
        if (!inAttackRange()) ChangeState(EnemyState.enemyInSight);
    }
    // Detect if the player is within aggro range and if the player is visible to the mob
	bool inAgrroRange()
    {
        if (Vector3.Distance(transform.position, stats.player.position) < stats.aggroRange && RayCastPlayer()) {    
            return true;             
        }

        else {
            return false;
        }
    }
    
    // Detects if the player is within attack range
	bool inAttackRange()
    {
        if (Vector3.Distance(transform.position, stats.player.position) < stats.attackRange) {
            RotateToTarget(stats.player.transform.position); // Always look towards the player
            return true;
        }
        else {
            return false;
        }
    }

    void OnMouseOver()
    {
        Highlight skeleton = gameObject.GetComponentInChildren<Highlight>();
        skeleton.changeShader();
    }

    void OnMouseExit()
    {
        Highlight skeleton = gameObject.GetComponentInChildren<Highlight>();
        skeleton.revertShader();
    }
    
    // Deals damage to the player
    public void dealDamage()
    {
        if (inAttackRange())
            Player.Instance.takeDamage(stats.damagePoints);   
    }

    // Takes damage from the player
    public void takeDamage(int damage, GameObject obj)
    {
        KnockBack(obj);
        if (currentState != EnemyState.dead)
        {
            stats.TakeDamage(damage);
            animation.CrossFade(stats.anim[2].name);
            if (currentState != EnemyState.attack)
                ChangeState(EnemyState.forcedAttack);
        }

        if (stats.HealthPoints <= 0 && currentState != EnemyState.dead) {
			ChangeState (EnemyState.dead);
            skeletonKills++;
        }
    }

    // Rotates toward a target
    void RotateToTarget(Vector3 target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        // Disable rotation on the xz axis
        targetRotation.x = 0.0f;
        targetRotation.z = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 8);
    }

    // Sets the time of initiation 
	void SetTime()
    {
        stateTime = Time.time;
    }

    // Calculates the distance between 2 points, ignoring the y-axis
    protected float Distance2D(Vector3 target1, Vector3 target2) 
    {
        float distance;
        distance = Mathf.Sqrt(Mathf.Pow(target1.x - target2.x, 2) + Mathf.Pow(target1.z - target2.z, 2));
        return distance;
    }

    // Raycat infront the enemy
    bool RayCastFront()
    {
        if(Physics.Raycast (transform.position, transform.forward, 2))
        {
            if (!hitObjectFront)
            {
                hitObjectFront = true;
                return true;
            }

        }
        
        hitObjectFront = false;
        return false;
    }

    // Raycast to the player.
    bool RayCastPlayer()
    {       
        RaycastHit hit;

        int layerMask = ~(1 << 8 | 1 << 11); // ignore Ground layer
        if (Physics.Raycast(transform.position, (stats.player.position - transform.position), out hit, stats.aggroRange,  layerMask))
        {
            Debug.DrawLine(transform.position, stats.player.position);
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    protected void MoveTo(Vector3 position)
    {
        animation.CrossFade(stats.anim[1].name);
        agent.SetDestination(position);
    }

    protected void KnockBack(GameObject obj)
    {
        Vector3 distance = transform.position - obj.transform.position;
        distance.Normalize();
        rigidbody.AddForce(distance * 1000);
    }

    protected void EndAttackState()
    {
        Debug.Log("testing end state");
        ChangeState(EnemyState.attackDelay);
    }

}