// John van den Berg

/*=============================================================================
	Actionbar
=============================================================================*/
using UnityEngine;
using System.Collections;
using Manager;
/**
 * The Actionbar class 
 * 
 * User Interface class which handles the display of an actionbar
 * to hold the player's attacks and abilities.
 * 
 * Supports up to 4 slots. 
 */
public class Actionbar : MonoBehaviour {

    public Texture2D actionBar;
  
    public Rect actionbarAnchor;
    private float uiScale;
    

    void Awake()
    {
        uiScale = UIManager.uiScale;
        InitializeAnchors();
    }

	void OnGUI()
    {
        DrawActionBar();
    }

    void InitializeAnchors()
    {
        actionbarAnchor = new Rect((Screen.width / 2) - (actionBar.width / 2) * uiScale, (Screen.height - actionBar.height * uiScale), actionBar.width * uiScale, actionBar.height * uiScale);
    }

    void DrawActionBar()
    {
        GUI.DrawTexture(actionbarAnchor, actionBar);
    }
}
