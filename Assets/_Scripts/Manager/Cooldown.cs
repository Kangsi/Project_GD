// John van den Berg

/*=============================================================================
	Cooldown
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * Cooldown
 * 
 * Cooldown is a non MonoBehaviour so a new instance has to 
 * be instantiated to use this class. 
 */
public class Cooldown {

    public float CooldownTime;
    private float _nextUse = 0.0f;
 
    public bool IsCooldownDone
    {
	    get
	    {
	        return Time.time >= _nextUse -0.5f;
	    }
    } 
 
    public void TriggerCooldown()
    {
	    _nextUse = Time.time + CooldownTime;
    }

    public string cooldownTimerRemaining()
    {
        if (Time.time >= _nextUse)
            return "";
        else
            if (((int)_nextUse - (int)Time.time) > 0)
                return ((int)_nextUse - (int)Time.time).ToString();
            else
                return "";
    }
 }
