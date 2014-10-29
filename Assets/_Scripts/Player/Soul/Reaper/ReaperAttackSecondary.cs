// John van den Berg

/*=============================================================================
	Derived Ability
=============================================================================*/
using UnityEngine;
using System.Collections;

public class ReaperAttackSecondary : Ability {
     public ReaperAttackSecondary(Texture2D abilityIcon)
        : base("Attack", abilityIcon)
    {        
        cooldown.CooldownTime = 0.5f;
        energyCost = 80;
    }
    public override void useAbility()
    {
        if (cooldown.IsCooldownDone && hasEnoughEnergy())
        {
            playerController.animator.SetInteger("AttackChain", 2);
            playerController.animator.SetTrigger("Attack");
            base.useAbility();
        } 	    
    }
}
