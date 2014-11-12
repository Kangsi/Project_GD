using UnityEngine;
using System.Collections;
using Manager;

public class goldPurse : MonoBehaviour {




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(25, 400, 100, 50));
        GUILayout.Box("Gold: " + playerGold.money);

        GUILayout.EndArea();

        if (NPCDialogue.activateQuest)
        {
            GUILayout.BeginArea(new Rect(900, 200, 200, 50));
            GUILayout.Box("Quest Tracker \n Skeleton Kills:  " + PlayerStatManager.getSkeletonKills()); //aanpassen
            GUILayout.EndArea();
        }

    }
}
