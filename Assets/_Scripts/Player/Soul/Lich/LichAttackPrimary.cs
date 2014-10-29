// John van den Berg

/*=============================================================================
	Derived Ability
=============================================================================*/
using UnityEngine;
using System.Collections;

public class LichAttackPrimary : Ability {

	public LichAttackPrimary(Texture2D abilityIcon)
        : base("Attack", abilityIcon)
    {
        cooldown.CooldownTime = 4f;
    }

    public override void useAbility()
    {
        if (player.targetInRange())
        {
            if (cooldown.IsCooldownDone)
            {
                cooldown.TriggerCooldown();
                playerController.animator.SetTrigger("Attack");
            }
        }
    }
}
