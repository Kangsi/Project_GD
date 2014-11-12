using UnityEngine;
using System.Collections;
using Manager;

public abstract class MeleeAbility : Ability {

    ///////////////////////
    // Variables
    //----------------------------------------
    // protected 
    protected enum MeleeAnimation { Attack, HeavySwing };
    [SerializeField]
    protected MeleeAnimation meleeAnimation;
    
    ///////////////////////
    // Funcions
    //---------------------------------------
    // Extra layer on top of the already excisting castSpell, will get called after the base castSpell
    public override void CastSpell()
    {        
        base.CastSpell();

        switch(meleeAnimation)
        {
            case (MeleeAnimation.Attack):
                EventManager.Instance.PostEvent(this, "OnPlayAttackAnimation");
                break;
            case (MeleeAnimation.HeavySwing):
                EventManager.Instance.PostEvent(this, "OnPlayHeavySwingAnimation");
                break;
        }
    }
	
}
