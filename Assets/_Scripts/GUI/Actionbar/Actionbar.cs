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

    public GUISkin uiSkin;
    public Texture2D actionBar;
    
    public int buttonCount = 2;
    public int paddingBottom;
    public int marginButtons;
    
    public GUITexture test;
    private SkillSlot[] skill;
    private AbilitySlot[] ability;
    private Rect[] buttonAnchor;
    private float uiScale;
    
    void Start()
    {
        uiScale = UIManager.uiScale;
        InitializeButtons();
        InitializeSkillSlots();
        InitializeAbilitySlots();
    }

	void OnGUI()
    {
        GUI.skin = uiSkin;
        DrawActionBar();
        DrawSkillSlots();
        DrawAbilitySlots();
        DrawCooldowns();
    }

    void InitializeButtons()
    {
        buttonAnchor = new Rect[buttonCount];

        for (int i = 0; i < buttonAnchor.Length; i++)
        {
            buttonAnchor[i] = new Rect(new Rect( ((Screen.width / 2) - ((((actionBar.width + marginButtons) * buttonCount) / 2)*uiScale) + (((actionBar.width + marginButtons) * i))*uiScale), 
                                                Screen.height - (actionBar.height * uiScale) - paddingBottom, 
                                                actionBar.width * uiScale, 
                                                actionBar.height * uiScale));
        }
    }
    void InitializeSkillSlots()
    {
        skill = new SkillSlot[2];
        for (int i = 0; i < skill.Length; i++)
        {
            skill[i] = new SkillSlot(i, buttonAnchor[i]);
        }
    }
    void InitializeAbilitySlots()
    {
        ability = new AbilitySlot[2];
        for (int i = 0; i < ability.Length; i++)
        {
            ability[i] = new AbilitySlot(i, buttonAnchor[i + 2]);
        }
    }

    void DrawActionBar()
    {
        for (int i = 0; i < buttonCount; i++)
        {
            GUI.DrawTexture(buttonAnchor[i], actionBar);
        }
    }
    void DrawSkillSlots()
    {
        for (int i = 0; i < skill.Length; i++)
        {
            skill[i].SetIcon();
            skill[i].DrawSkillSlot();
        }
    }
    void DrawAbilitySlots()
    {
        for (int i = 0; i < ability.Length; i++)
        {
            ability[i].SetIcon();
            ability[i].DrawAbilitySlot();
        }
    }
    void DrawCooldowns()
    {
        for (int i = 0; i < 2; i++)
        {
            GUI.Label(buttonAnchor[i], GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().activeSoul.attacks[i].getCooldownTimeRemaining());
            GUI.Label(buttonAnchor[i + 2], GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().generalClass.ability[i].getCooldownTimeRemaining());
        }
    }
}
