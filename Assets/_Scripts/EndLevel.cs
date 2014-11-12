using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            Application.LoadLevel(1);
        }
    }
}
