// John van den Berg

/*=============================================================================
	Base Spell
=============================================================================*/
using UnityEngine;
using System.Collections;
using Manager;
/**
 * Base for other spells to derive from.
 * 
 * Each derived class must set the icon through the inspector.
 * Within the derived class's constructor the energycost and 
 * cooldowntime must be set otherwhise they will be handled as 0
 */
public abstract class Ability : MonoBehaviour {

    ///////////////////////
    // Variables
    //----------------------------------------
    // Public 
    public string name;
    public string description;
    public int spellID;
    [HideInInspector]
    public Texture2D spellIcon;
    //----------------------------------------
    // Protected 
    protected Cooldown cooldown = new Cooldown();
    public Texture2D spellIcon_Active;
    public Texture2D spellIcon_Cooldown;
    protected GameObject castEffect;
    protected GameObject impactEffect;
    public int spellCost;
    public float cdTime;


    ///////////////////////
    // Funcions
    //---------------------------------------
    // Initialize spell data
    public void InitializeSpell()
    {
        GenerateSpellID();
        cooldown.CooldownTime = cdTime;
    }
    //---------------------------------------
    // Base castSpell function. Should always be called
    public virtual void CastSpell()
    {   
        cooldown.TriggerCooldown();
        EventManager.Instance.PostEvent(this, "OnAbilityUse");
    }
    public bool IsOnCooldown()
    {
        if (cooldown.IsCooldownDone)
            return false;
        else
            return true;
    }

    public string getCooldownTimeRemaining()
    {
        return cooldown.cooldownTimerRemaining();
    }

    //---------------------------------------
    // Function to check wether we can cast the spell or not
    protected bool CanCast()
    {
        if (Player.Instance.energyPoints >= spellCost && cooldown.IsCooldownDone)
        {
            Player.Instance.energyPoints -= spellCost;
            return true;
        }
        else
            return false;
    }
    //---------------------------------------
    // Function which forces the player to face in a certain direction 
    protected void FaceDirection(Vector3 direction)
    {
        PlayerController.Instance.lookRotation = Quaternion.LookRotation(direction - Player.Instance.transform.position); 
    }
    //---------------------------------------
    // Function to change the spell icon. Used for cooldown display
    protected void SetIconActive()
    {
        spellIcon = spellIcon_Active;
    }
    protected void SetIconCooldown()
    {
        spellIcon = spellIcon_Cooldown;
    }
    //---------------------------------------
    // Function which spawns a gameobject at a certain position
    protected void SpawnEffect(GameObject effect, Vector3 position)
    {
        Instantiate(effect, position, Quaternion.identity);
    }
    //---------------------------------------
    // Function to generate a unique spellID
    private void GenerateSpellID()
    {
        spellID = IDManager.GetSpellID();
        IDManager.AddSpellID();
    }
}
