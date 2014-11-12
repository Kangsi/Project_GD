using UnityEngine;
using System.Collections;
using Manager;

public class AreaAbility : Ability {
    
    ///////////////////////
    // Variables
    //----------------------------------------
    // protected 
    protected enum AreaAnimation { Pound, Charge };
    [SerializeField]
    protected AreaAnimation areaAnimation;

    ///////////////////////
    // Funcions
    //---------------------------------------
    // Extra layer on top of the already excisting castSpell, will get called after the base castSpell
    public override void CastSpell()
    {
        base.CastSpell();

        switch (areaAnimation)
        {
            case (AreaAnimation.Pound):
                EventManager.Instance.PostEvent(this, "OnPlayPoundAnimation");
                break;
            case (AreaAnimation.Charge):
                EventManager.Instance.PostEvent(this, "OnPlayChargeAnimation");
                break;
        }
    }
}
