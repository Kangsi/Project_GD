// John van den Berg

/*=============================================================================
	Soul Super
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * Base to derive from
 * 
 * Each derived class must set the icon and Ability icons through the inspector.
 * Assigning new abilities to the Ability array is handled 
 * within the constructor of the derived class. 
 * 
 * Up to two abilities are supported.
 */
public abstract class Soul : MonoBehaviour {

    [System.NonSerialized]
    public Ability[] attacks;
    public string soulName;
    public Texture2D soulIcon;
    public Texture2D primaryAttackIcon;
    public Texture2D secondaryAttackIcon;  
	
    public Soul(string name, int abilityCount)
    {
        soulName = name;
        attacks = new Ability[abilityCount];
    }
}
