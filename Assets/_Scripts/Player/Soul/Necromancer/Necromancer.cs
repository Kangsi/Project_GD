// John van den Berg

/*=============================================================================
	Derived Soul
=============================================================================*/
using UnityEngine;
using System.Collections;

public class Necromancer : Soul {

	public Necromancer()
        : base("Necromancer", 2)
    {
    }

    void Awake()
    {
        attacks[0] = new NecromancerAttackPrimary(primaryAttackIcon);
        attacks[1] = new NecromancerAttackSecondary(secondaryAttackIcon); 
    }
}
