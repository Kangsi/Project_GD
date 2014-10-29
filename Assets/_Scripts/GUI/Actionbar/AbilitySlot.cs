// John van den Berg

/*=============================================================================
	AbilitySlot
=============================================================================*/
using UnityEngine;
using System.Collections;
using Manager;
/**
 * The AbilitySlot class 
 * 
 * User Interface class which handles the display of the player's abilities.
 */
public class AbilitySlot {

	public Texture2D icon;

    private Rect skillSlotAnchor;
    private int index;
    private float uiScale;

    public AbilitySlot(int slotNumber, Rect anchor)
    {
        this.index = slotNumber;
        skillSlotAnchor = anchor;
        uiScale = UIManager.uiScale;
    }

    public void SetIcon()
    {
        icon = Player.Instance.generalClass.ability[index].abilityIcon;
    }

    public void DrawAbilitySlot()
    {
        GUI.DrawTexture(new Rect(   skillSlotAnchor.x + ((skillSlotAnchor.width-(icon.width*uiScale))/2), 
                                    skillSlotAnchor.y + ((skillSlotAnchor.height-(icon.height*uiScale))/2), 
                                    icon.width * uiScale, 
                                    icon.height * uiScale), 
                                    icon);
    }
}
