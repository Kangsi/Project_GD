// John van den Berg

/*=============================================================================
	Derived Ability
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * Soul Switch loops through the player's soul array
 * and switches the current active soul to the next element
 * within the soul array. If the last element is reached we 
 * reset it to 0.
 * 
 * Triggers the SoulSwitch animation within the mecanim animator.
 */
public class SoulSwitch : Ability {

    private GameObject ParticlePrefab; 
	public SoulSwitch(Texture2D abilityIcon, GameObject particlePrefab)
        : base("SoulSwitch", abilityIcon)
    {
        cooldown.CooldownTime = 5f;
        energyCost = 80;
        ParticlePrefab = particlePrefab;
    }

    public override void useAbility()
    {
        if (cooldown.IsCooldownDone && hasEnoughEnergy())
        {
            player.soulIndex++;
            playerController.animator.SetTrigger("SoulSwitch");

            if (player.soulIndex < player.souls.Length)
                player.activeSoul = player.souls[player.soulIndex];
            else
            {
                player.soulIndex = 0;
                player.activeSoul = player.souls[player.soulIndex];
            }
            base.useAbility();
            Instantiate(ParticlePrefab, player.transform.position, Quaternion.identity);
        }        
    }
}
