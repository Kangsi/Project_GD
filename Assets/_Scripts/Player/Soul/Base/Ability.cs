// John van den Berg

/*=============================================================================
	Ability Super
=============================================================================*/
using UnityEngine;
using System.Collections;
using Manager;
/**
 * Base for other abilities to derive from.
 * Each derived class must set the icon through the inspector.
 * Within the derived class's constructor the energycost and 
 * cooldowntime must be set otherwhise they will be handled as 0
 */
public class Ability : MonoBehaviour {

    public Texture2D abilityIcon;
    public string animationType;

    protected Cooldown cooldown = new Cooldown();
    protected PlayerController playerController;
    protected Player player;
    protected float energyCost;
    
    public Ability(string animationType, Texture2D icon)
    {
        this.animationType = animationType;
        this.abilityIcon = icon;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public virtual void useAbility()
    {
        if (cooldown.IsCooldownDone)
        {
            cooldown.TriggerCooldown();
            Player.Instance.energyPoints -= energyCost;
        }
    }

    public string getCooldownTimeRemaining()
    {
        return cooldown.cooldownTimerRemaining().ToString();
    }

    protected bool hasEnoughEnergy()
    {
        if (Player.Instance.energyPoints > energyCost)
            return true;
        else
            return false;
    }

    protected int getRandomNumber(int min, int max)
    {

        return Random.Range(min, max);
    }
}
