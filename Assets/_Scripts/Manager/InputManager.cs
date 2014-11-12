// John van den Berg

/*=============================================================================
	InputManager
=============================================================================*/
using UnityEngine;
using System.Collections;
/**
 * Manager: InputManager
 * 
 * The InputManager is used to map all input keys to game actions.
 */
namespace Manager
{
    public class InputManager : MonoBehaviour
    {
        ///////////////////////
        // Public variables
        //----------------------------------------
        // Public Instance
        public static InputManager Instance
        {
            get
            {
                if (instance == null) instance = new InputManager();

                return instance;
            }
        }

        ///////////////////////
        // Private variables
        private KeyCode PrimaryAttackKey = KeyCode.Mouse0;
        private KeyCode SecondaryAttackKey = KeyCode.Mouse1;

        private KeyCode ActionbarButton1Key = KeyCode.Alpha1;
        private KeyCode ActionbarButton2Key = KeyCode.Alpha2;
        private KeyCode ActionbarButton3Key = KeyCode.Alpha3;
        private KeyCode ActionbarButton4Key = KeyCode.Alpha4;

        private KeyCode PetAttackOrderKey = KeyCode.Q;
        private KeyCode PetFollowOrderKey = KeyCode.W;

        private KeyCode MainMenuKey = KeyCode.Escape;
        private KeyCode CharacterMenuKey = KeyCode.C;
        private KeyCode IventoryKey = KeyCode.I;
        //---------------------------------------
        // Internal reference
        private static InputManager instance = null;

        ///////////////////////
        // Unity
        //---------------------------------------
        void Update()
        {
            if (Input.GetKeyUp(PrimaryAttackKey) || Input.GetKey(PrimaryAttackKey))
                EventManager.Instance.PostEvent(this, "OnMouseDown");
            if (Input.GetKeyUp(SecondaryAttackKey))
                EventManager.Instance.PostEvent(this, "OnSecondaryAttack");

            if (Input.GetKeyUp(ActionbarButton1Key))
                EventManager.Instance.PostEvent(this, "OnActionbar1");
            if (Input.GetKeyUp(ActionbarButton2Key))
                EventManager.Instance.PostEvent(this, "OnActionbar2");
            if (Input.GetKeyUp(ActionbarButton3Key))
                EventManager.Instance.PostEvent(this, "OnActionbar3");
            if (Input.GetKeyUp(ActionbarButton4Key))
                EventManager.Instance.PostEvent(this, "OnActionbar4");

            if (Input.GetKeyUp(PetAttackOrderKey))
                EventManager.Instance.PostEvent(this, "OnPetAttackOrder");
            if (Input.GetKeyUp(PetFollowOrderKey))
                EventManager.Instance.PostEvent(this, "OnPetFollowOrder");
        }
    }
}
