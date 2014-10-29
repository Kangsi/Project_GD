using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public string targetTag;

    public int damage;
    protected bool hitSomething;

	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (hitSomething)
        {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == targetTag && targetTag == "Enemy")
        {
            Mob mob = col.gameObject.GetComponent<Mob>();
            mob.takeDamage(damage);
        }
        if (col.gameObject.tag != "Player")
        {
            hitSomething = true;
        }
    }

    public void SetVelocity(Vector3 direction, float speed)
    {
        transform.rigidbody.velocity = direction * speed;
    }
}
