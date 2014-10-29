// John van den Berg

/*=============================================================================
	Derived Ability
=============================================================================*/
using UnityEngine;
using System.Collections;

public class ReaperAttackPrimary : Ability {
    
    public ReaperAttackPrimary(Texture2D abilityIcon)
        : base("Attack", abilityIcon)
    {
        cooldown.CooldownTime = 1.5f;
        energyCost = 5f;
    }
    public override void useAbility()
    {        
        if (cooldown.IsCooldownDone && hasEnoughEnergy())
        {
            if (playerController.animator.GetInteger("AttackChain") == 0)
            {                
                playerController.animator.SetTrigger("Attack");
                playerController.animator.SetInteger("AttackChain", 1);
            }
            else
            {
                playerController.animator.SetTrigger("Attack");
                playerController.animator.SetInteger("AttackChain", 0);
            }                      
            base.useAbility();
        }              
    }
}
