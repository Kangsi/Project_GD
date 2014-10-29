using UnityEngine;
using System.Collections;

public class Second_Character : MonoBehaviour
{
    //State
    #region
    public enum MOVETYPE { followingPawn = 0, movingTowardsPoint = 1, attackingEnemy = 2, usingScenery = 3 };

    public MOVETYPE SecondCharacterState
    {
        get { return State; }
        set { State = value; }
    }

    protected MOVETYPE State = MOVETYPE.followingPawn;
    #endregion

    public Player player;
    public Bullet bullet;

    protected bool followingPlayer, movingTowardsPoint, movingTowardsPlayer;

    public float flySpeed, flyHeight;
    public float rotationSpeed,rotationDistancePlayer, rotationDistanceEnemy, rotationDistanceScenery;
    protected float rotationDistance;
    protected Vector3 targetPosition;

    //Shoot
    public float shotPerSecond, bulletSpeed;
    protected float shootTimer;

    protected GameObject target;

	void Start () 
    {
        followingPlayer = true;
        rotationDistance = rotationDistancePlayer;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (State == MOVETYPE.followingPawn)
        {
            rotationDistance = rotationDistancePlayer;
            FollowPlayer();
        }
        if (State == MOVETYPE.movingTowardsPoint)
        {
            MoveTowardsTarget();
        }
        if (State == MOVETYPE.attackingEnemy)
        {
            //AttackEnemy
            rotationDistance = rotationDistanceEnemy;
            if (target != null)
            {
                RotateAround(target.transform.position);
                AttackTarget();
            }
        }
        if (State == MOVETYPE.usingScenery)
        {
            //UseScenery
            rotationDistance = rotationDistanceScenery;
        }
	}

    public void SetTarget(GameObject gameObject)
    {
        target = gameObject;
    }

    public void MoveTo(Vector3 target)
    {
        State = MOVETYPE.movingTowardsPoint;
        targetPosition = new Vector3(target.x, target.y + flyHeight, target.z);
    }

    public void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, flySpeed * Time.deltaTime);
    }

    public void FollowPlayer()
    {
        RotateAround(new Vector3(player.transform.position.x, player.transform.position.y + flyHeight, player.transform.position.z));
    }

    public void RotateAround(Vector3 point)
    {
        float distanceToPoint = Vector3.Distance(transform.position, point);

        //transform.RotateAround(point, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
        if (distanceToPoint <= rotationDistance)
        {
            float posY = point.y + flyHeight ;
            transform.position = point + (transform.position - point).normalized * rotationDistance;
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            transform.RotateAround(point, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, point, flySpeed * Time.deltaTime);
        }
    }

    public void AttackTarget()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shotPerSecond)
        {
            Debug.Log("Shoot");
            shootTimer = 0;
            //Shoot bullet towards target;
            ShootTowards(target.transform.position);
        }
    }

    public void ShootTowards(Vector3 position)
    {
        Vector3 direction = position - transform.position;

        Bullet bulletClone = (Bullet)Instantiate(bullet, transform.position, transform.rotation);
        direction.Normalize();

        Bullet b = bulletClone.gameObject.GetComponent("Bullet") as Bullet;
        b.SetVelocity(direction,bulletSpeed);
    }
}
