// John van den Berg

/*=============================================================================
	Derived Soul
=============================================================================*/
using UnityEngine;
using System.Collections;

public class Lich : Soul 
{
    public Lich()
        : base("Lich", 2)
    {
    }

    void Awake()
    {
        attacks[0] = new LichAttackPrimary(primaryAttackIcon);
        attacks[1] = new LichAttackSecondary(secondaryAttackIcon);
    }
}
