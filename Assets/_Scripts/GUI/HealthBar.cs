// John van den Berg

/*=============================================================================
	Healthbar
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * The Healthbar class 
 * 
 * User Interface class which handles the display of the player's health.
 */
public class HealthBar : MonoBehaviour {

    public Texture2D healthBarGreen;
    public Texture2D healthBarRed;

    private Player player;
    private Rect healthBarPosition;
    private Rect damageBarPosition;
    private float healthPercentage;
    private float damageTaken;
    private int barWidth = 180;
    private int barHeight = 10;

	void Awake () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        SetPosition();
	}
	
	void OnGUI () 
    {
        CalculateHealth();
        DrawHealthBar();
        DrawDamageBar();
	}

    void SetPosition()
    {
        //Position the Healthbar
        healthBarPosition.x = 80;
        healthBarPosition.y = 20;
        healthBarPosition.height = barHeight;
        //Position the red Damagebar
        damageBarPosition.y = 20;
        damageBarPosition.height = barHeight;
    }
    void CalculateHealth()
    {
        healthPercentage = (float)player.healthPoints / (float)player.maxHealthPoints;
        damageTaken = barWidth * healthPercentage;
        damageTaken = barWidth - damageTaken;
    }
    void DrawHealthBar()
    {
        healthBarPosition.width = barWidth * healthPercentage;
        GUI.DrawTexture(healthBarPosition, healthBarGreen);
    }
    void DrawDamageBar()
    {
        damageBarPosition.x = healthBarPosition.x + healthBarPosition.width;
        damageBarPosition.width = damageTaken;
        GUI.DrawTexture(damageBarPosition, healthBarRed);
    }
}
