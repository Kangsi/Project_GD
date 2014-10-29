using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    public Door door;
    public float timer = 5f;
    public Mob mob;
    public bool canSpawn = true;
    public SpawnPoint spawnPoint;
	// Use this for initialization

	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider target)
    {        
        if (target.gameObject.tag == "Player")
        {
            Toggle(true);
        }              
    }
    void OnTriggerExit(Collider target)
    {

            if (target.gameObject.tag == "Player")
            {
                Toggle(false);
            }
       
        
    }


    public void Toggle(bool value)
    {
        if (value)
        {
            timer -= Time.deltaTime;
            if (timer <= 3)
            {
                if (canSpawn)
                {
                    Mob clone = Instantiate(mob, spawnPoint.transform.position, transform.rotation) as Mob;
                    canSpawn = false;
                }
            }

            if (timer <= 0)
            {
                if (door != null)
                {
                    Debug.Log("Destroy door");
                    Destroy(door.gameObject);
                }
                value = false;
            }
        }
        else
        {
            timer = 5f;
            canSpawn = true;
        }

    }
}
