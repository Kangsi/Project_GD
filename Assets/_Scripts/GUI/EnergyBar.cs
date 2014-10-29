// John van den Berg

/*=============================================================================
	Energybar
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * The Energybar class 
 * 
 * User Interface class which handles the display of the player's energy.
 */
public class EnergyBar : MonoBehaviour {

    public Texture2D energyFull;
    public Texture2D energyEmpty;

    private Player player;
    private Rect fullBarPosition;
    private Rect emptyBarPosition;
    private float energyPercentage;
    private float energyLost;
    private int barWidth = 180;
    private int barHeight = 10;

    void Awake()
    {
        player = Player.Instance;
        SetPosition();
    }

    void OnGUI()
    {
        CalculateEnergy();
        DrawFullBar();
        DrawEmptyBar();
    }

    void SetPosition()
    {
        //Position the Healthbar
        fullBarPosition.x = 80;
        fullBarPosition.y = 40;
        fullBarPosition.height = barHeight;
        //Position the red Damagebar
        emptyBarPosition.y = 40;
        emptyBarPosition.height = barHeight;
    }
    void CalculateEnergy()
    {
        energyPercentage = (float)player.energyPoints / (float)player.maxEnergyPoints;
        energyLost = barWidth * energyPercentage;
        energyLost = barWidth - energyLost;
    }
    void DrawFullBar()
    {
        fullBarPosition.width = barWidth * energyPercentage;
        GUI.DrawTexture(fullBarPosition, energyFull);
    }
    void DrawEmptyBar()
    {
        emptyBarPosition.x = fullBarPosition.x + fullBarPosition.width;
        emptyBarPosition.width = energyLost;
        GUI.DrawTexture(emptyBarPosition, energyEmpty);
    }
}
