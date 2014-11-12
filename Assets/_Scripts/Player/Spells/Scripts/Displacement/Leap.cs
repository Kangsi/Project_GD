using UnityEngine;
using System.Collections;
using Manager;

public class Leap : DisplaceAbility {

    ///////////////////////
    // Funcions
    //---------------------------------------
    // Extra layer on top of the already excisting castSpell, will get called after the base castSpell
    public override void CastSpell()
    {
        if (!CanCast())
            return;

        base.CastSpell(); 
        FaceDirection(WorkloadManager.getMousePosition());                
    }
}
