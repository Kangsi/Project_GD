using UnityEngine;
using System.Collections;

public class Crush : MeleeAbility {

    ///////////////////////
    // Funcions
    //---------------------------------------
    // Extra layer on top of the already excisting castSpell, will get called after the base castSpell
    public override void CastSpell()
    {
        if (!CanCast())
            return;

        base.CastSpell();
        Debug.Log("Crush Cast");
    }
}
