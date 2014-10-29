// John van den Berg

/*=============================================================================
	General Player Class
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * Unlike the Soul class this Player Class is used to
 * hold abilities which are available to the player regardless
 * of his current active soul. 
 * 
 * Supports up to two abilities
 */
public class GeneralClass : MonoBehaviour  {

    [System.NonSerialized]
    public Ability[] ability;

    public Texture2D soulSwitchIcon;
    public Texture2D soulDrainIcon;
    public GameObject particle;
    
    void Awake()
    {
        ability = new Ability[2];
        ability[0] = new SoulSwitch(soulSwitchIcon, particle);
        ability[1] = new SoulDrain(soulDrainIcon);
    }
}
