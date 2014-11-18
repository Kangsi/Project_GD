// John van den Berg

/*=============================================================================
	PlayerStatManager
=============================================================================*/
using UnityEngine;
using System.Collections;
using Manager;

namespace Manager
{
    public class PlayerStatManager : MonoBehaviour
    {
        ///////////////////////
        // Public variables
        //----------------------------------------
        // Public Instance
        public static PlayerStatManager Instance
        {
            get
            {
                if (instance == null) instance = new PlayerStatManager();

                return instance;
            }
        }

        ///////////////////////
        // Private variables
        //---------------------------------------
        // Internal reference
        private static PlayerStatManager instance = null;  

        void Awake()
        {
            //Check if an instance already exists 
            if (instance)
                DestroyImmediate(gameObject); //Delete duplicate
            else
            {
                instance = this; //Make this object the only instance
                DontDestroyOnLoad(gameObject); //Set as do not distroy
            } 
        }

        #region
        private static int level = 1;
        private static int experience;

        private static int strength;
        private static int stamina;
        private static int intelligence;

        private static int armorPoints;
        private static int damagePoints;

        public static void setLevel(int value) { level = value;  }
        public static void setExperience(int value) { experience = value; }
        public static void setStrength(int value) { strength = value; }
        public static void setStamina(int value) { stamina = value; }
        public static void setIntelligence(int value) { intelligence = value; }
        public static void setArmorPoints(int value) { armorPoints = value; }
        public static void setDamagePoints(int value) { damagePoints = value; }

        public static void addLevel(int value) { level += value; }
        public void addExperience(int value) { processExp(value); }
        public static void addStrength(int value) { strength += value; }
        public static void addStamina(int value) { stamina += value; }
        public static void addIntelligence(int value) { intelligence += value; }
        public static void addArmorPoints(int value) { armorPoints += value; }
        public static void addDamagePoints(int value) { damagePoints += value; }

        public static int getLevel() { return level; }
        public static int getExperience() { return experience; }
        public static int getRequiredExp() { return (int)(1000 * level + (300 * (0.101 * (level - 1)))); }
        public static int getStrength() { return 8 * level + strength; }
        public static int getStamina() { return 8 * level + stamina; }
        public static int getIntelligence() { return 8 * level + intelligence; }
        #endregion

        //Statistic tracking
        private static int skeletonKills;

        public static void setSkeletonKills(int value) { skeletonKills = value; }

        public static void addSkeletonKills(int value) { skeletonKills += value; }

        public static int getSkeletonKills() { return skeletonKills; }


        //Methods
        private void processExp(int value)
        {
            if (getExperience()+value >= getRequiredExp())
            {
                setExperience((getExperience() + value) - getRequiredExp());
                addLevel(1);
                EventManager.Instance.PostEvent(this, "OnLevelUp");
                Debug.Log("Reached level "+getLevel());
            }
            else
            {
                experience += value;
                Debug.Log("Experience added: "+value+" "+getRequiredExp()+" more points to go");
            }
        }
    }
}