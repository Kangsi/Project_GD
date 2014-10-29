// John van den Berg

/*=============================================================================
	Player - Singleton
=============================================================================*/
using UnityEngine;
using System.Collections;
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
    public Soul[] souls;    
    public Soul activeSoul;
    public GeneralClass generalClass;
    private static Player instance;
    public Second_Character secondCharacter;
    #endregion

    ///////////////////////
    // Variable Declaration
    #region    
    public int level = 1;
    public float healthPoints, maxHealthPoints;
    public float energyPoints, maxEnergyPoints;
    public int currentExperience, requiredExperience;
    
    public int strength, baseStrength, addStrength;
    public int intelligence, baseIntelligence, addIntelligence;
    public int stamina, baseStamina, addStamina;
    public int dexterity, baseDexterity, addDexterity;
    public float damagePoints;
    public float armorPoints;
    public float attackRange = 2f;

    public int soulIndex;
    public float healthRegenerationRate = 5f;
    public float energyRegenerationRate = 10f;
    #endregion

    ///////////////////////
    // Unity
    void Awake()
    {
        animator = GetComponentInParent<Animator>();        
        generalClass = GetComponent<GeneralClass>();
        InitializeSouls();
        calculateBaseStats();
        calculateStats();
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
    private void InitializeSouls()
    {
        souls = new Soul[3];
        souls[0] = GetComponent<Reaper>();
        souls[1] = GetComponent<Lich>();
        souls[2] = GetComponent<Necromancer>();
        activeSoul = souls[0];
    }    
    public void calculateBaseStats()
    {
        baseStrength = level * 8;
        baseStamina = level * 8;
        baseIntelligence = level * 8;
        baseDexterity = level * 8;
        maxEnergyPoints = 100;
        maxHealthPoints = (int)(100 * level * 1.5f);
        requiredExperience = (int)(1000 * level * 1.5f);
    }
    public void calculateStats()
    {
        strength = addStrength + baseStrength;
        intelligence = addIntelligence + baseIntelligence;
        stamina = addStamina + baseStamina;
        dexterity = addDexterity + baseDexterity;
        maxHealthPoints += (stamina * 10);
    }
    public void resetHealth()
    {
        healthPoints = 100;
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
    public void addExperience(int exp)
    {        
        if (currentExperience + exp > requiredExperience)
        {
            level += 1;
            exp = (currentExperience + exp) - requiredExperience;
            currentExperience = exp;
            calculateBaseStats();
            calculateStats();                
        }
        else
            currentExperience += exp;
    }
    public void takeDamage(int damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
            healthPoints = 0;
    }
}



