using UnityEngine;
using System.Collections;

public class PatrolPoints : InvisibleObject {

    public Transform[] patrolPoints;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Transform[] PatrolPoint
    {
        get { return patrolPoints; }
    }
}
