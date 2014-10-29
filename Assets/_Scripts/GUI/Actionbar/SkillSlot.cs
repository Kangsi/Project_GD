// John van den Berg

/*=============================================================================
	Skillslot
=============================================================================*/
using UnityEngine;
using System.Collections;
using Manager;
/**
 * The SkillSlot class 
 * 
 * User Interface class which handles the display of the player's attacks.
 */

public class SkillSlot {

    public Texture2D icon;

    private Rect skillSlotAnchor;
    private int slotNumber;
    private float uiScale;

    public SkillSlot(int slotNumber, Rect anchor)
    {
        this.slotNumber = slotNumber;
        skillSlotAnchor = anchor;
        uiScale = UIManager.uiScale;
    }

    public void SetIcon()
    {
        icon = Player.Instance.activeSoul.attacks[slotNumber].abilityIcon;       
    }

    public void DrawSkillSlot()
    {
        GUI.DrawTexture(new Rect(   skillSlotAnchor.x + ((skillSlotAnchor.width-(icon.width*uiScale))/2), 
                                    skillSlotAnchor.y + ((skillSlotAnchor.height-(icon.height*uiScale))/2), 
                                    icon.width * uiScale, 
                                    icon.height * uiScale), 
                                    icon);
    }
}
