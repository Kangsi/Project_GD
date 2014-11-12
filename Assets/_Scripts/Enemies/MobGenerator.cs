using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MobGenerator : MonoBehaviour
{
	public enum State
    {
		Idle,
		Initialize,
		Setup, 
		SpawnMob
	}
	public GameObject[] spawnPoints; // an array to hold all spawnpoints in the scene

	public State state; // local variable thats holds our current state

	void Awake() 
	{
		state = MobGenerator.State.Initialize;
	}

	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			switch(state)
			{
				case State.Initialize:
					Initialize();
				    break;
				case State.Setup:
					Setup();
				    break;
				case State.SpawnMob:
					SpawnMob();
				    break;
			}
			yield return 0;
		}
	}
	
	// Initialize state: check if the arrays are not empty
	private void Initialize()
	{		 
		if (!CheckForSpawnPoints ())
						return;
		state = MobGenerator.State.Setup;
	}

	private void Setup()
	{ 
		state = MobGenerator.State.SpawnMob;
	}

	// SpawnMob state: spawns a random mob at every available spawnpoint
	private void SpawnMob()
	{ 
		GameObject[] gos = AvailableSpawnPoints ();
		for (int cnt = 0; cnt < gos.Length; cnt++) {

            int randomMob = Random.Range(0, gos[cnt].GetComponent<SpawnPointEnemy>().gameObject.Length);
            Vector3 randomPosition = new Vector3(Random.Range(-10,10), 0 ,Random.Range(-10,10));
            randomPosition += gos[cnt].transform.position;
			GameObject go = Instantiate(gos[cnt].GetComponent<SpawnPointEnemy>().gameObject[randomMob], 
			                            randomPosition, 
			                            Quaternion.identity) as GameObject;
			go.transform.parent = gos[cnt].transform;
            //go.GetComponent<Enemy_Stats>().enabled = true;
			go.GetComponent<Mob>().enabled = true;
				}
		state = MobGenerator.State.Initialize;

	}

	// check to see that we have at least 1 spawnpoint
	private bool CheckForSpawnPoints()
	{
		if (spawnPoints.Length > 0)
						return true;
				else
						return false;

	}

	// generate a list of available spawnpoints that do not have any mob childed to it
	private GameObject[] AvailableSpawnPoints(){
		List<GameObject> gos = new List<GameObject> ();
		for (int cnt = 0; cnt < spawnPoints.Length; cnt++) {
            if (spawnPoints[cnt] != null)
            {
                if (spawnPoints[cnt].transform.childCount != spawnPoints[cnt].GetComponent<SpawnPointEnemy>().nMobs)
                {
                    gos.Add(spawnPoints[cnt]);
                }
            }
				}
		return gos.ToArray ();
	}
}
