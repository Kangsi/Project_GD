// John van den Berg

/*=============================================================================
	Derived Ability
=============================================================================*/
using UnityEngine;
using System.Collections;

public class NecromancerAttackSecondary : Ability {

	public NecromancerAttackSecondary(Texture2D abilityIcon)
        : base("Attack", abilityIcon)
    {
        //base.cooldownTime = cooldownTime;
    }
}
