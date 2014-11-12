using UnityEngine;
using System.Collections;
using Manager;

public class SpellSlots : MonoBehaviour {

    ///////////////////////
    // Public variables
    //----------------------------------------
    public Texture2D debugTexture;
    public GUISkin guiSkin;

    ///////////////////////
    // Private variables
    //---------------------------------------
    private Actionbar actionbar;
    private Rect[] spellAnchor;
    private Rect[] primaryAnchor;
    private Rect[] petOrderAnchor;
    private float uiScale;

    ///////////////////////
    // Unity
    //---------------------------------------
    void Awake()
    {
        actionbar = GetComponent<Actionbar>();
        spellAnchor = new Rect[4];
        primaryAnchor = new Rect[2];
        petOrderAnchor = new Rect[2];
        uiScale = UIManager.uiScale;
    }
    void Start()
    {
        //Add as listener
        EventManager.Instance.AddListener(this, "OnUIScaleUpdate");
        EventManager.Instance.AddListener(this, "OnAbilityUse");
        //Initialize anchors
        InitializeAnchors();
    }
    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUI.depth = -1;
        DrawSpells();
        DrawCooldownDisplay();
    }

    ///////////////////////
    // Funcions
    //---------------------------------------
    // Function to initialze all anchors
    private void InitializeAnchors()
    {
        for (int i = 0; i < spellAnchor.Length; i++)
        {
            spellAnchor[i] = new Rect(actionbar.actionbarAnchor.x + ((33+(67*i)) * uiScale), actionbar.actionbarAnchor.y + (48 * uiScale), 59*uiScale, 59*uiScale);
        }

        for (int i = 0; i < primaryAnchor.Length; i++)
        {
            primaryAnchor[i] = new Rect(actionbar.actionbarAnchor.x + ((303+(66*i)) * uiScale), actionbar.actionbarAnchor.y + (50 * uiScale), 56*uiScale,55*uiScale);
        }
        for (int i = 0; i < petOrderAnchor.Length; i++)
        {
            petOrderAnchor[i] = new Rect(actionbar.actionbarAnchor.x + ((435+(66*i)) * uiScale), actionbar.actionbarAnchor.y + (48 * uiScale), 59*uiScale,59*uiScale);
        }
    }
    //---------------------------------------
    // Function to draw the spells owned by the player
    private void DrawSpells()
    {
        //Get the primary en secondary abilities. Index entry 0 and 1 in the spellbook
        for (int i = 0; i < primaryAnchor.Length; i++)
        {
            if (Spellbook.Instance.getIcon(i.ToString()) != null)
                GUI.DrawTexture(primaryAnchor[i], Spellbook.Instance.getIcon(i.ToString()));
        }
        //Get index entry 2 to 4 in our spellbook
        for (int i = 0; i < 2; i++)
        {
            if (Spellbook.Instance.getIcon((i+2).ToString()) != null)
                GUI.DrawTexture(spellAnchor[i], Spellbook.Instance.getIcon((i+2).ToString()));
        }
    }
    //---------------------------------------
    // Function to draw the cooldown timers of each spell
    private void DrawCooldownDisplay()
    {
        for (int i = 0; i < primaryAnchor.Length; i++)
        {
            if (Spellbook.Instance.getIcon(i.ToString()) != null)
                GUI.Label(primaryAnchor[i], Spellbook.Instance.GetSpell(i.ToString()).getCooldownTimeRemaining());
        }

        for (int i = 0; i < 2; i++)
        {
            if (Spellbook.Instance.getIcon((i + 2).ToString()) != null)
                GUI.Label(spellAnchor[i], Spellbook.Instance.GetSpell((i+2).ToString()).getCooldownTimeRemaining());
        }
    }
    //---------------------------------------
    // Function to update the uiScale 
    private void OnUIScaleUpdate()
    {
        uiScale = UIManager.uiScale;
    }
}
