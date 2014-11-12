using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Manager;

public class Spellbook : MonoBehaviour {

    ///////////////////////
    // Public variables
    //----------------------------------------
    // Public Instance
    public static Spellbook Instance
    {
        get
        {
            if (instance == null) instance = new Spellbook();

            return instance;
        }
    }
    //----------------------------------------
    // Ability storage
    public Ability[] ability;
    
    ///////////////////////
    // Private variables
    //---------------------------------------
    // Internal reference
    private static Spellbook instance = null;           
    //----------------------------------------
    // SpellBook
    private Dictionary<string, List<Ability>> SpellBook = new Dictionary<string, List<Ability>>();
    //----------------------------------------
    // IconBook
    private Dictionary<string, List<Texture2D>> IconBook = new Dictionary<string, List<Texture2D>>();
   

    ///////////////////////
    // Unity
    //---------------------------------------
    void Awake()
    {
        //Check if an instance already exists 
        if (instance)
            DestroyImmediate(gameObject); //Delete duplicate
        else
        {
            instance = this; //Make this object the only instance
            DontDestroyOnLoad(gameObject); //Set as do not distroy
        }       
    }
  
    void Start()
    {
        EventManager.Instance.AddListener(this, "OnAbilityUse");
        foreach (Ability spell in ability)
        {
            if (ability != null)
            {
                spell.InitializeSpell();
                AddSpell(spell, spell.spellID.ToString());
                addIcon(spell.spellIcon_Active, spell.spellID.ToString());
                Debug.Log("Added spell: " + spell.spellID);
            }
        }
    }

    ///////////////////////
    // Functions
    //---------------------------------------
    // Function to add new spells to our spellbook
    public void AddSpell(Ability spell, string spellID)
    {
        //Add spell to dictionary
        if (!SpellBook.ContainsKey(spellID))
            SpellBook.Add(spellID, new List<Ability>());

        //Add object to listener list for this event
        SpellBook[spellID].Add(spell);
    }
    //---------------------------------------
    // Function to retrieve a spell from our dictionary
    public Ability GetSpell(string spellID)
    {
        //If the spellID does not exist in the dictionary, then exit
        if (!SpellBook.ContainsKey(spellID))
        {
            Debug.Log("No spell with that spellID exists.");
            return null;
        }
        return SpellBook[spellID][0];
    }
    //---------------------------------------
    // Function to use a spell in our dictionary
    public void UseSpell(string spellID)
    {
        //If the spellID does not exist in the dictionary, then exit
        if (!SpellBook.ContainsKey(spellID))
        {
            Debug.Log("No spell with that spellID exists.");
            return;
        }

        //Else use the spell
        foreach (Ability spell in SpellBook[spellID])
            spell.CastSpell();
    }
    //---------------------------------------
    // Function to add new icons to our IconBook
    public void addIcon(Texture2D icon, string spellID)
    {
        //Add spell to dictionary
        if (!IconBook.ContainsKey(spellID))
            IconBook.Add(spellID, new List<Texture2D>());

        //Add object to listener list for this event
        IconBook[spellID].Add(icon);
    }
    //---------------------------------------
    // Function to update an icon in our dictionary. Replaces any existing icon
    public void updateIcon(Texture2D icon, string spellID)
    {
        //If the spellID does not exist in the dictionary, then exit
        if (!IconBook.ContainsKey(spellID))
        {
            Debug.Log("No icon with that spellID exists. Cannot get icon!");
            return;
        }

        //Add object to listener list for this event
        IconBook[spellID].Clear();
        IconBook[spellID].Add(icon);
    }
    //---------------------------------------
    // Function to get icons from our IconBook. Uses the spellID to retrieve the right icon
    public Texture2D getIcon(string spellID)
    {
        //If the spellID does not exist in the dictionary, then exit
        if (!IconBook.ContainsKey(spellID))
        {
            Debug.Log("No icon with that spellID exists. Cannot get icon!");
            return null;
        }

        //Else use the spell
        return IconBook[spellID][0];
    }

    //---------------------------------------
    // Event function which gets called whenever an ability is used. It checks our abilities
    // to see if any of them are on cooldown and if so updates the icon to reflect its current
    // state. If an ability is on cooldown we start a coroutine to check the status of our spells
    // again when the cooldown time of that spell has finished.
    private void OnAbilityUse()
    {
        foreach (Ability spell in ability)
        {
            if (spell.IsOnCooldown())
            {
                updateIcon(spell.spellIcon_Cooldown, spell.spellID.ToString());
                StartCoroutine(CheckSpellOnCooldown(spell.cdTime));
            }
            else
            {
                updateIcon(spell.spellIcon_Active, spell.spellID.ToString());
            }
        }
    }

    IEnumerator CheckSpellOnCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        OnAbilityUse();
    }
}
