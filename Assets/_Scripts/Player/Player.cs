// John van den Berg

/*=============================================================================
	Player - Singleton
=============================================================================*/
using UnityEngine;
using System.Collections;
using Manager;
/**
 * The Player class 
 * 
 * The Player class holds everything related to the character of the player
 * such as statistics, classes, abilities, health, energy, experience and such.
 * 
 * Reference to this class is handled by calling the Instance of this class
 * Reference call: Player.Instance.functionName();
 */

public class Player : MonoBehaviour {

    ///////////////////////
    // Instance
    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType(typeof(Player)) as Player;
            return instance;
        }
    }

    ///////////////////////
    // References
    #region
    private Animator animator;
    public GameObject target;
    private static Player instance;
    #endregion

    ///////////////////////
    // Variable Declaration
    #region    
    public float healthPoints, maxHealthPoints;
    public float energyPoints, maxEnergyPoints;

    public float attackRange = 2f;
    public float healthRegenerationRate = 5f;
    public float energyRegenerationRate = 10f;

    #endregion

    ///////////////////////
    // Unity
    void Awake()
    {
        animator = GetComponentInParent<Animator>();   
        getResources();
        resetHealth();
        resetEnergy();
    }
    void Update()
    {
        regainEnergy();
        regainHealth();
    }

    ///////////////////////
    // Methods
    private void getResources()
    {
        maxHealthPoints = PlayerStatManager.getStamina() * 10;
        maxEnergyPoints = 100;
    }
    public void resetHealth()
    {
        healthPoints = maxHealthPoints;
    }
    public void resetEnergy()
    {
        energyPoints = maxEnergyPoints;
    }
    public void regainHealth()
    {  
        if (healthPoints < maxHealthPoints)
        {
            healthPoints += 1 * (Time.deltaTime*healthRegenerationRate);
        }
        else
            healthPoints = maxHealthPoints;
    }
    public void regainEnergy()
    {
        if (energyPoints < maxEnergyPoints)
        {
            energyPoints += 1 * (Time.deltaTime * energyRegenerationRate);
        }
        else
            energyPoints = maxEnergyPoints;
    }
    public bool targetInRange()
    {
        if (target)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
                return true;
            else
                return false;
        }
        else
            return false;
    }          
    public void takeDamage(int damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
            healthPoints = 0;
    }
}



