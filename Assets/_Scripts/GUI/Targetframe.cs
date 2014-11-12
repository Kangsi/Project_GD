// John van den Berg

/*=============================================================================
	TarGetframe
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * The Targetframe class 
 * 
 * User Interface class which handles the display of the player's target.
 * Displays a dynamic healthbar and the target's name. 
 */
public class Targetframe : MonoBehaviour {

    public Texture2D healthBarRed;
    public Texture2D healthBarGreen;

    private Player player;
    private Enemy_Stats target;
    private GUIText targetName;
    private Rect healthBarPosition;
    private Rect damageBarPosition;
    private float healthPercentage;
    private float damageTaken;
    private int barWidth = 200;
    private int barHeight = 15;
    
	void Awake()
    {
        targetName = GetComponent<GUIText>();
        SetPosition();
    }

	void OnGUI()
    {
        if (Player.Instance.target != null && Player.Instance.target.tag == "Enemy")
        {
            target = Player.Instance.target.GetComponent<Enemy_Stats>();
            targetName.text = target.name;
            CalculateHealth();
            DrawHealthBar();
            DrawDamageBar();
        }
        else
            targetName.text = "";
    }

    void SetPosition()
    {
        //Position the Healthbar
        healthBarPosition.x = (Screen.width / 2) - (barWidth / 2);
        healthBarPosition.y = 40;
        healthBarPosition.height = barHeight;
        //Position the red Damagebar
        damageBarPosition.y = 40;
        damageBarPosition.height = barHeight;
        //Position the GUIText
        targetName.transform.position = new Vector2(0.5f, 0.95f);
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
    void CalculateHealth()
    {
        healthPercentage = (float)target.healthPoints / (float)target.maxHealthPoints;
        damageTaken = barWidth * healthPercentage;
        damageTaken = barWidth - damageTaken;
    }
}
