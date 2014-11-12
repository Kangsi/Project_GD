using UnityEngine;
using System.Collections;
using Manager;

public class NPCDialogue : MonoBehaviour {

    public string[] answerButtons;
    public string[] questions;
    public int rewardAmount;
    bool displayDialogue = true;
    public static bool activateQuest = false;
    bool hasDoneQuest = false;
    public Texture texture;

    private int requiredKills;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (activateQuest == true)
        {
            if (PlayerStatManager.getSkeletonKills() >= 3)
            {
                hasDoneQuest = true;
                displayDialogue = true;
            }
        }
	}

    void OnGUI()
    {
        if (displayDialogue && !activateQuest)
        {
            GUILayout.BeginArea(new Rect(300, 200, 400, 400), texture);
            GUILayout.Label(questions[0]);
            if (GUILayout.Button(answerButtons[0])){
            activateQuest = true;
            hasDoneQuest = false;
            displayDialogue = false;
            GUILayout.EndArea();
            }

            if (GUILayout.Button(answerButtons[1]) && !activateQuest && hasDoneQuest)
            {
                GUILayout.BeginArea(new Rect(300, 200, 400, 400), texture);

                displayDialogue = false;

                GUILayout.EndArea();
            }
            
        }

        if (displayDialogue && activateQuest)
        {
            GUILayout.BeginArea(new Rect(300, 200, 400, 400), texture);

            GUILayout.Label(questions[1]);
            if (GUILayout.Button(answerButtons[2]))
            {

                displayDialogue = false;
                QuestCompleted();
                activateQuest = false;
                GUILayout.EndArea();

            }
        }
        GUILayout.EndArea();        
    }

    void QuestCompleted()
    {
        playerGold.money += rewardAmount;
    }
}
