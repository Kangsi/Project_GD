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
            Transform plane = gos[cnt].transform.Find("WalkablePlane");
            Vector3 randomPosition = new Vector3(Random.Range(-5 * plane.localScale.x, 5 * plane.localScale.x),
                                   0,
                                  Random.Range(-5 * plane.localScale.z, 5 * plane.localScale.z));
            //Vector3 randomPosition = new Vector3(Random.Range(-10,10), 0 ,Random.Range(-10,10));
            randomPosition += gos[cnt].transform.position;
			GameObject go = Instantiate(gos[cnt].GetComponent<SpawnPointEnemy>().gameObject[randomMob], 
			                            randomPosition, 
			                            Quaternion.identity) as GameObject;
			go.transform.parent = gos[cnt].transform;
            string enemy_AI = gos[cnt].GetComponent<SpawnPointEnemy>().enemyAI;

            AddComponentToEnemy(enemy_AI, go);
            
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
                SpawnPointEnemy spawnPointStats = spawnPoints[cnt].GetComponent<SpawnPointEnemy>();
                if (spawnPointStats.NSpawnEnemy == spawnPointStats.maxSpawnEnemies + 1)
                    Destroy(spawnPoints[cnt]);
            
                else if (spawnPoints[cnt].transform.childCount != spawnPoints[cnt].GetComponent<SpawnPointEnemy>().nMobs + 2)
                {
                    gos.Add(spawnPoints[cnt]);
                    spawnPointStats.NSpawnEnemy++;
                }
            }
				}
		return gos.ToArray ();
	}

    private void AddComponentToEnemy(string component, GameObject got)
    {
        switch (component.ToUpper())
        {
            case "WALK": got.AddComponent<AI_Walk>();
                break;
            case "PATROL": got.AddComponent<AI_Patrol>(); 
                break;
            case "STANDGUARD": got.AddComponent<AI_StandGuard>(); 
                break;
            default: got.AddComponent<AI_StandGuard>();
                Debug.Log("Unable to find the script file, AI set to stand guard.");
                break;
        }
    }
}
