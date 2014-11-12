using UnityEngine;
using System.Collections;

public class oldMob : EnemyBehaviour {

    protected int idleTime;

    protected Vector3 newPosition;
    protected float time;
    protected int patrolWalkTo;

    void Start()
    {
        //stats.controller = GetComponent<CharacterController>();
        stats.healthPoints = stats.maxHealthPoints;
    }

	public override void Update () {    
		base.Update ();

		// Move to or attacks if the player is in range
		if (inAgrroRange () && currentState != EnemyState.dead) {
			ChangeState (EnemyState.attack); 
		} 
	}

	// Set random idleTime
	protected override void StartIdle()	
	{
		SetTime ();
		idleTime = Random.Range (stats.minIdleTime, stats.maxIdleTime);
	}

	// Idle state: the object will stay at his location for a certain amount of time
	protected override void Idle()
	{
		animation.CrossFade (stats.anim[0].name);

		// Change state to walk
		if (Time.time > (time + idleTime)) 
        {
			ChangeState (EnemyState.walk);
		}
	}
	
	protected override void StartDead()
	{
		// Play our die animation
		animation.CrossFade(stats.anim[5].name);
		
		// Disable the Character Controller to avoid collision
		//stats.controller.enabled = false;
		transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);
		
        ////////////////////////////////////////////////////
        ////////////////////////////////////////////////////
        // Dit moet veranderen want nu wordt de target van de
        // speler altijd gewist ook als hij eigenlijk al een
        // andere target heeft.
        ////////////////////////////////////////////////////
        ////////////////////////////////////////////////////

		if (Player.Instance.target != null)                                                                          
            Player.Instance.target = null;
		SetTime ();
	}
   
	// Dead state: the object will be destroyed when the time is up. When the time is up a new object will spawn.
	protected override void Dead ()
	{
		if (Time.time > time + stats.deadTime) 
        {
			Destroy (gameObject);
		}
	}

	// Generate a location to walk to near the spawnpoint
	protected override void StartWalk()
	{
		newPosition = new Vector3 (Random.Range (-stats.movingRadius, stats.movingRadius) + transform.parent.position.x, 
		                           transform.parent.position.y, 
		                           Random.Range (-stats.movingRadius, stats.movingRadius) + transform.parent.position.z);

	}

	// Walk state: walk to a random position near the spawn point
	protected override void Walk()
	{

        RotateToTarget(newPosition);
       // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        //transform.LookAt(newPosition);
		//controller.SimpleMove(transform.forward * (runSpeed * Time.deltaTime));
		animation.CrossFade(stats.anim[1].name);


        rigidbody.velocity = new Vector3(transform.forward.x * (stats.runSpeed * Time.deltaTime), rigidbody.velocity.y, transform.forward.z * (stats.runSpeed * Time.deltaTime));
		// when reaching his destination
		if (Distance2D(transform.position, newPosition) < 1) {
			ChangeState (EnemyState.idle);
		}
	}
	
	// Attack state: runs toward the player and attacks
	protected override void Attack()
	{
		if (!inAttackRange())
			moveToPlayer();
		else if (inAttackRange()) {
			animation.CrossFade(stats.anim[4].name);
		}

		if (!inAttackRange()){
			ChangeState (EnemyState.idle);
		}   	
	}

    // Detect if the player is within aggro range
	bool inAgrroRange()
    {
        if (Vector3.Distance(transform.position, stats.player.position) < stats.aggroRange) {
            return true;
        }
        else {
            return false;
        }
    }
    
    // Dettect if the player is within attack range
	bool inAttackRange()
    {
        if (Vector3.Distance(transform.position, stats.player.position) < stats.attackRange) {
            return true;
        }
        else {
            return false;
        }
    }

    // Move towards the player
    void moveToPlayer()
    {

        RotateToTarget(stats.player.position);
		//transform.LookAt(player.position);
		//controller.SimpleMove(transform.forward * (runSpeed * Time.deltaTime));
        rigidbody.velocity = new Vector3(transform.forward.x * (stats.runSpeed * Time.deltaTime), rigidbody.velocity.y, transform.forward.z * (stats.runSpeed * Time.deltaTime));
		animation.CrossFade(stats.anim[1].name);
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
    
    void dealDamage(int dmg)
    {
        if (inAttackRange())
            Player.Instance.takeDamage(dmg);   
    }

    public void takeDamage(int damage)
    {
        stats.healthPoints = (stats.healthPoints - damage);
        animation.CrossFade(stats.anim[2].name);

        if (stats.healthPoints <= 0 && currentState != EnemyState.dead) {
            stats.healthPoints = 0;
			ChangeState (EnemyState.dead);
           // Player.Instance.skeletonKills++;
        }
    }

    void RotateToTarget(Vector3 target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        // Disable rotation on the xz axis
        targetRotation.x = 0.0f;
        targetRotation.z = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 8);
    }

	void SetTime()
	{
		time = Time.time;
	}

    float Distance2D(Vector3 target1, Vector3 target2)
    {
        float distance;
        distance = Mathf.Sqrt(Mathf.Pow(target1.x - target2.x, 2) + Mathf.Pow(target1.z - target2.z, 2));
        return distance;
    }
}
