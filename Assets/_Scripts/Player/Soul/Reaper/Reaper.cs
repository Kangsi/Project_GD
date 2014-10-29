// John van den Berg

/*=============================================================================
	Derived Soul
=============================================================================*/
using UnityEngine;
using System.Collections;

public class Reaper : Soul
{    
    public Reaper()
        : base("Reaper", 2)
    {        
    }
    
    void Awake()
    {
        attacks[0] = new ReaperAttackPrimary(primaryAttackIcon);
        attacks[1] = new ReaperAttackSecondary(secondaryAttackIcon); 
    }
}