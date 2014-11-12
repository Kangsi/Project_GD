using UnityEngine;
using System.Collections;
using Manager;

public class DisplaceAbility : Ability {
    ///////////////////////
    // Variables
    //----------------------------------------
    // protected 
    protected enum DisplaceAnimation { Jump, DoNotUse };
    [SerializeField]
    protected DisplaceAnimation displaceAnimation;
    protected float displacementSpeed;
    
    ///////////////////////
    // Funcions
    //---------------------------------------
    // Extra layer on top of the already excisting castSpell, will get called after the base castSpell
    public override void CastSpell()
    {
        base.CastSpell();

        switch (displaceAnimation)
        {
            case (DisplaceAnimation.Jump):
                EventManager.Instance.PostEvent(this, "OnPlayJumpAnimation");
                break;
            case (DisplaceAnimation.DoNotUse):
                break;
        }
    }
}
