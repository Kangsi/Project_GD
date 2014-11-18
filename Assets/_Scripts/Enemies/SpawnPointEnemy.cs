using UnityEngine;
using System.Collections;

public class SpawnPointEnemy : MonoBehaviour
{
    public GameObject[] gameObject;
    public int nMobs;
    public string enemyAI;
    public int maxSpawnEnemies;
    protected int nSpawnEnemy;

    public int NSpawnEnemy
    {
        get { return nSpawnEnemy; }
        set { nSpawnEnemy = value; }
    }

}
