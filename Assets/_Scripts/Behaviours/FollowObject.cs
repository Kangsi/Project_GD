using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

    public Transform target;
    public float followSpeed;
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
	}
}
