// John van den Berg

/*=============================================================================
	Soulframe
=============================================================================*/
using UnityEngine;
using System.Collections;
using Manager;
/**
 * The Soulframe class 
 * 
 * User Interface class which handles the display of the player's active soul.
 */


/// <summary>
/// TODO: 
/// Implement an eventlistener to update the soul icon instead of using the OnGUI function.
/// </summary>

public class Soulframe : MonoBehaviour {

    public GameObject player;
    public Rect position;

    private Texture2D icon;

    void Start()
    {
        icon = player.GetComponent<Player>().activeSoul.soulIcon;
    }

    void OnGUI()
    {
        icon = player.GetComponent<Player>().activeSoul.soulIcon;
        DrawSoulIcon();
    }

    void DrawSoulIcon()
    {
       GUI.DrawTexture(new Rect(position.x, position.y, icon.width, icon.height), icon);  
    }
}
